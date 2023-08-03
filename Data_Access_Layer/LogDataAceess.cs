using Data_Transfer_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Data_Access_Layer
{
    public class LogDataAceess:PostDataContext
    {
        public static void AddLogData(int ProcessType, string TableName,int ProcessID)
        {
            Log_Table log =new Log_Table();
            log.UserID = UserStatic.UserId;
            log.ProcessType = ProcessType;  
            log.ProcessID = ProcessID;
            log.ProcessCategoryType = TableName;
            log.ProcessDate= DateTime.Now;
            log.IPAddress = HttpContext.Current.Request.UserHostAddress;

            dbcontext.Log_Table.Add(log);   
            dbcontext.SaveChanges();
        }
    }
}
