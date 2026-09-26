//using Dapper;
//using RMCERPAPI.Models;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using Microsoft.Data.SqlClient;
//using System.Linq;
//using System.Web;
//using Microsoft.Extensions.Configuration;

//namespace RMCERPAPI.Repository
//{
//    public class PropMasterRepository : BaseRepository
//    {
//        public PropMasterRepository(IConfiguration configuration)
//            : base(configuration)
//        {
//        }
//        // SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
//        public int AddPropMaster(PropMasterModel ModelDatas)
//        {
//            using (IDbConnection connection = OpenConnection())
//            {
//                IDbTransaction transaction = connection.BeginTransaction();
//                try
//                {

//                    string sql = "ProcPropMaster";
//                    var param = new
//                    {
//                        PrcType = 1,
//                        PropID = ModelDatas.PropID,
//                        PropTypeName = ModelDatas.PropTypeName,
//                        PropName = ModelDatas.PropName,
//                        PropValue = ModelDatas.PropValue,
//                        Status = ModelDatas.Status,
//                        CUID = ModelDatas.CUID,

//                    };
//                    int varID = connection.ExecuteScalar<int>(sql, param, transaction, 2000, CommandType.StoredProcedure);
//                    transaction.Commit();
//                    return varID;
//                }
//                catch (Exception)
//                {
//                    //EConverter.WriteToLibraryFile(ex.Message);
//                    transaction.Rollback();
//                    throw;
//                }
//            }
//        }
//        public int EditPropMaster(PropMasterModel ModelDatas)
//        {
//            using (IDbConnection connection = OpenConnection())
//            {
//                IDbTransaction transaction = connection.BeginTransaction();
//                try
//                {

//                    string sql = "ProcPropMaster";
//                    var param = new
//                    {
//                        PrcType = 2,
//                        PropID = ModelDatas.PropID,
//                        PropTypeName = ModelDatas.PropTypeName,
//                        PropName = ModelDatas.PropName,
//                        PropValue = ModelDatas.PropValue,
//                        Status = ModelDatas.Status,
//                        CUID = ModelDatas.CUID,

//                    };
//                    int varID = connection.ExecuteScalar<int>(sql, param, transaction, 2000, CommandType.StoredProcedure);
//                    transaction.Commit();
//                    return varID;
//                }
//                catch (Exception)
//                {
//                    //EConverter.WriteToLibraryFile(ex.Message);
//                    transaction.Rollback();
//                    throw;
//                }
//            }
//        }

//        public int DeletePropMaster(string id)
//        {
//            using (IDbConnection connection = OpenConnection())
//            {
//                IDbTransaction transaction = connection.BeginTransaction();
//                try
//                {
//                    string sql = "ProcPropMaster";
//                    var param = new
//                    {
//                        PrcType = 3,
//                        PropID = id
//                    };
//                    int varID = connection.ExecuteScalar<int>(sql, param, transaction, 2000, CommandType.StoredProcedure);
//                    transaction.Commit();
//                    return varID;
//                }
//                catch (Exception)
//                {
//                    //EConverter.WriteToLibraryFile(ex.Message);
//                    transaction.Rollback();
//                    throw;
//                }
//            }
//        }
//        public List<PropMasterModel> GetPropMasterList()
//        {
//            using (IDbConnection connection = OpenConnection())
//            {
//                string sql = "Select * From ViewPropMaster";

//                List<PropMasterModel> data = new List<PropMasterModel>();
//                data = connection.Query<PropMasterModel>(sql).ToList();
//                if (data == null || data.Count == 0)
//                {
//                    return new List<PropMasterModel>();
//                }
//                else
//                {
//                    return data;
//                }
//            }
//        }


//        public PropMasterModel GetDataByID(Decimal PropID)
//        {
//            using (IDbConnection connection = OpenConnection())
//            {
//                //  IDbTransaction transaction = connection.BeginTransaction();
//                try
//                {
//                    string sql = "ProcPropMaster";
//                    //string sql = "insert into SchoolSession (SchoolID,StartDate,EndDate,ActiveStatus,CreatedByID,CreateDate,ModifiedByID,ModifiedDate,Guids) values (@SchoolID,@StartDate,@EndDate,@ActiveStatus,@CreatedByID,@CreateDate,@ModifiedByID,@ModifiedDate,@Guids)      select isnull(scope_identity(),0) as [IDResults]      ";
//                    var param = new
//                    {
//                        PrcType = 4,
//                        PropID = PropID

