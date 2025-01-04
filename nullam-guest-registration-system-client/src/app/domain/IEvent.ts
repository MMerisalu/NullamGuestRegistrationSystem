import { IBaseEntity } from "./IBaseEntity"

interface IEvent extends IBaseEntity {
    lineNumber : string
    name: string,
    eventDateAndTime: string,
    eventDateAndTimeFormatted : string,
    location: string,
    numberOfAttendees: number,
    additionalInfo?: string,
    eventDateTimeAndName?: string

}
export default IEvent