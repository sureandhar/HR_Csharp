using System;
using System.Collections.Generic;

namespace Dhrms.DataAccess.Models
{
    public partial class Skills
    {
        public int Skillid { get; set; }
        public string Primaryskill { get; set; }
        public int Candidateid { get; set; }
        public string Secondaryskill { get; set; }

        public virtual Candidatedetails Candidate { get; set; }
    }
}
