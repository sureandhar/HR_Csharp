using System;
using System.Collections.Generic;
using System.Linq;
using Dhrms.DataAccess.Models;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;
using System.Data;

namespace Dhrms.DataAccess
{
    public class DhrmsRepository
    {
        postgresContext context;
        public DhrmsRepository()
        {
            context = new postgresContext();
        }

        public List<Hr> GetAllHRDetails()
        {
            List<Hr> Hrlist = null;

            try
            {
                Hrlist = context.Hr.ToList();
            }
            catch (Exception ex)
            {
                Hrlist = null;
            }

            return Hrlist;
        }

        public string validatelogin(string EmailId, string Password)
        {
            List<Users> UsersList = null;
            List<Roles> RolesList = null;

            string RoleName = string.Empty;
            try
            {
                UsersList = context.Users.ToList();
                RolesList = context.Roles.ToList();
                if (UsersList!=null && UsersList.Where(i=>i.Email==EmailId).FirstOrDefault()!=null)
                {
                    var RoleId = (from usr in UsersList where usr.Email == EmailId && usr.Userpassword == Password select usr.Roleid).FirstOrDefault();

                    if (RoleId!=0)
                    {
                        RoleName = (from role in RolesList where role.Roleid == RoleId select role.Rolename).FirstOrDefault();
                    }
                    else
                    {
                        return RoleName = "-1";
                    }

                }
                else
                {
                    return RoleName = "-1";
                }

                return RoleName;

            }
            catch (Exception ex)
            {
                return RoleName = "-99";
            }
        }

        public List<Candidatedetails> GetAllCandidate()
        {
            List<Candidatedetails> CandidateList = null;
            List<Skills> SkillList = null;
            List<Interviewdetails> InterviewList = null;
            string Skills = string.Empty;
            try
            {
                
                CandidateList = context.Candidatedetails.ToList();
                SkillList = context.Skills.ToList();
                InterviewList = context.Interviewdetails.ToList();

                foreach (var item in CandidateList)
                {

                    if (item.Skills.Count > 0)
                    {
                        Skills _skill = item.Skills.First();
                        item.Skillset = _skill.Primaryskill + "," + _skill.Secondaryskill;
                    }
                    else
                    {
                        item.Skillset = Skills;
                    }
                    if (item.Interviewdetails.Count > 0)
                    {
                        Interviewdetails _interview = item.Interviewdetails.First();
                        item.Status = _interview.Status;
                        item.Scheduleddate = _interview.Scheduleddate.ToString();
                        item.Scheduledtime = _interview.Scheduledtime.ToString();
                    }
                    else
                    {
                        item.Status = "Not scheduled";
                        item.Scheduleddate = string.Empty;
                    }

                }

            }
            catch (Exception ex)
            {
                CandidateList = null;
            }
            return CandidateList;
        }
        public Candidatedetails GetCandidate(int id)
        {
            Candidatedetails CandidateDetail = null;
            List<Skills> SkillList = null;
            List<Workexperiencedetails> WorkexperienceList = null;
            string Skills = string.Empty;
            try
            {

                List<Candidatedetails> _candidateList = context.Candidatedetails.ToList();
                foreach (var item in _candidateList)
                {
                    if (item.Candidateid==id)
                    {
                        CandidateDetail = item;
                    }
                }
                WorkexperienceList = context.Workexperiencedetails.ToList();
                if (WorkexperienceList.Count>0)
                {
                    foreach (var item in WorkexperienceList)
                    {
                        if (item.Candidateid == id)
                        {
                            CandidateDetail.Workexperiencedetails.Add(item);
                        }
                    }
                }
                
                SkillList = context.Skills.ToList();
                if (CandidateDetail.Skills.Count>0)
                {
                    CandidateDetail.Skillset = CandidateDetail.Skills.First().Primaryskill + "," + CandidateDetail.Skills.First().Secondaryskill;
                }
                else
                {
                    CandidateDetail.Skillset = string.Empty;
                }
                if (CandidateDetail.Interviewdetails.Count > 0)
                {
                    Interviewdetails _interview = CandidateDetail.Interviewdetails.First();
                    CandidateDetail.Status = _interview.Status;
                    CandidateDetail.Scheduleddate = _interview.Scheduleddate.ToString();
                    CandidateDetail.Scheduledtime = _interview.Scheduledtime.ToString();
                }
                else
                {
                    CandidateDetail.Status = "Not scheduled";
                    CandidateDetail.Scheduleddate = string.Empty;
                }


            }
            catch (Exception ex)
            {
                CandidateDetail = null;
            }
            return CandidateDetail;
        }


