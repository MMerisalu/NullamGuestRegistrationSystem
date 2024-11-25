using Base.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.DTO
{
    public class EventAndAttendeeDTO: DomainEntityId<int>
    {
        public int AttendeeId { get; set; }
        public int EventId { get; set; }
        public int NumberOfPeopleFromCompany { get; set; }
    }
}
