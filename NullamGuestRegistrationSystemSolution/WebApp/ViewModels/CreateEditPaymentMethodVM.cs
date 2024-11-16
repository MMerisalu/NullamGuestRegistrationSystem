using Base.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class CreateEditPaymentMethodVM : DomainEntityId<int>
    {
        [Required(ErrorMessage = "Väli Maksemeetodi nimetus on kohustuslik!")]
        [MaxLength(64, ErrorMessage = "Väljale Maksemeetod sisestatava teksti pikkus võib olla kuni 64 tähemärki! ")]
        [StringLength(64)]
        [DisplayName("Maksemeetod")]
        public string Name { get; set; } = default!;
    }
}
