"use client"
import BackToButton from "@/components/BackToButton";
import { use } from "react";


const AddAttendeeToAnotherEvent = (props: {
  params: Promise<{ id?: string }>;
}) => {
  const { id } = use(props.params);
  return (
    <div className="container">
      <main role="main" className="pb-3">
        <h1>Osavõtja lisamine teisele üritusele</h1>

        <hr />
        <div className="row">
          <div className="col-md-4">
            <form action="/Attendees/Create" method="post">
              <div className="form-group">
                <label className="control-label" htmlFor="EventId">
                  Üritus
                </label>
                <select
                  name="EventId"
                  className="form-control"
                >
                  <option value="">Palun valige</option>
                  <option value="4">28.12.2024 16:00 - riiete myyk</option>
                  
                </select>
                <span
                  className="text-danger field-validation-valid"
                  
                ></span>
              </div>
              <br />

              <div className="form-group">
                <input
                  type="submit"
                  value="Lisa"
                  className="btn btn-primary"
                  /* action="/Attendees/AddAttendeeToAnotherEvent/4" */
                />
              </div>
              <input
                name="__RequestVerificationToken"
                type="hidden"
                value="CfDJ8MtBwhwBGkZDhmeJqCjKJQ4liIGZjidAiAL2SrhulP9z2-ga0oxiYNMOKbLzh8spBQfSPEn4_7HGyijx6O2RNl_a2sqdBQcUzdDdnMdNlIWxNoQjaAbEcOC0pA7X2lF59j1eXuuJkQjT562BAQp3n14"
              />
            </form>
          </div>
        </div>
        <br />
        <div>
          <BackToButton to={`/events/list_of_attendees/${id}`}>Tagasi </BackToButton>
        </div>
      </main>
    </div>
  );
};

export default AddAttendeeToAnotherEvent;
