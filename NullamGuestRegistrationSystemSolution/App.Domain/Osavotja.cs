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
    public class Osavotja: DomainEntityId
    {
        public OsavotjaTyyp OsavotjaTyyp { get; set; }
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
        public string? EraisikuLisainfo { get; set; }
        #endregion
        [MaxLength(64)]
        #region Ettevõte info
        public string? EttevõtteJuriidilineNimi { get; set; }

        [RegularExpression("^[0-9]+$")]
        [MaxLength(8)]
        public string? EttevõtteRegistrikood{ get; set; }
        
        [Range(1, 250)]
        public int EttevõttestTulevateOsavõtjateArv { get; set; }
        #endregion
        public int OsavotumaksuMaksmiseViisId { get; set; }
        
        public OsavotumaksuMaksmiseViis? OsavotumaksuMaksmiseViis { get; set; }

    }
}
