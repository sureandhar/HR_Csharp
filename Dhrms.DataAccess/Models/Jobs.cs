using System;
using System.Collections.Generic;

namespace Dhrms.DataAccess.Models
{
    public partial class Jobs
    {
        public Jobs()
        {
            Appliedjobs = new HashSet<Appliedjobs>();
        }

        public int Jobid { get; set; }
        public string Jobname { get; set; }
        public int Hrid { get; set; }
        public string Jobdescription { get; set; }
        public decimal Ctc { get; set; }
        public string Skills { get; set; }
        public string Status { get; set; }
        public DateTime Postedon { get; set; }
        public string Location { get; set; }

        public virtual Hr Hr { get; set; }
        public virtual ICollection<Appliedjobs> Appliedjobs { get; set; }
    }
}
