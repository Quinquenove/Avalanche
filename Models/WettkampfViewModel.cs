using Microsoft.AspNetCore.Mvc.Rendering;

namespace Avalanche.Models
{
    public class WettkampfViewModel
    {
        public string Name { get; set; } = null!;
        public byte[] Jahr { get; set; } = null!;
        public string? Berg { get; set; }
        public string? Sponsor { get; set; }
        public List<SelectListItem> BergList { get; set; }

    }
}
