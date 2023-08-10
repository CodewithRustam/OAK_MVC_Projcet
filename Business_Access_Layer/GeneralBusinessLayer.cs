using Data_Access_Layer;
using Data_Transfer_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Access_Layer
{
    public class GeneralBusinessLayer
    {
        GeneralDataAccess dataAccess=new GeneralDataAccess();
        public GeneralDataTransfer GetAllPosts()
        {
            GeneralDataTransfer generalData=new GeneralDataTransfer();
            generalData.SliderPost = dataAccess.GetSliderPosts();
            generalData.BreakingPost = dataAccess.GetBreakingPost();
            generalData.PopularPost = dataAccess.GetPopularPost();
            generalData.MostViewedPost = dataAccess.GetMostViewedPosts();
            return generalData;
        }
    }
}
