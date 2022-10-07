using System;
using System.Collections.Generic;

namespace Avalanche.Data
{
    public partial class Sponsor
    {
        public Sponsor()
        {
            Sponsorings = new HashSet<Sponsoring>();
            Wettkampfs = new HashSet<Wettkampf>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Sponsoring> Sponsorings { get; set; }
        public virtual ICollection<Wettkampf> Wettkampfs { get; set; }
    }
}
