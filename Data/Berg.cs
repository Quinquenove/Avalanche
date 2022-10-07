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

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public long GebirgeId { get; set; }
        public long? SchwierigkeitId { get; set; }

        public virtual Gebirge Gebirge { get; set; } = null!;
        public virtual Schwierigkeit? Schwierigkeit { get; set; }
        public virtual ICollection<Snowboarder> Snowboarders { get; set; }
        public virtual ICollection<Wettkampf> Wettkampfs { get; set; }
    }
}
