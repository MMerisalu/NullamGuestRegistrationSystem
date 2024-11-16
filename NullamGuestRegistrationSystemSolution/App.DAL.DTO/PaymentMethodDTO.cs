using Base.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App.DAL.DTO
{
    public class PaymentMethodDTO : DomainEntityId<int>
    {
        [Required(ErrorMessage = "Väli Maksemeetodi nimetus on kohustuslik!")]
        [MaxLength(64)]
        [DisplayName("Maksemeetodi nimetus")]
        public string Name { get; set; } = default!;
    }
}
