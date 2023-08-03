using Data_Access_Layer;
using Data_Transfer_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Access_Layer
{
    public class AdsBusinessLayer
    {
        AdsDataAccess adsDataAccess=new AdsDataAccess();
        public bool AddAddsBusiness(AdsDataTranfer model)
        {
            Ad ads = new Ad();
            ads.Name = model.Name;
            ads.Link = model.Link;
            ads.Size = model.ImageSize;
            ads.ImagePath = model.ImagePath;
            ads.AddDate = DateTime.Now;
            ads.LastUpdateDate = DateTime.Now;
            ads.LastUpdateUserID = UserStatic.UserId;
          
            int adsID = adsDataAccess.AddAdsDataLayer(ads);
            if (adsID > 0)
            {
                LogDataAceess.AddLogData(General.ProcessType.AdsAdd, General.TableName.Ads, adsID);
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<AdsDataTranfer> AdsListBusiness()
        {
            List<AdsDataTranfer> adsList = new List<AdsDataTranfer>();

            List<Ad> ads = adsDataAccess.adsListDataAccess();

            foreach (Ad ad in ads)
            {
                AdsDataTranfer adsDataList = new AdsDataTranfer();
                adsDataList.Name = ad.Name;
                adsDataList.ImagePath = ad.ImagePath;
                adsDataList.Link = ad.Link;
                adsDataList.AdsID = ad.AdsID;

                adsList.Add(adsDataList);
            }
            return adsList;
        }

        public AdsDataTranfer GetAdByIdBusiness(int ID)
        {
            AdsDataTranfer adsData = new AdsDataTranfer();
            Ad ads = adsDataAccess.GetAdsByIdDataAccess(ID);
            if (ads != null && ads.AdsID != 0)
            {
                adsData.AdsID = ads.AdsID;
                adsData.Name = ads.Name;
                adsData.Link = ads.Link;
                adsData.ImagePath = ads.ImagePath;
                adsData.ImageSize = ads.Size;
                
                return adsData;
            }
            return adsData;
        }

        public string UpdateAdsByIdBusiness(AdsDataTranfer model)
        {
            string imagePath = adsDataAccess.UpdateAdsByIdBusiness(model);
            if (imagePath != null)
            {
                LogDataAceess.AddLogData(General.ProcessType.AdsUpdate, General.TableName.Ads, model.AdsID);
                return imagePath;
            }
            return imagePath;
        }
    }
}
