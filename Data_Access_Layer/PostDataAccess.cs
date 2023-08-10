using Data_Transfer_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class PostDataAccess : PostDataContext
    {
        public int AddImageDataAccess(PostImage item)
        {
            PostImage postImage=dbcontext.PostImages.Add(item);
            dbcontext.SaveChanges();
            if(postImage != null&&postImage.PostImageID>0)
            {
                return postImage.PostImageID;
            }
            return 0;
        }

        public int AddPostDataAccess(Post post)
        {
            try
            {
                Post addPost = dbcontext.Posts.Add(post);
                dbcontext.SaveChanges();
                if (addPost != null && addPost.PostID > 0)
                {
                    return addPost.PostID;
                }
                return 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int AddTagDataAccess(PostTag item)
        {
            PostTag addTag=dbcontext.PostTags.Add(item);
            dbcontext.SaveChanges();
            if(addTag!=null && addTag.PostTagID > 0)
            {
                return addTag.PostTagID;
            }
            return 0;
        }

        public List<PostImageDataTransfer> DeletePostDataAccess(int ID)
        {
            Post post = dbcontext.Posts.FirstOrDefault(x => x.PostID == ID);
            post.isDeleted = true;
            post.DeletedDate = DateTime.Now;
            post.LastUpdateDate = DateTime.Now;
            post.LastUpdateUserID = UserStatic.UserId;
            dbcontext.SaveChanges();

            List<PostImage> imageList = dbcontext.PostImages.Where(x=>x.PostID== ID).ToList();
            List<PostImageDataTransfer> postImageDataTransfer = new List<PostImageDataTransfer>();

            foreach(var item in imageList)
            {
                PostImageDataTransfer postImageData=new PostImageDataTransfer();
                postImageData.ImagePath= item.ImagePath;
                item.isDeleted = true;
                item.DeletedDate = DateTime.Now;
                item.LastUpdateDate = DateTime.Now;
                item.LastUpdateUserID = UserStatic.UserId;
                postImageDataTransfer.Add(postImageData);
            }
            dbcontext.SaveChanges();
            return postImageDataTransfer;
        }

        public string DeletePostImageDataAccess(int ID)
        {
            PostImage postImage = dbcontext.PostImages.FirstOrDefault(x => x.PostImageID == ID);
            string imagePath = postImage.ImagePath;
            postImage.isDeleted = true;
            postImage.DeletedDate = DateTime.Now;
            postImage.LastUpdateDate = DateTime.Now;
            postImage.LastUpdateUserID = UserStatic.UserId;
            dbcontext.SaveChanges();

            if (imagePath != null)
            {
                return imagePath;
            }
            return imagePath;
        }

        public void DeleteTags(int postID)
        {
            List<PostTag> postTags= dbcontext.PostTags.Where(x => x.PostID == postID).ToList();
            foreach(var item in postTags)
            {
                item.isDeleted= true;
                item.DeletedDate= DateTime.Now;
                item.LastUpdateDate= DateTime.Now;
                item.LastUpdateUserID= UserStatic.UserId;
            }
            dbcontext.SaveChanges();
        }

        public List<PostDataTransfer> GetHotNews()
        {
            var postList = dbcontext.Posts.Where(x => x.isDeleted == false).Join(dbcontext.Categories, p => p.CategoryId, c => c.CategoryID, (posts, categories) => new
            {
                ID = posts.PostID,
                Title = posts.Title,
                CategoryName = categories.CategoryName,
                AddDate = posts.AddDate,
                seoLink=posts.SeoLink
            }).OrderByDescending(x => x.AddDate).Take(8).ToList();

            List<PostDataTransfer> postDataTransfers = new List<PostDataTransfer>();
            foreach (var item in postList)
            {
                PostDataTransfer post = new PostDataTransfer();
                post.Title = item.Title;
                post.ID = (int)item.ID;
                post.CategoryName = item.CategoryName;
                post.AddDate = (DateTime)item.AddDate;
                post.SeoLink = item.seoLink;
                postDataTransfers.Add(post);
            }
            return postDataTransfers;
        }

        public Post GetPostbyIDDataAccess(int iD)
        {
            Post postData=dbcontext.Posts.FirstOrDefault(x=>x.PostID== iD);
            if(postData!=null && postData.PostID > 0)
            {
                return postData;
            }
            return postData;
        }

        public List<PostImage> GetpostImagesbyIDDataAccess(int iD)
        {
            List<PostImage> listImages=dbcontext.PostImages.Where(x=>x.isDeleted==false && x.PostID== iD).ToList();
            if (listImages.Count > 0)
            {
                return listImages;
            }
            return listImages;
        }

        public List<PostTag> GetpostTagsbyIDDataAccess(int iD)
        {
            List<PostTag> listTags = dbcontext.PostTags.Where(x => x.PostID == iD).ToList();
            if (listTags.Count > 0)
            {
                return listTags;
            }
            return listTags;
        }

        public List<PostDataTransfer> PostListDataAccess()
        {
            var postList=dbcontext.Posts.Where(x=>x.isDeleted==false).Join(dbcontext.Categories,p=>p.CategoryId,c=>c.CategoryID,(posts, categories) =>new
                         {
                             ID=posts.PostID,
                             Title=posts.Title,
                             CategoryName=categories.CategoryName,
                             AddDate=posts.AddDate
                         }).OrderByDescending(x=>x.AddDate).ToList();

            List<PostDataTransfer> postDataTransfers= new List<PostDataTransfer>();
            foreach(var item in postList)
            {
                PostDataTransfer post=new PostDataTransfer();
                post.Title= item.Title;
                post.ID= (int)item.ID;
                post.CategoryName= item.CategoryName;
                post.AddDate= (DateTime)item.AddDate;   
                postDataTransfers.Add(post);
            }
            return postDataTransfers;
        }

        public int UpdatePostDataAccess(PostDataTransfer model)
        {
            Post post=dbcontext.Posts.FirstOrDefault(x=>x.PostID==model.ID);
            if (post != null && post.PostID > 0)
            {
                post.Title = model.Title;
                post.Area1 = model.Area1;
                post.Area2 = model.Area2;
                post.Area3 = model.Area3;
                post.CategoryId = model.CategoryID;
                post.LanguageName = model.Language;
                post.LastUpdateDate = DateTime.Now;
                post.LastUpdateUserID = UserStatic.UserId;
                post.Notification = model.Notification;
                post.PostContent = model.PostContent;
                post.SeoLink = model.SeoLink;
                post.ShortContent = model.ShortContent;
                post.Slider = model.Slider;
                dbcontext.SaveChanges();
                return post.PostID;
            }
            return post.PostID;
        }
    }
}
