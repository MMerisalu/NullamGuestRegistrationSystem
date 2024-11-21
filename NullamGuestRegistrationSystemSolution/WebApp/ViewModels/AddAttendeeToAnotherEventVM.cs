using Base.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace WebApp.ViewModels
{
    public class AddAttendeeToAnotherEventVM : DomainEntityId<int>
    {
        [DisplayName("Osavõtjate arvu muutmine?")]
        public bool IsNumberOfAttendeesChanged { get; set; }

        public int EventId { get; set; }

        public SelectList? Events { get; set; }
    }
}
