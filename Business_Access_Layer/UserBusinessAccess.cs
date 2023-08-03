using Data_Access_Layer;
using Data_Transfer_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Access_Layer
{
    public class UserBusinessAccess
    {
        UserDataAccess userDataAccess = new UserDataAccess();
        public UserDataTransfer LoginUserNamePassword(UserDataTransfer userDataTransfer)
        {
            UserDataTransfer userdata = null;
            if (userDataTransfer != null)
            {
                userdata = userDataAccess.LoginByUsernamePassword(userDataTransfer);
                return userdata;
            }
            return userdata;
        }
        public bool AddUserBusiness(UserDataTransfer userDataTransfer)
        {
            T_User user = new T_User();
            user.Username = userDataTransfer.Username;
            user.Password = userDataTransfer.Password;
            user.ImagePath = userDataTransfer.Imagepath;
            user.isAdmin = userDataTransfer.isAdmin;
            user.NameSurname = userDataTransfer.Name;
            user.Email = userDataTransfer.Email;
            user.AddDate = DateTime.Now;
            user.LastUpdateDate = DateTime.Now;
            user.LastUpdateUserID = UserStatic.UserId;

            int userID = userDataAccess.AddUserDataLayer(user);
            if (userID > 0)
            {
                LogDataAceess.AddLogData(General.ProcessType.UserAdd, General.TableName.User, userID);
                return true;
            }
            else
            {
                return false;
            }

        }

        public List<UserDataTransfer> UserListBusiness()
        {
            List<UserDataTransfer> userList = new List<UserDataTransfer>();

            List<T_User> users = userDataAccess.UseListDataAccess();

            foreach (T_User user in users)
            {
                UserDataTransfer userDataList = new UserDataTransfer();
                userDataList.Name = user.NameSurname;
                userDataList.Imagepath = user.ImagePath;
                userDataList.Username = user.Username;
                userDataList.Id = user.T_UserID;

                userList.Add(userDataList);
            }
            return userList;
        }

        public UserDataTransfer GetUserByIdBusiness(int ID)
        {

            UserDataTransfer userData = new UserDataTransfer();
            T_User user = userDataAccess.GetUserByIdDataAccess(ID);
            if (user != null && user.T_UserID != 0)
            {
                userData.Id = user.T_UserID;
                userData.Username = user.Username;
                userData.Password = user.Password;
                userData.Name = user.NameSurname;
                userData.isAdmin = user.isAdmin;
                userData.Email = user.Email;
                userData.Imagepath = user.ImagePath;
                return userData;
            }
            return userData;
        }

        public string UpdateUserByIdBusiness(UserDataTransfer model)
        {
            string imagePath = userDataAccess.UpdateUserByIdBusiness(model);
            if (imagePath != null)
            {
                LogDataAceess.AddLogData(General.ProcessType.UserUpdate, General.TableName.User, model.Id);
                return imagePath;
            }
            return imagePath;
        }
    }
}
