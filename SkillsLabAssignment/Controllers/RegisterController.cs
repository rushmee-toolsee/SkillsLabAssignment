using BusinessLayer.Services;
using DataAccessLayer.Common;
using DataAccessLayer.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace SkillsLabAssignment.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IRegisterService _registerService;
        private readonly IDepartmentService _departmentService;
        public RegisterController(IRegisterService register, IDAL layer, IDepartmentService dept)
        {
            _registerService = register;
            _departmentService = dept;
        }
        // GET: Register
        public ActionResult Index()
        {
            ViewBag.Login = "Register";
            return View();
        }

        [HttpPost]
        public JsonResult HandleRegistration(User user)
        {
            List<ValidationResult> validationResults = _registerService.AccountRegistered(user);

            if (validationResults.Count == 0)
            {
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Login") });
            }
            else
            {
                var errorMessages = validationResults.Select(vr => vr.ErrorMessage).ToList();
                return Json(new { success = false, errors = errorMessages });
            }
        }

        [HttpPost]
        public ActionResult GetManagersByDepartment(string departmentName)
        {
            int departmentId = _departmentService.RetrieveDepartmentIdUsingName(departmentName);
            List<string> managers = _departmentService.RetrieveManagersUsingDepartment(departmentId);
            return Json(new { success = true, managers });
        }
    }
}
