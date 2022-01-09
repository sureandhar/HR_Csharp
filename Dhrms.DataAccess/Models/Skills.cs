using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dhrms.DataAccess.Models
{
    public partial class Skills
    {
        public int Skillid { get; set; }
        public string Primaryskill { get; set; }
        public int Candidateid { get; set; }
        public string Secondaryskill { get; set; }
        [JsonIgnore]
        public virtual Candidatedetails Candidate { get; set; }

        public static implicit operator Skills(Candidatedetails v)
        {
            throw new NotImplementedException();
        }
    }
}
