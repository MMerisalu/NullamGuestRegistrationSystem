using App.Domain.Enum;
using Base.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain
{
    public class Osavõtja: DomainEntityId
    {
        public OsavõtjaTüüp OsavõtjaTüüp { get; set; }
        #region Eraisiku info
        [MaxLength(64)]
        
        public string? Eesnimi { get; set; }
        
        [MaxLength(64)]
        public string? Perekonnanimi { get; set; }

        public string? EesnimiJaPerekonnanimi => $"{Eesnimi} {Perekonnanimi}";
        public string? PerekonnanimiJaEesnimi => $"{Perekonnanimi} {Eesnimi}";
        
        [MaxLength(11)]
        [RegularExpression("^[0-9]+$")]
        public string? Isikukood { get; set; }

        [MaxLength(1500)]
        [DataType(DataType.MultilineText)]
        // Tegelikult peaks display attribuut olema view model'is, mis vastutab selle eest, kuidas
        // kasutajale andmeid kuvada, hetkel jätan selle siia.
        [Display(Name = "Eraisiku lisainfo" )]
        public string? EraisikuLisainfo { get; set; }
        #endregion
        #region Ettevõte info
        [MaxLength(64)]
        // Tegelikult peaks display attribuut olema view model'is, mis vastutab selle eest, kuidas
        // kasutajale andmeid kuvada, hetkel jätan selle siia.
        [Display(Name = "Ettevõtte Juriidliine Nimi")]
        public string? EttevõtteJuriidilineNimi { get; set; }

        [RegularExpression("^[0-9]+$")]
        [MaxLength(8)]
        [Display(Name = "Ettevõtte Registrikood")]
        public string? EttevõtteRegistrikood{ get; set; }
        [Display(Name = "Ettevõttest Tulevate Osavõtjate Arv")]
        [Range(1, 250)]
        public int EttevõttestTulevateOsavõtjateArv { get; set; }
        #endregion
        [Display(Name = "Osavõtumaksu Maksmise Viis")]
        public int OsavotumaksuMaksmiseViisId { get; set; }
        
        public OsavõtumaksuMaksmiseViis? OsavotumaksuMaksmiseViis { get; set; }

    }
}
