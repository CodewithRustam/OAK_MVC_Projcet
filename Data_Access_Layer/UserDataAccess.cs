using Data_Transfer_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class UserDataAccess:PostDataContext
    {
        public UserDataTransfer LoginByUsernamePassword(UserDataTransfer userDataTransfer)
        {
            UserDataTransfer model= new UserDataTransfer();
            T_User user= dbcontext.T_User.Where(x=>x.Username== userDataTransfer.Username && x.Password==userDataTransfer.Password).FirstOrDefault();

            if(user!=null&&user.T_UserID!=0)
            {
                model.Id= user.T_UserID;
                model.Username= user.Username;
                model.Name= user.NameSurname;
                model.Imagepath= user.ImagePath;
                model.isAdmin= user.isAdmin;
            }
            
            return model;
        }
        public int AddUserDataLayer(T_User user)
        {
            try
            {
                T_User addUser = dbcontext.T_User.Add(user);
                dbcontext.SaveChanges();

                if (addUser.T_UserID > 0)
                {
                    return addUser.T_UserID;
                }
                else
                {
                    return addUser.T_UserID;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public List<T_User> UseListDataAccess()
        {
            try
            {
                List<T_User> listUsers = dbcontext.T_User.OrderBy(x => x.AddDate).ToList();
                if (listUsers.Count > 0)
                {
                    return listUsers;
                }
                return listUsers;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public T_User GetUserByIdDataAccess(int iD)
        {
            try
            {
                T_User userData = dbcontext.T_User.Where(x => x.T_UserID == iD).FirstOrDefault();
                if (userData != null && userData.T_UserID != 0)
                {
                    return userData;
                }
                return userData;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string UpdateUserByIdBusiness(UserDataTransfer model)
        {
            try
            {
                T_User updateData = dbcontext.T_User.FirstOrDefault(x => x.T_UserID == model.Id);
                string oldImagePath = updateData.ImagePath;
                updateData.NameSurname = model.Name;
                updateData.Email = model.Email;
                updateData.Username = model.Username;
                updateData.Password = model.Password;
                if (model.Imagepath != null)
                {
                    updateData.ImagePath = model.Imagepath;
                }
                updateData.LastUpdateDate = DateTime.Now;
                updateData.LastUpdateUserID = UserStatic.UserId;
                updateData.isAdmin = model.isAdmin;
                dbcontext.SaveChanges();
                if (oldImagePath != null)
                {
                    return oldImagePath;
                }
                return oldImagePath;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
