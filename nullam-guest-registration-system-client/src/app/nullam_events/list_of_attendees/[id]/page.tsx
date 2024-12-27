"use client"
import IAttendeeDetails from "@/app/domain/IAttendeeDetail";
import { AttendeeService } from "@/services/AttendeeService";
import { EventService } from "@/services/EventService";
import Link from "next/link";
import React, { use, useEffect, useState } from "react";


const service = new EventService();
const attendeeService = new AttendeeService();
const ListOfAttendees = (props: { params: Promise<{ id: string }> }) => {
  const [isLoading, setIsLoading] = useState(false)
  const { id } = use(props.params);
  const [attendees, setAttendees] = useState<IAttendeeDetails[]>();
  const fetchAttendees = async() => {

    
    try {
      const response = await service.getAttendeesByEventId(id);
      console.log(response);
      setAttendees(response || []);
    } catch (error) {
      console.error("Failed to fetch attendees:", error);
      setAttendees([]);
    } finally {
      setIsLoading(false);
    }
  }
  useEffect(() => {
    fetchAttendees()
  }, [id])
  
  if (isLoading) {
    return <p>...laadimine</p>;
  }
  return (
    <>
      <div className="container">
        <main role="main" className="pb-3">
          <h1>Üritusest osavõtjad</h1>

          <p></p>
          <table className="table">
            <thead>
              <tr>
                <th>Osavõtja</th>
                <th>Osavõtjate arv</th>
                <th>Isikukood / registrikood</th>
                <th></th>
              </tr>
            </thead>
            <tbody>
              {attendees?.map((item) => (
                <tr key={item.id}>
                <td>
                  <Link
                    style={{textDecoration: "none"}}
                    href={`/attendees/edit/${item.attendeeId}/${item.id}`}
                  >
                    {item.name}
                  </Link>
                </td>
                <td>{item.numberOfPeople}</td>
                <td>{item.code}</td>
                <td>
                  <Link
                     style={{textDecoration: "none"}} href={`/attendees/add_attendee_to_another_event/${item.attendeeId}`}
                  >
                    Lisa osavõtja teisele üritusele
                  </Link>
                </td>
                <td>
                  <form method="post" onSubmit={() => {
                    attendeeService.deleteAttendee(item.eventId, item.id)
                    .then(() => fetchAttendees());
                  } }>
                    <link className="link-danger" />
                    <button type="submit" className="btn btn-link">
                      <svg
                         style={{color: "red"}}
                        xmlns="http://www.w3.org/2000/svg"
                        width="16"
                        height="16"
                        fill="currentColor"
                        className="bi bi-trash"
                        viewBox="0 0 16 16"
                      >
                        <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5m2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5m3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0z" />
                        <path d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1zM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4zM2.5 3h11V2h-11z" />
                      </svg>
                    </button>
                    
                  </form>
                </td>
              </tr>
            ) )}
              
            </tbody>
          </table>
        </main>
      </div>
    </>
  );
};

export default ListOfAttendees;
