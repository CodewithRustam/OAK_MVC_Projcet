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
    public class AddressController : BaseController
    {
        AddressBusiness addressBusiness = new AddressBusiness();
        public ActionResult AddAddress()
        {
            AddreesDataTransfer addreesDataTransfer = new AddreesDataTransfer();
            return View(addreesDataTransfer);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddAddress(AddreesDataTransfer model)
        {           
            if (ModelState.IsValid)
            {

                bool isAdded = addressBusiness.AddAddressBusiness(model);
                if (isAdded)
                {
                    ViewBag.ProcessState = General.Message.AddSuccess;
                    model = new AddreesDataTransfer();
                    ModelState.Clear();
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

        public ActionResult AddressList()
        {
            List<AddreesDataTransfer> addressList = addressBusiness.AddressListBusiness();
            if (addressList.Count > 0)
            { return View(addressList); }
            return View(addressList);
        }
        public ActionResult UpdateAddress(int ID)
        {
            AddreesDataTransfer addressDetails = new AddreesDataTransfer();
            if (ID > 0)
            {
                addressDetails = addressBusiness.GetAddressByIdBusiness(ID);
                return View(addressDetails);
            }
            return View(addressDetails);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateAddress(AddreesDataTransfer model)
        {
            AddreesDataTransfer userDetails = new AddreesDataTransfer();
            if (!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Message.EmptyArea;
            }
            else
            {
               bool isUpdate = addressBusiness.UpdateAddressByIdBusiness(model);
                if (isUpdate)
                {
                    ViewBag.ProcessState = General.Message.UpdateSuccess;
                }
                else
                {
                    ViewBag.ProcessState = General.Message.GeneralError;
                }
               
               return View(userDetails);
            }

            return View(userDetails);
        }
        [HttpPost]
        public ActionResult DeleteAddress(int ID)
        {
            addressBusiness.DeleteAddressBusiness(ID);
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}