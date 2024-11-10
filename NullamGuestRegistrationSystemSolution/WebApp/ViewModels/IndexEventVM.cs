using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Base.Domain;
using App.Domain;

namespace WebApp.ViewModels
{
    public class IndexEventVM: DomainEntityId<int>
    {
        public int LineNumber { get; set; }

        [DisplayName("")]
        public string LineNumberFormatted => $"{LineNumber}.";

        [Required]
        [MaxLength(64)]
        [DisplayName("Ürituse nimi")]
        public string Name { get; set; } = default!;

        [DisplayFormat(DataFormatString = "{0:g}")]
        [Required]
        [DisplayName("Toimumisaeg")]
        public DateTime EventDateAndTime { get; set; } = default!;

        [Required]
        [MaxLength(64)]
        [DisplayName("Koht")]
        public string Location { get; set; } = default!;

        [MaxLength(1000)]
        [DisplayName("Lisainfo")]
        public string? AdditionalInfo { get; set; }

        
    }
}
