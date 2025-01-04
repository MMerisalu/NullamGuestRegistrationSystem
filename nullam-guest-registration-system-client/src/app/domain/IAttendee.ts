import { IBaseEntity } from "./IBaseEntity";

interface IAttendee extends IBaseEntity {
attendeeType: number,
surName?: string,
givenName? : string,
personalIdentifier? : string
personAdditionalInfo? : string,
companyName? : string,
registryCode? : string,
numberOfPeopleFromCompany? : number,
companyAdditionalInfo? : string
paymentMethodId: number
}
export default IAttendee;