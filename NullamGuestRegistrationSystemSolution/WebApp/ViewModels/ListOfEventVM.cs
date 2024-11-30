namespace WebApp.ViewModels
{
    public class ListOfEventVM
    {
        public ICollection<IndexEventVM> FutureEvents { get; set; } = new List<IndexEventVM>();
        public ICollection<IndexEventVM> PastEvents { get; set; } = new List<IndexEventVM>();
    }
}
