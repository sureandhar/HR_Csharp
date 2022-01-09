using System;
using System.Collections.Generic;

namespace Dhrms.DataAccess.Models
{
    public partial class Highereducationaldetails
    {
        public Highereducationaldetails()
        {
            Educationaldetails = new HashSet<Educationaldetails>();
        }

        public int Highereducationalid { get; set; }
        public int? Diplomaid { get; set; }
        public int? Pgid { get; set; }
        public int? Ugid { get; set; }

        public virtual Diplomadetails Diploma { get; set; }
        public virtual Pgdetails Pg { get; set; }
        public virtual Ugdetails Ug { get; set; }
        public virtual ICollection<Educationaldetails> Educationaldetails { get; set; }
    }
}
