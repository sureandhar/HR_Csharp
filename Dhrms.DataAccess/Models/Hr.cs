using System;
using System.Collections.Generic;

namespace Dhrms.DataAccess.Models
{
    public partial class Hr
    {
        public Hr()
        {
            Jobs = new HashSet<Jobs>();
        }

        public int Hrid { get; set; }
        public string Firstname { get; set; }
        public int Userid { get; set; }
        public string Email { get; set; }
        public string Lastname { get; set; }
        public string Contactnumber { get; set; }
        public string Employeeid { get; set; }
        public string Gender { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<Jobs> Jobs { get; set; }
    }
}
