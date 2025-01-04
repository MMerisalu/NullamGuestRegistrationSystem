import { CreateAttendeeFormik, EditAttendeeFormik } from "@/types";
import React from "react";

const PersonAttendeeForm = (props: {
  formik: CreateAttendeeFormik | EditAttendeeFormik;
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
        <span className="text-danger field-validation-valid">
          {props.formik.errors.surName ? (
            <div className="text-danger">{props.formik.errors.surName}</div>
          ) : null}
        </span>
      </div>
      <div id="given_name_div" className="form-group">
        <label className="control-label" htmlFor="givenName">
          Perekonnanimi
        </label>
        <input
          className="form-control"
          type="text"
          name="givenName"
          value={props.formik.values.givenName}
          onChange={props.formik.handleChange}
          onBlur={props.formik.handleBlur}
        />
        <span className="text-danger field-validation-valid">
          {props.formik.errors.givenName ? (
            <div className="text-danger">{props.formik.errors.givenName}</div>
          ) : null}
        </span>
      </div>

      <div id="personal_identifier_div" className="form-group">
        <label className="control-label" htmlFor="personalIdentifier">
          Isikukood
        </label>
        <input
          className="form-control"
          type="text"
          name="personalIdentifier"
          value={props.formik.values.personalIdentifier}
          onChange={props.formik.handleChange}
          onBlur={props.formik.handleBlur}
        />
        <span className="text-danger field-validation-valid">
          {props.formik.errors.personalIdentifier ? (
            <div className="text-danger">
              {props.formik.errors.personalIdentifier}
            </div>
          ) : null}
        </span>
      </div>

      <div id="person_additional_info_div" className="form-group">
        <label className="control-label" htmlFor="PersonAdditionalInfo">
          Lisainfo
        </label>
        <textarea
          className="form-control"
          name="personAdditionalInfo"
          value={props.formik.values.personAdditionalInfo}
          onChange={props.formik.handleChange}
          onBlur={props.formik.handleBlur}
        ></textarea>
        <span className="text-danger field-validation-valid">
          {props.formik.errors.personAdditionalInfo ? (
            <div className="text-danger">
              {props.formik.errors.personAdditionalInfo}
            </div>
          ) : null}
        </span>
      </div>
    </>
  );
};

export default PersonAttendeeForm;
