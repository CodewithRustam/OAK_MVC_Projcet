using Data_Transfer_Layer;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class FavDataAccess : PostDataContext
    {
        public FavLogoTitle GEtFavDataAccess()
        {
            FavLogoTitle favLogoTitle = dbcontext.FavLogoTitles.Where(x=>x.isDeleted==false).FirstOrDefault();
           if(favLogoTitle != null && favLogoTitle.FavLogoTitleID!=0)
           {
                return favLogoTitle;
           }
            return favLogoTitle;
        }

        public FavDataTransfer UpdateFavDataAccess(FavDataTransfer model)
        {
            try
            {
                FavDataTransfer favDataTransfer = new FavDataTransfer();
                FavLogoTitle fav=dbcontext.FavLogoTitles.FirstOrDefault();

                fav.Fav = model.Fav;
                fav.Logo = model.Logo;
                fav.Title= model.Title;
                fav.isDeleted = false;
                fav.LastUpdateDate = DateTime.Now;

                if (model.Logo != null)
                {
                    fav.Logo = model.Logo;
                }
                if(model.Fav!= null)
                {
                    fav.Fav = model.Fav;
                }
                dbcontext.SaveChanges();

                if(model.Fav!= null)
                {
                    favDataTransfer.FavID = fav.FavLogoTitleID;
                    favDataTransfer.Fav = fav.Fav;
                    favDataTransfer.Logo = fav.Logo;
                    favDataTransfer.Title = fav.Title;
                    
                }
                return favDataTransfer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
