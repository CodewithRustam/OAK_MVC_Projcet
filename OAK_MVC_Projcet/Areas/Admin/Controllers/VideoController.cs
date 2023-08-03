using Business_Access_Layer;
using Data_Transfer_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OAK_MVC_Projcet.Areas.Admin.Controllers
{
    public class VideoController : Controller
    {
        VideoBusinessLayer videoBusinessLayer=new VideoBusinessLayer();
        // GET: Admin/Video
        public ActionResult AddVideo()
        {
            VideoDataTransfer videoDataTransfer= new VideoDataTransfer();
            return View(videoDataTransfer);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddVideo(VideoDataTransfer model)
        {
            // < iframe width = "560" height = "315" src = "https://www.youtube.com/embed/rvSaWYqFC44?si=3AtcQhqNARZfyc-B" title = "YouTube video player" frameborder = "0" allow = "accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen ></ iframe >;
            //https://www.youtube.com/watch?v=rvSaWYqFC44
            VideoDataTransfer videoDataTransfer = new VideoDataTransfer();
            if(ModelState.IsValid)
            {
                string path = model.OriginalVideoPath.Substring(32);
                string mergelink = "https://www.youtube.com/embed";
                mergelink += path;
                model.VideoPath = String.Format(@"< iframe width = ""300"" height = ""200"" src =""{0}""  frameborder = ""0"" allowfullscreen ></ iframe >",mergelink);
                if (videoBusinessLayer.AddVideoBusiness(model))
                {

                }
                else
                {

                }

            }
            else
            {
                ViewBag.ProcessState = General.Message.EmptyArea;
            }

            return View(videoDataTransfer);
        }
    }
}