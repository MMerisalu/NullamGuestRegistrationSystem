import * as yup from "yup";

const PAYMENTMETHODCREATEEDITSCHEMA = yup.object().shape({
    name: yup.string().required("Väli Maksemeetodi nimetus on kohustuslik!")
});

export default PAYMENTMETHODCREATEEDITSCHEMA;