using System;
using System.Collections.Generic;

namespace Dhrms.DataAccess.Models
{
    public partial class Ugdetails
    {
        public Ugdetails()
        {
            Highereducationaldetails = new HashSet<Highereducationaldetails>();
        }

        public int Ugid { get; set; }
        public string Institutionname { get; set; }
        public decimal Percentage { get; set; }
        public string Yearofpassing { get; set; }
        public string Streamname { get; set; }

        public virtual ICollection<Highereducationaldetails> Highereducationaldetails { get; set; }
    }
}
