using Base.Domain;
using System.ComponentModel.DataAnnotations;

namespace App.DAL.DTO
{
    public class PaymentMethodDTO : DomainEntityId<int>
    {
        [Required]
        [MaxLength(64)]
        public string Name { get; set; } = default!;
    }
}
