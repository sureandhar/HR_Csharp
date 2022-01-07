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
                    RoleName = (from role in RolesList where role.Roleid == RoleId select role.Rolename).FirstOrDefault();
                   
                }
                else
                {
                    return RoleName = "No user found";
                }

                return RoleName;

            }
            catch (Exception ex)
            {
                return RoleName = "Some thing went wrong!";
            }
        }

        public List<Candidatedetails> GetAllCandidate()
        {
            List<Candidatedetails> CandidateList = null;
            try
            {
                
                CandidateList = context.Candidatedetails.ToList();
            }
            catch (Exception ex)
            {
                CandidateList = null;
            }
            return CandidateList;
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
        public int AddCandidate(Candidatedetails candidate)
        {
            int status = 1;
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
            //NpgsqlParameter prmstatus = new NpgsqlParameter("@status", status)
            //{
            //    Direction = ParameterDirection.InputOutput
            //};
            prmstatus.Direction = System.Data.ParameterDirection.InputOutput; 
            try
            {
                result = context.Database.ExecuteSqlRaw("CALL usp_addcandidate (@firstname,@lastname,@email,@dob,@contactnumber,@roleid,@gender,@currentaddress,@permanentaddress,@city,@status)"
                    ,prmfirstname,prmlastname,prmemail,prmdob,prmcontactnumber,prmroleid,prmgender,prmcurraddress,prmpermntaddress,prmcity,prmstatus);
                status = prmstatus.Value!=null ? Convert.ToInt32(prmstatus.Value):1;
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
    }
}
