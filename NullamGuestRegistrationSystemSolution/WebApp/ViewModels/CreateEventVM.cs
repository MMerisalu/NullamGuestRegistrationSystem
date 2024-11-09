using App.Domain;
using Base.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class CreateEventVM : DomainEntityId<int>
    {
        [Required(ErrorMessage = "Väli Ürituse nimi on kohustuslik!")]
        [MaxLength(64, ErrorMessage = "Väljale Ürituse nimi sisestatava teksti pikkus võib olla kuni 64 tähemärki!")]
        [StringLength(64)]
        [DisplayName("Ürituse nimi")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "Väli Toimumisaeg on kohustuslik!")]
        [DisplayName("Toimumisaeg")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:g}", ApplyFormatInEditMode = true)]
        public string EventDateAndTime { get; set; } = default!;

        [Required(ErrorMessage = "Väli Koht on kohustuslik!")]
        [MaxLength(64, ErrorMessage = "Väljale Koht sisestatava teksti pikkus võib olla kuni 64 tähemärki!")]
        [StringLength(64)]
        [DisplayName("Koht")]
        public string Location { get; set; } = default!;

        [MaxLength(1000, ErrorMessage = "Väljale Lisainfo sisestatava teksti pikkus võib olla kuni 1000 tähemärki!")]
        [StringLength(1000)]
        [DisplayName("Lisainfo")]
        public string? AdditionalInfo { get; set; }
    }
}
