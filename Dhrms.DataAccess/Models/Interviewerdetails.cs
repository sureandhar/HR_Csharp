using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Dhrms.DataAccess.Models
{
    public partial class Interviewerdetails
    {
        public int Intervievwerid { get; set; }
        public string Firstname { get; set; }
        public int Userid { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Contactnumber { get; set; }
        public string Jobrole { get; set; }
        public string Unitname { get; set; }
        public string Primaryskills { get; set; }
        public string Gender { get; set; }
        [NotMapped]
        public int RoleId { get; set; }
        [NotMapped]
        public string Password { get; set; }
        [JsonIgnore]
        public virtual Users User { get; set; }
    }
}
