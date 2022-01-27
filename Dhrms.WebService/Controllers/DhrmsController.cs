using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dhrms.DataAccess;
using Dhrms.DataAccess.Models;
using Microsoft.AspNetCore.Cors;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dhrms.WebService.Controllers
{
    //[EnableCors("dhrmscontrollerpolicy")]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DhrmsController : Controller
    {
        DhrmsRepository _repository;
        // GET: /<controller>/
        //public IActionResult Index()
        //{
        //   return View();
        //}
        public DhrmsController()
        {
            _repository = new DhrmsRepository();
        }

        [HttpGet]
        //[Authorize]
        public JsonResult GetHRDetails()
        {
            List<Hr> hrlist = new List<Hr>();
            try
            {
                hrlist = _repository.GetAllHRDetails();
                if (hrlist==null || hrlist.Count==0)
                {
                    return Json("No Records Found");
                }
            }
            catch (Exception ex)
            {
                hrlist = null;
            }
            return Json(hrlist);
        }

        [HttpPost]
        public JsonResult ValidateUserCredntials(Users user)
        {
            try
            {
                return Json(_repository.validatelogin(user.Email, user.Userpassword));
            }
            catch (Exception ex)
            {
                return Json("Something went wrong");
            }
        }

        [HttpPost]
        public JsonResult AddInterviewer(Interviewerdetails interviewer)
        {
            int status;
            try
            {
                 status = _repository.AddInterviewer(interviewer);
                string message = string.Empty;
                if (status==0)
                {
                    message = "Success";
                }
                else if(status==1)
                {
                    message = "No changes were made Try again after somtime ";
                }
                else if(status ==-1)
                {
                    message = "Entered email already exists";
                }
                else if(status==-2)
                {
                    message = "User creation failed Try again after somtime";
                }
                else if(status==-99)
                {
                    message = "Something went wrong Try again after somtime";
                }
                //return Json(message);
            }
            catch (Exception ex)
            {
                //return Json("Something went wrong");
                status = -99;
            }
            return Json(status);
        }
        [HttpPost]
        public JsonResult AddCandidate(object candidateObj)
        {
            try
            {
                
                
                
                dynamic _candidaateObj = Newtonsoft.Json.JsonConvert.DeserializeObject(Convert.ToString(candidateObj));
                
                dynamic _candidate = _candidaateObj.candidatedetail;
                dynamic _skill = _candidaateObj.skill;
                dynamic _education = _candidaateObj.education;
                dynamic _diploma = _education.diploma;
                dynamic _ug = _education.ug;
                dynamic _pg = _education.pg;
                dynamic _sslc = _education.sslc;
                dynamic _puc = _education.puc;
                dynamic _experience = _candidaateObj.experience;
                //mapping configuration
                var config = new MapperConfiguration(cfg => { });
                var mapper = config.CreateMapper();

                
                Candidatedetails candidate = mapper.Map<Candidatedetails>(_candidate);

                Skills Skill = mapper.Map<Skills>(_skill);
                Sslcdetails Sslc = mapper.Map<Sslcdetails>(_sslc);
                Pucdetails Puc = mapper.Map<Pucdetails>(_puc);
                Diplomadetails Diploma = mapper.Map<Diplomadetails>(_diploma);
                Ugdetails Ug = mapper.Map<Ugdetails>(_ug);
                Pgdetails Pg = mapper.Map<Pgdetails>(_pg);
                List<dynamic> experienceList = _experience.ToObject<List<dynamic>>();


                int candidateid = 0;
                int status = _repository.AddCandidate(candidate,out candidateid);

                
                //int status = -99;
                string message = string.Empty;
                if (status==0)
                {
                    if (Skill != null)
                    {
                        //adding candidateid to skill entity
                        Skill.Candidateid = candidateid;
                        //call add method to insert
                        int skillStatus= _repository.AddCandidateSkill(Skill);
                        if (skillStatus==-1)
                        {
                            //return Json("Failed to add Skills");
                            return Json("-3");
                        }
                    }
                    if (_experience!=null)
                    {
                        
                        //call add method to insert
                        foreach (var item in experienceList)
                        {
                            Workexperiencedetails Experience = mapper.Map<Workexperiencedetails>(item);
                            //adding candidateid to skill entity
                            Experience.Candidateid = candidateid;

                            int experienceStatus = _repository.AddCandidateExperience(Experience);
                            if (experienceStatus == -1)
                            {
                                //return Json("Failed to add Experience");
                                return Json("-4");
                            }
                        }
                       
                        
                    }
                    if (_education!=null)
                    {
                        //call add method to insert sslc,puc,diploma,ug,pg
                        int educationStatus = _repository.AddCandidateEducation(candidateid, Sslc, Puc, Diploma, Ug, Pg);
                        if (educationStatus == -1)
                        {
                            //return Json("Failed to add Education");
                            return Json("-5");
                        }
                    }
                    
                    message = "Success";
                }
                else if(status==1)
                {
                    message = "No changes were made Try again after somtime ";
                }
                else if(status ==-1)
                {
                    message = "Entered email already exists";
                }
                else if(status==-2)
                {
                    message = "User creation failed Try again after somtime";
                }
                else if(status==-99)
                {
                    message = "Something went wrong Try again after somtime";
                }
                return Json(status);
            }
            catch (Exception ex)
            {
                //return Json("Something went wrong");
                return Json("-99");
            }
        }

        [HttpGet]
        //[Authorize]
        public JsonResult GetAllCandates()
        {
            List<Candidatedetails> CandidateList = new List<Candidatedetails>();
            try
            {
                CandidateList = _repository.GetAllCandidate();
                if (CandidateList==null || CandidateList.Count == 0)
                {
                    return Json("No Records Found");
                }
            }
            catch (Exception ex)
            {
                return Json("No Records Found");
            }
            var settings = new JsonSerializerSettings();
            // This tells your serializer that multiple references are okay.
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            
            //used JSON type to preserve same format case
            return Json(JsonConvert.SerializeObject(CandidateList, settings));
            //return Json(CandidateList);
        }

        [HttpGet]
        public JsonResult GetAllInterViewerDetails()
        {
            List<Interviewerdetails> InterviewerdetailsList = null;
            try
            {
                InterviewerdetailsList = _repository.GetAllInterviewerdetails();
                if (InterviewerdetailsList==null || InterviewerdetailsList.Count == 0)
                {
                    return Json("No Records Found");
                }
            }
            catch (Exception ex)
            {
                return Json("No Records Found");
            }
            return Json(InterviewerdetailsList);

        }
        [HttpGet]
        public JsonResult getCandidateInterviewdetails(int candidateId)
        {
            List<Interviewdetails> InterviewdetailsList = null;
            try
            {
                InterviewdetailsList = _repository.getCandidateInterviewdetails(candidateId);
                if (InterviewdetailsList==null || InterviewdetailsList.Count == 0)
                {
                    return Json("No Records Found");
                }
            }
            catch (Exception ex)
            {
                return Json("No Records Found");
            }
            var settings = new JsonSerializerSettings();
            // This tells your serializer that multiple references are okay.
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            //used JSON type to preserve same format case
            return Json(JsonConvert.SerializeObject(InterviewdetailsList, settings));
            //return Json(InterviewdetailsList);

        }

        [HttpGet]
        public JsonResult getScheduledCandidates(int id)
        {
            List<Candidatedetails> candidateDetailsList = null;
            try
            {
                candidateDetailsList = _repository.getScheduledCandidates(id);
                if (candidateDetailsList==null || candidateDetailsList.Count == 0)
                {
                    return Json("No Records Found");
                }
            }
            catch (Exception ex)
            {
                return Json("No Records Found");
            }
            var settings = new JsonSerializerSettings();
            // This tells your serializer that multiple references are okay.
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            //used JSON type to preserve same format case
            return Json(JsonConvert.SerializeObject(candidateDetailsList, settings));
            //return Json(CandidateList);
        }
        [HttpGet]
        public JsonResult GetCandidate(int id)
        {
            Candidatedetails candidatedetail = null;
            try
            {
                candidatedetail = _repository.GetCandidate(id);
                if (candidatedetail == null)
                {
                    return Json("No Records Found");
                }
            }
            catch (Exception ex)
            {
                return Json("No Records Found");
            }
            var settings = new JsonSerializerSettings();
            // This tells your serializer that multiple references are okay.
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Json(JsonConvert.SerializeObject(candidatedetail, settings));

        }
        [HttpPost]
        public JsonResult Updatepassword(dynamic passwordObj)
        {
            string message = "";
            try
            {
                //int status = _repository.Updatepassword(Email, TempPassword,Password);
                dynamic _passwordObj = Newtonsoft.Json.JsonConvert.DeserializeObject(Convert.ToString(passwordObj));

                string _email = _passwordObj.Email;
                string _temppass = _passwordObj.TempPassword;
                string _pass = _passwordObj.Password;
                int status = _repository.Updatepassword(_email, _temppass, _pass);

                if (status==0)
                {
                    message= "success";
                }
                else if(status==1)
                {
                    message ="No users found";
                }
                else if(status==2)
                {
                    message="Temporary password expired";
                }
                else if(status==-1)
                {
                    message="Failed to update try after sometime";
                }
            }
            catch (Exception ex)
            {
                message="Failed to update try again after sometime";
            }
            return Json(message);
        }


        public JsonResult Scheduleinterview(dynamic scheduleObj)
        {
            string message = "";
            try
            {
                dynamic _scheduleObj = Newtonsoft.Json.JsonConvert.DeserializeObject(Convert.ToString(scheduleObj));

                //mapping configuration
                var config = new MapperConfiguration(cfg => { });
                var mapper = config.CreateMapper();

                Interviewdetails scheduleDetails = mapper.Map<Interviewdetails>(_scheduleObj);

                int status = _repository.Scheduleinterview(scheduleDetails);
                if (status == 0)
                {
                    message = "success";
                }
                else if (status == 1)
                {
                    message = "value should not null";
                }
                else if (status == -1)
                {
                    message = "Failed to update try after sometime";
                }
            }
            catch (Exception ex)
            {
                message = "Failed to schedule interview try again after sometime";
            }
            return Json(message);
        }
        public JsonResult addInterviewFeedback(Interviewdetails interview)
        {
            string message = "";
            try
            {
                int status = _repository.addInterviewFeedback(interview);
                if (status == 0)
                {
                    message = "success";
                }
                else if (status == 1)
                {
                    message = "no candidates to update";
                }
                else if (status == -99)
                {
                    message = "Failed to update try after sometime";
                }
            }
            catch (Exception ex)
            {
                message = "Failed to update try after sometime";
            }
            return Json(message);
        }

        //end
    }
}