        /// <summary>
        /// This method will generate user based on the roles with temp password and also add interviewer details in interviewerdetails table
        /// </summary>
        /// <param name="interviwer"></param>
        /// <returns></returns>
        public int AddInterviewer(Interviewerdetails interviwer)
        {
            int status = 1;
            int result = 0;
            NpgsqlParameter prmfirstname = new NpgsqlParameter("@firstname", interviwer.Firstname);
            NpgsqlParameter prmlastname = new NpgsqlParameter("@lastname", interviwer.Lastname);
            NpgsqlParameter prmemail = new NpgsqlParameter("@email", interviwer.Email);
            NpgsqlParameter prmcontactnumber = new NpgsqlParameter("@contactnumber", interviwer.Contactnumber);
            NpgsqlParameter prmjobrole = new NpgsqlParameter("@jobrole", interviwer.Jobrole);
            NpgsqlParameter prmunitname = new NpgsqlParameter("@unitname", interviwer.Unitname);
            NpgsqlParameter prmprimaryskills = new NpgsqlParameter("@primaryskills", interviwer.Primaryskills);
            NpgsqlParameter prmroleid = new NpgsqlParameter("@roleid", interviwer.RoleId);
            NpgsqlParameter prmgender = new NpgsqlParameter("@gender", interviwer.Gender);

            NpgsqlParameter prmstatus = new NpgsqlParameter("@status", status);
            //NpgsqlParameter prmstatus = new NpgsqlParameter("@status", status)
            //{
            //    Direction = ParameterDirection.InputOutput
            //};
            prmstatus.Direction = System.Data.ParameterDirection.InputOutput; 
            try
            {
                result = context.Database.ExecuteSqlRaw("CALL usp_addinterviewer (@firstname,@lastname,@email,@contactnumber,@jobrole" +
                    ",@unitname,@primaryskills,@roleid,@gender, @status)"
                    ,prmfirstname,prmlastname,prmemail,prmcontactnumber,prmjobrole,prmunitname,prmprimaryskills,prmroleid,prmgender,prmstatus);
                status = prmstatus.Value!=null ? Convert.ToInt32(prmstatus.Value):1;
            }
            catch (Exception ex)
            {
                status = 1;
            }
            return status;
        }
        /// <summary>
        /// This method will generate user based on the roles with temp password and also add interviewer details in Candidatedetails table
        /// </summary>
        /// <param name="interviwer"></param>
        /// <returns></returns>
        public int AddCandidate(Candidatedetails candidate, out int candidateId)
        {

            int status = 1;
            candidateId = 0;
            int result = 0;
            NpgsqlParameter prmfirstname = new NpgsqlParameter("@firstname", candidate.Firstname);
            NpgsqlParameter prmlastname = new NpgsqlParameter("@lastname", candidate.Lastname);
            NpgsqlParameter prmemail = new NpgsqlParameter("@email", candidate.Email);
            //For removing timestamp from the dateofdbith
            NpgsqlParameter prmdob = new NpgsqlParameter("@dob",new NpgsqlDate(candidate.Dateofbirth));
            NpgsqlParameter prmcontactnumber = new NpgsqlParameter("@contactnumber", candidate.Contactnumber);
            NpgsqlParameter prmroleid = new NpgsqlParameter("@roleid", candidate.RoleId);
            NpgsqlParameter prmgender = new NpgsqlParameter("@gender", candidate.Gender);
            NpgsqlParameter prmcurraddress= new NpgsqlParameter("@currentaddress", candidate.Currentaddress);
            NpgsqlParameter prmpermntaddress = new NpgsqlParameter("@permanentaddress", candidate.Permanentaddress);
            NpgsqlParameter prmcity = new NpgsqlParameter("@city", candidate.City);

            NpgsqlParameter prmstatus = new NpgsqlParameter("@status", status);
            prmstatus.Direction = System.Data.ParameterDirection.InputOutput;
            NpgsqlParameter prmcandidateId= new NpgsqlParameter("@candidate_id", candidateId);
            prmcandidateId.Direction = System.Data.ParameterDirection.InputOutput; 
            try
            {
                result = context.Database.ExecuteSqlRaw("CALL usp_addcandidate (@firstname,@lastname,@email,@dob,@contactnumber,@roleid,@gender,@currentaddress,@permanentaddress,@city,@status,@candidate_id)"
                    , prmfirstname,prmlastname,prmemail,prmdob,prmcontactnumber,prmroleid,prmgender,prmcurraddress,prmpermntaddress,prmcity,prmstatus,prmcandidateId);
                status = prmstatus.Value!=null ? Convert.ToInt32(prmstatus.Value):1;
                if (status==0)
                {
                    candidateId = prmcandidateId.Value != null ? Convert.ToInt32(prmcandidateId.Value) : 0;
                }
                else
                {
                    candidateId = 0;
                }
            }
            catch (Exception ex)
            {
                status = 1;
            }
            return status;
        }

