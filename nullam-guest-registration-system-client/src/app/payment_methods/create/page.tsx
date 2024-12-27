"use client";
import { useFormik } from "formik";
import BackToButton from "@/components/BackToButton";
import React from "react";
import paymentMethodCreateEditSchema from "@/schemas/PAYMENTMETHODCREATEEEDITSCHEMA";
import { PaymentMethodService } from "@/services/PaymentMethodService";
import SharedPaymentMethodForm from "../../../components/SharedPaymentMethodForm";
import { SharedPaymentMethodFormValues } from "@/types";

const service = new PaymentMethodService();
const Create = () => {
  const handleSubmit = async (values: SharedPaymentMethodFormValues) => {
    const result = await service.create(values);
    if (result) {
      return (window.location.href = "/payment_methods");
    }
  };

  return <SharedPaymentMethodForm formType="create" onSubmit={handleSubmit} />;
};

export default Create;
