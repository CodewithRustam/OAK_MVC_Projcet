using Business_Access_Layer;
using Data_Transfer_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OAK_MVC_Projcet.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        CategoryBusinessAccess categoryBusinessLayer = new CategoryBusinessAccess();
        public ActionResult AddCategory()
        {
            CategoryDataTransfer categoryDataTransfer = new CategoryDataTransfer();
            return View(categoryDataTransfer);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddCategory(CategoryDataTransfer model)
        {
            if (ModelState.IsValid)
            {
                if (categoryBusinessLayer.AddCategoryBusiness(model))
                {
                    ViewBag.ProcessState = General.Message.AddSuccess;
                    ModelState.Clear();
                    model = new CategoryDataTransfer();
                }
                else
                {
                    ViewBag.ProcessState = General.Message.GeneralError;
                }

            }
            else
            {
                ViewBag.ProcessState = General.Message.EmptyArea;
            }

            return View(model);
        }
        public ActionResult CategoryList()
        {
            List<CategoryDataTransfer> categoryDataTransfer = categoryBusinessLayer.CategoryListBusiness();
            if (categoryDataTransfer != null&&categoryDataTransfer.Count>0)
            {
                return View(categoryDataTransfer);
            }
            return View(categoryDataTransfer);
        }
        public ActionResult UpdateCategory(int ID)
        {
            List<CategoryDataTransfer> categoryDataTransfers = categoryBusinessLayer.CategoryListBusiness();
            CategoryDataTransfer categoryList = categoryDataTransfers.Where(x => x.CategoryID == ID).FirstOrDefault();
            if (categoryList != null)
            {
                return View(categoryList);
            }
            return View(categoryList);
        }
        [HttpPost]
        public ActionResult UpdateCategory(CategoryDataTransfer model)
        {
            if (ModelState.IsValid)
            {
                if (categoryBusinessLayer.UpdateCategoryBusiness(model))
                {
                    ViewBag.ProcessState = General.Message.UpdateSuccess;
                }
                else
                {
                    ViewBag.ProcessState = General.Message.GeneralError;
                }
            }
            return View(model);
        }
    }
}