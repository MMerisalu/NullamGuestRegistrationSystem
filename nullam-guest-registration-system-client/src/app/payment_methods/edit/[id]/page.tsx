"use client";
import IPaymentMethod from "@/app/domain/IPaymentMethod";
import BackToButton from "@/components/BackToButton";
import paymentMethodCreateEditSchema from "@/schemas/paymentMethodCreateEditSchema";
import { PaymentMethodService } from "@/services/PaymentMethodService";
import { useFormik } from "formik";
import React, { useState, useEffect, use } from "react";
import SharedPaymentMethodForm from "../../../../components/SharedPaymentMethodForm";
import { SharedPaymentMethodFormValues } from "@/types";

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
  const handleSubmit = async (values: SharedPaymentMethodFormValues) => {
    const body = { id, ...values };
    const result = await service.edit(id, body);
    if (result) {
      return (window.location.href = "/payment_methods");
    }
  };

  return (
    <SharedPaymentMethodForm
      formType="edit"
      onSubmit={handleSubmit}
      initialName={method?.name ?? ""}
    />
  );
};

export default Edit;
