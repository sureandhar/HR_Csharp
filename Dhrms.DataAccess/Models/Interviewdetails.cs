﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dhrms.DataAccess.Models
{
    public partial class Interviewdetails
    {
        public int Interviewid { get; set; }
        public int Candidateid { get; set; }
        public int? Intervievwerid { get; set; }
        public string Status { get; set; }
        public DateTime Scheduleddate { get; set; }
        public string Scheduledtime { get; set; }
        public string Attended { get; set; }
        public string Interviewerfeedback { get; set; }
        public string Roundname { get; set; }
        [NotMapped]
        public string Interviewername { get; set; }
        public virtual Candidatedetails Candidate { get; set; }
        public virtual Interviewerdetails Intervievwer { get; set; }
    }
}
