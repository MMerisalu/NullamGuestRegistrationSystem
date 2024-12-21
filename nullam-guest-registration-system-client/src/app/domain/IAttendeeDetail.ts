import { IBaseEntity } from "./IBaseEntity";

interface IAttendeeDetails extends IBaseEntity {
    attendeeId : string,
    eventId : string,
    numberOfPeople : number,
    attendeeType : string,
    name? : string,
    code? : string

}

export default IAttendeeDetails