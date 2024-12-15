"use client";
import IPaymentMethod from "@/app/domain/IPaymentMethod";
import BackToButton from "@/components/BackToButton";
import paymentMethodCreateEditSchema from "@/schemas/paymentMethodCreateEdit";
import { PaymentMethodService } from "@/services/PaymentMethodService";
import { useFormik } from "formik";
import React, { useState, useEffect, use } from "react";

const Edit = (props: { params: Promise<{ id: string }> }) => {
  const [method, setMethod] = useState<IPaymentMethod>();
  const { id } = use(props.params);
  useEffect(() => {
    async function getPaymentMethod() {
      const service = new PaymentMethodService();
      const method = await service.getById(id);
      
      
      console.log("method", method);
      setMethod(method);
    }
    getPaymentMethod();
  }, [id]);

  const service = new PaymentMethodService();
  const { values, errors, handleChange, handleSubmit, handleBlur } = useFormik({
    initialValues: {
      name: method?.name ?? "",
    },

    validationSchema: paymentMethodCreateEditSchema,
    enableReinitialize: true,

    onSubmit: async (values) => {
      const body = { id, ...values };
      const result = await service.edit(id, body);
      if (result) {
        return (window.location.href = "/payment_methods");
      }
    },
  });

  console.log(errors);

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
              <input type="submit" value="Lisa" className="btn btn-primary" />
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

export default Edit;
