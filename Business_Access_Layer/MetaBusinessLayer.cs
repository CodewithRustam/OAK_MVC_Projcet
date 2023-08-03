using Data_Access_Layer;
using Data_Transfer_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Access_Layer
{
    public class MetaBusinessLayer
    {
        MetaDataAccess metaDataAccess = new MetaDataAccess();
        public bool AddMetaBusiness(MetaDataTransfer metaDataTransfer)
        {
            bool result;
            try
            {
                int metaId = metaDataAccess.AddMetaDataAccess(metaDataTransfer);
                if (metaId > 0)
                {
                    LogDataAceess.AddLogData(General.ProcessType.MetaAdd, General.TableName.Meta, metaId);
                    return result=true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        public MetaDataTransfer GetMetaDataWithIDBusiness(int ID)
        {
            MetaDataTransfer metaDataTransfer = new MetaDataTransfer();
            try
            {
                Meta metadatabyID = metaDataAccess.GetMetaDataWithIDDataAccess(ID);
                if (metadatabyID != null)
                {
                    metaDataTransfer.MetaID= metadatabyID.MetaID;
                    metaDataTransfer.Name= metadatabyID.Name;
                    metaDataTransfer.MetaContent= metadatabyID.MetaContent;
                    return metaDataTransfer;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MetaDataTransfer> GetMetaList()
        {
            try
            {
                List<MetaDataTransfer> metadatabusiness= metaDataAccess.GetMetaDataAccess();
                if(metadatabusiness != null )
                {
                    return metadatabusiness;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateMetaDataBusiness(MetaDataTransfer metadataUpdate)
        {
            try
            {
                bool isUpdated = metaDataAccess.UpdateMetaDataAccess(metadataUpdate);
                if(isUpdated)
                {
                    LogDataAceess.AddLogData(General.ProcessType.MetaUpdate, General.TableName.Meta, metadataUpdate.MetaID);
                    return true;
                }
                else
                {
                    return false;
                }
              
            }
            catch(Exception ex )           
            {
                throw ex;
            }
        }
    }
}
