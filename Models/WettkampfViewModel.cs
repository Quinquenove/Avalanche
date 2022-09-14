namespace Avalanche.Models
{
    public class WettkampfViewModel
    {
        public string Name { get; set; } = null!;
        public byte[] Jahr { get; set; } = null!;
        public string? Berg { get; set; }
        public int WettkampfId { get; set; }
    }
}