        /// <summary>
        /// To get all the records of interviewerdetails
        /// </summary>
        /// <returns></returns>
        public List<Interviewerdetails> GetAllInterviewerdetails()
        {
            List<Interviewerdetails> InterviewerdetailsList = null;
            try
            {
                InterviewerdetailsList = context.Interviewerdetails.ToList();
                List<Users> _userList = context.Users.ToList();


                foreach (var item in InterviewerdetailsList)
                {
                    //string _temp = (from user in _userList where user.Userid == item.Userid select user.Password).FirstOrDefault();
                    //var _temp1 = item.User.Password;
                    item.Password= item.User!=null? item.User.Userpassword:string.Empty;
                }

            }
            catch (Exception ex)
            {
                InterviewerdetailsList = null;
            }
            return InterviewerdetailsList;
        }


        public int AddCandidateSkill(Skills skill)
        {
            int status = 0;
            try
            {
                var result=context.Skills.Add(skill);
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                status = -1;
            }
            return status;
        }
        public int AddCandidateExperience(Workexperiencedetails experience)
        {
            int status = 0;
            try
            {
                var result=context.Workexperiencedetails.Add(experience);
                context.SaveChanges();


            }
            catch (Exception ex)
            {
                status = -1;
            }
            return status;
        }
        public int AddCandidateEducation(int candidateId,Sslcdetails sslc,Pucdetails puc, Diplomadetails diploma, Ugdetails ug, Pgdetails pg)
        {
            int status = 0;
            int sId = 0;
            int hId = 0;
            Secondaryeducationaldetails sDetails = new Secondaryeducationaldetails();
            Highereducationaldetails hDetails = new Highereducationaldetails();
            Educationaldetails eDetails = new Educationaldetails();
            try
            {
                //Secondary education section
                if (sslc != null && puc != null)
                {
                    var sslcResult = context.Sslcdetails.Add(sslc);
                    context.SaveChanges();
                    List<Sslcdetails> _rt = context.Sslcdetails.ToList();
                    int SSLCId = context.Sslcdetails.ToList().Last().Sslcid;
                    var pucResult = context.Pucdetails.Add(puc);
                    context.SaveChanges();
                    int PUCId = context.Pucdetails.ToList().Last().Pucid;
                    sDetails.Sslcid = SSLCId;
                    sDetails.Pucid = PUCId;
                    if (SSLCId != 0 && PUCId != 0)
                    {
                        var result = context.Secondaryeducationaldetails.Add(sDetails);
                        context.SaveChanges();
                        sId = context.Secondaryeducationaldetails.ToList().Last().Secondaryeducationalid;
                    }
                    else
                    {
                        status = -1;
                    }
                }
                else
                {
                    if (sslc != null)
                    {
                        var sslcResult = context.Sslcdetails.Add(sslc);
                        context.SaveChanges();
                        int SSLCId = context.Sslcdetails.ToList().Last().Sslcid;
                        sDetails.Sslcid = SSLCId;
                        if (SSLCId != 0)
                        {
                            var result = context.Secondaryeducationaldetails.Add(sDetails);
                            context.SaveChanges();
                            sId = context.Secondaryeducationaldetails.ToList().Last().Secondaryeducationalid;

                        }
                        else
                        {
                            status = -1;
                        }
                    }

                }
                //Higher education section

                if (diploma != null || ug != null || pg != null)
                {

                    if (diploma != null)
                    {
                        var diplomaResult = context.Diplomadetails.Add(diploma);
                        context.SaveChanges();
                        int diplomaId = context.Diplomadetails.ToList().Last().Diplomaid;
                        hDetails.Diplomaid = diplomaId;
                        if (diplomaId != 0)
                        {
                            var result = context.Highereducationaldetails.Add(hDetails);
                            context.SaveChanges();
                            hId = context.Highereducationaldetails.ToList().Last().Highereducationalid;

                        }
                        else
                        {
                            status = -1;
                        }
                    }
                    if (ug != null)
                    {
                        var ugResult = context.Ugdetails.Add(ug);
                        context.SaveChanges();
                        int ugId = context.Ugdetails.ToList().Last().Ugid;
                        hDetails.Ugid = ugId;
                        if (ugId != 0)
                        {
                            if (hId!=0 && diploma!=null)
                            {
                                Highereducationaldetails _hDetails= context.Highereducationaldetails.Find(hId);
                                _hDetails.Ugid = ugId;
                                context.SaveChanges();
                            }
                            else
                            {
                                var result = context.Highereducationaldetails.Add(hDetails);
                                context.SaveChanges();
                                hId = context.Highereducationaldetails.ToList().Last().Highereducationalid;
                            }
                        }
                        else
                        {
                            status = -1;
                        }
                    }
                    if (pg != null)
                    {
                        var pgResult = context.Pgdetails.Add(pg);
                        context.SaveChanges();
                        int pgId = context.Pgdetails.ToList().Last().Pgid;
                        hDetails.Pgid = pgId;
                        if (pgId != 0)
                        {
                            if (hId!=0 && (diploma!=null || ug!=null))
                            {
                                Highereducationaldetails _hDetails= context.Highereducationaldetails.Find(hId);
                                _hDetails.Pgid = pgId;
                                context.SaveChanges();
                            }
                            else
                            {
                                var result = context.Highereducationaldetails.Add(hDetails);
                                context.SaveChanges();
                                hId = context.Highereducationaldetails.ToList().Last().Highereducationalid;
                            }
                        }
                        else
                        {
                            status = -1;
                        }
                    }



                }

                if (sId!=0 && hId!=0)
                {
                    eDetails.Candidateid = candidateId;
                    eDetails.Highereducationalid = hId;
                    eDetails.Secondaryeducationalid = sId;
                    context.Educationaldetails.Add(eDetails);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                status = -1;
            }
            return status;
        }

        public int Updatepassword(string Email, string TempPassword,string NewPassword)
        {
            List<Users> UsersList = context.Users.ToList();
            int status = 0;
            try
            {
                Users _user = UsersList.Where(i => i.Email == Email).FirstOrDefault();
                if (_user!=null)
                {
                    if (_user.Userpassword==TempPassword)
                    {
                        _user.Userpassword = NewPassword;
                        context.SaveChanges();
                    }
                    else
                    {
                        status = 2;
                    }
                    
                    
                }
                else
                {
                    status = 1;
                }
            }
            catch (Exception ex)
            {
                status = -1;
            }
            return status;
        }

        public int Scheduleinterview(Interviewdetails scheduleObj)
        {
            int status = 0;
            try
            {
                if (scheduleObj!=null)
                {
                    context.Interviewdetails.Add(scheduleObj);
                    context.SaveChanges();
                }
                else
                {
                    status = 1;
                }
            }
            catch (Exception ex)
            {
                status = -1;
            }
            return status;
        }

        public List<Candidatedetails> getScheduledCandidates(int interviewerid)
        {
            List<Candidatedetails> candidateList = new List<Candidatedetails>();
            try
            {
                List<Interviewdetails> _interviewList = context.Interviewdetails.ToList();
               List<int> cIdList = (from interview in _interviewList where interview.Intervievwerid == interviewerid select interview.Candidateid).ToList();

                if(cIdList!=null && cIdList.Count>0)
                {
                    foreach (var id in cIdList)
                    {
                        candidateList.Add(GetCandidate(id));
                    }
                   
                }
            }
            catch (Exception ex)
            {
                candidateList = null;
            }
            return candidateList;
        }

        public List<Interviewdetails> getCandidateInterviewdetails(int candidateId)
        {
            List<Interviewdetails> candidateInterInterviewdetailList = new List<Interviewdetails>();

            try
            {
                List<Interviewdetails> _interviewdetailList = context.Interviewdetails.ToList();
                List<Interviewerdetails> _interviewerdetailsList = context.Interviewerdetails.ToList();
                candidateInterInterviewdetailList = (from interviewdetail in _interviewdetailList where interviewdetail.Candidateid == candidateId select interviewdetail).ToList();
                if (candidateInterInterviewdetailList!=null && candidateInterInterviewdetailList.Count>0)
                {
                    foreach (var item in candidateInterInterviewdetailList)
                    {
                        string _interviewername = "";
                        _interviewername = (from interviewer in _interviewerdetailsList where interviewer.Intervievwerid == item.Intervievwerid select (interviewer.Firstname + interviewer.Lastname)).FirstOrDefault();
                        item.Interviewername = _interviewername==null?string.Empty: _interviewername;
                    }
                }

            }
            catch (Exception ex)
            {
                candidateInterInterviewdetailList= null;
            }
            return candidateInterInterviewdetailList;
        }
        //end
    }
}
