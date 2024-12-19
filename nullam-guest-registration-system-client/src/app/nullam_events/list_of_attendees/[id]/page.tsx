import { useParams } from 'next/navigation'
import React, { use } from 'react'

interface IProps {
  id: string
}

const ListOfAttendees = (props: { params: Promise<{ id: string }> }) => {
  const {id} = use(props.params);
  return (
    <div>ListOfAttendees event id: {id}</div>
  )
}

export default ListOfAttendees