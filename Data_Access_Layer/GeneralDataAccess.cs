using Data_Transfer_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class GeneralDataAccess : PostDataContext
    {
        public List<PostDataTransfer> GetSliderPosts()
        {
            List<PostDataTransfer> allPosts=new List<PostDataTransfer>();
            var postList = dbcontext.Posts.Where(x => x.Slider == true && x.isDeleted==false).Join(dbcontext.Categories, p => p.CategoryId, c => c.CategoryID, (posts, categories) => new
            {
                PostID = posts.PostID,
                Title = posts.Title,
                CategoryName = categories.CategoryName,
                seoLink=posts.SeoLink,
                viewCount=posts.ViewCount,
                AddDate = posts.AddDate,
            }).OrderByDescending(x => x.AddDate).Take(8).ToList();

            foreach(var item in postList)
            {
                PostDataTransfer postData=new PostDataTransfer();
                postData.ID=item.PostID; 
                postData.Title=item.Title;
                postData.CategoryName=item.CategoryName;
                postData.SeoLink = item.seoLink;
                postData.ViewCount = (int)item.viewCount;

                PostImage image=dbcontext.PostImages.FirstOrDefault(x=>x.isDeleted==false && x.PostID==item.PostID);
                if (image != null)
                {
                    postData.ImagePath = image.ImagePath;
                }
                
                postData.CommentCount=dbcontext.Comments.Where(x=>x.isDeleted==false && x.PostID==item.PostID && x.isApproved==true).Count();
                postData.AddDate = (DateTime)item.AddDate;

                allPosts.Add(postData);  
            }
            return allPosts;
        }

        public List<PostDataTransfer> GetBreakingPost()
        {
            List<PostDataTransfer> allPosts = new List<PostDataTransfer>();
            var postList = dbcontext.Posts.Where(x => x.Slider == true && x.isDeleted == false).Join(dbcontext.Categories, p => p.CategoryId, c => c.CategoryID, (posts, categories) => new
            {
                PostID = posts.PostID,
                Title = posts.Title,
                CategoryName = categories.CategoryName,
                seoLink = posts.SeoLink,
                viewCount = posts.ViewCount,
                AddDate = posts.AddDate,
            }).OrderByDescending(x => x.AddDate).Take(5).ToList();

            foreach (var item in postList)
            {
                PostDataTransfer postData = new PostDataTransfer();
                postData.ID = item.PostID;
                postData.Title = item.Title;
                postData.CategoryName = item.CategoryName;
                postData.SeoLink = item.seoLink;
                postData.ViewCount = (int)item.viewCount;

                PostImage image = dbcontext.PostImages.FirstOrDefault(x => x.isDeleted == false && x.PostID == item.PostID);
                if (image != null)
                {
                    postData.ImagePath = image.ImagePath;
                }

                postData.CommentCount = dbcontext.Comments.Where(x => x.isDeleted == false && x.PostID == item.PostID && x.isApproved == true).Count();
                postData.AddDate = (DateTime)item.AddDate;

                allPosts.Add(postData);
            }
            return allPosts;
        }

        public List<PostDataTransfer> GetPopularPost()
        {
            List<PostDataTransfer> allPosts = new List<PostDataTransfer>();
            var postList = dbcontext.Posts.Where(x => x.isDeleted == false && x.Area2==true).Join(dbcontext.Categories, p => p.CategoryId, c => c.CategoryID, (posts, categories) => new
            {
                PostID = posts.PostID,
                Title = posts.Title,
                CategoryName = categories.CategoryName,
                seoLink = posts.SeoLink,
                viewCount = posts.ViewCount,
                AddDate = posts.AddDate,
            }).OrderByDescending(x => x.AddDate).Take(5).ToList();

            foreach (var item in postList)
            {
                PostDataTransfer postData = new PostDataTransfer();
                postData.ID = item.PostID;
                postData.Title = item.Title;
                postData.CategoryName = item.CategoryName;
                postData.SeoLink = item.seoLink;
                postData.ViewCount = (int)item.viewCount;

                PostImage image = dbcontext.PostImages.FirstOrDefault(x => x.isDeleted == false && x.PostID == item.PostID);
                if (image != null)
                {
                    postData.ImagePath = image.ImagePath;
                }

                postData.CommentCount = dbcontext.Comments.Where(x => x.isDeleted == false && x.PostID == item.PostID && x.isApproved == true).Count();
                postData.AddDate = (DateTime)item.AddDate;

                allPosts.Add(postData);
            }
            return allPosts;
        }

        public List<PostDataTransfer> GetMostViewedPosts()
        {
            List<PostDataTransfer> allPosts = new List<PostDataTransfer>();
            var postList = dbcontext.Posts.Where(x => x.isDeleted == false).Join(dbcontext.Categories, p => p.CategoryId, c => c.CategoryID, (posts, categories) => new
            {
                PostID = posts.PostID,
                Title = posts.Title,
                CategoryName = categories.CategoryName,
                seoLink = posts.SeoLink,
                viewCount = posts.ViewCount,
                AddDate = posts.AddDate,
            }).OrderByDescending(x => x.viewCount).Take(5).ToList();

            foreach (var item in postList)
            {
                PostDataTransfer postData = new PostDataTransfer();
                postData.ID = item.PostID;
                postData.Title = item.Title;
                postData.CategoryName = item.CategoryName;
                postData.SeoLink = item.seoLink;
                postData.ViewCount = (int)item.viewCount;

                PostImage image = dbcontext.PostImages.FirstOrDefault(x => x.isDeleted == false && x.PostID == item.PostID);
                if (image != null)
                {
                    postData.ImagePath = image.ImagePath;
                }

                postData.CommentCount = dbcontext.Comments.Where(x => x.isDeleted == false && x.PostID == item.PostID && x.isApproved == true).Count();
                postData.AddDate = (DateTime)item.AddDate;

                allPosts.Add(postData);
            }
            return allPosts;
        }
    }
}
