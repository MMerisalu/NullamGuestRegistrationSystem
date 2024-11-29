using App.DAL.DTO;

namespace WebApp.ViewModels
{
    public class EventIndexVM: IndexEventVM
    {
        public ICollection<IndexEventVM?>? PastEvents { get; set; }
        public ICollection<IndexEventVM?>? FutureEvents { get; set; }
    }
}
