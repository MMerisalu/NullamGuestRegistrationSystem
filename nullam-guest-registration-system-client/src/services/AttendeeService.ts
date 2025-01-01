import IAttendee from "@/app/domain/IAttendee";
import { BaseEntityService } from "./BaseEntityService";
import IEvent from "@/app/domain/IEvent";


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


      async getAllFutureEventsOrdedByTimeAndName(id? : string | number ) : Promise<IEvent[] | undefined> {
        try {
          const response = await this.axios.get<IEvent[]>(`/GetFutureEvents/${id}`);
          console.log("response", response);
          if (response.status === 200) {
            return response.data;
          }
        } catch (e) {
          console.log("error: ", (e as Error).message);
          return undefined;
        }
        
    
      }
     
      async addAttendeeToAnotherEvent(
        id: string,
        body: IAttendee
      ): Promise<number | undefined> {
        console.log("body", body);
        try {
          console.log("this.axios", this.axios.defaults.baseURL);
          let response = await this.axios.post(`/AddAttendeeToAnotherEvent/${id}`, body);
          console.log("response.status:", response.status);
          if (response.status === 204) {
            return response.status;
          }
          return undefined;
        } catch (e) {
          console.log("Details -  error: ", (e as Error).message);
          return undefined;
        }
      }

      async addAttendeeToAnEvent(
        id: string,
        body: IAttendee
      ): Promise<number | undefined> {
        console.log("body", body);
        try {
          console.log("this.axios", this.axios.defaults.baseURL);
          let response = await this.axios.post(`/PostAttendee/${id}`, body);
          console.log("response.status:", response.status);
          if (response.status === 204) {
            return response.status;
          }
          if (response.status === 404) {
            return response.status;
          }
          return undefined;
        } catch (e) {
          console.log("Details -  error: ", (e as Error).message);
          return undefined;
        }
      }
}