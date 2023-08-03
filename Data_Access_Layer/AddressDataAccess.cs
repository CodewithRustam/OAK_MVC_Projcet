using Data_Transfer_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class AddressDataAccess:PostDataContext
    {
        public int AddAddressDataAccess(Address address)
        {
            Address addAddresss=dbcontext.Addresses.Add(address);

            dbcontext.SaveChanges();
            if (addAddresss.AddressID > 0)
            {
                return addAddresss.AddressID;
            }
            else
            {
                return addAddresss.AddressID;
            }
        }

        public List<Address> AddressListDataList()
        {
            try
            {
                List<Address> addressList= dbcontext.Addresses.OrderBy(a => a.AddDate).ToList();
                if(addressList.Count > 0)
                {
                    return addressList;
                }
                return addressList;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Address GetAddressByIDDataList(int ID)
        {
            Address addressDetails = dbcontext.Addresses.Where(x=>x.AddressID==ID).FirstOrDefault();
            return addressDetails;
        }

        public int UpdateAddressByIdDataAccess(AddreesDataTransfer model)
        {
            Address address= dbcontext.Addresses.Where(x => x.AddressID == model.AddressID).FirstOrDefault();
            if (address != null)
            {
                address.Address1 = model.AddressContent;
                address.Phone = model.Phone;
                address.Phone2 = model.Phone2;
                address.Fax = model.Fax;
                address.MapPathLarge = model.LargeMapPath;
                address.MapPathSmall = model.SmallMapPath;
                address.Email = model.Email;
                dbcontext.SaveChanges();
            }
            if(address.AddressID>0)
            {
                return address.AddressID;
            }
            else
            {
                return address.AddressID;
            }
        }
    }
}
