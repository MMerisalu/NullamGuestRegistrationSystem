using Base.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain
{
    public class EventAndAttendee : DomainEntityId<int>
    {
        public int AttendeeId { get; set; }

        public Attendee? Attendee { get; set; }

        public int EventId { get; set; }

        public Event? Event { get; set; }
    }
}
