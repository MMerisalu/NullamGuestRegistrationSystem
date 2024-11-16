using System.ComponentModel;

namespace WebApp.ViewModels
{
    public class ListOfAttendeeVM
    {
        [DisplayName("Osavõtja")]
        public string Name { get; set; } = default!;
    }
}
