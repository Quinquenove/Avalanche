using Microsoft.AspNetCore.Mvc.Rendering;

namespace Avalanche.Models
{
    public class WettkaempferViewModel
    {
        public long WettkampfId { get; set; }
        public IEnumerable<string> SelectedSnowboarder { get; set; }
        public IEnumerable<SnowboarderViewModel> SnowboarderList { get; set; }
    }
}
