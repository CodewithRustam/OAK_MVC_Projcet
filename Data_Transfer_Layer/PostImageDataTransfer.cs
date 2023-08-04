using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Transfer_Layer
{
    public class PostImageDataTransfer
    {
        public int ID { get; set; }
        public int PostID { get; set; }
        public string ImagePath { get; set; }
    }
}
