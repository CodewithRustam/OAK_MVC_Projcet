using Business_Access_Layer;
using Data_Transfer_Layer;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Web;
using System;
using System.Web.Mvc;

namespace OAK_MVC_Projcet.Areas.Admin.Controllers
{
    public class FavController : Controller
    {
        FavBusinessAccess FavBusinessAccess=new FavBusinessAccess();
        // GET: Admin/Fav
        public ActionResult UpdateFav()
        {
            FavDataTransfer favDataTransfer= new FavDataTransfer();
            favDataTransfer = FavBusinessAccess.GetFavBusiness();
            return View(favDataTransfer);
        }
        [HttpPost]
        public ActionResult UpdateFav(FavDataTransfer model)
        {
            FavDataTransfer favDataTransfer = new FavDataTransfer();
            if (!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Message.EmptyArea;
            }
            else
            {
                if (model.FavImage != null)
                {
                    HttpPostedFileBase postedFileBase = model.FavImage;
                    Bitmap favImage = new Bitmap(postedFileBase.InputStream);
                    Bitmap resizeImage = new Bitmap(favImage, 128, 128);
                    string extension = Path.GetExtension(postedFileBase.FileName);
                    string filename = "";
                    if (extension == ".jpg" || extension == ".png" || extension == ".jpeg" || extension == ".ico")
                    {
                        string uniqueNumber = Guid.NewGuid().ToString();
                        filename = uniqueNumber + postedFileBase.FileName;
                        resizeImage.Save(Server.MapPath("~/Areas/Admin/Content/FavImage/" + filename));
                        model.Fav = filename;
                    }
                    ViewBag.ProcessState = General.Message.ExtensionError;
                }

                if (model.FavImage != null)
                {
                    HttpPostedFileBase postedFileLogo = model.FavImage;
                    Bitmap logoImage = new Bitmap(postedFileLogo.InputStream);
                    Bitmap resizelogoImage = new Bitmap(logoImage, 100, 100);
                    string extension = Path.GetExtension(postedFileLogo.FileName);
                    string logofile = "";
                    if (extension == ".jpg" || extension == ".png" || extension == ".jpeg" || extension == ".ico")
                    {
                        string uniqueNumber = Guid.NewGuid().ToString();
                        logofile = uniqueNumber + postedFileLogo.FileName;
                        resizelogoImage.Save(Server.MapPath("~/Areas/Admin/Content/FavImage/" + logofile));
                        model.Fav = logofile;
                    }
                    else
                    ViewBag.ProcessState = General.Message.ExtensionError;
                }

                favDataTransfer = FavBusinessAccess.UpdateFavBusiness(model);
                if (model.FavImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/FavImage/" + favDataTransfer.Fav)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/FavImage/" + favDataTransfer.Fav));
                    }

                }
                if (model.LogoImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/FavImage/" + favDataTransfer.Logo)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/FavImage/" + favDataTransfer.Logo));
                    }

                }
                ViewBag.ProcessState = General.Message.UpdateSuccess;

                return View(favDataTransfer);
            }

            return View(favDataTransfer);
        }
    }
}