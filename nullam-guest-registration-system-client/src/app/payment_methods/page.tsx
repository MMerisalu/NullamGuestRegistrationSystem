"use client";
import Link from "next/link";
import React, { useEffect, useState } from "react";
import IPaymentMethod from "../domain/IPaymentMethod";
import { PaymentMethodService } from "@/services/PaymentMethodService";

const paymentMethodService = new PaymentMethodService();
const PaymentMethods = () => {

  const [isLoading, setIsLoading] = useState(true);
  const [paymentMethods, setPaymentMethods] = useState([] as IPaymentMethod[])
  useEffect(() => {
    
    paymentMethodService.getAll()
        .then(
            response => {
                console.log(response)
                if (response)
                    setPaymentMethods(response)
                else {
                    setPaymentMethods([])
                }
                setIsLoading(false)
            }
        )
}, []);
  
      
  if (isLoading) {
    return <p>...laadimine</p>;
  } else {
    return (
      <>
        <h1></h1>

        <p>
          <Link href={"/payment_methods/create/"}>Loo Uus</Link>
        </p>
        <table className="table">
          <thead>
            <tr>
              <th>Maksemeetodi nimetus</th>

              <th></th>
            </tr>
          </thead>
          <tbody>
            {paymentMethods.map((item) => (
              <tr key={item.id}>
                <td>{item.name}</td>
                <td>
                  <Link href={"/payment_methods/edit"}>Muuda</Link>
                </td>
                <td>
                <form onSubmit={() => {
                paymentMethodService.delete(item.id);
                }}
                >
                    <button
                      type="submit"
                      className="btn btn-link text-danger"
                      aria-label="Delete"
                    >
                      <svg
                        xmlns="http://www.w3.org/2000/svg"
                        width="16"
                        height="16"
                        fill="currentColor"
                        className="bi bi-trash"
                        viewBox="0 0 16 16"
                      >
                        <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5m2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5m3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0z" />
                        <path d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h3.5a1 1 0 0 1 1 1zM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4zM2.5 3h11V2h-11z" />
                      </svg>
                    </button>
                  </form>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </>
    );
  }
};

export default PaymentMethods;
