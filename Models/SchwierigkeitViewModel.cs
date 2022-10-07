using System.ComponentModel.DataAnnotations;

namespace Avalanche.Models
{
    public class SchwierigkeitViewModel
    {
        public long? Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
