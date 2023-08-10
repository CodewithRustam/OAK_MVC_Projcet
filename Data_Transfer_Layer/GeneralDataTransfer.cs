using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Transfer_Layer
{
    public class GeneralDataTransfer
    {
        public List<PostDataTransfer> SliderPost { get; set; }
        public List<PostDataTransfer> PopularPost { get; set; }
        public List<PostDataTransfer> MostViewedPost { get; set; }
        public List<PostDataTransfer> BreakingPost { get; set; }
        public List<VideoDataTransfer> Videos { get; set; }
        public List<AdsDataTranfer> AdsList { get; set; }
        public PostDataTransfer PostDetail { get; set; }
    }
}
