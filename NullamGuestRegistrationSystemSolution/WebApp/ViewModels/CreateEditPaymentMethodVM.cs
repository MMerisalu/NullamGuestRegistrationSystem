using Base.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class CreateEditPaymentMethodVM : DomainEntityId<int>
    {
        [Required]
        [MaxLength(64)]
        [DisplayName("Maksemeetod")]
        public string Name { get; set; } = default!;
    }
}
