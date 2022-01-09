using System;
using System.Collections.Generic;

namespace Dhrms.DataAccess.Models
{
    public partial class Educationaldetails
    {
        public int Educationalid { get; set; }
        public int Candidateid { get; set; }
        public int Highereducationalid { get; set; }
        public int Secondaryeducationalid { get; set; }
        public byte[] Resumea { get; set; }

        public virtual Candidatedetails Candidate { get; set; }
        public virtual Highereducationaldetails Highereducational { get; set; }
        public virtual Secondaryeducationaldetails Secondaryeducational { get; set; }
    }
}
