using Base.Domain;
using System.ComponentModel.DataAnnotations;

namespace App.Domain
{
    public class Üritus: DomainEntityId
    {
        [Required]
        [MaxLength(64)]
        public string ÜrituseNimi { get; set; } = default!;

        public DateTime Toimumisaeg { get; set; }
        
        [Required]
        [MaxLength(64)]
        public string Koht { get; set; } = default!;

        [MaxLength(1000)]
        public string? Lisainfo { get; set; }

        public ICollection<Osavõtja>? Osavõtjad { get; set; }

        // Võib-olla ei tuleks seda andmebaasis hoida, sest välja väärtus on arvutatav

        public int OsavõtjateArv { get; set; }


    }

    
}
