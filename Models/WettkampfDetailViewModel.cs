namespace Avalanche.Models
{
    public class WettkampfDetailViewModel
    {
        public WettkampfViewModel Wettkampf { get; set; }
        public IEnumerable<SnowboarderViewModel> SnowboarderList { get; set; }
        public BergViewModel Berg { get; set; }
        public SponsorViewModel Sponsor { get; set; }
    }
}
