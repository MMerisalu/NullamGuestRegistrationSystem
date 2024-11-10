using Base.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class DeleteEventVM : DomainEntityId<int>
    {
        [DisplayName("Ürituse nimi:")]
        public string Name { get; set; } = default!;

        [DisplayName("Toimumisaeg:")]
        public DateTime EventDateAndTime { get; set; } = default!;

        [DisplayName("Koht:")]
        public string Location { get; set; } = default!;

        [DisplayName("Lisainfo:")]
        public string? AdditionalInfo { get; set; }

        [DisplayName("Osalejate arv:")]
        public int NumberOfAttendees { get; set; }
    }
}
