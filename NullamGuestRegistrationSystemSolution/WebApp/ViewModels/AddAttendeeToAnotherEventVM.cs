using App.DAL.DTO;
using App.Enum;
using Base.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace WebApp.ViewModels
{
    public class AddAttendeeToAnotherEventVM : DomainEntityId<int>
    {

        public int AttendeeId { get; set; }
        
        public AttendeeType? AttendeeType { get; set; }
        [DisplayName("Osavõtjate arvu muutmine?")]
        public bool IsNumberOfPeopleFromCompanyChanged { get; set; }

        public int NumberOfPeopleFromCompany { get; set; }

        [DisplayName("Üritus")]

        public int EventId { get; set; }

        public SelectList? Events { get; set; }
    }
}
