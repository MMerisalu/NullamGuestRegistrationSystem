import IEvent from "@/app/domain/IEvent";
import axios from "axios";
import { BaseEntityService } from "./BaseEntityService";
import { format } from "date-fns";
import IAttendeeDetails from "@/app/domain/IAttendeeDetail";
import { number, string } from "yup";

export class EventService extends BaseEntityService<IEvent> {
  constructor() {
    super("events");
  }

  async getAllEventsWithFormattedData(): Promise<IEvent[] | undefined> {
    try {
      const response = await this.axios.get<IEvent[]>("");
      console.log("response", response);
      if (response.status === 200) {
        response.data.forEach((element) => {
          element.eventDateAndTime = format(
            element.eventDateAndTime,
            "dd.MM.yyyy HH:mm"
          );
        });
        return response.data;
      }
    } catch (e) {
      console.log("error: ", (e as Error).message);
      return undefined;
    }
  }

  async getAttendeesByEventId(id? : string | number) : Promise<IAttendeeDetails[] | undefined> {
    try {
      const response = await this.axios.get<IAttendeeDetails[]>(`/ListOfAttendees/${id}`);
      console.log("response", response);
      if (response.status === 200) {
        return response.data;
      }
    } catch (e) {
      console.log("error: ", (e as Error).message);
      return undefined;
    }


  }


 
}
