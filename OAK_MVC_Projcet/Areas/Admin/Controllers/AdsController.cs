using Business_Access_Layer;
using Data_Transfer_Layer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OAK_MVC_Projcet.Areas.Admin.Controllers
{
    public class AdsController : BaseController
    {
        AdsBusinessLayer adsBusinessLayer= new AdsBusinessLayer();
        public ActionResult AddAds()
        {
            AdsDataTranfer adsDataTranfer=new AdsDataTranfer();
            return View(adsDataTranfer);
        }
        [HttpPost]
        public ActionResult AddAds(AdsDataTranfer model)
        {
            if (model.AdsImage == null)
            {
                ViewBag.ProcessState = General.Message.ImageMissing;
            }
            else if (ModelState.IsValid)
            {
                HttpPostedFileBase postedFileBase = model.AdsImage;
                Bitmap adsImage = new Bitmap(postedFileBase.InputStream);
                Bitmap resizeImage = new Bitmap(adsImage, 128, 128);
                string extension = Path.GetExtension(postedFileBase.FileName);
                string filename = "";
                if (extension == ".jpg" || extension == ".png" || extension == ".jpeg" || extension == ".gif")
                {
                    string uniqueNumber = Guid.NewGuid().ToString();
                    filename = uniqueNumber + postedFileBase.FileName;
                    adsImage.Save(Server.MapPath("~/Areas/Admin/Content/AdsImages/" + filename));
                    model.ImagePath = filename;

                    bool isAdded = adsBusinessLayer.AddAddsBusiness(model);
                    if (isAdded)
                    {
                        ViewBag.ProcessState = General.Message.AddSuccess;
                        model = new AdsDataTranfer();
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
        public ActionResult AdsList()
        {
            List<AdsDataTranfer> adsList = adsBusinessLayer.AdsListBusiness();
            if (adsList.Count > 0)
            { return View(adsList); }
            return View(adsList);
        }

        public ActionResult UpdateAds(int ID)
        {
            AdsDataTranfer adsDetails = new AdsDataTranfer();
            if (ID > 0)
            {
                adsDetails = adsBusinessLayer.GetAdByIdBusiness(ID);
                return View(adsDetails);
            }
            return View(adsDetails);

        }
        [HttpPost]
        public ActionResult UpdateAds(AdsDataTranfer model)
        {
            AdsDataTranfer userDetails = new AdsDataTranfer();
            if (!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Message.EmptyArea;
            }
            else
            {
                if (model.AdsImage != null)
                {
                    HttpPostedFileBase postedFileBase = model.AdsImage;
                    Bitmap adsImage = new Bitmap(postedFileBase.InputStream);
                    Bitmap resizeImage = new Bitmap(adsImage, 128, 128);
                    string extension = Path.GetExtension(postedFileBase.FileName);
                    string filename = "";
                    if (extension == ".jpg" || extension == ".png" || extension == ".jpeg" || extension == ".gif")
                    {
                        string uniqueNumber = Guid.NewGuid().ToString();
                        filename = uniqueNumber + postedFileBase.FileName;
                        adsImage.Save(Server.MapPath("~/Areas/Admin/Content/AdsImages/" + filename));
                        model.ImagePath = filename;
                    }
                }

                string oldImagePath = adsBusinessLayer.UpdateAdsByIdBusiness(model);
                if (model.AdsImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/AdsImages/" + oldImagePath)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/AdsImages/" + oldImagePath));
                    }

                }
                ViewBag.ProcessState = General.Message.UpdateSuccess;

                return View(userDetails);
            }

            return View(userDetails);
        }
        [HttpPost]
        public ActionResult DeleteAds(int ID)
        {
            string imagePath=adsBusinessLayer.DeleteAdsBusiness(ID);
            if(imagePath!=null)
            {
                if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/AdsImages/" + imagePath)))
                {
                    System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/AdsImages/" + imagePath));
                }
            }
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}