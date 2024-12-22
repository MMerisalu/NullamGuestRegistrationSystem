import { use } from "react"


/* interface IProps {
    id?: string
} */
const AddAttendeeToAnotherEvent = (props:{params: Promise<{id?: string}>}) => {
    const {id} = use(props.params)
  return (
    <div>add to another event attendee id: {id}</div>
  )
}

export default AddAttendeeToAnotherEvent