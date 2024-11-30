using App.DAL.DTO;
using App.Enum;
using Base.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class AddAttendeeToAnotherEventVM : DomainEntityId<int>
    {
        public int AttendeeId { get; set; }
        
        public AttendeeType? AttendeeType { get; set; }


        [DisplayName("Osavõtjate arvu muutmine?")]
        public bool IsNumberOfPeopleFromCompanyChanged { get; set; }


        [DisplayName("Ettevõtest tulevate osavõtjate arv")]
        public int? NumberOfPeopleFromCompany { get; set; }


        [Required(ErrorMessage = "Väli Üritus on kohustuslik!")]
        [DisplayName("Üritus")]
        public int EventId { get; set; }

        public SelectList? Events { get; set; }
    }
}
