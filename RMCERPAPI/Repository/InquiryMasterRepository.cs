using Dapper;
using RMCERPAPI.Helpers;
using RMCERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RMCERPAPI.Repository
{
    public class InquiryMasterRepository : BaseRepository
    {
        public int AddInquiryMaster(InquiryMasterModel ModelDatas)
        {
            using (IDbConnection connection = OpenConnection())
            {
                try
                {
                    DataTable dtblInquiryItem =
                        CommonHelpers.ModelToUTT.ConvertToUTT(
                            "uttInquiryItem",
                            ModelDatas.InquiryItemList
                        );

                    string sql = "ProcInquiryMaster";

                    var param = new
                    {
                        PrcType = 1,
                        InquiryNo = ModelDatas.InquiryNo,
                        BranchID = ModelDatas.BranchID,
                        OnAcID = ModelDatas.OnAcID,
                        Date = ModelDatas.Date,
                        InquiryTypeID = ModelDatas.InquiryTypeID,
                        PartyName = ModelDatas.PartyName,
                        Address = ModelDatas.Address,
                        Area = ModelDatas.Area,
                        State = ModelDatas.State,
                        ContactName = ModelDatas.ContactName,
                        Mobile = ModelDatas.Mobile,
                        Mobile2 = ModelDatas.Mobile2,
                        EmailID = ModelDatas.EmailID,
                        EmailID2 = ModelDatas.EmailID2,
                        ReferenceBy = ModelDatas.ReferenceBy,
                        Currency = ModelDatas.Currency,
                        SendMail = ModelDatas.SendMail,
                        GUIDs = ModelDatas.GUIDs,
                        Remarks = ModelDatas.Remarks,
                        Status = ModelDatas.Status,
                        MKTBy = ModelDatas.MKTBy,
                        CUID = ModelDatas.CUID,
                        InquiryItemData = dtblInquiryItem
                    };

                    int varID = connection.ExecuteScalar<int>(
                        sql,
                        param,
                        null,
                        2000,
                        CommandType.StoredProcedure
                    );

                    return varID;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }


        public int EditInquiryMaster(InquiryMasterModel ModelDatas)
        {
            using (IDbConnection connection = OpenConnection())
            {
                IDbTransaction transaction = connection.BeginTransaction();

                try
                {
                    DataTable dtblInquiryItem =
                        CommonHelpers.ModelToUTT.ConvertToUTT(
                            "uttInquiryItem",
                            ModelDatas.InquiryItemList
                        );

                    string sql = "ProcInquiryMaster";

                    var param = new
                    {
                        PrcType = 2,
                        InquiryID = ModelDatas.InquiryID,
                        InquiryNo = ModelDatas.InquiryNo,
                        BranchID = ModelDatas.BranchID,
                        OnAcID = ModelDatas.OnAcID,
                        Date = ModelDatas.Date,
                        InquiryTypeID = ModelDatas.InquiryTypeID,
                        PartyName = ModelDatas.PartyName,
                        Address = ModelDatas.Address,
                        Area = ModelDatas.Area,
                        State = ModelDatas.State,
                        ContactName = ModelDatas.ContactName,
                        Mobile = ModelDatas.Mobile,
                        Mobile2 = ModelDatas.Mobile2,
                        EmailID = ModelDatas.EmailID,
                        EmailID2 = ModelDatas.EmailID2,
                        ReferenceBy = ModelDatas.ReferenceBy,
                        Currency = ModelDatas.Currency,
                        SendMail = ModelDatas.SendMail,
                        GUIDs = ModelDatas.GUIDs,
                        Remarks = ModelDatas.Remarks,
                        Status = ModelDatas.Status,
                        MKTBy = ModelDatas.MKTBy,
                        InquiryItemData = dtblInquiryItem
                    };

                    int varID = connection.ExecuteScalar<int>(
                        sql,
                        param,
                        transaction,
                        2000,
                        CommandType.StoredProcedure
                    );

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


        public InquiryMasterModel GetDataByID(decimal InquiryID)
        {
            using (IDbConnection connection = OpenConnection())
            {
                try
                {
                    string sql = "ProcInquiryMaster";

                    var param = new
                    {
                        PrcType = 4,
                        InquiryID = InquiryID
                    };

                    InquiryMasterModel data =
                        connection.Query<InquiryMasterModel>(
                            sql,
                            param,
                            null,
                            true,
                            2000,
                            CommandType.StoredProcedure
                        ).ElementAtOrDefault(0);

                    if (data == null)
                    {
                        return null;
                    }

                    data.InquiryItemList =
                        GetFilterInquiryItemData(InquiryID);

                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }


        public int DeleteInquiryMaster(string InquiryID)
        {
            using (IDbConnection connection = OpenConnection())
            {
                IDbTransaction transaction = connection.BeginTransaction();

                try
                {
                    string sql = "ProcInquiryMaster";

                    var param = new
                    {
                        PrcType = 3,
                        InquiryID = InquiryID
                    };

                    int varID = connection.ExecuteScalar<int>(
                        sql,
                        param,
                        transaction,
                        2000,
                        CommandType.StoredProcedure
                    );

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


        public List<InquiryItemModel> GetFilterInquiryItemData(decimal InquiryID)
        {
            using (IDbConnection connection = OpenConnection())
            {
                try
                {
                    string sql = "ProcInquiryItem";

                    var param = new
                    {
                        InquiryID = InquiryID
                    };

                    List<InquiryItemModel> data =
                        connection.Query<InquiryItemModel>(
                            sql,
                            param,
                            null,
                            true,
                            2000,
                            CommandType.StoredProcedure
                        ).ToList();

                    if (data == null || data.Count == 0)
                    {
                        return new List<InquiryItemModel>();
                    }

                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }


        public List<InquiryMasterModel> GetInquiryMasterList()
        {
            using (IDbConnection connection = OpenConnection())
            {
                string sql =
                    "SELECT * FROM ViewInquiryMaster ORDER BY InquiryID DESC";

                return connection.Query<InquiryMasterModel>(
                    sql,
                    commandType: CommandType.Text
                ).ToList();
            }
        }
    }
}