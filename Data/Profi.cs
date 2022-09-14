using System;
using System.Collections.Generic;

namespace Avalanche.Data
{
    public partial class Profi
    {
        public string Lizenznummer { get; set; } = null!;
        public long? Weltcuppunkte { get; set; }
        public string Mitgliedsnummer { get; set; } = null!;
        public string? BestTrick { get; set; }

        public virtual Trick? BestTrickNavigation { get; set; }
        public virtual Snowboarder MitgliedsnummerNavigation { get; set; } = null!;
    }
}
