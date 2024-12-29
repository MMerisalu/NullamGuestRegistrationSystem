import { FormikValues, useFormik } from "formik";

export interface SharedPaymentMethodFormValues
{
  name: string;
}

export interface SharedBaseProps
{
  onSubmit: (values: SharedPaymentMethodFormValues) => void;
}

export interface SharedCreateProps
{
  formType: "create";
}

export interface SharedEditProps
{
  formType: "edit";
  initialName: string;
}

export type SharedProps = SharedBaseProps &
  (SharedCreateProps | SharedEditProps);

export interface CreateAttendeeValues
{
  attendeeType: string;
  PaymentMethodId: string;
  surName: string;
}

export type FormikInstance<Values extends FormikValues> = ReturnType<typeof useFormik<Values>>
export type CreateAttendeeFormik = FormikInstance<CreateAttendeeValues>