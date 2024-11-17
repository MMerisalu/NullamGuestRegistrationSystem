using App.Domain;
using App.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Base.Domain;

namespace WebApp.ViewModels
{
    public class DeleteAttendeeVM : DomainEntityId<int>
    {
        [DisplayName("Osavõtja tüüp")]
        public AttendeeType? AttendeeType { get; set; }

        #region Person info

        [DisplayName("Eesnimi")]
        public string? SurName { get; set; }


        [DisplayName("Perekonnanimi")]
        public string? GivenName { get; set; }


        [DisplayName("Ees- ja perekonnanimi")]
        public string SurAndGivenName => $"{SurName} {GivenName}";

        [DisplayName("Perekonna- ja eesnimi")]
        public string GivenAndSurName => $"{GivenName} {SurName}";


        [DisplayName("Isikukood")]
        public string? PersonalIdentifier { get; set; }


        [MaxLength(1000)]
        [DisplayName("Lisainfo")]
        public string? PersonAdditionalInfo { get; set; }

        #endregion

        #region Company info

        [DisplayName("Ettevõtte juriidiline nimi")]
        public string? CompanyName { get; set; }


        [DisplayName("Ettevõtte registrikood")]
        public string? RegistryCode { get; set; }


        [DisplayName("Ettevõttest tulevate osavõtjate arv")]
        
        public int? NumberOfPeopleFromCompany { get; set; }


        [MaxLength(5000)]
        [DisplayName("Lisainfo")]
        public string? CompanyAdditionalInfo { get; set; }

        #endregion

        [DisplayName("Maksemeetod")]
        public int PaymentMethodId { get; set; }

    }
}
  
