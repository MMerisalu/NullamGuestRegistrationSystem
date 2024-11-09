using Base.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain
{
    public class PaymentMethod : DomainEntityId<int>
    {
        [Required]
        [MaxLength(64)]
        [DisplayName("Maksemeetod")]
        public string Name { get; set; } = default!;
    }
}
