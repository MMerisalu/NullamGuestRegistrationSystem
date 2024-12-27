import * as yup from "yup";

const ADDATTENDEETOANOTHEREVENTSCHEMA = yup.object().shape({
  numberOfPeople : yup
    .number()
    .notRequired()
    .min(1, "Ettevõtest tulevate osavõtjate arv jääb vahemikku 1 kuni 250! Palun sisestage ettevõttest tulevate isikute arv uuesti.")
    .max(250,"Ettevõtest tulevate osavõtjate arv jääb vahemikku 1 kuni 250! Palun sisestage ettevõttest tulevate isikute arv uuesti." ),
    eventId: yup.string().required("Väli üritus on kohustuslik!")
    })
    

  export default ADDATTENDEETOANOTHEREVENTSCHEMA;


