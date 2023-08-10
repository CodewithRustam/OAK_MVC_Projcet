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
    public class PostController : BaseController
    {
        PostBusinessAccess postBusinessLayer = new PostBusinessAccess();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddPost()
        {
            PostDataTransfer postDataTransfer = new PostDataTransfer();
            postDataTransfer.Categories = CategoryBusinessAccess.GetCategories();
            return View(postDataTransfer);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddPost(PostDataTransfer model)
        {
            if (model.PostImage[0] == null)
            {
                ViewBag.ProcessState = General.Message.ImageMissing;
            }
            else if (ModelState.IsValid)
            {
                foreach(var item in model.PostImage)
                {
                    Bitmap bitmap = new Bitmap(item.InputStream);
                    string ext = Path.GetExtension(item.FileName);
                    if(ext!=".png"&& ext != ".jpg"&& ext != ".jpeg")
                    {
                        ViewBag.ProcessState=General.Message.ExtensionError;
                        model.Categories= CategoryBusinessAccess.GetCategories();
                        return View(model);
                    }
                }
                List<PostImageDataTransfer> imageList = new List<PostImageDataTransfer>();
                foreach(var postedFile in model.PostImage)
                {
                    Bitmap image= new Bitmap(postedFile.InputStream);
                    Bitmap resizeImage=new Bitmap(image,750,422);
                    string filename = "";
                    string uniqNumber=Guid.NewGuid().ToString();
                    filename = uniqNumber + postedFile.FileName;
                    resizeImage.Save(Server.MapPath("~/Areas/Admin/Content/PostImage/"+ filename));

                    PostImageDataTransfer imageDataTranfer=new PostImageDataTransfer();
                    imageDataTranfer.ImagePath = filename;
                    imageList.Add(imageDataTranfer);
                }
                model.PostImages= imageList;

                if (postBusinessLayer.AddPostBusiness(model))
                {
                    ViewBag.ProcessState = General.Message.AddSuccess;
                    ModelState.Clear();
                    model=new PostDataTransfer();
                }
                else
                {
                    ViewBag.ProcessState = General.Message.GeneralError;
                }
            }
            else
            {
                ViewBag.ProcessState=General.Message.EmptyArea;
            }
            model.Categories = CategoryBusinessAccess.GetCategories();
            return View(model);
        }
        public ActionResult PostList()
        {
            List<PostDataTransfer> postDataTransfer = postBusinessLayer.PostListBusiness();
            if (postDataTransfer != null && postDataTransfer.Count > 0)
            {
                return View(postDataTransfer);
            }
            return View(postDataTransfer);
        }
        public ActionResult UpdatePost(int ID)
        {
            PostDataTransfer postDataTransfer = new PostDataTransfer();
            postDataTransfer = postBusinessLayer.GetPostbyID(ID);
            postDataTransfer.Categories= CategoryBusinessAccess.GetCategories();
            postDataTransfer.isUpdate= true;
            return View(postDataTransfer);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdatePost(PostDataTransfer model)
        {
            IEnumerable<SelectListItem> selectListItems= CategoryBusinessAccess.GetCategories();
            if (ModelState.IsValid)
            {
                if (model.PostImage[0] != null)
                {
                    foreach (var item in model.PostImage)
                    {
                        Bitmap bitmap = new Bitmap(item.InputStream);
                        string ext = Path.GetExtension(item.FileName);
                        if (ext != ".png" && ext != ".jpg" && ext != ".jpeg")
                        {
                            ViewBag.ProcessState = General.Message.ExtensionError;
                            model.Categories = CategoryBusinessAccess.GetCategories();
                            return View(model);
                        }
                    }
                    List<PostImageDataTransfer> imageList = new List<PostImageDataTransfer>();
                    foreach (var postedFile in model.PostImage)
                    {
                        Bitmap image = new Bitmap(postedFile.InputStream);
                        Bitmap resizeImage = new Bitmap(image, 750, 422);
                        string filename = "";
                        string uniqNumber = Guid.NewGuid().ToString();
                        filename = uniqNumber + postedFile.FileName;
                        resizeImage.Save(Server.MapPath("~/Areas/Admin/Content/PostImage/" + filename));

                        PostImageDataTransfer imageDataTranfer = new PostImageDataTransfer();
                        imageDataTranfer.ImagePath = filename;
                        imageList.Add(imageDataTranfer);
                    }
                    model.PostImages = imageList;
                }

                if (postBusinessLayer.UpdatePostBusinessAccess(model))
                {
                    ViewBag.ProcessState = General.Message.UpdateSuccess;
                    model.Categories = selectListItems;
                }
                else
                {
                    ViewBag.ProcessState = General.Message.GeneralError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Message.EmptyArea;
                model= postBusinessLayer.GetPostbyID(model.ID);
                model.Categories = selectListItems;
                model.isUpdate= true;
                return View(model);
            }
            model.Categories = selectListItems;
            return View(model);
        }

        [HttpPost]
        public ActionResult DeletePostImage(int ID)
        {
            string imagePath=postBusinessLayer.DeletePostImageBusiness(ID);
            if (imagePath != null)
            {
                if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/PostImage/" + imagePath)))
                {
                    System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/PostImage/" + imagePath));
                }
            }
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeletePost(int ID)
        {
            List<PostImageDataTransfer> imageList = postBusinessLayer.DeletePostBusiness(ID);
            foreach(var image in imageList)
            {
                if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/PostImage/" + image.ImagePath)))
                {
                    System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/PostImage/" + image.ImagePath));
                }
            }
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}