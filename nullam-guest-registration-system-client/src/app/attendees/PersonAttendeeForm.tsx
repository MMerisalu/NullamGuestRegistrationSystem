import { CreateAttendeeFormik } from "@/types";
import React from "react";

const PersonAttendeeForm = (props: {
  formik: CreateAttendeeFormik
}) => {
  return (
    <>
      <div id="sur_name_div" className="form-group">
        <label className="control-label" htmlFor="surName">
          Eesnimi
        </label>
        <input
          className="form-control"
          type="text"
          name="surName"
          value={props.formik.values.surName}
          onChange={props.formik.handleChange}
          onBlur={props.formik.handleBlur}
        />
        {props.formik.errors.surName ? (
                  <div className="text-danger">{props.formik.errors.surName}</div>
                ) : null}
        <span className="text-danger field-validation-valid"></span>
      </div>
      <div id="given_name_div" className="form-group">
        <label className="control-label" htmlFor="GivenName">
          Perekonnanimi
        </label>
        <input
          className="form-control"
          type="text"
          name="GivenName"
          //value=""
        />
        <span className="text-danger field-validation-valid"></span>
      </div>

      <div id="personal_identifier_div" className="form-group">
        <label className="control-label" htmlFor="PersonalIdentifier">
          Isikukood
        </label>
        <input
          className="form-control"
          type="text"
          name="PersonalIdentifier"
          //value=""
        />
        <span className="text-danger field-validation-valid"></span>
      </div>
      <div id="person_additional_info_div" className="form-group">
        <label className="control-label" htmlFor="PersonAdditionalInfo">
          Lisainfo
        </label>
        <textarea
          className="form-control"
          name="PersonAdditionalInfo"
        ></textarea>
        <span className="text-danger field-validation-valid"></span>
      </div>
    </>
  );
};

export default PersonAttendeeForm;
