using Business_Access_Layer;
using Data_Transfer_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OAK_MVC_Projcet.Controllers
{
    public class HomeController : Controller
    {
        LayoutBusinessLayer layoutBusinessLayer = new LayoutBusinessLayer();
        GeneralBusinessLayer generalBusiness=new GeneralBusinessLayer();
        public ActionResult Index()
        {
            HomeLayoutDataTransfer homeLayout=new HomeLayoutDataTransfer();
            homeLayout = layoutBusinessLayer.GetLayoutData();
            ViewData["LayoutDataAccess"]=homeLayout;

            GeneralDataTransfer generalData=new GeneralDataTransfer();
            generalData=generalBusiness.GetAllPosts();
            return View(generalData);
        }
    }
}