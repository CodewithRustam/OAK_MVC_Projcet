using Data_Transfer_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class MetaDataAccess:PostDataContext
    {
        public int AddMetaDataAccess(MetaDataTransfer metaDataTransfer)
        {
            Meta meta = new Meta();
            try
            {
                meta.Name = metaDataTransfer.Name;
                meta.MetaContent = metaDataTransfer.MetaContent;
                meta.AddDate = DateTime.Now;
                meta.LastUpdateDate = DateTime.Now;
                meta.LastUpdateUserID = UserStatic.UserId;

                dbcontext.Metas.Add(meta);
                dbcontext.SaveChanges();
                return meta.MetaID;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<MetaDataTransfer> GetMetaDataAccess()
        {
            List<MetaDataTransfer> metaDataTransferList = new List<MetaDataTransfer>();
            try
            {
                List<Meta> metaList = dbcontext.Metas.OrderByDescending(x=>x.AddDate).ToList();

                foreach(Meta meta in metaList)
                {
                    MetaDataTransfer metadata = new MetaDataTransfer();
                    metadata.MetaID = meta.MetaID;
                    metadata.Name = meta.Name;
                    metadata.MetaContent = meta.MetaContent;
                    metaDataTransferList.Add(metadata);
                }
                return metaDataTransferList;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            throw new NotImplementedException();
        }

        public Meta GetMetaDataWithIDDataAccess(int iD)
        {
            try
            {
                Meta metadataByID = dbcontext.Metas.Where(x => x.MetaID == iD).FirstOrDefault();
                if(metadataByID!=null)
                {
                    return metadataByID;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateMetaDataAccess(MetaDataTransfer updateMeta)
        {
            try
            {
                Meta meta=dbcontext.Metas.FirstOrDefault(x=>x.MetaID== updateMeta.MetaID);
                if(meta!=null)
                {
                    meta.Name = updateMeta.Name;
                    meta.MetaContent = updateMeta.MetaContent;
                    meta.LastUpdateDate = DateTime.Now;
                    meta.LastUpdateUserID = UserStatic.UserId;

                    return true;
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
    }
}
