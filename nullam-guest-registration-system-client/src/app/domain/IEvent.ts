import { IBaseEntity } from "./IBaseEntity"

interface IEvent extends IBaseEntity {
    lineNumber : string
    name: string,
    eventDateAndTime: string,
    location: string,
    numberOfAttendees: number,
    additionalInfo?: string

}
export default IEvent