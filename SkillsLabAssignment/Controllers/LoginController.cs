using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using BusinessLayer.Services;
using DataAccessLayer.Models;

namespace SkillsLabAssignment.Controllers
{
    public class LoginController : Controller
    {

        private readonly ILoginService _loginService;
        private readonly IAccountService _accountService;
        public LoginController(ILoginService loggingIn, IAccountService accountService)
        {
            _loginService = loggingIn;
            _accountService = accountService;
        }
        // GET: Login
        public ActionResult Index()
        {
            ViewBag.Login = "Login";
            return View();
        }
         
        [HttpPost]
        public JsonResult VerifyLogin(UserAccount account)
        {
            List<ValidationResult> validationResults = _loginService.LoggedIn(account);       
                if ((validationResults.Count == 0) )
                {
                int accountId = _accountService.GetLoginAccountID(account.EmailAddress);
                Session["accountId"] = _accountService.GetLoginAccountID(account.EmailAddress);
                Session["userName"] = _accountService.GetUsername(account.EmailAddress);
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Training") });
                }
                else
                {
                    var errorMessages = validationResults.Select(vr => vr.ErrorMessage).ToList();
                    return Json(new { success = false, errors = errorMessages });
                }
        }
    }
}
