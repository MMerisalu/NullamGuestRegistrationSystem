import IPaymentMethod from "@/app/domain/IPaymentMethod";
import axios from "axios";
import { BaseEntityService } from "./BaseEntityService";

export class PaymentMethodService extends BaseEntityService<IPaymentMethod> {
    constructor(){
        super('paymentmethods');
    } 
    
     /* private static httpClient = axios.create({
        baseURL: "https://localhost:7272/api/paymentMethods"
    })  */
    
}
    
   




