import IEvent from "@/app/domain/IEvent";
import axios from "axios";
import { BaseEntityService } from "./BaseEntityService";
import { format } from "date-fns";
import IAttendeeDetails from "@/app/domain/IAttendeeDetail";
import { number, string } from "yup";

export class EventService extends BaseEntityService<IEvent>
{
  constructor()
  {
    super("events");
  }

  async getAllEventsWithFormattedData(): Promise<IEvent[] | undefined>
  {
    try {
      const response = await this.axios.get<IEvent[]>("");
      console.log("response", response);
      if (response.status === 200) {
        response.data.forEach((element) =>
        {
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

  async getAttendeesByEventId(id?: string | number): Promise<IAttendeeDetails[] | undefined>
  {
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

  isAfterNow(eventDateTimeString: string)
  {
    const nowDateTime = new Date();
    console.log("eventDateTimeString", eventDateTimeString);
    const day = Number(eventDateTimeString.substring(0, 2));
    console.log("day", day);
    const monthString = eventDateTimeString.substring(3, 5);
    const month = Number(monthString) - 1;
    console.log("month", month);
    const year = Number(eventDateTimeString.substring(6, 10));
    console.log("year", year);
    const hours = Number(eventDateTimeString.substring(11, 13));
    console.log("hours", hours);
    const minutes = Number(eventDateTimeString.substring(14, 16));
    console.log("minutes", minutes);

    const eventDateTime = new Date(year, month, day, hours, minutes);
    console.log("eventDateTime", eventDateTime);
    const afterNow = eventDateTime > nowDateTime;
    return afterNow;
  }



}
