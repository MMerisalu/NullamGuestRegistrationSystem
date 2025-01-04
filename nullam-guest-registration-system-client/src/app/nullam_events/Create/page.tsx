"use client";
import { useFormik } from "formik";
import BackToButton from "@/components/BackToButton";
import React from "react";
import { EventService } from "@/services/EventService";
import SharedEventForm from "@/components/SharedEventForm";
import { SharedEventFormValues } from "@/types";

const service = new EventService();
const Create = () => {
  const handleSubmit = async (values: SharedEventFormValues) => {
    const result = await service.create(values);
    if (result) {
      window.location.href = "/";
    }
  };

  return <SharedEventForm formType="create" onSubmit={handleSubmit} />;
};

export default Create;
