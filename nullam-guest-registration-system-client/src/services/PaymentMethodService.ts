import IPaymentMethod from "@/app/domain/IPaymentMethod";
import axios from "axios";
import { BaseEntityService } from "./BaseEntityService";

export class PaymentMethodService extends BaseEntityService<IPaymentMethod> {
    constructor(){
        super('paymentmethods');
    }
    
    
}
    
   




