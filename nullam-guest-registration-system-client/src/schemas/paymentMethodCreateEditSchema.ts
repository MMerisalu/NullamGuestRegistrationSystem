import * as yup from "yup";

const paymentMethodCreateEditSchema = yup.object().shape({
    name: yup.string().required("Väli Maksemeetodi nimetus on kohustuslik!")
});

export default paymentMethodCreateEditSchema;