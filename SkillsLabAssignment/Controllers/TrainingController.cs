using BusinessLayer.Services;
using DataAccessLayer.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SkillsLabAssignment.Controllers
{
   
    public class TrainingController : Controller
    {

        private readonly ITrainingService _trainingService;
        private readonly IAccountService _accountService;
        public TrainingController(ITrainingService training, IAccountService accountService )
        {
            _trainingService = training;
            _accountService = accountService;
        }
 
        public ActionResult Index()
        {
            string userName = Session["UserName"] as string;
            ViewBag.UserName = userName;
            return View();
        }
        public ActionResult ViewDetails()
        {
            return View();
        }
       
        [HttpGet]
        public JsonResult GetAllTrainingJson()
        {
            List<Training> trainingData = _trainingService.RetrieveTraining();
            return Json(new {trainings = trainingData}, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTrainingDetails(int trainingId)
        {
            Training trainingData = _trainingService.RetrieveTrainingById(trainingId);
            return Json(trainingData, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public string DisplayUsername(string emailAddress)
        {
            return _accountService.GetUsername(emailAddress);
        }
    }
}