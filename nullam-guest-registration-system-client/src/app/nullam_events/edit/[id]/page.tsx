"use client";

import IEvent from "@/app/domain/IEvent";
import NotFound from "@/app/not-found";
import ErrorPage from "@/app/not-found";
import BackToButton from "@/components/BackToButton";
import eventCreateEditSchema from "@/schemas/EVENTCREATEEDITSCHEMA";
import { EventService } from "@/services/EventService";
import { useFormik } from "formik";
import Error from "next/error";
import React, { useState, useEffect, use } from "react";

const Edit = (props: { params: Promise<{ id: string }> }) => {
  const [event, setEvent] = useState<IEvent>();
  const { id } = use(props.params);
  console.log(id);
  useEffect(() => {
    async function getEvent() {
      const service = new EventService();
      const event = await service.getById(id);
      console.log("event", event);
      setEvent(event);
    }
    getEvent();
  }, [id]);

  const service = new EventService();
  const afterNow = event && service.isAfterNow(event.eventDateAndTime);
  if (!afterNow) {
    return <NotFound />
  } 
  const { values, errors, handleChange, handleSubmit, handleBlur } = useFormik({
    initialValues: {
      name: event?.name ?? "",
      eventDateAndTime: event?.eventDateAndTime ?? "",
      location: event?.location ?? "",
      additionalInfo: event?.additionalInfo ?? "",
    },

    validationSchema: eventCreateEditSchema,
    enableReinitialize: true,

    onSubmit: async (values) => {
      const body = { id, ...values };
      const result = await service.edit(id, body);
      if (result) {
        return (window.location.href = "/");
      }
    },
  });

  console.log(errors);
  return (
    <div className="container">
      <main role="main" className="pb-3">
        <h1>Ürituse muutmine</h1>

        <hr />
        <div className="row">
          <div className="col-md-4">
            <form onSubmit={handleSubmit} method="post">
              <div
                className="text-danger validation-summary-valid"
                data-valmsg-summary="true"
              >
                <ul>
                  <li style={{ display: "none" }}></li>
                </ul>
              </div>
              <div className="form-group">
                <label className="control-label" htmlFor="Name">
                  Ürituse nimi
                </label>
                <input
                  className="form-control"
                  type="text"
                  name="name"
                  placeholder="Sisestage ürituse nimetus"
                  value={values.name}
                  onChange={handleChange}
                  onBlur={handleBlur}
                />
                {errors.name ? (
                  <div className="text-danger">{errors.name}</div>
                ) : null}
              </div>
              <div className="form-group">
                <label className="control-label" htmlFor="EventDateAndTime">
                  Toimumisaeg
                </label>
                <input
                  name="EventDateAndTime"
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
              </div>
              <div className="form-group">
                <label className="control-label" htmlFor="Location">
                  Koht
                </label>
                <input
                  className="form-control"
                  type="text"
                  name="Location"
                  placeholder="Sisestage koht"
                  value={values.location}
                  onChange={handleChange}
                  onBlur={handleBlur}
                />
                {errors.location ? (
                  <div className="text-danger">{errors.location}</div>
                ) : null}
              </div>
              <div className="form-group">
                <label className="control-label" htmlFor="AdditionalInfo">
                  Lisainfo
                </label>
                <textarea
                  className="form-control"
                  placeholder="Sisestage ürituse lisainfo"
                  value={values.additionalInfo}
                  onChange={handleChange}
                  onBlur={handleBlur}
                  name="AdditionalInfo"
                ></textarea>
                {errors.additionalInfo ? (
                  <div className="text-danger">{errors.additionalInfo}</div>
                ) : null}
              </div>

              <br />
              <div className="form-group">
                <input
                  type="submit"
                  value="Muuda"
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

        <script src="/lib/jquery-validation/dist/jquery.validate.min.js"></script>
        <script src="/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js"></script>
      </main>
    </div>
  );
};
export default Edit;
