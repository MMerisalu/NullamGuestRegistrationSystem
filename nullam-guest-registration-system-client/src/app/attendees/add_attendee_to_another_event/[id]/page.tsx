"use client";
import IAttendee from "@/app/domain/IAttendee";
import IEvent from "@/app/domain/IEvent";
import BackToButton from "@/components/BackToButton";
import ADDATTENDEETOANOTHEREVENTSCHEMA from "@/schemas/ADDATTENDEETOANOTHEREVENT";
import { AttendeeService } from "@/services/AttendeeService";
import { useFormik } from "formik";
import React, { use, useEffect, useState } from "react";


const service = new AttendeeService();

const AddAttendeeToAnotherEvent = (props: { params: Promise<{ id: string }> }) => {
  const { id } = use(props.params);
  const [events, setEvents] = useState<IEvent[]>([]);
  const [attendee, setAttendee] = useState<IAttendee>();
  const [isChecked, setIsChecked] = useState(false);
  

  useEffect(() => {
    const fetchData = async () => {
      const [fetchedEvents, fetchedAttendee] = await Promise.all([
        service.getAllFutureEventsOrdedByTimeAndName(id),
        service.getById(id),
      ]);
      setEvents(fetchedEvents ?? []);
      setAttendee(fetchedAttendee);
    };
    fetchData();
  }, [id]);

  const formik = useFormik({
    initialValues: {
      eventId : "",
      attendeeType: attendee?.attendeeType ?? 0,
      numberOfPeople: attendee?.numberOfPeopleFromCompany ?? 1,
      isChecked: false,
    },
    validationSchema: ADDATTENDEETOANOTHEREVENTSCHEMA,
    enableReinitialize: true,
    onSubmit: async (values) => {
       const result = await service.addAttendeeToAnotherEvent(id, values); 
       if (result) {
        return (window.location.href = "/") 
      } 
    },
  });

  const { values, errors, handleBlur, handleChange, handleSubmit } = formik;

  return (
    <div className="container">
      <main role="main" className="pb-3">
        <h1>Osavõtja lisamine teisele üritusele</h1>
        <hr />
        <div className="row">
          <div className="col-md-4">
            <form onSubmit={handleSubmit}>
              <div className="form-group form-check">
                {values?.attendeeType === 2 && (
                  <label className="form-check-label" htmlFor="IsNumberOfPeopleFromCompanyChanged">
                    <input
                      className="form-check-input"
                      type="checkbox"
                      name="isChecked"
                      onChange={() => setIsChecked(!isChecked)}
                      checked={isChecked}
                    />
                    Osavõtjate arvu muutmine?
                  </label>
                )}
              </div>
              {values?.attendeeType === 2 && isChecked && (
                <div className="form-group">
                  <label htmlFor="NumberOfPeopleFromCompany">Ettevõtest tulevate osavõtjate arv</label>
                  <input
                    className="form-control"
                    type="number"
                    name="numberOfPeople"
                    value={values.numberOfPeople}
                    onChange={handleChange}
                    onBlur={handleBlur}
                  />
                  {errors.numberOfPeople && (
                    <span className="text-danger">{errors.numberOfPeople}</span>
                  )}
                </div>
              )}
              <div className="form-group">
                <label htmlFor="EventId">Üritus</label>
                <select
                  name="eventId"
                  className="form-control"
                  onChange={handleChange}
                  onBlur={handleBlur}
                >
                  {events?.length === 0 ? (
                    <option value="">Tulevasi üritusi pole</option>
                  ) : (
                    <>
                      <option value="">Palun valige</option>
                      {events.map((event) => (
                        <option key={event.id} value={event.id}>
                          {event.eventDateTimeAndName}
                        </option>
                      ))}
                    </>
                  )}
                </select>
                {errors.eventId && <span className="text-danger">{errors.eventId}</span>}
              </div>
              <br />
              <div className="form-group">
                <button type="submit" className="btn btn-primary">
                  Lisa
                </button>
              </div>
            </form>
          </div>
        </div>
        <br />
        <BackToButton to={`/nullam_events/list_of_attendees/${id}`}>Tagasi</BackToButton>
      </main>
    </div>
  );
};

export default AddAttendeeToAnotherEvent;
