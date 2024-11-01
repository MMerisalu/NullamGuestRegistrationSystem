using Base.Domain;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class LisaMuudaÜritusVM
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        [StringLength(64)]
        public string ÜrituseNimi { get; set; } = default!;
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:g}", ApplyFormatInEditMode = false)]
        public string Toimumisaeg { get; set; } = default!;

    }
}
