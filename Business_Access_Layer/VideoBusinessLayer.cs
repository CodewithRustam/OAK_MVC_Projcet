using Data_Access_Layer;
using Data_Transfer_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Access_Layer
{
    public class VideoBusinessLayer
    {
        VideoDataAccess dataAccess=new VideoDataAccess();
        public bool AddVideoBusiness(VideoDataTransfer model)
        {
            Video video = new Video();
            video.VideoPath = model.VideoPath;
            video.OriginalVideoPath = model.OriginalVideoPath;
            video.Title = model.Title;
            video.AddDate = DateTime.Now;
            video.LastUpdateDate = DateTime.Now;
            video.LastUpdateUserID = UserStatic.UserId;
            int id=dataAccess.AddVideoDataAccess(video);
            throw new NotImplementedException();
        }
    }
}
