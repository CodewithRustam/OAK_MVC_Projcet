using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Access_Layer
{
    public class LogDataBusiness
    {
        public static void AddLogDataBusiness(int ProcessType, string TableName, int ProcessID)
        {
            LogDataAceess.AddLogData(ProcessType, TableName, ProcessID);
        }
    }
}
