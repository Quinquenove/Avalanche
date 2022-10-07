using System;
using System.Collections.Generic;

namespace Avalanche.Data
{
    public partial class Schwierigkeit
    {
        public Schwierigkeit()
        {
            Bergs = new HashSet<Berg>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Berg> Bergs { get; set; }
    }
}
