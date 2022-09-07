using System;
using System.Collections.Generic;

namespace Avalanche.Data
{
    public partial class Berg
    {
        public Berg()
        {
            Snowboarders = new HashSet<Snowboarder>();
            Wettkampfs = new HashSet<Wettkampf>();
        }

        public string Name { get; set; } = null!;
        public string? Gebirge { get; set; }
        public string? Schwierigkeit { get; set; }

        public virtual Gebirge? GebirgeNavigation { get; set; }
        public virtual Schwierigkeit? SchwierigkeitNavigation { get; set; }
        public virtual ICollection<Snowboarder> Snowboarders { get; set; }
        public virtual ICollection<Wettkampf> Wettkampfs { get; set; }
    }
}
