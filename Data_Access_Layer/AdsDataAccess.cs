using Data_Transfer_Layer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class AdsDataAccess: PostDataContext
    {
        public int AddAdsDataLayer(Ad ads)
        {
            try
            {
                Ad addAd = dbcontext.Ads.Add(ads);
                dbcontext.SaveChanges();

                if (addAd.AdsID > 0)
                {
                    return addAd.AdsID;
                }
                else
                {
                    return addAd.AdsID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Ad> adsListDataAccess()
        {
            try
            {
                List<Ad> listAds = dbcontext.Ads.Where(x=>x.isDeleted==false).OrderByDescending(x => x.AddDate).ToList();
                if (listAds.Count > 0)
                {
                    return listAds;
                }
                return listAds;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string DeleteAdsDataAccess(int iD)
        {
            Ad ads = dbcontext.Ads.FirstOrDefault(x => x.AdsID == iD);
            string imagePath = ads.ImagePath;
            ads.isDeleted = true;
            ads.DeletedDate = DateTime.Now;
            ads.LastUpdateDate = DateTime.Now;
            ads.LastUpdateUserID = UserStatic.UserId;
            dbcontext.SaveChanges();

            if (imagePath != null)
            {
                return imagePath;
            }
            return imagePath;
        }

        public Ad GetAdsByIdDataAccess(int iD)
        {
            try
            {
                Ad adsData = dbcontext.Ads.Where(x => x.AdsID == iD).FirstOrDefault();
                if (adsData != null && adsData.AdsID != 0)
                {
                    return adsData;
                }
                return adsData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string UpdateAdsByIdBusiness(AdsDataTranfer model)
        {
            try
            {
                Ad adsData = dbcontext.Ads.FirstOrDefault(x => x.AdsID == model.AdsID);
                string oldImagePath = adsData.ImagePath;
                adsData.Name = model.Name;
                adsData.Link = model.Link;               
                adsData.Size = model.ImageSize;               
                if (model.ImagePath != null)
                {
                    adsData.ImagePath = model.ImagePath;
                }
                adsData.LastUpdateDate = DateTime.Now;
                adsData.LastUpdateUserID = UserStatic.UserId;
                dbcontext.SaveChanges();
                if (oldImagePath != null)
                {
                    return oldImagePath;
                }
                return oldImagePath;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
