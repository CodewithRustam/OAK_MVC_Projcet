using Business_Access_Layer;
using Data_Transfer_Layer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace OAK_MVC_Projcet.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        UserBusinessAccess userBusinessAccess=new UserBusinessAccess();
        public ActionResult AddUser()
        {
            UserDataTransfer userDataTransfer = new UserDataTransfer();
            return View(userDataTransfer);
        }
        [HttpPost]
        public ActionResult AddUser(UserDataTransfer userDataTransfer)
        {
            if (userDataTransfer.UserImage == null)
            {
                ViewBag.ProcessState = General.Message.ImageMissing;
            }
            else if (ModelState.IsValid)
            {
                HttpPostedFileBase postedFileBase = userDataTransfer.UserImage;
                Bitmap userImage = new Bitmap(postedFileBase.InputStream);
                Bitmap resizeImage = new Bitmap(userImage, 128, 128);
                string extension = Path.GetExtension(postedFileBase.FileName);
                string filename = "";
                if (extension == ".jpg" || extension == ".png" || extension == ".jpeg" || extension == ".gif")
                {
                    string uniqueNumber = Guid.NewGuid().ToString();
                    filename = uniqueNumber + postedFileBase.FileName;
                    userImage.Save(Server.MapPath("~/Areas/Admin/Content/UserImages/" + filename));
                    userDataTransfer.Imagepath = filename;

                    bool isAdded = userBusinessAccess.AddUserBusiness(userDataTransfer);
                    if (isAdded)
                    {
                        ViewBag.ProcessState = General.Message.AddSuccess;
                        userDataTransfer = new UserDataTransfer();
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.ProcessState = General.Message.GeneralError;
                    }
                }
                else
                {
                    ViewBag.ProcessState = General.Message.ExtensionError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Message.EmptyArea;
            }
            return View(userDataTransfer);
        }

        public ActionResult UserList()
        {
            List<UserDataTransfer> userList = userBusinessAccess.UserListBusiness();
            if (userList.Count > 0)
            { return View(userList); }
            return View(userList);
        }
        public ActionResult UpdateUser(int ID)
        {
            UserDataTransfer userDetails = new UserDataTransfer();
            if (ID > 0)
            {
                userDetails = userBusinessAccess.GetUserByIdBusiness(ID);
                return View(userDetails);
            }
            return View(userDetails);

        }
        [HttpPost]
        public ActionResult UpdateUser(UserDataTransfer model)
        {
            UserDataTransfer userDetails = new UserDataTransfer();
            if (!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Message.EmptyArea;
            }
            else
            {
                if (model.UserImage != null)
                {
                    HttpPostedFileBase postedFileBase = model.UserImage;
                    Bitmap userImage = new Bitmap(postedFileBase.InputStream);
                    Bitmap resizeImage = new Bitmap(userImage, 128, 128);
                    string extension = Path.GetExtension(postedFileBase.FileName);
                    string filename = "";
                    if (extension == ".jpg" || extension == ".png" || extension == ".jpeg" || extension == ".gif")
                    {
                        string uniqueNumber = Guid.NewGuid().ToString();
                        filename = uniqueNumber + postedFileBase.FileName;
                        userImage.Save(Server.MapPath("~/Areas/Admin/Content/UserImages/" + filename));
                        model.Imagepath = filename;
                    }
                }

                string oldImagePath = userBusinessAccess.UpdateUserByIdBusiness(model);
                if (model.UserImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/UserImages/" + oldImagePath)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/UserImages/" + oldImagePath));
                    }

                }
                ViewBag.ProcessState = General.Message.UpdateSuccess;

                return View(userDetails);
            }

            return View(userDetails);
        }

        public ActionResult DeleteUser(int id)
        {
           string imagePath= userBusinessAccess.DeleteUserBusiness(id);
           if (imagePath != null)
           {
               if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/UserImages/" + imagePath)))
               {
                   System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/UserImages/" + imagePath));
               }
           
           }
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}