using System;
using System.Collections.Generic;

namespace motoGP.Models
{
    public partial class Pilot
    {
        public Pilot()
        {
            PilotRaceRes = new HashSet<PilotRaceRe>();
            PilotYearInfos = new HashSet<PilotYearInfo>();
            TeamYearInfoPilots = new HashSet<TeamYearInfoPilot>();
        }
        public Pilot(long id)
        {
            PilotId = id;
            /*PilotName = "";
            PilotNumber = 0;
            Nationality = "";
            BirthYear = 0;*/
        }

        public long PilotId { get; set; }
        public string? PilotName { get; set; }
        public long? PilotNumber { get; set; }
        public string? Nationality { get; set; }
        public long? BirthYear { get; set; }

        public virtual ICollection<PilotRaceRe> PilotRaceRes { get; set; }
        public virtual ICollection<PilotYearInfo> PilotYearInfos { get; set; }
        public virtual ICollection<TeamYearInfoPilot> TeamYearInfoPilots { get; set; }
    }
}
