"use client";
import { useFormik } from "formik";
import React from "react";
import { EventService } from "@/services/EventService";
import eventCreateEditSchema from "@/schemas/EVENTCREATEEDITSCHEMA";
import BackToButton from "@/components/BackToButton";

const service = new EventService();
const Create = () => {
  const { values, errors, handleChange, handleSubmit, handleBlur } = useFormik({
    initialValues: {
      name: "",
      eventDateAndTime: "",
      location: "",
      additionalInfo: "",
    },

    validationSchema: eventCreateEditSchema,

    onSubmit: async (values) => {
      const result = await service.create(values);
      if (result) {
        return (window.location.href = "/");
      }
    },
  });

  console.log(errors);

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
                {errors.name ? (
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
                {errors.name ? (
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
                {errors.name ? (
                  <div className="text-danger">{errors.additionalInfo}</div>
                ) : null}
                <span className="text-danger field-validation-valid"></span>
              </div>
              <br />
              <div className="form-group">
                <input type="submit" value="Lisa" className="btn btn-primary" />
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

export default Create;
