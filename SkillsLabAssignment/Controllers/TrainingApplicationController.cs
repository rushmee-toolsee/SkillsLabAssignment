using BusinessLayer.Services;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace SkillsLabAssignment.Controllers
{
    public class TrainingApplicationController : Controller
    {
        private readonly ITrainingApplicationService _trainingApplicationService;
        private readonly IAccountService _accountService;

        public TrainingApplicationController(ITrainingApplicationService TrainingApplicationService, IAccountService AccountService)
        {
            _trainingApplicationService = TrainingApplicationService;
            _accountService = AccountService;
        }
            // GET: TrainingApplication
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ApplyForTraining(int trainingId)
        {
            HttpPostedFileBase file = Request.Files["file"];

            if (file != null && file.ContentLength > 0)
            {
                using (Stream fileStream = file.InputStream)
                {
                    if (_trainingApplicationService.SubmitTrainingApplication(trainingId, file.FileName, fileStream))
                    {
                        return Json(new { success = true, message = "Application submitted successfully!" });
                    }
                }
            }
            return Json(new { success = false, message = "Error applying for training." });
        }

        public ActionResult UserApplications()
        {
            int userId = _accountService.GetUserId();

            var userApplications = _trainingApplicationService.GetUserApplications(userId);
            return View(userApplications);
        }
    }    
}