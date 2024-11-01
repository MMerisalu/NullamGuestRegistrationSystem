using Base.Domain;
using System.ComponentModel.DataAnnotations;

namespace App.Domain
{
    public class Üritus: DomainEntityId
    {
        [Required]
        [MaxLength(64)]
        // Tegelikult peaks display attribuut olema view model'is, mis vastutab selle eest, kuidas
        // kasutajale andmeid kuvada, hetkel jätan selle siia.

        [Display(Name = "Ürituse Nimi")]
        public string ÜrituseNimi { get; set; } = default!;

        // Tegelikult peaks display attribuut olema view model'is, mis vastutab selle eest, kuidas
        // kasutajale andmeid kuvada, hetkel jätan selle siia.
        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime Toimumisaeg { get; set; }
        
        [Required]
        [MaxLength(64)]
        public string Koht { get; set; } = default!;

        [MaxLength(1000)]
        public string? Lisainfo { get; set; }

        public ICollection<Osavõtja>? Osavõtjad { get; set; }

        // Võib-olla ei tuleks seda andmebaasis hoida, sest välja väärtus on arvutatav
        [Display(Name = "Osavõtjate Arv")]
        [Range(1, 4)]
        public int OsavõtjateArv { get; set; }
    }
  
}
