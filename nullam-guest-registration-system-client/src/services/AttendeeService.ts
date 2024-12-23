import IAttendee from "@/app/domain/IAttendee";
import { BaseEntityService } from "./BaseEntityService";


export class AttendeeService extends BaseEntityService<IAttendee> {
    constructor() {
        super('attendees');
    }

    async deleteAttendee(eventId? : string | number, 
        id? : string | number ) : Promise<number | undefined> {
          try {
            const response = await this.axios.delete(`/${eventId}/${id}`, {});
            console.log("response.status:", response.status);
            if (response.status === 204) {
                return response.status;
              }
        
              return undefined;
            } catch (e) {
              if (!(e instanceof Error)) {
                console.log('Details - unknown error')
                throw e
              }
              console.log("Details -  error: ", e.message);
              return undefined;
          } 
      }
}