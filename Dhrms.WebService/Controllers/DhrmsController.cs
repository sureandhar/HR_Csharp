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
        public JsonResult GetHRDetails()
        {
            List<Hr> hrlist = new List<Hr>();
            try
            {
                hrlist = _repository.GetAllHRDetails();
                if (hrlist.Count==0)
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
            try
            {
                int status = _repository.AddInterviewer(interviewer);
                string message = string.Empty;
                if (status==0)
                {
                    message = "Success";
                }
                else if(status==1)
                {
                    message = "No changes were made Try again after somtime ";
                }
                //else if(status==-1)
                //{
                //    message = "Invalid Email address ";
                //}
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
                return Json(message);
            }
            catch (Exception ex)
            {
                return Json("Something went wrong");
            }
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
                var config = new MapperConfiguration(cfg => { });
                var mapper = config.CreateMapper();

                Candidatedetails candidate = mapper.Map<Candidatedetails>(_candidate);

                Skills skill = mapper.Map<Skills>(_skill);
                int candidateid = 0;
                int status = _repository.AddCandidate(candidate,out candidateid);

                
                //int status = -99;
                string message = string.Empty;
                if (status==0)
                {
                    if (skill!=null)
                    {
                        //adding candidateid to skill entity
                        skill.Candidateid = candidateid;
                        //call add method to insert
                        _repository.AddCandidateSkill(skill);
                    }
                    
                    message = "Success";
                }
                else if(status==1)
                {
                    message = "No changes were made Try again after somtime ";
                }
                //else if(status==-1)
                //{
                //    message = "Invalid Email address ";
                //}
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
                return Json(message);
            }
            catch (Exception ex)
            {
                return Json("Something went wrong");
            }
        }

        [HttpGet]
        public JsonResult GetAllCandates()
        {
            List<Candidatedetails> CandidateList = new List<Candidatedetails>();
            try
            {
                CandidateList = _repository.GetAllCandidate();
                if (CandidateList.Count == 0)
                {
                    return Json("No Records Found");
                }
            }
            catch (Exception ex)
            {
                return Json("No Records Found");
            }
            
            return Json(JsonConvert.SerializeObject(CandidateList, Formatting.Indented));
        }

        [HttpGet]
        public JsonResult GetAllInterViewerDetails()
        {
            List<Interviewerdetails> InterviewerdetailsList = null;
            try
            {
                InterviewerdetailsList = _repository.GetAllInterviewerdetails();
                if (InterviewerdetailsList.Count == 0)
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
    }
}
