"use client";
import React, { use, useEffect, useState } from "react";
import { AttendeeService } from "@/services/AttendeeService";
import { PaymentMethodService } from "@/services/PaymentMethodService";
import BackToButton from "@/components/BackToButton";
import IPaymentMethod from "@/app/domain/IPaymentMethod";
import { ErrorMessage, useFormik } from "formik";
import attendeeCreateEditSchema from "@/schemas/ATTENDEECREATEEDITSCHEMA";
import PersonAttendeeForm from "../../PersonAttendeeForm";
import CompanyAttendeeForm from "../../CompanyAttendeeForm";
import { number } from "yup";
import { APIErrorData, CreateAttendeeValues } from "@/types";
import { EventService } from "@/services/EventService";
import IAttendee from "@/app/domain/IAttendee";
import { match } from "node:assert";

const BaseAttendeeForm = (props: { params: Promise<{ id: string }> }) => {
  const service = new AttendeeService();
  const paymentMethodService = new PaymentMethodService();
  const eventService = new EventService();
  const [paymentMethods, setPaymentMethods] = useState<IPaymentMethod[]>([]);
  const [loading, setLoading] = useState(true);
  const { id } = use(props.params);
  

  const fetchPaymentMethods = async () => {
    try {
      const response = await paymentMethodService.getAll();
      setPaymentMethods(response || []);
    } catch (error) {
      console.error("Failed to fetch payment methods:", error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchPaymentMethods();
  }, []);

  const formik = useFormik<CreateAttendeeValues>({
    initialValues: {
      attendeeType: "1",
      paymentMethodId: "",
      surName: "",
      givenName: "",
      personalIdentifier: "",
      personAdditionalInfo: "",
      companyName : "",
      registryCode: "",
      numberOfPeopleFromCompany : "",
      companyAdditionalInfo: ""
    },
    validationSchema: attendeeCreateEditSchema,
    onSubmit: async (values) => {
      console.log("onSubmit values", values);

      let dto : IAttendee = {
        attendeeType: Number(values.attendeeType),
        paymentMethodId: Number(values.paymentMethodId)
      };
      if (values.attendeeType === "1") {
        //dto.attendeeType = 5;
        //dto.paymentMethodId = 4;
        dto.givenName = values.givenName;
        dto.surName = values.surName;
        dto.personalIdentifier = values.personalIdentifier;
        dto.personAdditionalInfo = values.personAdditionalInfo;
      }
      else {
        dto.companyName = values.companyName;
        dto.registryCode = values.registryCode;
        dto.companyAdditionalInfo = values.companyAdditionalInfo;
        dto.numberOfPeopleFromCompany = Number(values.numberOfPeopleFromCompany);
      }
      
      try {
        const result = await service.addAttendeeToAnEvent(id, dto);
        if (typeof result === 'number') {
          window.location.href = "/";
        }
        else if (typeof result === 'string') {
          alert(result);
        }
        else
        {
          let apiError = result as APIErrorData;
          let formFieldNames = Object.entries(formik.values).map(x => x[0]);
          let errorFields = Object.entries(apiError.errors);
          errorFields.forEach(([key, value]) => {
            // find the field that matches the key
            let formFieldName = formFieldNames.find(x => x.toLowerCase() === key.toLowerCase());
            if (formFieldName)
              formik.setFieldError(formFieldName, value.join(', '));
            else
              alert(`${key}: ${value}`);
          });       
        }
      } catch (error) {
        console.error("Failed to create attendee:", error);
        alert("Submission failed. Please try again.");
      }
    },
  });

  const { values, errors, handleBlur, handleChange, handleSubmit } = formik;
  console.log("values", values);
  console.log("errors", errors);
  if (loading) {
    return <div>Laadimine...</div>;
  }

  return (
    <div className="container">
      <main role="main" className="pb-3">
        <h1>Osavõtja lisamine</h1>
        <hr />
        <div className="row">
          <div className="col-md-4">
            <form onSubmit={handleSubmit}>
              <div className="form-group">
                <label htmlFor="attendeeType">Osavõtja tüüp</label>

                <div className="form-check">
                  <input
                    type="radio"
                    value="1"
                    className="form-check-input"
                    checked={ values.attendeeType === "1"}
                    name="attendeeType"
                    onChange={handleChange}
                  />
                  <label
                    htmlFor="AttendeeType_Person"
                    className="form-check-label"
                  >
                    Eraisik
                  </label>
                  
                </div>
                <div className="form-check">
                  <input
                    name="attendeeType"
                    type="radio"
                    value="2"
                    checked={ values.attendeeType === "2"}
                    className="form-check-input"
                    onChange={(e) => handleChange(e)}
                  />
                  <label
                    htmlFor="AttendeeType_Company"
                    className="form-check-label"
                  >
                    Ettevõtte
                  </label>
                </div>
              </div>

              {values.attendeeType === "1" ? (
                <PersonAttendeeForm formik={formik} />
              ) : values.attendeeType === "2" ? (
                <CompanyAttendeeForm formik={formik} />
              ) : null}
              {formik.errors.attendeeType ? (
                <div className="text-danger">{formik.errors.attendeeType}</div>
              ) : null}

              <div className="form-group">
                <label htmlFor="paymentMethodId">Maksemeetod</label>
                <select
                  name="paymentMethodId"
                  className={`form-control ${
                    errors.paymentMethodId ? "is-invalid" : ""
                  }`}
                  value={values.paymentMethodId || ""}
                  onChange={handleChange}
                  onBlur={handleBlur}
                >
                  <option value="">Palun valige</option>
                  {paymentMethods.map((item) => (
                    <option key={item.id} value={item.id}>
                      {item.name}
                    </option>
                  ))}
                </select>
                {errors.paymentMethodId && (
                  <span className="text-danger">{errors.paymentMethodId}</span>
                )}
              </div>
              <br />
              <div className="form-group">
                <input type="submit" value="Lisa" className="btn btn-primary" />
              </div>
            </form>
            <br />
            <BackToButton to="/">Tagasi</BackToButton>
          </div>
        </div>
      </main>
    </div>
  );
};

export default BaseAttendeeForm;
