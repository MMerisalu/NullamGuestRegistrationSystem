import { useEffect, useState } from "react";
import { IBaseEntity } from "@/app/domain/IBaseEntity";
import BaseService from "./BaseService";

export abstract class BaseEntityService<TEntity extends IBaseEntity> extends BaseService {
  constructor(baseUrl: string) {
    console.log('baseentityserivce constructor baseurl', baseUrl)
    super(baseUrl);
  }

  async getAll(): Promise<TEntity[] | undefined> {
    try {
       
        const response = await this.axios.get<TEntity[]>('',
          {
            
          });

        console.log('response', response);
        if (response.status === 200) {
          return response.data;

        }

    } catch (e) {
      console.log('error: ', (e as Error).message);
      return undefined;
    }

  }


  async delete(id?: string): Promise<number | undefined> {
    console.log('id', id)
    
    try {
        let response = await this.axios.delete(`/${id}`,
          { 
          });
        console.log('response.status:', response.status)
        if (response.status === 204) {
          return response.status
        }
      
      return undefined;
    } catch (e) {
      console.log('Details -  error: ', (e as Error).message);
      return undefined;
    }
  }
 

  async create(body: IPaymentMethodCreateEditData): Promise<number | undefined> {
    console.log('body', body)
    try {
    
      
        console.log('this.axios', this.axios.defaults.baseURL)
        let response = await this.axios.post(`/`, body);
        console.log('response.status:', response.status)
        if (response.status === 201) {
          return response.status
        }
      
     
    } catch (e) {
      console.log('Details -  error: ', (e as Error).message);
      return undefined;
    }
  } 
  /* async edit(id: string, body: IVehicleFormData| IScheduleFormData | ICommentFormData): Promise<number | undefined> {
    console.log('body', body)
    try {
      let user = IdentityService.getCurrentUser();
      if (user) {
        console.log('this.axios', this.axios.defaults.baseURL)
        let response = await this.axios.put(`/${id}`, body,
          {
            headers: {
              'Authorization': 'Bearer ' + user.token
            }
          });
        console.log('response.status:', response.status)
        if (response.status === 204) {
          return response.status
        }
      }
      else {
        throw Error("User is not logged in");
      }
      return undefined;
    } catch (e) {
      console.log('Details -  error: ', (e as Error).message);
      return undefined;
    }
  } */
} 