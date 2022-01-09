using System;
using System.Collections.Generic;

namespace Dhrms.DataAccess.Models
{
    public partial class Secondaryeducationaldetails
    {
        public Secondaryeducationaldetails()
        {
            Educationaldetails = new HashSet<Educationaldetails>();
        }

        public int Secondaryeducationalid { get; set; }
        public int Sslcid { get; set; }
        public int? Pucid { get; set; }

        public virtual Pucdetails Puc { get; set; }
        public virtual Sslcdetails Sslc { get; set; }
        public virtual ICollection<Educationaldetails> Educationaldetails { get; set; }
    }
}
