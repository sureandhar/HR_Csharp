using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dhrms.DataAccess;
using Dhrms.DataAccess.Models;
using Microsoft.AspNetCore.Cors;

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
        public JsonResult AddCandidate(Candidatedetails candidate)
        {
            try
            {
                int status = _repository.AddCandidate(candidate);
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
            return Json(CandidateList);
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
