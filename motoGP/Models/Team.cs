using System;
using System.Collections.Generic;

namespace motoGP.Models
{
    public partial class Team
    {
        public Team()
        {
            TeamYearInfos = new HashSet<TeamYearInfo>();
        }

        public long TeamId { get; set; }
        public string? TeamName { get; set; }

        public virtual ICollection<TeamYearInfo> TeamYearInfos { get; set; }
    }
}
