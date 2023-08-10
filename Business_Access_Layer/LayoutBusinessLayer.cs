using Data_Access_Layer;
using Data_Transfer_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Access_Layer
{
    public class LayoutBusinessLayer
    {
        CategoryBusinessAccess categoryBusiness =new CategoryBusinessAccess();
        SocialMediaBusinessLayer socialMedia=new SocialMediaBusinessLayer();
        FavBusinessAccess favBusiness =new FavBusinessAccess();
        MetaBusinessLayer metaData=new MetaBusinessLayer();
        AddressBusiness addressBusiness=new AddressBusiness();
        PostDataAccess postDataAccess=new PostDataAccess();
        public HomeLayoutDataTransfer GetLayoutData()
        {
            HomeLayoutDataTransfer homeLayoutData = new HomeLayoutDataTransfer();
            
            //Categories
            homeLayoutData.categories = categoryBusiness.CategoryListBusiness();

            //Social Media
            List<SocialMediaDataTransfer> socialMediaList= new List<SocialMediaDataTransfer>();
            socialMediaList = socialMedia.SocialMediaListBusiness();
            homeLayoutData.Facebook = socialMediaList.First(x => x.Link.Contains("facebook"));
            homeLayoutData.Twitter = socialMediaList.First(x => x.Link.Contains("twitter"));
            homeLayoutData.Youtube = socialMediaList.First(x => x.Link.Contains("youtube"));
            homeLayoutData.Instagram = socialMediaList.First(x => x.Link.Contains("instagram"));
            homeLayoutData.Linkedin = socialMediaList.First(x => x.Link.Contains("linkedin"));

            //Fav Icon
            homeLayoutData.FavDataTransfer = favBusiness.GetFavBusiness();

            //Meta Data
            homeLayoutData.MetaList= metaData.GetMetaList();

            //Address
            List<AddreesDataTransfer> addressList = addressBusiness.AddressListBusiness();
            homeLayoutData.Address= addressList.First();

            //Post
            homeLayoutData.HotNews= postDataAccess.GetHotNews();

            return homeLayoutData;
        }
    }
}
