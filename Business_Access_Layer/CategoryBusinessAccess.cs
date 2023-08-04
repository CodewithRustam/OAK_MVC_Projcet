using Data_Access_Layer;
using Data_Transfer_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Business_Access_Layer
{
    public class CategoryBusinessAccess
    {
        CategoryDataAccess categoryDataAccess=new CategoryDataAccess();

        public static IEnumerable<SelectListItem> GetCategories()
        {
            return CategoryDataAccess.GetCategoriesForDropdown();
        }

        public bool AddCategoryBusiness(CategoryDataTransfer model)
        {
            Category category = new Category();
            category.CategoryName= model.CategoryName;
            category.AddDate = DateTime.Now;
            category.LastUpdateUserID =UserStatic.UserId;
            int id=categoryDataAccess.AddCategoryDataAccess(category);
            if (id > 0)
            {
                LogDataAceess.AddLogData(General.ProcessType.CategoryAdd, General.TableName.Category, id);
                return true;
            }
            else { return false; }
        }

        public List<CategoryDataTransfer> CategoryListBusiness()
        {
            List<CategoryDataTransfer> categories= new List<CategoryDataTransfer>();
            var categoryData=categoryDataAccess.CategoryListDataAccess();
            if(categoryData!=null&&categoryData.Count > 0)
            {
                foreach (var category in categoryData)
                {
                    CategoryDataTransfer categorydata = new CategoryDataTransfer();
                    categorydata.CategoryID = category.CategoryID;
                    categorydata.CategoryName = category.CategoryName;
                    categories.Add(categorydata);
                }
            }
           return categories;
        }
        public bool UpdateCategoryBusiness(CategoryDataTransfer model)
        {
            int id = categoryDataAccess.UpdateCategoryDataAccess(model);
            if (id > 0)
            {
                LogDataAceess.AddLogData(General.ProcessType.CategoryUpdate, General.TableName.Category, id);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
