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
    public class SocialMediaController : BaseController
    {
        SocialMediaBusinessLayer socialMediaBusiness = new SocialMediaBusinessLayer(); 
        public ActionResult AddSocialMedia()
        {
            SocialMediaDataTransfer socialMediaDataTransfer = new SocialMediaDataTransfer();
            return View(socialMediaDataTransfer);
        }
        [HttpPost]
        public ActionResult AddSocialMedia(SocialMediaDataTransfer model)
        {
            if (model.SocialImage== null)
            {
                ViewBag.ProcessState = General.Message.ImageMissing;
            }
            else if(ModelState.IsValid)
            {
                HttpPostedFileBase postedFileBase = model.SocialImage;
                Bitmap socialMedia = new Bitmap(postedFileBase.InputStream);
                string extension = Path.GetExtension(postedFileBase.FileName);
                string filename = "";
                if (extension == ".jpg" || extension == ".png" || extension == ".jpeg" || extension == ".gif")
                {
                    string uniqueNumber=Guid.NewGuid().ToString();
                    filename=uniqueNumber+postedFileBase.FileName;
                    socialMedia.Save(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + filename));
                    model.ImagePath = filename;

                    bool isAdded=socialMediaBusiness.AddSocialMediaBusiness(model);
                    if (isAdded)
                    {
                        ViewBag.ProcessState = General.Message.AddSuccess;
                        model = new SocialMediaDataTransfer();
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
            return View(model);
        }

        public ActionResult SocialMediaList()
        {
            List<SocialMediaDataTransfer> socialMediaDataTransfer= socialMediaBusiness.SocialMediaListBusiness();
            return View(socialMediaDataTransfer);

        }
        public ActionResult UpdateSocialMedia(int ID)
        {
            SocialMediaDataTransfer socialMediaData=new SocialMediaDataTransfer();
            if (ID > 0)
            {
                socialMediaData= socialMediaBusiness.GetSocialMediaByIdBusiness(ID);
                return View(socialMediaData);
            }
            return View(socialMediaData);

        }
        [HttpPost]
        public ActionResult UpdateSocialMedia(SocialMediaDataTransfer socialMediaDataTransfer)
        {
            SocialMediaDataTransfer socialMediaData = new SocialMediaDataTransfer();
            if(!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Message.EmptyArea;
            }
            else
            {
                if (socialMediaDataTransfer.SocialImage != null)
                {
                    HttpPostedFileBase postedFileBase = socialMediaDataTransfer.SocialImage;
                    Bitmap socialMedia = new Bitmap(postedFileBase.InputStream);
                    string extension = Path.GetExtension(postedFileBase.FileName);
                    string filename = "";
                    if (extension == ".jpg" || extension == ".png" || extension == ".jpeg" || extension == ".gif")
                    {
                        string uniqueNumber = Guid.NewGuid().ToString();
                        filename = uniqueNumber + postedFileBase.FileName;
                        socialMedia.Save(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + filename));
                        socialMediaDataTransfer.ImagePath = filename;
                    }
                }

                string oldImagePath = socialMediaBusiness.UpdateSocialMediaByIdBusiness(socialMediaDataTransfer);
                if(socialMediaDataTransfer.SocialImage != null)
                {
                    if(System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" +oldImagePath)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + oldImagePath));
                    }
                    
                }
                ViewBag.ProcessState = General.Message.UpdateSuccess;
                
                return View(socialMediaData);
            }

            return View(socialMediaData);

        }
        [HttpPost]
        public ActionResult DeleteSocialMedia(int ID)
        {
            string imagePath = socialMediaBusiness.DeleteSocialMediaBusiness(ID);
            if (imagePath != null)
            {
                if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + imagePath)))
                {
                    System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + imagePath));
                }
            }
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}