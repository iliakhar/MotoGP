using System;
using System.Collections.Generic;

namespace motoGP.Models
{
    public partial class PilotRaceRe
    {
        public long? RaceId { get; set; }
        public long? PilotId { get; set; }
        public long PilotRaceResId { get; set; }
        public string? Time { get; set; }
        public long? Laps { get; set; }
        public long? Points { get; set; }

        public virtual Pilot? Pilot { get; set; }
        public virtual Race? Race { get; set; }
    }
}
