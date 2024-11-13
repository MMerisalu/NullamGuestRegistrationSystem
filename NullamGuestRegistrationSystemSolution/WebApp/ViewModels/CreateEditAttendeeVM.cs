using App.Domain;
using Base.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
//using UoN.ExpressiveAnnotations.Net8.Attributes;

namespace WebApp.ViewModels
{
    public class CreateEditAttendeeVM : DomainEntityId<int>
    {
        [DisplayName("Osavõtja tüüp")]
        [Required(ErrorMessage = "Osavõtja tüübi valimine on kohustuslik!")]
        [EnumDataType(typeof(AttendeeType))]
        public AttendeeType? AttendeeType { get; set; }
        
        [Required(ErrorMessage = "Väli eesnimi on kohustuslik!")]
        //[RequiredIf("AttendeeType == App.Domain.AttendeeType.Person")]
        [MaxLength(64, ErrorMessage = "Väljale eesnimi sisestava teksti pikkus on maksimaalselt 64 tähemärki!")]
        [StringLength(64, MinimumLength = 1)]
        [DisplayName("Eesnimi")]
        public string SurName { get; set; } = default!;
        [Required(ErrorMessage = "Väli perekonnanimi on kohustuslik!")]
        [DisplayName("Perekonnanimi")]
        public string GivenName { get; set; } = default!;

        [Required(ErrorMessage = "Väli ettevõtte juurdiline nimi on kohustuslik")]
       // [RequiredIf("AttendeeType == App.Domain.AttendeeType.Company", ErrorMessage = "Väli ettevõtte juurdiline nimi on kohustuslik")]
        [MaxLength(64, ErrorMessage = "Väljale ettevõtte juurdiline nimi sisestava teksti pikkus on maksimaalselt 64 tähemärki!")]
        [StringLength(64, MinimumLength = 1)]
        [DisplayName("Ettevõtte juurdiline nimi")]
        public string CompanyName { get; set; } = default!;
    }
}

