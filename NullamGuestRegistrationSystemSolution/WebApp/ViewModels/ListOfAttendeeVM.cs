using App.Enum;
using Base.Domain;
using System.ComponentModel;

namespace WebApp.ViewModels
{
    public class ListOfAttendeeVM : DomainEntityId<int>
    {
        public AttendeeType? AttendeeType { get; set; }

        [DisplayName("Osavõtja")]
        public string Name { get; set; } = default!;


        //[DisplayName("Isikukood")]
        //public string? PersonalIdentifier { get; set; }


        //[DisplayName("Ettevõtte registrikood")]
        //public string? RegistryCode { get; set; }

        [DisplayName("Isikukood / registrikood ")]
        public string Code { get; set; } = default!;
    }
}
