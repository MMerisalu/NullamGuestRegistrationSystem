using App.Domain.Enum;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class LisaMuudaOsavõtja
    {
        [Required(ErrorMessage = "Väli Osavõtja tüüp on kohustuslik!")]
        [DisplayName("Osavõtja tüüp")]
        [EnumDataType(typeof(OsavõtjaTüüp))]
        public OsavõtjaTüüp OsavõtjaTüüp { get; set; }
    }
}
