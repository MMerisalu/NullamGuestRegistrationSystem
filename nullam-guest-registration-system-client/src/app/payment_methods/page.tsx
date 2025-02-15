"use client";
import Link from "next/link";
import React, { useEffect, useState } from "react";
import IPaymentMethod from "../domain/IPaymentMethod";
import { PaymentMethodService } from "@/services/PaymentMethodService";
import { Axios, AxiosError } from "axios";
import { string } from "yup";

const paymentMethodService = new PaymentMethodService();

const PaymentMethods = () => {
  const [isLoading, setIsLoading] = useState(true);
  const [paymentMethods, setPaymentMethods] = useState<IPaymentMethod[]>([]);
  const [serverErrors, setServerErrors] = useState<string[]>([]);
  // Function to fetch payment methods
  const fetchPaymentMethods = async () => {
    try {
      const response = await paymentMethodService.getAll();
      console.log(response);
      setPaymentMethods(response || []);
    } catch (error) {
      console.error("Failed to fetch payment methods:", error);
      setPaymentMethods([])
    } finally {
      setIsLoading(false);
    }
  };

  // UseEffect to call the fetch function on component mount
  useEffect(() => {
    fetchPaymentMethods();
  }, []);

  if (isLoading) {
    return <p>...laadimine</p>;
  }

  return (
    <>
    {serverErrors.length !== 0 ? <p>{serverErrors.join(",")}</p> : ""} 
      <h1>Maksemeetodid</h1>
      <p>
        <Link style={{textDecoration:"none"}} href="/payment_methods/create/">Lisa uus maksemeetod</Link>
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
                <Link style={{textDecoration:"none"}} href={`/payment_methods/edit/${item.id}`}>MUUDA</Link>
              </td>
              <td>
                <form
                  
                >
                  <button
                    type="button"
                    className="btn btn-link text-danger"
                    aria-label="Delete"
                    onClick={(e) => {
                      setServerErrors([]);
                      paymentMethodService.delete(item.id).then(() => {
                        fetchPaymentMethods();
                      }).catch(error => {
                        alert(error);
                        setServerErrors([].concat(error));
                        e.preventDefault;
                        });
                        return false;
                      }
                    }
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
};

export default PaymentMethods;
