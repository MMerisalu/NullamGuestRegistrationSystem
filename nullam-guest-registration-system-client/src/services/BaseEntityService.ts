import { useEffect, useState } from "react";
import { IBaseEntity } from "@/app/domain/IBaseEntity";
import BaseService from "./BaseService";
import { APIErrorData } from "@/types";
import { AxiosError } from "axios";

export abstract class BaseEntityService<
  TEntity extends IBaseEntity
> extends BaseService {
  constructor(baseUrl: string) {
    console.log("baseentityserivce constructor baseurl", baseUrl);
    super(baseUrl);
  }

  async getAll(): Promise<TEntity[] | undefined> {
    console.log("getting all...");
    try {
      const response = await this.axios.get<TEntity[]>("");

      console.log("response", response);
      if (response.status === 200) {
        return response.data;
      }
    } catch (e) {
      console.log("error: ", (e as Error).message);
      return undefined;
    }
  }

  async delete(id?: string | number): Promise<number | APIErrorData> {
    console.log("id", id);

    try {
      const response = await this.axios.delete(`/${id}`, {});
      console.log("response.status:", response.status);
      if (response.status >= 400)
        return response.data as APIErrorData;
      else
        return response.status;
    } catch (e) {
      let axiosError = e as AxiosError;
      if (axiosError) {
        console.log("Details -  error: ", axiosError.response);
        if (axiosError.response)
          throw axiosError.response.data ?? axiosError.response;
      }
      
      throw e;
    }
  }
  async getById(id?: string | number): Promise<TEntity | undefined> {
    try {
      const response = await this.axios.get(`/${id}`);
      if (response.status === 200) {
        return response.data;
      }
      return undefined;
    } catch (e) {
      if (!(e instanceof Error)) {
        console.log('Details - unknown error')
      } else {
      console.log("Details -  error: ", e.message);
      }
      throw e;
    }
  }

  async create(
    body: IPaymentMethodCreateEditData
  ): Promise<number | undefined> {
    console.log("body", body);
    try {
      const response = await this.axios.post(`/`, body);
      console.log("response.status:", response.status);
      if (response.status === 201) {
        return response.status;
      }
    } catch (e) {
      if (!(e instanceof Error)) {
        console.log('Details - unknown error')
      } else {
      console.log("Details -  error: ", e.message);
      }
      throw e;
    }
  }
  async edit(
    id: string,
    body: IPaymentMethodCreateEditData
  ): Promise<number | undefined> {
    console.log("body", body);
    try {
      console.log("this.axios", this.axios.defaults.baseURL);
      let response = await this.axios.put(`/${id}`, body);
      console.log("response.status:", response.status);
      if (response.status === 204) {
        return response.status;
      }
      return undefined;
    } catch (e) {
      if (!(e instanceof Error)) {
        console.log('Details - unknown error')
      } else {
      console.log("Details -  error: ", e.message);
      }
      throw e;
    }
  }
}
