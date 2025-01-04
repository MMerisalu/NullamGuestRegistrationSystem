import IAttendee from "@/app/domain/IAttendee";
import { BaseEntityService } from "./BaseEntityService";
import IEvent from "@/app/domain/IEvent";
import { APIErrorData, APIErrorResponse, CreateAttendeeValues } from "@/types";
import { AxiosError } from "axios";


export class AttendeeService extends BaseEntityService<IAttendee>
{
  constructor()
  {
    super('attendees');
  }

  async deleteAttendee(eventId?: string | number,
    id?: string | number): Promise<number | undefined>
  {
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
  async createAttendee( eventId: string, body: CreateAttendeeValues,): Promise<number | undefined>
  {
    let response = await this.axios.post(`/${eventId}`, body);
    console.log("CreateAttendee response.status:", response.status);
    if (response.status === 204) {
      return response.status;
    }
    return response.status;
  }
  async editAttendee(attendeeId: string, eventId: string, body: CreateAttendeeValues,)
  {
    let response = await this.axios.put(`/${attendeeId}/${eventId}`, body);
    console.log("editAttendee response.status:", response.status);
    if (response.status === 204) {
      return response.status;
    }
    return response;
  }


  async getAllFutureEventsOrdedByTimeAndName(id?: string | number): Promise<IEvent[] | undefined>
  {
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
id: string, values: { eventId: string; attendeeType: string | number; numberOfPeople: string | number; isChecked: boolean; }): Promise<number | undefined>
  {
    console.log("body", values);
    try {
      console.log("this.axios", this.axios.defaults.baseURL);
      let response = await this.axios.post(`/AddAttendeeToAnotherEvent/${id}`, values);
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
  ): Promise<number | APIErrorData>
  {
    console.log("body", body);
    try {
      console.log("this.axios", this.axios.defaults.baseURL);
      let response = await this.axios.post(`/${id}`, body);

      console.log(id)
      console.log("response.status:", response.status);

      if (response.status >= 400)
        return response.data as APIErrorData;
      else
        return response.status;

    } catch (e) {
      let error = e as AxiosError;
      console.log("Details -  error: ", error.message);
      return (error.response as unknown as APIErrorResponse).data;
    }
  } 
}