using Base.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain
{
    public class Event : DomainEntityId<int>
    {
        

        [Required]
        [MaxLength(64)]
        
        public string Name { get; set; } = default!;

        
        [Required]
    
        public DateTime EventDateAndTime { get; set; } = default!;

        [Required]
        [MaxLength(64)]
        
        public string Location { get; set; } = default!;

        [MaxLength(1000)]
        
        public string? AdditionalInfo { get; set; }

        public ICollection<EventAndAttendee>? Attendees { get; set; }
    }
}
