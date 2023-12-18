﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SkillsLabAssignment.Custom
{
    public class CustomAuthorizationAttribute : ActionFilterAttribute
    {
        public string Roles {  get; set; }
        public string[] AuthorizedRoles { get; set; }
        public CustomAuthorizationAttribute(string roles) 
        { 
            this.Roles = roles;
            AuthorizedRoles = this.Roles.Split(',');
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var dfController = filterContext.Controller as Controller;
            if (dfController != null && dfController.Session["CurrentRole"] != null)
            {
                var currentRole = dfController.Session["CurrentRole"] as string;
                if(!AuthorizedRoles.Contains(currentRole))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Common", action = "Action Denied" }));
                }
            }
            else { }
        }
    }
}