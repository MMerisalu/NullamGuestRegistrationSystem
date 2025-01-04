using App.Enum;
using Base.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using UoN.ExpressiveAnnotations.Net8.Attributes;

namespace WebApp.ViewModels
{
    public class CreateAttendeeVM : DomainEntityId<int>
    {
        #region Event Details (Readonly)

        [DisplayName("Ürituse nimi")]
        public required string EventName { get; set; }

        [DisplayName("Toimumisaeg")]
        public required string FormattedEventDate { get; set; }
        [DisplayName("Koht")]
        public required string Location { get; set; }

        public ICollection<ListOfAttendeeVM> Attendees { get; set; } = new List<ListOfAttendeeVM>();

        #endregion  Event Details (Readonly)


        [DisplayName("Osavõtja tüüp")]
        [Required(ErrorMessage = "Osavõtja tüübi valimine on kohustuslik!")]
        [EnumDataType(typeof(AttendeeType))]
        public AttendeeType? AttendeeType { get; set; }

        [RequiredIf("AttendeeType == App.Enum.AttendeeType.Person")]
        [MaxLength(64, ErrorMessage = "Väljale eesnimi sisestava teksti pikkus on maksimaalselt 64 tähemärki!")]
        [StringLength(64, MinimumLength = 1)]
        [DisplayName("Eesnimi")]
        public string? SurName { get; set; }

        [RequiredIf("AttendeeType == App.Enum.AttendeeType.Person")]
        [MaxLength(64, ErrorMessage = "Väljale perekonnanimi sisestava teksti pikkus on maksimaalselt 64 tähemärki!")]
        [StringLength(64, MinimumLength = 1)]
        [DisplayName("Perekonnanimi")]
        public string? GivenName { get; set; }

        [RequiredIf("AttendeeType == App.Enum.AttendeeType.Person")]
        [RegularExpression("^[0-9]{11,11}$", ErrorMessage = "Eesti isikukood pikkuseks on 11 numbrit! " +
            "Palun sisestage uus isikukood!")]
        [StringLength(11)]
        [DisplayName("Isikukood")]
        public string? PersonalIdentifier { get; set; }

        [MaxLength(1000, ErrorMessage = "Sisestava teksti pikkus peab olema kuni 1000 tähemärki.")]
        [DisplayName("Lisainfo")]

        public string? PersonAdditionalInfo { get; set; }

        [RequiredIf("AttendeeType == App.Enum.AttendeeType.Company")]
        [MaxLength(64, ErrorMessage = "Väljale ettevõtte juurdiline nimi sisestava teksti pikkus on maksimaalselt 64 tähemärki!")]
        [StringLength(64, MinimumLength = 1)]
        [DisplayName("Ettevõtte juurdiline nimi")]
        public string? CompanyName { get; set; }

        [RequiredIf("AttendeeType == App.Enum.AttendeeType.Company")]
        [RegularExpression("^[0-9]{8,8}$", ErrorMessage = "Ettevõtte registrikoodi pikkuseks on 8 numbrit! " +
            "Palun sisestage uus registrikood!")]
        [StringLength(8)]
        [DisplayName("Ettevõtte registrikood")]
        public string? RegistryCode { get; set; }

        [RequiredIf("AttendeeType == App.Enum.AttendeeType.Company")]
        [DisplayName("Ettevõttest tulevate osavõtjate arv")]
        [Range(1, 250, ErrorMessage = "Ettevõtest tulevate osavõtjate arv jääb vahemikku 1 kuni 250! Palun sisestage ettevõttest tulevate isikute arv uuesti.")]
        public int? NumberOfPeopleFromCompany { get; set; }

        [MaxLength(5000, ErrorMessage = "Sisestava teksti pikkus peab olema kuni 5000 tähemärki")]
        [DisplayName("Lisainfo")]

        public string? CompanyAdditionalInfo { get; set; }
        [DisplayName("Maksemeetod")]
        [Required(ErrorMessage = "Maksemeetodi valimine on kohustuslik!")]
        public int PaymentMethodId { get; set; }

        public SelectList? PaymentMethods { get; set; }
    }
}
