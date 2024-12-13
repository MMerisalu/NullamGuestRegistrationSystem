'use client'
import {useFormik} from "formik"
import Button from "@/components/Button";
import React from "react";
import paymentMethodCreateEditSchema from "@/schemas/paymentMethodCreateEdit";
import { PaymentMethodService } from "@/services/PaymentMethodService";

const service = new PaymentMethodService;
const create = () => {
 
  const { values, errors, handleChange, handleSubmit, handleBlur} = useFormik({
    initialValues: {
      name: "",  
    },

    

    validationSchema: paymentMethodCreateEditSchema,

    onSubmit : async (values) => {
      const result = await service.create(values);
      if (result) {
        return window.location.href = '/payment_methods';
        
      }
    } 
  });
  
  console.log(errors);

  return (
    <>
      <div className="row">
        <div className="col-md-4">
          <form onSubmit={handleSubmit}>
            <div className="form-group">
              <label htmlFor="name">Nimetus</label>
              <input className={`form-control ${errors.name ? 'is-invalid' : ''}`} id="name" type="text" placeholder="Sisestage maksemeetodi nimetus" value={values.name} 
              onChange={handleChange}
              onBlur={handleBlur}/>
              
              {errors.name ? (<div className="text-danger">{errors.name}</div>) : null}
            </div>
            <br />
            <div className="form-group">
              <input type="submit" value="Lisa" className="btn btn-primary" />
            </div>
          </form>
        </div>
      </div>
      <br />
      <div>
       <Button to="/payment_methods">Tagasi</Button>  
      </div>
    </> 
  );
};

export default create;
 

 