//                    };
//                    Models.PropMasterModel data = new Models.PropMasterModel();
//                    Models.PropMasterModel objPropMst = new Models.PropMasterModel();
//                    data = connection.Query<PropMasterModel>(sql, param, null, true, 2000, CommandType.StoredProcedure).ElementAt(0);
//                    if (data == null)
//                    {
//                        return objPropMst;
//                    }
//                    else
//                    {
//                        return data;
//                    }


//                    //  return varID;
//                }
//                catch (Exception)
//                {
//                    //EConverter.WriteToLibraryFile(ex.Message);
//                    throw;
//                }
//            }
//        }

//        public List<Models.PropMasterModel> GetFilterPropMasterByType(string PropTypeName)
//        {
//            try
//            {
//                var param = new
//                {
//                    PropTypeName = "%" + PropTypeName + "%"
//                };
//                string Wr = "";
//                Wr = "Where 1=1";
//                if (PropTypeName != "")
//                { Wr = Wr + " And PropTypeName Like @PropTypeName"; }
//                using (IDbConnection connection = OpenConnection())
//                {
//                    string str = "";
//                    str += "Select * from ";
//                    str += "PropMaster(nolock)";
//                    str += Wr + "";
//                    str += "  Order by PropID";

//                    List<Models.PropMasterModel> data = new List<Models.PropMasterModel>();

//                    data = connection.Query<PropMasterModel>(str, param, null, true, 2000).ToList();
//                    if (data == null || data.Count == 0)
//                    {
//                        return new List<Models.PropMasterModel>();
//                    }
//                    return data;
//                }

//            }

//            catch (Exception)
//            {
//                //EConverter.WriteToLibraryFile(ex.Message);
//                throw;
//            }
//        }


//        //public List<PropMasterModel> GetPropMasterList()
//        //{
//        //    return GetPropMasterDatas(new PropMasterModel());
//        //}

//    }//Class
//}



using RMCERPAPI.Models;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace RMCERPAPI.Repository
{
    public class PropMasterRepository : BaseRepository
    {
        //SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        public int AddPropMaster(PropMasterModel ModelDatas)
        {
            using (IDbConnection connection = OpenConnection())
            {
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    string sql = "ProcPropMaster";
                    var param = new
                    {
                        PrcType = 1,
                        PropID = ModelDatas.PropID,
                        PropTypeName = ModelDatas.PropTypeName,
                        PropName = ModelDatas.PropName,
                        PropValue = ModelDatas.PropValue,
                        Status = ModelDatas.Status,
                        CUID = ModelDatas.CUID,
                    };
                    int varID = connection.ExecuteScalar<int>(sql, param, transaction, 2000, CommandType.StoredProcedure);
                    transaction.Commit();
                    return varID;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public int EditPropMaster(PropMasterModel ModelDatas)
        {
            using (IDbConnection connection = OpenConnection())
            {
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    string sql = "ProcPropMaster";
                    var param = new
                    {
                        PrcType = 2,
                        PropID = ModelDatas.PropID,
                        PropTypeName = ModelDatas.PropTypeName,
                        PropName = ModelDatas.PropName,
                        PropValue = ModelDatas.PropValue,
                        Status = ModelDatas.Status,
                        CUID = ModelDatas.CUID,
                    };
                    int varID = connection.ExecuteScalar<int>(sql, param, transaction, 2000, CommandType.StoredProcedure);
                    transaction.Commit();
                    return varID;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public int DeletePropMaster(string id)
        {
            using (IDbConnection connection = OpenConnection())
            {
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    string sql = "ProcPropMaster";
                    var param = new
                    {
                        PrcType = 3,
                        PropID = id,
                    };
                    int varID = connection.ExecuteScalar<int>(sql, param, transaction, 2000, CommandType.StoredProcedure);
                    transaction.Commit();
                    return varID;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public List<PropMasterModel> GetPropMasterList()
        {
            using (IDbConnection connection = OpenConnection())
            {
                string sql = "Select * From ViewPropMaster";

                List<PropMasterModel> data = new List<PropMasterModel>();
                data = connection.Query<PropMasterModel>(sql).ToList();
                if (data == null || data.Count == 0)
                {
                    return new List<PropMasterModel>();
                }
                else
                {
                    return data;
                }
            }
        }

        public PropMasterModel GetDataByID(Decimal ID)
        {
            using (IDbConnection connection = OpenConnection())
            {
                try
                {
                    string sql = "ProcPropMaster";
                    var param = new
                    {
                        PrcType = 4,
                        PropID = ID
                    };

                    PropMasterModel data = new PropMasterModel();
                    data = connection.Query<PropMasterModel>(sql, param, null, true, 2000, CommandType.StoredProcedure).ElementAtOrDefault(0);

                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
