using Base.Domain;
using System.ComponentModel.DataAnnotations;

namespace App.Domain
{
    public class Event: DomainEntityId
    {
        [Required]
        [MaxLength(64)]
        public string Name { get; set; } = default!;
    }
}
