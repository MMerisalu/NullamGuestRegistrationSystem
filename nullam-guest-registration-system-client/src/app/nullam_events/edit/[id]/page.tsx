"use client";

import IEvent from "@/app/domain/IEvent";
import NotFound from "@/app/not-found";
import SharedEventForm from "@/components/SharedEventForm";
import { EventService } from "@/services/EventService";
import { SharedEventFormValues } from "@/types";
import React, { useState, useEffect, use } from "react";

const Edit = (props: { params: Promise<{ id: string }> }) => {
  const [event, setEvent] = useState<IEvent>();
  console.log("event", event);
  const { id } = use(props.params);
  console.log(id);
  useEffect(() => {
    async function getEvent() {
      const service = new EventService();
      const event = await service.getById(id);
      console.log("event", event);
      setEvent(event);
    }
    getEvent();
  }, [id]);

  const service = new EventService();
  const afterNow = event && service.isAfterNow(event.eventDateAndTime);
  if (!afterNow) {
    return <NotFound />;
  }
  const initialValues = {
    name: event?.name ?? "",
    eventDateAndTime: event?.eventDateAndTime ?? "",
    location: event?.location ?? "",
    additionalInfo: event?.additionalInfo ?? "",
  };
  // const {
  //   values,
  //   errors,
  //   handleChange,
  //   handleSubmit: handleSubmit2,
  //   handleBlur,
  // } = useFormik({
  //   initialValues,

  //   validationSchema: eventCreateEditSchema,
  //   enableReinitialize: true,

  //   onSubmit: async (values) => {
  //     const body = { id, ...values };
  //     const result = await service.edit(id, body);
  //     if (result) {
  //       return (window.location.href = "/");
  //     }
  //   },
  // });

  // console.log(errors);

  async function handleSubmit(values: SharedEventFormValues) {
    const body = { id, ...values };
    const result = await service.edit(id, body);
    if (result) {
      window.location.href = "/";
    }
  }

  return (
    <div className="container">
      
      <SharedEventForm
        formType="edit"
        initialValues={initialValues}
        onSubmit={handleSubmit}
      />
    </div>
  );
};
export default Edit;
