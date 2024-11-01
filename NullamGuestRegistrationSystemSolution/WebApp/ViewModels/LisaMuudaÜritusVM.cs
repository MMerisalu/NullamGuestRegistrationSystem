using Base.Domain;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class LisaMuudaÜritusVM
    {
        public int Id { get; set; }
        
        [Display(Name = "Ürituse Nimi")]
        [Required(ErrorMessage = "Väli Ürituse nimi on kohustuslik!")]
        [MaxLength(64, ErrorMessage = "Väljale sisestava teksti maksimaalne pikkus on 64 tähemärki!")]
        [StringLength(64)]
        public string ÜrituseNimi { get; set; } = default!;

        [Required(ErrorMessage = "Väli Toimumisaeg on kohustuslik!")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:g}", ApplyFormatInEditMode = false)]
        public string Toimumisaeg { get; set; } = default!;

        [Required(ErrorMessage = "Väli Koht on kohustuslik!")]
        [MaxLength(64, ErrorMessage = "Väljale sisestava teksti maksimaalne pikkus on 64 tähemärki!")]
        [StringLength(64, MinimumLength = 2, ErrorMessage = "Väljale sisestava teksti pikkus peab jääma vahemikku 2 kuni 64 tähemärki!")]
        public string Koht { get; set; } = default!;

        [MaxLength(1000, ErrorMessage = "Väljale sisestava teksti maksimaalne pikkus on 1000 tähemärki!")]
        [DataType(DataType.MultilineText)]
        public string? Lisainfo { get; set; }





    }
}
