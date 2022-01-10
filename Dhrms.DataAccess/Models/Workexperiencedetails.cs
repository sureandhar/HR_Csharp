using System;
using System.Collections.Generic;

namespace Dhrms.DataAccess.Models
{
    public partial class Workexperiencedetails
    {
        public int Experienceid { get; set; }
        public int Candidateid { get; set; }
        public decimal Noofmonths { get; set; }
        public decimal Noofyears { get; set; }
        public string Domainname { get; set; }
        public string Companyname { get; set; }
        public string Project { get; set; }

        public virtual Candidatedetails Candidate { get; set; }
    }
}
