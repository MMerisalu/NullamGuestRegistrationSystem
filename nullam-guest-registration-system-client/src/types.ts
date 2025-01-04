import { FormikValues, useFormik } from "formik";

export interface SharedPaymentMethodFormValues
{
  name: string;
}
export interface SharedEventFormValues
{
  name: string;
  eventDateAndTime: string,
  location: string,
  additionalInfo: string
}

export interface SharedEventFormBaseProps
{
  onSubmit: (values: SharedEventFormValues) => Promise<void>;
}

export interface SharedEventFormCreateProps
{
  formType: 'create'
}
export interface SharedEventFormEditProps
{
  initialValues: SharedEventFormValues
  formType: 'edit'
}

export type SharedEventFormTypeProps = SharedEventFormCreateProps | SharedEventFormEditProps
export type SharedEventFormProps = SharedEventFormBaseProps & SharedEventFormTypeProps

export interface CreateAttendeeValues
{
  attendeeType: string | number;
  paymentMethodId: string | number;
  surName?: string;
  givenName?: string;
  personalIdentifier?: string;
  personAdditionalInfo?: string;
  companyName?: string;
  registryCode?: string;
  numberOfPeopleFromCompany?: string | number;
  companyAdditionalInfo?: string;

}

export interface APIErrorResponse {
  data: APIErrorData
}
export interface APIErrorData {
  title: string,
  status: number,
  errors: APIErrorMessages
}

export type APIErrorMessages = {
  [key:string]: Array<string>
}

export interface CreateAttendeePersonValues extends CreateAttendeeValues{
 
}


export interface EditAttendeeValues extends CreateAttendeeValues
{
  attendeeId: string | number
}
export interface SharedEventValues
{
  eventName: string,
  eventDateAndTime: string,
  location: string,
  additionalInfo: string
}


export type FormikInstance<Values extends FormikValues> = ReturnType<typeof useFormik<Values>>
export type CreateAttendeeFormik = FormikInstance<CreateAttendeeValues>
export type EditAttendeeFormik = FormikInstance<EditAttendeeValues>