using Microsoft.AspNetCore.Mvc.Rendering;

namespace Avalanche.Models
{
    public class BergViewModel
    {
        public string Name { get; set; }
        public string Gebirge { get; set; }
        public string Schwierigkeit { get; set; }

        public List<SelectListItem> GebirgeListe { get; set; }
        public List<SelectListItem> SchwierigkeitListe { get; set; }
    }
}
