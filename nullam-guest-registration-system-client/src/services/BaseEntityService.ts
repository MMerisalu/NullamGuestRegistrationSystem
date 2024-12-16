import { useEffect, useState } from "react";
import { IBaseEntity } from "@/app/domain/IBaseEntity";
import BaseService from "./BaseService";

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

  async delete(id?: string | number): Promise<number | undefined> {
    console.log("id", id);

    try {
      const response = await this.axios.delete(`/${id}`, {});
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
        throw e
      }
      console.log("Details -  error: ", e.message);
      return undefined;
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
        console.log("Details - unknown error")

        throw e
      }
      console.log("Details -  error: ", e.message);
      return undefined;
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
      console.log("Details -  error: ", (e as Error).message);
      return undefined;
    }
  }
}
