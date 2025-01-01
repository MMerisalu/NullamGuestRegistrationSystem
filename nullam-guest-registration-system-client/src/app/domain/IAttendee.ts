import { IBaseEntity } from "./IBaseEntity";

interface IAttendee extends IBaseEntity {
attendeeType: number | string,
surName?: string,
givenName? : string,
personalIdentifier? : string
personAdditionalInfo? : string,
companyName? : string,
registryCode? : string,
numberOfPeopleFromCompany? : number | string,
companyAdditionalInfo? : string
}
export default IAttendee;