using Data_Transfer_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class SocialMediaDataAccess : PostDataContext
    {
        public int AddSocialMediaDataAccess(SocialMedia socialMedia)
        {
            try
            {
                SocialMedia socialmedia = dbcontext.SocialMedias.Add(socialMedia);
                dbcontext.SaveChanges();
                if (socialmedia.SocialMediaID > 0)
                {
                    return socialmedia.SocialMediaID;
                }
                return socialmedia.SocialMediaID;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string DeleteSocialMediaDataAccess(int ID)
        {
            SocialMedia socialMedia = dbcontext.SocialMedias.FirstOrDefault(x => x.SocialMediaID == ID);
            string imagePath = socialMedia.ImagePath;
            socialMedia.isDeleted = true;
            socialMedia.DeletedDate = DateTime.Now;
            socialMedia.LastUpdateDate = DateTime.Now;
            socialMedia.LastUpdateUserID = UserStatic.UserId;
            dbcontext.SaveChanges();

            if (imagePath != null)
            {
                return imagePath;
            }
            return imagePath;
        }

        public SocialMedia GetSocialMediaByIdDataAccess(int ID)
        {
            try
            {
                SocialMedia socialMedia =new SocialMedia();
                if (ID > 0)
                {
                    socialMedia= dbcontext.SocialMedias.Where(x=>x.SocialMediaID== ID).FirstOrDefault();
                    return socialMedia;
                }
                return socialMedia;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<SocialMedia> SocialMediaDataAccessList()
        {
            try
            {
                List<SocialMedia> listSocial = dbcontext.SocialMedias.Where(x=>x.isDeleted==false).OrderBy(x => x.AddDate).ToList();
                if (listSocial.Count > 0)
                {
                    return listSocial;
                }
                return listSocial;
            }
           catch(Exception ex)
            {
                throw ex;
            }
        }

        public string UpdateSocialMediaByIdBusiness(SocialMediaDataTransfer socialMediaData)
        {
            try
            {
                SocialMedia updateData = dbcontext.SocialMedias.FirstOrDefault(x => x.SocialMediaID == socialMediaData.ID);
                string oldImagePath=updateData.ImagePath;
                updateData.SocialMediaID = socialMediaData.ID;
                updateData.Name = socialMediaData.Name;
                updateData.Link = socialMediaData.Link;
                if (socialMediaData.ImagePath != null)
                {
                    updateData.ImagePath = socialMediaData.ImagePath;
                }
                updateData.LastUpdateDate = DateTime.Now;
                updateData.LastUpdateUserID = UserStatic.UserId;
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
