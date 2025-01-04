import React, { useMemo } from "react";
import BackToButton from "./BackToButton";
import { SharedEventFormProps } from "@/types";
import eventCreateEditSchema from "@/schemas/eventCreateEditSchema";
import { useFormik } from "formik";

const emptyValues = {
  name: "",
  eventDateAndTime: "",
  location: "",
  additionalInfo: "",
};

const SharedEventForm = (props: SharedEventFormProps) => {
  const initialValues =
    "initialValues" in props ? props.initialValues : emptyValues;
  const { values, errors, handleChange, handleSubmit, handleBlur } = useFormik({
    initialValues,
    enableReinitialize: true,
    validationSchema: eventCreateEditSchema,
    onSubmit: async (values) => {
      await props.onSubmit(values);
    },
  });

  const creative = props.formType === "create";
  const submitLabel = creative ? "Lisa" : "Muuda";
  return (
    <div className="container">
      <main role="main" className="pb-3">
        <h1>Ürituse lisamine</h1>

        <hr />
        <div className="row">
          <div className="col-md-4">
            <form onSubmit={handleSubmit} method="post">
              <div className="form-group">
                <label className="control-label" htmlFor="name">
                  Ürituse nimi
                </label>
                <input
                  className={`form-control ${errors.name ? "is-invalid" : ""}`}
                  name="name"
                  type="text"
                  placeholder="Sisestage ürituse nimi"
                  value={values.name}
                  onChange={handleChange}
                  onBlur={handleBlur}
                />
                {errors.name ? (
                  <div className="text-danger">{errors.name}</div>
                ) : null}
                <span className="text-danger field-validation-valid"></span>
              </div>
              <div className="form-group">
                <label className="control-label" htmlFor="eventDateAndTime">
                  Toimumisaeg
                </label>
                <input
                  name="eventDateAndTime"
                  className="form-control"
                  type="datetime-local"
                  placeholder="pp.kk.aaaa"
                  value={values.eventDateAndTime}
                  onChange={handleChange}
                  onBlur={handleBlur}
                />
                {errors.eventDateAndTime ? (
                  <div className="text-danger">{errors.eventDateAndTime}</div>
                ) : null}
                <span className="text-danger field-validation-valid"></span>
              </div>
              <div className="form-group">
                <label className="control-label" htmlFor="location">
                  Koht
                </label>
                <input
                  className="form-control"
                  type="text"
                  name="location"
                  placeholder="Sisestage koha nimetus"
                  value={values.location}
                  onChange={handleChange}
                  onBlur={handleBlur}
                />
                {errors.location ? (
                  <div className="text-danger">{errors.location}</div>
                ) : null}
                <span className="text-danger field-validation-valid"></span>
              </div>
              <div className="form-group">
                <label className="control-label" htmlFor="additionalInfo">
                  Lisainfo
                </label>
                <textarea
                  className="form-control"
                  name="additionalInfo"
                  placeholder="Sisestage lisainfo"
                  value={values.additionalInfo}
                  onChange={handleChange}
                  onBlur={handleBlur}
                ></textarea>
                {errors.additionalInfo ? (
                  <div className="text-danger">{errors.additionalInfo}</div>
                ) : null}
                <span className="text-danger field-validation-valid"></span>
              </div>
              <br />
              <div className="form-group">
                <input
                  type="submit"
                  value={submitLabel}
                  className="btn btn-primary"
                />
              </div>
            </form>
          </div>
        </div>
        <br />
        <div>
          <BackToButton to="/">Tagasi</BackToButton>
        </div>
      </main>
    </div>
  );
};

export default SharedEventForm;
