using Data_Access_Layer;
using Data_Transfer_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business_Access_Layer
{
    public class AddressBusiness
    {
        AddressDataAccess addressDataAccess=new AddressDataAccess();
        public bool AddAddressBusiness(AddreesDataTransfer model)
        {
            Address address = new Address();
            address.Address1 = model.AddressContent;
            address.Email = model.Email;
            address.Phone = model.Phone;
            address.Phone2 = model.Phone2;
            address.Fax = model.Fax;
            address.MapPathLarge = model.LargeMapPath;
            address.MapPathSmall = model.SmallMapPath;
            address.AddDate = DateTime.Now;
            address.LastUpdateDate = DateTime.Now;
            address.LastUpdateUserID = UserStatic.UserId;

            int addressID = addressDataAccess.AddAddressDataAccess(address);
            if(addressID>0)
            {
                LogDataAceess.AddLogData(General.ProcessType.AddressAdd,General.TableName.Address,addressID);
                return true;
            }
            else { return false; }
        }

        public List<AddreesDataTransfer> AddressListBusiness()
        {
            List<AddreesDataTransfer> addressDataList= new List<AddreesDataTransfer>();
            List<Address> addressList= addressDataAccess.AddressListDataList();
            foreach(Address address in addressList)
            {
                AddreesDataTransfer addreesDataTransfer = new AddreesDataTransfer();
                addreesDataTransfer.AddressContent = address.Address1;
                addreesDataTransfer.AddressID = address.AddressID;
                addreesDataTransfer.Email = address.Email;
                addreesDataTransfer.Fax = address.Fax;
                addreesDataTransfer.LargeMapPath = address.MapPathLarge;
                addreesDataTransfer.Phone = address.Phone;
                addreesDataTransfer.Phone2 = address.Phone2;
                addreesDataTransfer.SmallMapPath = address.MapPathSmall;

                addressDataList.Add(addreesDataTransfer);
            }
            return addressDataList;
        }

        public AddreesDataTransfer GetAddressByIdBusiness(int iD)
        {
            AddreesDataTransfer addreesDataTransfer = new AddreesDataTransfer();
            Address address = addressDataAccess.GetAddressByIDDataList(iD);
            addreesDataTransfer.AddressContent = address.Address1;
            addreesDataTransfer.AddressID = address.AddressID;
            addreesDataTransfer.Email = address.Email;
            addreesDataTransfer.Fax = address.Fax;
            addreesDataTransfer.LargeMapPath = address.MapPathLarge;
            addreesDataTransfer.Phone = address.Phone;
            addreesDataTransfer.Phone2 = address.Phone2;
            addreesDataTransfer.SmallMapPath = address.MapPathSmall;

            return addreesDataTransfer;
        }

        public bool UpdateAddressByIdBusiness(AddreesDataTransfer model)
        {
            int addressId=addressDataAccess.UpdateAddressByIdDataAccess(model);
            if (addressId > 0)
            {
                LogDataAceess.AddLogData(General.ProcessType.AddressUpdate, General.TableName.Address, addressId);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
