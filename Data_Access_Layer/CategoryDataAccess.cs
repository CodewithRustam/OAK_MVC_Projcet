using Data_Transfer_Layer;
using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Data_Access_Layer
{
    public class CategoryDataAccess:PostDataContext
    {
        public static IEnumerable<SelectListItem> GetCategoriesForDropdown()
        {
            IEnumerable<SelectListItem> categoryList = dbcontext.Categories.OrderByDescending(x => x.AddDate).Select
            (x => new SelectListItem()
            {
                Text = x.CategoryName,
                Value =SqlFunctions.StringConvert((double)x.CategoryID)
            }).ToList();
            return categoryList;
        }

        public int AddCategoryDataAccess(Category category)
        {
            Category addCategory=dbcontext.Categories.Add(category);
            dbcontext.SaveChanges();
            if (addCategory.CategoryID > 0)
            {
                return addCategory.CategoryID;
            }
            else
            {
                return 0;
            }
        }
        public List<Category> CategoryListDataAccess()
        {
            List <Category > categories=dbcontext.Categories.Where(x=>x.isDeleted==false).OrderByDescending(x=>x.AddDate).ToList();
            if(categories.Count > 0)
            {
                return categories;
            }
            else
            {
                return null;
            }
        }

        public List<Post> DeleteCategoryDataAccess(int ID)
        {
            Category category= dbcontext.Categories.FirstOrDefault(x=>x.CategoryID == ID);
            if (category != null)
            {
                category.isDeleted = true;
                category.DeletedDate = DateTime.Now;
                category.LastUpdateDate = DateTime.Now;
                category.LastUpdateUserID = UserStatic.UserId;
                dbcontext.SaveChanges();
            }
            List<Post> postList= dbcontext.Posts.Where(x=>x.isDeleted==false && x.CategoryId==ID).ToList();
            if (postList.Count > 0)
            {
                return postList;
            }
            return postList;
        }

        public int UpdateCategoryDataAccess(CategoryDataTransfer model)
        {
            Category category = dbcontext.Categories.Where(x=>x.CategoryID==model.CategoryID).FirstOrDefault();
            if(category != null&&category.CategoryID!=0)
            {
                category.CategoryName=model.CategoryName;
                category.LastUpdateDate=DateTime.Now;
                category.LastUpdateUserID=UserStatic.UserId;
                dbcontext.SaveChanges();
                return category.CategoryID;
            }
            return category.CategoryID;
        }
    }
}
