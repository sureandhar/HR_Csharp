using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Dhrms.DataAccess.Models
{
    public partial class Candidatedetails
    {
        public Candidatedetails()
        {
            Appliedjobs = new HashSet<Appliedjobs>();
            Educationaldetails = new HashSet<Educationaldetails>();
            Skills = new HashSet<Skills>();
        }

        public int Candidateid { get; set; }
        public string Firstname { get; set; }
        public int Userid { get; set; }
        public string Lastname { get; set; }
        public DateTime Dateofbirth { get; set; }
        public string Email { get; set; }
        public string Currentaddress { get; set; }
        public string Permanentaddress { get; set; }
        public string Contactnumber { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        [NotMapped]
        public int RoleId { get; set; }
        [JsonIgnore]
        public virtual Users User { get; set; }
        [JsonIgnore]
        public virtual ICollection<Appliedjobs> Appliedjobs { get; set; }
        [JsonIgnore]
        public virtual ICollection<Educationaldetails> Educationaldetails { get; set; }
        [JsonIgnore]
        public virtual ICollection<Skills> Skills { get; set; }
    }
}
