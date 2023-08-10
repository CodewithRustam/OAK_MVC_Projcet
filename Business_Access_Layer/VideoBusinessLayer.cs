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
            if (id > 0)
            {
                LogDataAceess.AddLogData(General.ProcessType.VideoAdd, General.TableName.Video, id);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DeleteVideoBusiness(int ID)
        {
            dataAccess.DeleteVideoDataAccess(ID);
            LogDataAceess.AddLogData(General.ProcessType.VideoDelete, General.TableName.Video, ID);
        }

        public bool UpdateVideoBusiness(VideoDataTransfer model)
        {
            int id = dataAccess.UpdateVideoDataAccess(model);
            if (id > 0)
            {
                LogDataAceess.AddLogData(General.ProcessType.VideoUpdate, General.TableName.Video, id);
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<VideoDataTransfer> VideoListBusiness()
        {
            List<VideoDataTransfer> videoDatas= new List<VideoDataTransfer>();
            var videoList = dataAccess.VideoListDataAccess();
            foreach (var video in videoList)
            {
                VideoDataTransfer videoData=new VideoDataTransfer();
                videoData.Title= video.Title;
                videoData.OriginalVideoPath= video.OriginalVideoPath;
                videoData.VideoPath= video.VideoPath;
                videoData.VideoID= video.VideoID;
                videoData.AddDate= video.AddDate;
                videoDatas.Add(videoData);
            }
            

            return videoDatas;
        }
    }
}
