"use client";
import IPaymentMethod from "@/app/domain/IPaymentMethod";
import BackToButton from "@/components/BackToButton";
import paymentMethodCreateEditSchema from "@/schemas/paymentMethodCreateEdit";
import { PaymentMethodService } from "@/services/PaymentMethodService";
import { SharedProps } from "@/types";
import { useFormik } from "formik";
import React, { useState } from "react";

const SharedPaymentMethodForm = (props: SharedProps) => {
  const [method, setMethod] = useState<IPaymentMethod>();

  const service = new PaymentMethodService();
  const initialName = "initialName" in props ? props.initialName : "";
  const { values, errors, handleChange, handleSubmit, handleBlur } = useFormik({
    initialValues: {
      name: initialName,
    },

    validationSchema: paymentMethodCreateEditSchema,
    enableReinitialize: true,

    onSubmit: async (values) => {
      props.onSubmit(values);
    },
  });

  console.log(errors);

  const label = props.formType === "create" ? "Lisa" : "Muuda";

  return (
    <>
      <div className="row">
        <div className="col-md-4">
          <form onSubmit={handleSubmit}>
            <div className="form-group">
              <label htmlFor="name">Nimetus</label>
              <input
                className={`form-control ${errors.name ? "is-invalid" : ""}`}
                id="name"
                type="text"
                placeholder="Sisestage maksemeetodi nimetus"
                value={values.name}
                onChange={handleChange}
                onBlur={handleBlur}
              />

              {errors.name ? (
                <div className="text-danger">{errors.name}</div>
              ) : null}
            </div>
            <br />
            <div className="form-group">
              <input type="submit" value={label} className="btn btn-primary" />
            </div>
          </form>
        </div>
      </div>
      <br />
      <div>
        <BackToButton to="/payment_methods">Tagasi</BackToButton>
      </div>
    </>
  );
};

export default SharedPaymentMethodForm;
