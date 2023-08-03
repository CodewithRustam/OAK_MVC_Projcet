using Data_Access_Layer;
using Data_Transfer_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Access_Layer
{
    public class FavBusinessAccess
    {
        FavDataAccess favDataAccess=new FavDataAccess();
        public FavDataTransfer GetFavBusiness()
        {
            FavDataTransfer favDataTransfer = new FavDataTransfer();
            FavLogoTitle favLogoTitle = favDataAccess.GEtFavDataAccess();
            favDataTransfer.FavID = favLogoTitle.FavLogoTitleID;
            favDataTransfer.Logo = favLogoTitle.Logo;
            favDataTransfer.Title = favLogoTitle.Title;
            favDataTransfer.Fav = favLogoTitle.Fav;
           return favDataTransfer;
        }

        public FavDataTransfer UpdateFavBusiness(FavDataTransfer model)
        {
            FavDataTransfer favDataTransfer= new FavDataTransfer();
            favDataTransfer = favDataAccess.UpdateFavDataAccess(model);
            if (favDataTransfer.FavID != 0)
            {
                LogDataAceess.AddLogData(General.ProcessType.IconUpdate, General.TableName.Icon, favDataTransfer.FavID);
                return favDataTransfer;
            }
            return favDataTransfer;
        }
    }
}
