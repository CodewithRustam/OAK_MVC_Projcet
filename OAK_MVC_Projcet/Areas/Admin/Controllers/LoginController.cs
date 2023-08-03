using Business_Access_Layer;
using Data_Transfer_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OAK_MVC_Projcet.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        UserBusinessAccess userBusinessAccess=new UserBusinessAccess();
        public ActionResult Index()
        {
            UserDataTransfer user = new UserDataTransfer();
            return View(user);
        }
        [HttpPost]
        public ActionResult Index(UserDataTransfer userdata)
        {
            if (ModelState.IsValid)
            {
               UserDataTransfer userDataTransfer= userBusinessAccess.LoginUserNamePassword(userdata);

                if(userDataTransfer!=null&&userDataTransfer.Id!=0)
                {
                    UserStatic.UserId = userDataTransfer.Id;
                    UserStatic.isAdmin = userDataTransfer.isAdmin;
                    UserStatic.Namesurname = userDataTransfer.Name;
                    UserStatic.ImagePath = userDataTransfer.Imagepath;

                    LogDataBusiness.AddLogDataBusiness(General.ProcessType.Login, General.TableName.Login, 12);
                    ViewBag.message = "Login Successfull";
                    return RedirectToAction("Index", "Post");
                }
                else
                {
                    ViewBag.message = "Login Failed";
                }
            }
            return View();
        }
    }
}