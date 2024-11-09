using Base.Domain;
using System.ComponentModel;

namespace WebApp.ViewModels
{
    public class DeletePaymentMethodVM : DomainEntityId<int>
    {
        [DisplayName("Maksemeetod")]
        public string Name { get; set; } = default!;

    }
}
