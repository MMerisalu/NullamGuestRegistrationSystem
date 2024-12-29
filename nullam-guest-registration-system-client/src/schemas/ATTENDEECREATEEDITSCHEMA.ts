import * as yup from 'yup';

const ATTENDEECREATEEDITSCHEMA = yup.object().shape({
  attendeeType: yup
    .string()
    .required("Väli on kohustuslik!"),
  surName: yup.string().when("attendeeType", (attendeeType, schema) =>
  {
    console.log('surName attendeeType', attendeeType)
    if (attendeeType[0] === '1') {
      console.log('surname required')
      return schema
        .required("Väli eesnimi on kohustuslik!")
        .max(64, "Väljale eesnimi sisestava teksti pikkus on maksimaalselt 64 tähemärki!");
    }
    return schema.notRequired();
  }),
  givenName: yup.string().when("attendeeType", (attendeeType, schema) =>
  {
    if (attendeeType[0] === '1') {
      return schema
        .required("Väli perekonnanimi on kohustuslik!")
        .max(64, "Väljale perekonnanimi sisestava teksti pikkus on maksimaalselt 64 tähemärki!");
    }
    return schema.notRequired();
  }),
  personalIdentifier: yup.string().when("attendeeType", (attendeeType, schema) =>
  {
    if (attendeeType[0] === '1') {
      return schema
        .required("Väli isikukood on kohustuslik!")
        .matches(/^\d+$/, "Eesti isikukood koosneb numbritest!")
        .max(11, "Eesti isikukoodi pikkuseks on 11 numbrit! Palun sisestage uus isikukood!");
    }
    return schema.notRequired();
  }),
  personAdditionalInfo: yup
    .string()
    .notRequired()
    .max(1000, "Väljale lisainfo sisestava teksti pikkus on maksimaalselt 1000 tähemärki!"),
  companyName: yup.string().when("attendeeType", (attendeeType, schema) =>
  {
    if (attendeeType[0] === '2') {
      return schema
        .required("Väli ettevõtte juurdiline nimi on kohustuslik!")
        .max(64, "Väljale ettevõtte juurdiline nimi sisestava teksti pikkus on maksimaalselt 64 tähemärki!");
    }
    return schema.notRequired();
  }),
  registryCode: yup.string().when("attendeeType", (attendeeType, schema) =>
  {
    if (attendeeType[0] === '2') {
      return schema
        .required("Väli ettevõtte registrikood on kohustuslik!")
        .matches(/^\d+$/, "Eesti ettevõte registrikood koosneb numbritest!")
        .max(8, "Eesti ettevõtte registrikoodi pikkuseks on 8 numbrit! Palun sisestage uus registrikood!");
    }
    return schema.notRequired();
  }),
  numberOfPeopleFromCompany: yup.number().when("attendeeType", (attendeeType, schema) =>
  {
    if (attendeeType[0] === '2') {
      return schema
        .required("Väli ettevõttest tulevate osavõtjate arv on kohustuslik!")
        .min(1, "Ettevõttest tulevate osavõtjate arv jääb vahemikku 1 kuni 250! Palun sisestage ettevõttest tulevate isikute arv uuesti.")
        .max(250, "Ettevõttest tulevate osavõtjate arv jääb vahemikku 1 kuni 250! Palun sisestage ettevõttest tulevate isikute arv uuesti.");
    }
    return schema.notRequired();
  }),
  companyAdditionalInfo: yup
    .string()
    .notRequired()
    .max(5000, "Väljale ettevõtte lisainfo sisestava teksti pikkus on maksimaalselt 5000 tähemärki!"),
});

export default ATTENDEECREATEEDITSCHEMA;
