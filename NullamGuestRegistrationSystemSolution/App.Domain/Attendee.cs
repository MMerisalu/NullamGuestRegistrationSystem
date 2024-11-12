using App.Validators;
using Base.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UoN.ExpressiveAnnotations.Net8.Attributes;

namespace App.Domain
{
    public class Attendee : DomainEntityId<int>
    {
        [DisplayName("Osavõtja tüüp")]
        public AttendeeType? AttendeeType { get; set; }

        #region Person info

        [RequiredIfCustom(nameof(AttendeeType), nameof(AttendeeType.Value.Person))]
        [DisplayName("Eesnimi")]
        public string SurName { get; set; } = default!;

        [RequiredIfCustom(nameof(AttendeeType), nameof(AttendeeType.Value.Person))]
        [DisplayName("Perekonnanimi")]
        public string GivenName { get; set; } = default!;

        [RequiredIfCustom(nameof(AttendeeType), nameof(AttendeeType.Value.Person))]
        [RegularExpression("^[0-9]{11,11}$", ErrorMessage = "Eesti isikukood pikkuseks on 11 numbrit! " +
            "Palun sisestage uus isikukood!")]
        [StringLength(11)]
        [DisplayName("Isikukood")]
        public string PersonalIdentifier { get; set; } = default!;

        [RequiredIfCustom(nameof(AttendeeType), nameof(AttendeeType.Value.Person))]
        [MaxLength(1000)]
        [DisplayName("Lisainfo")]
        public string? PersonAdditionalInfo { get; set; }

        #endregion

        #region Company info

        [RequiredIfCustom(nameof(AttendeeType), nameof(AttendeeType.Value.Company))]
        [DisplayName("Ettevõtte juriidiline nimi")]
        public string CompanyName { get; set; } = default!;

        [RequiredIfCustom(nameof(AttendeeType), nameof(AttendeeType.Value.Company))]
        [RegularExpression("^[0-9]{8,8}$", ErrorMessage = "Ettevõtte registrikoodi pikkuseks on 8 numbrit! " +
            "Palun sisestage uus registrikood!")]
        [StringLength(8)]
        [DisplayName("Ettevõtte registrikood")]
        public string RegistryCode { get; set; } = default!;

        [DisplayName("Ettevõttest tulevate osavõtjate arv")]
        [Range(0, 250)]
        public int NumberOfPeopleFromCompany { get; set; }

        [RequiredIfCustom(nameof(AttendeeType), nameof(AttendeeType.Value.Company))]
        [MaxLength(5000)]
        [DisplayName("Lisainfo")]
        public string? CompanyAdditionalInfo { get; set; }

        #endregion

        [DisplayName("Maksemeetod")]
        public int PaymentMethodId { get; set; }

        public PaymentMethod? PaymentMethod { get; set; }

        public ICollection<EventAndAttendee>? Events { get; set; }
    }
}
