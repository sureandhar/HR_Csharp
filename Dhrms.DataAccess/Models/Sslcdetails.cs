using System;
using System.Collections.Generic;

namespace Dhrms.DataAccess.Models
{
    public partial class Sslcdetails
    {
        public Sslcdetails()
        {
            Secondaryeducationaldetails = new HashSet<Secondaryeducationaldetails>();
        }

        public int Sslcid { get; set; }
        public string Institutionname { get; set; }
        public decimal Percentage { get; set; }
        public string Yearofpassing { get; set; }

        public virtual ICollection<Secondaryeducationaldetails> Secondaryeducationaldetails { get; set; }
    }
}
