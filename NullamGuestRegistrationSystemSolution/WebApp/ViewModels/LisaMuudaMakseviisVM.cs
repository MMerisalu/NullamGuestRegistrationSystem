using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class LisaMuudaMakseviisVM
    {
        [Required]
        [MaxLength(64)]
        [Display(Name = "Osavõtumaksu makseviis")]
        public string OsavõtumaksuMaksmiseViisiNimetus { get; set; } = default!;
    }
}
