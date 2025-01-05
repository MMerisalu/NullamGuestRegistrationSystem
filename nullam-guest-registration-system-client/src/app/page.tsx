"use client";

import { useRouter } from "next/navigation";
import Footer from "@/components/Footer";
import Header from "@/components/Header";
import { EventService } from "@/services/EventService";
import { useState, useEffect } from "react";
import Link from "next/link";
import IEvent from "./domain/IEvent";
const eventService = new EventService();

export default function Home() {
  const [isLoading, setIsLoading] = useState(true);
  const router = useRouter();
  const [events, setEvents] = useState<IEvent[]>([]);
  const [date, setDate] = useState<string>();
  const [serverErrors, setServerErrors] = useState<string[]>([]);
  // Function to fetch events
  const fetchEvents = async () => {
    try {
      const response = await eventService.getAllEventsWithFormattedData();
      console.log(response);
      setEvents(response || []);
    } catch (error) {
      console.error("Failed to fetch events:", error);
      setEvents([]);
    } finally {
      setIsLoading(false);
      getDate();
    }
  };
  const getDate = () => {
    const dt = null;
  };
  const handelDate = () => {
    let dt = new Date().toLocaleDateString();
    setDate(dt);
  };
  // UseEffect to call the fetch function on component mount
  useEffect(() => {
    fetchEvents();
  }, []);
  if (isLoading) {
    return <p>...laadimine</p>;
  }

  return (
    <>
      <h1>Üritused</h1>
      <p>
        <Link style={{ textDecoration: "none" }} href="/nullam_events/Create/">
          Lisa uus üritus
        </Link>
      </p>
      <table className="table">
        <thead>
          <tr>
            <th></th>
            <th>Ürituse nimi</th>
            <th>Toimumisaeg</th>
            <th>Koht</th>
            <th>Osavõtjate arv</th>
            <th>Lisainfo</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {events.map((item, index) => (
            <tr key={item.id}>
              <td>{index + 1}. after now: </td>
              <td>
                <Link
                  style={{ textDecoration: "none" }}
                  href={`/nullam_events/list_of_attendees/${item.id}`}
                >
                  {item.name}
                </Link>
              </td>
              <td>{item.eventDateAndTimeFormatted}</td>
              <td>{item.location}</td>
              <td>{item.numberOfAttendees}</td>
              <td>{item.additionalInfo}</td>
              <td>
                {eventService.isAfterNow(item.eventDateAndTime) && (
                  <>
                    <Link
                      style={{ textDecoration: "none" }}
                      href={`/nullam_events/edit/${item.id}`}
                    >
                      Muuda
                    </Link>{" "}
                    |
                    <Link
                      style={{ textDecoration: "none" }}
                      href={`/attendees/create/${item.id}`}
                    >
                      OSAVÕTJAD
                    </Link>
                    <form>
                  <button
                    type="submit"
                    className="btn btn-link text-danger"
                    aria-label="Delete"
                    onClick={() => {
                      eventService.delete(item.id).then(() => {
                        fetchEvents();
                      })
                        .catch(error => {
                          alert(error);
                          setServerErrors([].concat(error));
                      });
                    }}
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
                  </>
                )}
              </td>

              <td>
                
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </>
  );
}
