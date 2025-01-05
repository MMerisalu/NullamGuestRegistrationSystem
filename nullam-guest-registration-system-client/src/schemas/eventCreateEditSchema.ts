import * as yup from "yup";

const DATETIME: Date = new Date();

const EVENTCREATEEDITSCHEMA = yup.object().shape({
    name: yup.string()
    .required("Väli Ürituse nimi on kohustuslik!")
    .max(64, "Väljale sisestatava teksti maksimaalne pikkus peab jääma vahemikku 1 - 64 tähemärki!"),
    eventDateAndTime: yup.date()
    .required("Väli Ürituse toimumisaeg on kohustuslik!")
    .typeError("Väli Ürituse nimi on kohustuslik!")
    .min(DATETIME, "Sisestatud kuupäev / kellaaeg on juba möödunud. Palun valige uus."),
    location: yup.string()
    .required("Väli Koht on kohustuslik!")
    .max(64,"Väljale sisestatava teksti maksimaalne pikkus peab jääma vahemikku 1 - 64 tähemärki!" ),
    additionalInfo: yup.string()
    .notRequired()
    .max(1000, "Väljale sisestatava teksti maksimaalne pikkus peab jääma vahemikku 1 - 1000 tähemärki!" )
});

export default EVENTCREATEEDITSCHEMA;


