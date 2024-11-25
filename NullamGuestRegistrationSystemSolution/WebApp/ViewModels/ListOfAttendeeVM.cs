using App.Enum;
using Base.Domain;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace WebApp.ViewModels
{
    public class ListOfAttendeeVM : DomainEntityId<int>
    {
        public AttendeeType? AttendeeType { get; set; }

        [DisplayName("Osavõtja")]
        public string Name { get; set; } = default!;


        [DisplayName("Osavõtjate arv")]
        public int NumberOfAttendees { get; set; }


        [DisplayName("Isikukood / registrikood ")]
        public string Code { get; set; } = default!;

        public int EventId { get; set; }
    }
}
