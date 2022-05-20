using System;
using System.Collections.Generic;

namespace motoGP.Models
{
    public partial class TeamYearInfoPilot
    {
        public long TeamYearInfoId { get; set; }
        public long PilotId { get; set; }

        public virtual Pilot Pilot { get; set; } = null!;
    }
}
