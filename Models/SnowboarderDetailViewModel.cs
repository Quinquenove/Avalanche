namespace Avalanche.Models
{
    public class SnowboarderDetailViewModel
    {
        public SnowboarderViewModel Snowboarder { get; set; }
        public BergViewModel Berg { get; set; }
        public List<SponsoringViewModel> Sponsoring { get; set; }
        public ProfiViewModel Profi { get; set; }
    }
}
