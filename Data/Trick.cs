using System;
using System.Collections.Generic;

namespace Avalanche.Data
{
    public partial class Trick
    {
        public Trick()
        {
            Profis = new HashSet<Profi>();
        }

        public string Name { get; set; } = null!;
        public string? Beschreibung { get; set; }

        public virtual ICollection<Profi> Profis { get; set; }
    }
}
