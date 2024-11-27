using App.Enum;
using Base.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.DTO
{
    public class AttendeeDetailDTO : DomainEntityId<int>
    {
        public int AttendeeId { get; set; }
        public int EventId { get; set; }
        public int NumberOfPeople { get; set; }
        public AttendeeType? AttendeeType { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }

    }
}
