using System;
using System.Collections.Generic;

namespace Avalanche.Data
{
    public partial class Sponsoring
    {
        public string Snowboarder { get; set; } = null!;
        public string Sponsor { get; set; } = null!;
        public string? Vertragsart { get; set; }

        public virtual Vertragsart? VertragsartNavigation { get; set; }
    }
}
