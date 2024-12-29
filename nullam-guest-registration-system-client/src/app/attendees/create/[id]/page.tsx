"use client";
import React, { useEffect, useState } from "react";
import { AttendeeService } from "@/services/AttendeeService";
import { PaymentMethodService } from "@/services/PaymentMethodService";
import BackToButton from "@/components/BackToButton";
import IPaymentMethod from "@/app/domain/IPaymentMethod";
import { useFormik } from "formik";
import ATTENDEECREATEEDITSCHEMA from "@/schemas/ATTENDEECREATEEDITSCHEMA";
import PersonAttendeeForm from "../../PersonAttendeeForm";
import CompanyAttendeeForm from "../../CompanyAttendeeForm";
import { number } from "yup";
import { CreateAttendeeValues } from "@/types";

const BaseAttendeeForm = () => {
  // const service = new AttendeeService();
  const paymentMethodService = new PaymentMethodService();
  const [paymentMethods, setPaymentMethods] = useState<IPaymentMethod[]>([]);
  const [loading, setLoading] = useState(true);

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
      attendeeType: "",
      paymentMethodId: "",
      surName: "",
      givenName: "",
      personalIdentifier: "",
      personAdditionalInfo: "",
      companyName: "",
      registryCode: "",
      numberOfPeopleFromCompany: "",
      companyAdditionalInfo: "",
    },
    validationSchema: ATTENDEECREATEEDITSCHEMA,
    onSubmit: async (values) => {
      console.log("onSubmit values", values);
      try {
        //const result = await service.create(values);
        /* if (result) {
          window.location.href = "/";
        } */
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
                    checked={values.attendeeType === "1"}
                    name="attendeeType"
                    onChange={(e) => {
                      console.log("e", e);
                      handleChange(e);
                    }}
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
                    checked={values.attendeeType === "2"}
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
