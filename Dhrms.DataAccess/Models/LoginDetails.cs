using System;
using System.Collections.Generic;

namespace Dhrms.DataAccess.Models
{
    public partial class LoginDetails
    {
        public int LoginId { get; set; }
        public int Userid { get; set; }
        public string LoginTime { get; set; }
        public string IpAddress { get; set; }
        public string Location { get; set; }

        public virtual Users User { get; set; }
    }
}
