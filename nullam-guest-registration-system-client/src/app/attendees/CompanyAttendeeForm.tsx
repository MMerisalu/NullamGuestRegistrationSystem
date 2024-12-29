import React from "react";

const CompanyAttendeeForm = () => {
  return (
    <>
      <div id="company_name_div" className="form-group">
        <label className="control-label" htmlFor="CompanyName">
          Ettevõtte juurdiline nimi
        </label>
        <input
          name="CompanyName"
          className="form-control"
          type="text"
          //value=""
        />
        <span className="text-danger field-validation-valid"></span>
      </div>
      <div id="registry_code_div" className="form-group">
        <label className="control-label" htmlFor="RegistryCode">
          Ettevõtte registrikood
        </label>
        <input
          className="form-control"
          type="text"
          name="RegistryCode"
          //value=""
        />
        <span className="text-danger field-validation-valid"></span>
      </div>
      <div id="number_of_people_from_company_div" className="form-group">
        <label className="control-label" htmlFor="NumberOfPeopleFromCompany">
          Ettevõttest tulevate osavõtjate arv
        </label>
        <input
          className="form-control"
          type="number"
          name="NumberOfPeopleFromCompany"
          //value=""
        />
        <span className="text-danger field-validation-valid"></span>
      </div>
      <div id="company_additional_info_div" className="form-group">
        <label className="control-label" htmlFor="CompanyAdditionalInfo">
          Lisainfo
        </label>
        <textarea
          className="form-control"
          name="CompanyAdditionalInfo"
        ></textarea>
        <span className="text-danger field-validation-valid"></span>
      </div>
    </>
  );
};

export default CompanyAttendeeForm;
