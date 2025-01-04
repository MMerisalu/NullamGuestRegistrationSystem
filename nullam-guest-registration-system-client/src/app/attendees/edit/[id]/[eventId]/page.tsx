"use client";

import CompanyAttendeeForm from "@/app/attendees/CompanyAttendeeForm";
import PersonAttendeeForm from "@/app/attendees/PersonAttendeeForm";
import IAttendee from "@/app/domain/IAttendee";
import IPaymentMethod from "@/app/domain/IPaymentMethod";
import BackToButton from "@/components/BackToButton";
import attendeeCreateEditSchema from "@/schemas/ATTENDEECREATEEDITSCHEMA";
import { AttendeeService } from "@/services/AttendeeService";
import { PaymentMethodService } from "@/services/PaymentMethodService";
import { CreateAttendeeValues, EditAttendeeValues } from "@/types";
import { useFormik } from "formik";
import React, { use, useEffect, useState } from "react";

const AttendeeEdit = (props: {
  params: Promise<{ id: string; eventId: string }>;
}) => {
  const service = new AttendeeService();
  const paymentMethodService = new PaymentMethodService();
  const [attendee, setAttendee] = useState<IAttendee>();
  const [attendeeLoading, setAttendeeLoading] = useState(false);
  const [paymentMethods, setPaymentMethods] = useState<IPaymentMethod[]>([]);
  const [methodsLoading, setMethodsLoading] = useState(true);
  const [errorMessage, setErrorMessage] = useState();
  const params = use(props.params);

  const fetchAttendee = async () => {
    setAttendeeLoading(true);
    const attendee = await service.getById(params.id);
    setAttendee(attendee);
    setAttendeeLoading(false);
  };

  const fetchPaymentMethods = async () => {
    try {
      const response = await paymentMethodService.getAll();
      setPaymentMethods(response || []);
    } catch (error) {
      console.error("Failed to fetch payment methods:", error);
    } finally {
      setMethodsLoading(false);
    }
  };

  useEffect(() => {
    fetchAttendee();
    fetchPaymentMethods();
  }, []);
  const formik = useFormik<EditAttendeeValues>({
    initialValues: {
      attendeeType: String(attendee?.attendeeType) ?? "",
      paymentMethodId: attendee?.paymentMethodId ?? "",
      surName: attendee?.surName ?? "",
      givenName: attendee?.givenName ?? "",
      personalIdentifier: attendee?.personalIdentifier ?? "",
      personAdditionalInfo: attendee?.personAdditionalInfo ?? "",
      companyName: attendee?.companyName ?? "",
      code: attendee?.registryCode ?? "",
      numberOfPeopleFromCompany: attendee?.numberOfPeopleFromCompany
        ? String(attendee.numberOfPeopleFromCompany)
        : "",
      companyAdditionalInfo: attendee?.companyAdditionalInfo ?? "",
    },
    enableReinitialize: true,
    validationSchema: attendeeCreateEditSchema,
    onSubmit: async (values) => {
      console.log("onSubmit values", values);
      try {
        const params = await props.params;
        const result = await service.editAttendee(
          params.id,
          params.eventId,
          values
        );
        console.log("result", result);
        if (result) {
          window.location.href = "/";
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
  if (methodsLoading) {
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
                    disabled
                    value="1"
                    className="form-check-input"
                    checked={values.attendeeType === "1"}
                    name="attendeeType"
                    onChange={(e) => {
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
                    disabled
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

export default AttendeeEdit;
