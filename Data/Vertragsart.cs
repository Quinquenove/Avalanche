using System;
using System.Collections.Generic;

namespace Avalanche.Data
{
    public partial class Vertragsart
    {
        public Vertragsart()
        {
            Sponsorings = new HashSet<Sponsoring>();
        }

        public string Name { get; set; } = null!;

        public virtual ICollection<Sponsoring> Sponsorings { get; set; }
    }
}
