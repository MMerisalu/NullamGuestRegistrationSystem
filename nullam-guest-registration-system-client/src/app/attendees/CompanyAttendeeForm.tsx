import { CreateAttendeeFormik } from "@/types";
import React from "react";

const CompanyAttendeeForm = (props: { formik: CreateAttendeeFormik }) => {
  return (
    <>
      <div id="company_name_div" className="form-group">
        <label className="control-label" htmlFor="CompanyName">
          Ettevõtte juurdiline nimi
        </label>
        <input
          name="companyName"
          className="form-control"
          type="text"
          value={props.formik.values.companyName}
          onChange={props.formik.handleChange}
          onBlur={props.formik.handleBlur}
        />
        <span className="text-danger field-validation-valid">
          {props.formik.errors.companyName ? (
            <div className="text-danger">{props.formik.errors.companyName}</div>
          ) : null}
        </span>
      </div>
      <div id="registry_code_div" className="form-group">
        <label className="control-label" htmlFor="RegistryCode">
          Ettevõtte registrikood
        </label>
        <input
          className="form-control"
          type="text"
          name="registryCode"
          value={props.formik.values.registryCode}
          onChange={props.formik.handleChange}
          onBlur={props.formik.handleBlur}
        />
        <span className="text-danger field-validation-valid">
          {props.formik.errors.registryCode ? (
            <div className="text-danger">{props.formik.errors.registryCode}</div>
          ) : null}
        </span>
      </div>
      <div id="number_of_people_from_company_div" className="form-group">
        <label className="control-label" htmlFor="NumberOfPeopleFromCompany">
          Ettevõttest tulevate osavõtjate arv
        </label>
        <input
          className="form-control"
          type="number"
          name="numberOfPeopleFromCompany"
          value={props.formik.values.numberOfPeopleFromCompany}
          onChange={props.formik.handleChange}
          onBlur={props.formik.handleBlur}
        />
        <span className="text-danger field-validation-valid">
          {props.formik.errors.numberOfPeopleFromCompany ? (
            <div className="text-danger">{props.formik.errors.numberOfPeopleFromCompany}</div>
          ) : null}
        </span>
      </div>
      <div id="company_additional_info_div" className="form-group">
        <label className="control-label" htmlFor="CompanyAdditionalInfo">
          Lisainfo
        </label>
        <textarea
          className="form-control"
          name="companyAdditionalInfo"
          value={props.formik.values.companyAdditionalInfo}
          onChange={props.formik.handleChange}
          onBlur={props.formik.handleBlur}
        ></textarea>
        <span className="text-danger field-validation-valid">
        {props.formik.errors.companyAdditionalInfo ? (
            <div className="text-danger">{props.formik.errors.companyAdditionalInfo}</div>
          ) : null}
        </span>
      </div>
    </>
  );
};

export default CompanyAttendeeForm;
