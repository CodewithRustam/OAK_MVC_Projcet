using Data_Access_Layer;
using Data_Transfer_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business_Access_Layer
{
    public class SocialMediaBusinessLayer
    {
        SocialMediaDataAccess dataAccess=new SocialMediaDataAccess();
        public bool AddSocialMediaBusiness(SocialMediaDataTransfer model)
        {
            SocialMedia socialMedia= new SocialMedia();
            socialMedia.Name= model.Name;
            socialMedia.Link= model.Link;
            socialMedia.ImagePath= model.ImagePath;
            socialMedia.AddDate = DateTime.Now;
            socialMedia.LastUpdateUserID= UserStatic.UserId;
            socialMedia.LastUpdateDate= DateTime.Now;

            int socialID=dataAccess.AddSocialMediaDataAccess(socialMedia);
            if (socialID > 0)
            {
                LogDataAceess.AddLogData(General.ProcessType.SocialAdd, General.TableName.Social, socialID);
                return true;
            }
            return false;
        }

        public SocialMediaDataTransfer GetSocialMediaByIdBusiness(int ID)
        {
            SocialMediaDataTransfer socialMediaData=new SocialMediaDataTransfer();
            SocialMedia socialMedia= dataAccess.GetSocialMediaByIdDataAccess(ID);
            if (socialMedia != null && socialMedia.SocialMediaID != 0)
            {
                socialMediaData.ID = socialMedia.SocialMediaID;
                socialMediaData.Name = socialMedia.Name;
                socialMediaData.ImagePath = socialMedia.ImagePath;
                socialMediaData.Link= socialMedia.Link;
                return socialMediaData;
            }
            return socialMediaData;
        }

        public List<SocialMediaDataTransfer> SocialMediaListBusiness()
        {
            List<SocialMediaDataTransfer> socialMediaDataTransfers= new List<SocialMediaDataTransfer>();
            List<SocialMedia> socialMediaList = dataAccess.SocialMediaDataAccessList();
            foreach (SocialMedia item in socialMediaList)
            {
                SocialMediaDataTransfer socialMedia = new SocialMediaDataTransfer();
                socialMedia.Name = item.Name;
                socialMedia.Link = item.Link;
                socialMedia.ImagePath = item.ImagePath;
                socialMedia.ID = item.SocialMediaID;

                socialMediaDataTransfers.Add(socialMedia);
            }
            return socialMediaDataTransfers;
        }

        public string UpdateSocialMediaByIdBusiness(SocialMediaDataTransfer socialMediaDataTransfer)
        {
            
            string imagePath=dataAccess.UpdateSocialMediaByIdBusiness(socialMediaDataTransfer);
            if (imagePath != null)
            {
                LogDataAceess.AddLogData(General.ProcessType.SocialUpdate, General.TableName.Social, socialMediaDataTransfer.ID);
                return imagePath;
            }
            return imagePath;
        }
    }
}
