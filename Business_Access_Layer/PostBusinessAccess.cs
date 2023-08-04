using Data_Access_Layer;
using Data_Transfer_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Business_Access_Layer
{
    public class PostBusinessAccess
    {
        PostDataAccess postDataAccess= new PostDataAccess();
        public bool AddPostBusiness(PostDataTransfer model)
        {
            Post post = new Post();
            post.Title=model.Title;
            post.PostContent=model.PostContent;
            post.ShortContent=model.ShortContent;
            post.Slider=model.Slider;
            post.Area1=model.Area1;
            post.Area2=model.Area2;
            post.Area3=model.Area3;
            post.Notification=model.Notification;
            post.CategoryId=model.CategoryID;
            post.SeoLink=SeoLink.GenerateUrl(model.Title);
            post.LanguageName = model.Language;
            post.AddDate = DateTime.Now;
            post.LastUpdateUserID = UserStatic.UserId;
            post.LastUpdateDate = DateTime.Now;
            int Id=postDataAccess.AddPostDataAccess(post);
            if (Id > 0)
            {
                LogDataAceess.AddLogData(General.ProcessType.PostAdd,General.TableName.Post,Id);
            }
            int savePostID=SavePostImage(model.PostImages, Id);
            int addTagID=AddTag(model.TagText, Id);
            if(savePostID>0 && addTagID>0 && Id > 0)
            {
                return true;
            }
            return false;
        }

        private int AddTag(string tagText, int id)
        {
            int tagID = 0;
            string[] tags;
            tags=tagText.Split(',');    
            List<PostTag> tagList= new List<PostTag>();
            foreach(var item in tags)
            {
                PostTag tag=new PostTag();
                tag.PostID=id;
                tag.TagContent=item;
                tag.AddDate=DateTime.Now;
                tag.LastUpdateDate= DateTime.Now;
                tag.LastUpdateUserID=UserStatic.UserId;
                tagList.Add(tag);
            }
            foreach(var item in tagList)
            {
                tagID = postDataAccess.AddTagDataAccess(item);
                if (tagID > 0)
                {
                    LogDataAceess.AddLogData(General.ProcessType.TagAdd, General.TableName.Tag, tagID);
                    return tagID;
                }
                return tagID;
            }
            return tagID;
        }

        private int SavePostImage(List<PostImageDataTransfer> postImages, int id)
        {
            int imageID = 0;
            List<PostImage> listImage = new List<PostImage>();
            foreach (var item in postImages)
            {
                PostImage image = new PostImage();
                image.PostID = id;
                image.ImagePath = item.ImagePath;
                image.AddDate = DateTime.Now;
                image.LastUpdateDate = DateTime.Now;
                image.LastUpdateUserID = UserStatic.UserId;
                listImage.Add(image);
            }
            foreach (var item in listImage)
            {
                imageID = postDataAccess.AddImageDataAccess(item);
                if (imageID > 0)
                {
                    LogDataAceess.AddLogData(General.ProcessType.ImageAdd, General.TableName.Image, imageID);
                    return imageID;
                }
                return imageID;
            }
            return imageID;
        }

        public List<PostDataTransfer> PostListBusiness()
        {
            List<PostDataTransfer>  postList=postDataAccess.PostListDataAccess();
            if(postList.Count > 0)
            {
                return postList;
            }
            return postList;
        }

        public bool UpdatePostBusiness(PostImageDataTransfer model)
        {
            throw new NotImplementedException();
        }

        public PostDataTransfer GetPostbyID(int iD)
        {
            PostDataTransfer postDataTransfer = new PostDataTransfer();
            Post post=postDataAccess.GetPostbyIDDataAccess(iD);
            if(post != null && post.PostID>0)
            {
                postDataTransfer.ID = post.PostID;
                postDataTransfer.Title = post.Title;
                postDataTransfer.ShortContent = post.ShortContent;
                postDataTransfer.PostContent = post.PostContent;
                postDataTransfer.Language = post.LanguageName;
                postDataTransfer.Notification = (bool)post.Notification;
                postDataTransfer.SeoLink = post.SeoLink;
                postDataTransfer.Area1 = (bool)post.Area1;
                postDataTransfer.Area2 = (bool)post.Area2;
                postDataTransfer.Area3 = (bool)post.Area3;
                postDataTransfer.Slider = (bool)post.Slider;
                postDataTransfer.CategoryID = (int)post.CategoryId;
            }
            List<PostImageDataTransfer> postimages= new List<PostImageDataTransfer>();
            List<PostImage> postImage = postDataAccess.GetpostImagesbyIDDataAccess(iD);
            foreach(var item  in postImage)
            {
                PostImageDataTransfer postImageDataTransfer= new PostImageDataTransfer();
                postImageDataTransfer.ID = item.PostImageID;
                postImageDataTransfer.ImagePath = item.ImagePath;
                postimages.Add(postImageDataTransfer);
            }
            postDataTransfer.PostImages = postimages;
            List<PostTag> postTag = postDataAccess.GetpostTagsbyIDDataAccess(iD);
            string tagvalue = string.Empty;
            foreach(var item in postTag)
            {
                tagvalue += item.TagContent;
                tagvalue += ",";
            }
            postDataTransfer.TagText = tagvalue;
            return postDataTransfer;
        }

        public bool UpdatePostBusinessAccess(PostDataTransfer model)
        {
             model.SeoLink=SeoLink.GenerateUrl(model.Title);
             int ID=postDataAccess.UpdatePostDataAccess(model);
            if(ID > 0)
            {
                LogDataAceess.AddLogData(General.ProcessType.PostUpdate, General.TableName.Post, ID);
            }
            if (model.PostImages != null)
            {
                SavePostImage(model.PostImages, ID);
            }
            postDataAccess.DeleteTags(ID);
            AddTag(model.TagText,ID);

            return true;
        }
    }
}
