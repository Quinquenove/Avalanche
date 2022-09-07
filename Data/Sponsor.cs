using System;
using System.Collections.Generic;

namespace Avalanche.Data
{
    public partial class Sponsor
    {
        public Sponsor()
        {
            Wettkampfs = new HashSet<Wettkampf>();
        }

        public string Name { get; set; } = null!;

        public virtual ICollection<Wettkampf> Wettkampfs { get; set; }
    }
}
