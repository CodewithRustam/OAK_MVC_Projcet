using Data_Transfer_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class VideoDataAccess:PostDataContext
    {
        public int AddVideoDataAccess(Video model)
        {
            dbcontext.Videos.Add(model);
            throw new NotImplementedException();
        }
    }
}
