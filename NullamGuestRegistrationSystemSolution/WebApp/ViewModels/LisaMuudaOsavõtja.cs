using App.Domain;
using App.Domain.Enum;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class LisaMuudaOsavõtja
    {
        #region Eraisiku info

        [Required(ErrorMessage = "Väli Osavõtja tüüp on kohustuslik!")]
        [DisplayName("Osavõtja tüüp")]
        [EnumDataType(typeof(OsavõtjaTüüp))]
        public OsavõtjaTüüp OsavõtjaTüüp { get; set; }

        [MaxLength(64)]
        [StringLength(64)]
        public string? Eesnimi { get; set; }

        [MaxLength(64)]
        [StringLength(64)]
        public string? Perekonnanimi { get; set; }

        public string? EesnimiJaPerekonnanimi => $"{Eesnimi} {Perekonnanimi}";
        public string? PerekonnanimiJaEesnimi => $"{Perekonnanimi} {Eesnimi}";

        [MaxLength(11)]
        [RegularExpression("^[0-9]+$")]
        public string? Isikukood { get; set; }

        [MaxLength(1500)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Eraisiku lisainfo")]
        public string? EraisikuLisainfo { get; set; }

        #endregion

        #region Ettevõtte info

        [MaxLength(64)]
        [Display(Name = "Ettevõtte Juriidliine Nimi")]
        public string? EttevõtteJuriidilineNimi { get; set; }

        [RegularExpression("^[0-9]+$")]
        [MaxLength(8)]
        [Display(Name = "Ettevõtte Registrikood")]
        public string? EttevõtteRegistrikood { get; set; }

        [Display(Name = "Ettevõttest Tulevate Osavõtjate Arv")]
        [Range(1, 250)]
        public int EttevõttestTulevateOsavõtjateArv { get; set; }

        #endregion

        [Display(Name = "Osavõtumaksu Maksmise Viis")]
        public int OsavotumaksuMaksmiseViisId { get; set; }
        public OsavõtumaksuMaksmiseViis? OsavotumaksuMaksmiseViis { get; set; }
    }
}
