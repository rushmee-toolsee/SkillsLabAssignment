using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace SkillsLabAssignment.Custom
{
    public class UserSessionAttribute  :ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["CurrentUser"] ==  null || filterContext.HttpContext.Session["CurrentRole"] == null)
            {
                filterContext.Result = new RedirectResult("~/Login/Index");
            }
        }
    }
}