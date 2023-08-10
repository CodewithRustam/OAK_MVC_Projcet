using Data_Transfer_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OAK_MVC_Projcet.Areas.Models.Attributes
{
    public class LoginControl : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (UserStatic.UserId == 0)
            {
                filterContext.HttpContext.Response.Redirect("/Admin/Login/Index");
            }

        }
    }
}