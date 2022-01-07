using System;
using System.Collections.Generic;

namespace Dhrms.DataAccess.Models
{
    public partial class Appliedjobs
    {
        public int Id { get; set; }
        public int Jobid { get; set; }
        public int Candidateid { get; set; }

        public virtual Candidatedetails Candidate { get; set; }
        public virtual Jobs Job { get; set; }
    }
}
