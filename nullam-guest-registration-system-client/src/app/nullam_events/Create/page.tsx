'use client'
import BackToButton from "@/components/BackToButton";
import DatetimePicker from "@/components/DatetimePicker";
import React from "react";

const page = () => {
  return (
    <div className="container">
      <main role="main" className="pb-3">
        <h1>Ürituse lisamine</h1>

        <hr />
        <div className="row">
          <div className="col-md-4">
            <form action="/Events/Create" method="post">
              <div className="form-group">
                <label className="control-label" htmlFor="Name">
                  Ürituse nimi
                </label>
                <input className="form-control" type="text" name="Name" />
              </div>
              <div className="form-group">
                <label className="control-label" htmlFor="EventDateAndTime">
                  Toimumisaeg
                </label>
                <DatetimePicker />

                <span></span>
              </div>
              <div className="form-group">
                <label className="control-label" htmlFor="Location">
                  Koht
                </label>
                <input className="form-control" type="text" />
              </div>
              <div className="form-group">
                <label className="control-label" htmlFor="AdditionalInfo">
                  Lisainfo
                </label>
                <textarea
                  className="form-control"
                  id="AdditionalInfo"
                  name="AdditionalInfo"
                ></textarea>
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
          <BackToButton to="/nullam_events">Tagasi</BackToButton>
        </div>
      </main>
    </div>
  );
};

export default page;
