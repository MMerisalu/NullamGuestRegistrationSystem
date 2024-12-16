export interface SharedPaymentMethodFormValues {
    name: string;
  }
  
  export interface SharedBaseProps {
    onSubmit: (values: SharedPaymentMethodFormValues) => void;
  }
  
  export interface SharedCreateProps {
    formType: "create";
  }
  
  export interface SharedEditProps {
    formType: "edit";
    initialName: string;
  }
  
  export type SharedProps = SharedBaseProps &
    (SharedCreateProps | SharedEditProps);