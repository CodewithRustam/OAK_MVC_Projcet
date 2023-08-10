using Business_Access_Layer;
using Data_Transfer_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OAK_MVC_Projcet.Areas.Admin.Controllers
{
    public class MetaController : BaseController
    {
        MetaBusinessLayer metaBusinessLayer = new MetaBusinessLayer();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddMeta()
        {
            MetaDataTransfer metaDataTransfer = new MetaDataTransfer();
            return View(metaDataTransfer);
        }
        [HttpPost]
        public ActionResult AddMeta(MetaDataTransfer metaModel)
        {
            if (ModelState.IsValid)
            {
                if (metaBusinessLayer.AddMetaBusiness(metaModel))
                {
                    ViewBag.processState = General.Message.AddSuccess;
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.processState = General.Message.GeneralError;
                }
            }
            else
            {
                ViewBag.processState = General.Message.EmptyArea;
            }
            MetaDataTransfer metaDataTransfer = new MetaDataTransfer();
            return View(metaDataTransfer);
        }

        public ActionResult MetaList()
        {
            List<MetaDataTransfer> metaDataModel = new List<MetaDataTransfer>();
            metaDataModel=metaBusinessLayer.GetMetaList();
            return View(metaDataModel);
        }
        public ActionResult UpdateMeta(int ID)
        {
            MetaDataTransfer metaDataModel = new MetaDataTransfer();
            metaDataModel = metaBusinessLayer.GetMetaDataWithIDBusiness(ID);
            return View(metaDataModel);
        }
        [HttpPost]
        public ActionResult UpdateMeta(MetaDataTransfer metadataUpdate)
        {
            if (ModelState.IsValid)
            {
                bool isUpdated = metaBusinessLayer.UpdateMetaDataBusiness(metadataUpdate);
                if (isUpdated)
                {
                    ViewBag.processState = General.Message.UpdateSuccess;
                }
                else
                {
                    ViewBag.processState = General.Message.GeneralError;
                }
            }
            else
            {
                ViewBag.processState = General.Message.EmptyArea;
                
            }
            return View(metadataUpdate);
        }
        [HttpPost]
        public JsonResult DeleteMeta(int ID)
        {
            metaBusinessLayer.DeleteMetaBusinessLayer(ID);
            return Json("success",JsonRequestBehavior.AllowGet);
        }
    }
}