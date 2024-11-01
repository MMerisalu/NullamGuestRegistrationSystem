using Base.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain
{
    public class OsavõtumaksuMaksmiseViis: DomainEntityId
    {
        [Required]
        [MaxLength(64)]
        
        // Tegelikult peaks display attribuut olema view model'is, mis vastutab selle eest, kuidas
        // kasutajale andmeid kuvada, hetkel jätan selle siia.

        [Display(Name = "Osavõtumaksu Maksmise Viisi Nimetus")]
        public string OsavõtumaksuMaksmiseViisiNimetus { get; set; } = default!;
    }
}
