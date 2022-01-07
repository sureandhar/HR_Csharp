using System;
using System.Collections.Generic;

namespace Dhrms.DataAccess.Models
{
    public partial class Users
    {
        public Users()
        {
            Candidatedetails = new HashSet<Candidatedetails>();
            Hr = new HashSet<Hr>();
            Interviewerdetails = new HashSet<Interviewerdetails>();
        }

        public int Userid { get; set; }
        public string Username { get; set; }
        public int Roleid { get; set; }
        public string Email { get; set; }
        public string Userpassword { get; set; }

        public virtual Roles Role { get; set; }
        public virtual ICollection<Candidatedetails> Candidatedetails { get; set; }
        public virtual ICollection<Hr> Hr { get; set; }
        public virtual ICollection<Interviewerdetails> Interviewerdetails { get; set; }
    }
}
