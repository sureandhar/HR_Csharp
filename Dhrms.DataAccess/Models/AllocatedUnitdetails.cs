using System;
using System.Collections.Generic;

namespace Dhrms.DataAccess.Models
{
    public partial class AllocatedUnitdetails
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public int Candidateid { get; set; }
        public string BaseLocation { get; set; }
        public decimal NoticePeriod { get; set; }
        public DateTime JoinedOn { get; set; }
        public string Designation { get; set; }
        public decimal MonthsRemaining { get; set; }

        public virtual Candidatedetails Candidate { get; set; }
    }
}
