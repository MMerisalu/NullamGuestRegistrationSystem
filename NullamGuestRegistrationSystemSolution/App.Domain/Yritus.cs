using Base.Domain;
using System.ComponentModel.DataAnnotations;

namespace App.Domain
{
    public class Yritus: DomainEntityId
    {
        [Required]
        [MaxLength(64)]
        public string YrituseNimi { get; set; } = default!;

        public DateTime Toimumisaeg { get; set; }
        
        [Required]
        [MaxLength(64)]
        public string Koht { get; set; } = default!;

        [MaxLength(1000)]
        public string? Lisainfo { get; set; }

        public ICollection<Osavotja>? Osavotjad { get; set; }

    }

    
}
