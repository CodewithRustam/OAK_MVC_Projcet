using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Transfer_Layer
{
    public class HomeLayoutDataTransfer
    {
        public List<CategoryDataTransfer> categories { get; set; }
        public SocialMediaDataTransfer Facebook { get; set; }
        public SocialMediaDataTransfer Twitter { get; set; }
        public SocialMediaDataTransfer Instagram { get; set; }
        public SocialMediaDataTransfer Youtube { get; set; }
        public SocialMediaDataTransfer Linkedin { get; set; }
        public SocialMediaDataTransfer GooglePlus { get; set; }
        public FavDataTransfer FavDataTransfer{ get; set; }
        public List<MetaDataTransfer> MetaList { get; set; }
        public AddreesDataTransfer Address { get; set; }
        public List<PostDataTransfer> HotNews { get; set; }
    }
}
