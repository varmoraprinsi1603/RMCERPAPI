using Dapper;
using RMCERPAPI.Helpers;
using RMCERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace RMCERPAPI.Repository
{
    public class CommonRepository : BaseRepository
    {
    //    public CommonRepository(IConfiguration configuration)
    //: base(configuration)
    //    {
    //    }
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);

        //public List<DropdownBindModel> CommonCascading(string TableName, string FieldName_Text, string FieldName_Value, string FieldName_Where, string FieldValue, string WhereCondition, string FieldOrderBy = "", string AsAlias_Text = "", string AsAlias_Value = "", string AsAlias_FieldOrderBy = "")
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(AsAlias_Text))
        //        {
        //            AsAlias_Text = "Texts";
        //        }
        //        if (string.IsNullOrEmpty(AsAlias_Value))
        //        {
        //            AsAlias_Value = "Value";
        //        }
        //        if (string.IsNullOrEmpty(FieldOrderBy))
        //        {
        //            FieldOrderBy = FieldName_Text;
        //        }
        //        if (!string.IsNullOrEmpty(FieldOrderBy) && string.IsNullOrEmpty(AsAlias_FieldOrderBy))
        //        {
        //            AsAlias_FieldOrderBy = FieldOrderBy;
        //        }

        //        using (IDbConnection connection = OpenConnection())
        //        {
        //            string str = "";
        //            str += " select distinct " + FieldName_Text + " as " + AsAlias_Text + "," + FieldName_Value + " as " + AsAlias_Value + " ";
        //            if (!string.IsNullOrEmpty(FieldOrderBy))
        //            {
        //                str += ", " + FieldOrderBy + " as " + AsAlias_FieldOrderBy + " ";
        //            }
        //            str += " from " + TableName + " (nolock) ";
        //            str += " Where 1=1 ";
        //            if (!string.IsNullOrEmpty(FieldName_Where) && !string.IsNullOrEmpty(FieldValue))
        //            {
        //                str += " and " + FieldName_Where + " = '" + FieldValue + "' ";
        //            }
        //            if (!string.IsNullOrEmpty(WhereCondition))
        //            {
        //                str += " " + WhereCondition + " ";
        //            }
        //            if (!string.IsNullOrEmpty(AsAlias_FieldOrderBy))
        //            {
        //                str += " Order By " + AsAlias_FieldOrderBy;
        //            }
        //            List<DropdownBindModel> data = new List<DropdownBindModel>();
        //            data = connection.Query<DropdownBindModel>(str, null, null, true, EnumClass.SQLTimeOut).ToList();
        //            data.Insert(0, new DropdownBindModel { Value = "", Texts = "Select" });
        //            if (data == null || data.Count == 0)
        //            {
        //                return new List<DropdownBindModel>();
        //            }
        //            return data;
        //        }
        //    }
        //    catch (Exception _Exception)
        //    {
        //        EConverter.WriteToLibraryFile(_Exception.Message);
        //        throw _Exception;
        //    }
        //}

        //public List<SelectListItem> GetSelectionList(string TableName, string FieldName_Text, string FieldName_Value, string FieldName_Where, string FieldValue, string WhereCondition, string FieldOrderBy = "", string AsAlias_Text = "", string AsAlias_Value = "", string AsAlias_FieldOrderBy = "")
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(AsAlias_Text))
        //        {
        //            AsAlias_Text = "Text";
        //        }
        //        if (string.IsNullOrEmpty(AsAlias_Value))
        //        {
        //            AsAlias_Value = "Value";
        //        }
        //        if (string.IsNullOrEmpty(FieldOrderBy))
        //        {
        //            FieldOrderBy = FieldName_Text;
        //        }
        //        if (!string.IsNullOrEmpty(FieldOrderBy) && string.IsNullOrEmpty(AsAlias_FieldOrderBy))
        //        {
        //            AsAlias_FieldOrderBy = FieldOrderBy;
        //        }

        //        using (IDbConnection connection = OpenConnection())
        //        {
        //            string str = "";
        //            str += " select distinct " + FieldName_Text + " as " + AsAlias_Text + "," + FieldName_Value + " as " + AsAlias_Value + " ";
        //            if (!string.IsNullOrEmpty(FieldOrderBy))
        //            {
        //                str += ", " + FieldOrderBy + " as " + AsAlias_FieldOrderBy + " ";
        //            }
        //            str += " from " + TableName + " (nolock) ";
        //            str += " Where 1=1 ";
        //            if (!string.IsNullOrEmpty(FieldName_Where) && !string.IsNullOrEmpty(FieldValue))
        //            {
        //                str += " and " + FieldName_Where + " = '" + FieldValue + "' ";
        //            }
        //            if (!string.IsNullOrEmpty(WhereCondition))
        //            {
        //                str += " " + WhereCondition + " ";
        //            }
        //            if (!string.IsNullOrEmpty(AsAlias_FieldOrderBy))
        //            {
        //                str += " Order By " + AsAlias_FieldOrderBy;
        //            }
        //            List<SelectListItem> data = new List<SelectListItem>();
        //            data = connection.Query<SelectListItem>(str, null, null, true, EnumClass.SQLTimeOut).ToList();

        //            if (data == null || data.Count == 0)
        //            {
        //                return new List<SelectListItem>();
        //            }
        //            return data;
        //        }
        //    }
        //    catch (Exception _Exception)
        //    {
        //        throw _Exception;
        //    }
        //}
        //public List<DynamicFieldModel> FillDynamicField(int OrganisationID, int FormID, int IdValue = 0)
        //{
        //    try
        //    {
        //        using (IDbConnection connection = OpenConnection())
        //        {
        //            string str = "SELECT FieldID,FieldName,FieldLabelText,OrganisationID,FormID,ColumnMapID,ColumnType,DataValue,DefaultValue,IsRequired,DataType,Section,SectionText,ActiveStatus FROM DynamicField where OrganisationID = @OrganisationID and ActiveStatus = @ActiveStatus and FormId=@FormID";
        //            List<DynamicFieldModel> data = connection.Query<DynamicFieldModel>(str, new { OrganisationID = OrganisationID, ActiveStatus = 1, FormID = FormID }, null, true, EnumClass.SQLTimeOut).ToList();

        //            if (IdValue > 0)
        //            {
        //                foreach (DynamicFieldModel dynamicfeild in data)
        //                {
        //                    str = "SELECT col" + dynamicfeild.ColumnMapID + " from " + (dynamicfeild.FormID == 243 ? "StudentMaster" : "StaffMaster") + " where " + (dynamicfeild.FormID == 243 ? "StudentId" : "StaffId") + "=" + IdValue;
        //                    string FieldValue = connection.ExecuteScalar<String>(str, null, null, EnumClass.SQLTimeOut, CommandType.Text);
        //                    dynamicfeild.DefaultValue = FieldValue;
        //                }
        //            }
        //            if (data == null || data.Count == 0)
        //            {
        //                return new List<DynamicFieldModel>();
        //            }
        //            return data;
        //        }
        //    }
        //    catch (Exception _Exception)
        //    {
        //        throw _Exception;
        //    }
        //}


        ////only for parth
        //public int CUDdata(string proc_name, object param)
        //{
        //    using (IDbConnection connection = OpenConnection())
        //    {
        //        IDbTransaction transaction = connection.BeginTransaction();
        //        try
        //        {
        //            int varID = connection.ExecuteScalar<int>(proc_name, param, transaction, 2000, CommandType.StoredProcedure);

        //            transaction.Commit();

        //            return varID;
        //        }
        //        catch (Exception ex)
        //        {
        //            transaction.Rollback();
        //            throw ex;
        //        }
        //    }
        //}


        //public int ExecuteQuery(string Query)
        //{
        //    using (IDbConnection connection = OpenConnection())
        //    {
        //        return connection.Execute(Query, null, null, null, CommandType.Text);
        //    }
        //}


        //public int CheckDuplicate(string TableName, string FieldName, string FieldIDName, int FieldID, string Value, string Where)
        //{
        //    try
        //    {

        //        using (IDbConnection connection = OpenConnection())
        //        {
        //            string query = "";

        //            if (FieldID > 0)
        //            {
        //                query += "select count(*) from " + TableName + " (nolock) where replace(replace(" + FieldName + ",' ',''),'.','') = '" + Value.Replace("'", "''").Replace(" ", "").Replace(".", "") + "' and " + Where + " And " + FieldIDName + " != " + FieldID;
        //            }
        //            else
        //            {
        //                query += "select count(*) from " + TableName + " (nolock) where replace(replace(" + FieldName + ",' ',''),'.','') = '" + Value.Replace("'", "''").Replace(" ", "").Replace(".", "") + "'  and " + Where + " and 1= 1";
        //            }
        //            int data = connection.ExecuteScalar<int>(query, null, null, EnumClass.SQLTimeOut, CommandType.Text);
        //            return data;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }

        //}
        //public List<MenuModel> GetMenuList(int UserID)
        //{
        //    try
        //    {
        //        using (IDbConnection connection = OpenConnection())
        //        {
        //            string str = "ProcGetFormByUserID";
        //            var param = new
        //            {
        //                UserID = UserID
        //            };
        //            List<MenuModel> data = new List<MenuModel>();
        //            data = connection.Query<MenuModel>(str, param, null, true, 2000, CommandType.StoredProcedure).ToList();
        //            if (data == null || data.Count == 0)
        //            {
        //                return new List<MenuModel>();
        //            }
        //            else
        //            {
        //                return data;
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        public DataSet GetUTTData(string UTTName)
        {
            try
            {
                using (IDbConnection connection = OpenConnection())
                {
                    string str = "select c.name from sys.table_types tt inner join sys.columns c on c.object_id = tt.type_table_object_id where tt.name='" + UTTName + "'";

                    SqlCommand cmd = new SqlCommand(str, new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString()));


                    DataSet DSet = new DataSet();

                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(DSet);
                    da.Dispose();
                    cmd.Dispose();
                    return DSet;

                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        //public List<StatusModel> GetStatus()
        //{
        //    try
        //    {
        //        using (IDbConnection connection = OpenConnection())
        //        {
        //            string str = "select PropName as StatusName,PropValue as Status from PropMaster where PropTypeName ='Status'";

        //            List<StatusModel> data = connection.Query<StatusModel>(str, new { }, null, true, EnumClass.SQLTimeOut).ToList();
        //            if (data == null || data.Count == 0)
        //            {
        //                return new List<StatusModel>();
        //            }
        //            return data;
        //        }
        //    }
        //    catch (Exception _Exception)
        //    {
        //        throw _Exception;
        //    }
        //}

        //public List<UserModel> GetUser()
        //{
        //    try
        //    {
        //        using (IDbConnection connection = OpenConnection())
        //        {
        //            string str = "select UserName ,UserID from UserMaster";

        //            List<UserModel> data = connection.Query<UserModel>(str, new { }, null, true, EnumClass.SQLTimeOut).ToList();
        //            if (data == null || data.Count == 0)
        //            {
        //                return new List<UserModel>();
        //            }
        //            return data;
        //        }
        //    }
        //    catch (Exception _Exception)
        //    {
        //        throw _Exception;
        //    }
        //}

        //public List<DropdownBindModel> FillTaxClassByVoucherTypeID(int voucherTypeID)
        //{
        //    try
        //    {
        //        using (IDbConnection connection = OpenConnection())
        //        {
        //            string str = @"select T.TaxClassID as Value,T.TaxClassName as Texts,T1.VoucherTypeID from TaxClassMaster T Left Outer Join
        //                            TaxClassVoucher T1 on T.TaxClassID=T1.TaxClassID 
        //                            where 1=1 and T1.VoucherTypeID=" + voucherTypeID;

        //            List<DropdownBindModel> data = connection.Query<DropdownBindModel>(str, new { }, null, true, EnumClass.SQLTimeOut).ToList();

        //            if (data == null || data.Count == 0)
        //            {
        //                return new List<DropdownBindModel>();
        //            }
        //            return data;
        //        }
        //    }
        //    catch (Exception _Exception)
        //    {
        //        throw _Exception;
        //    }
        //}

        //public List<ParentItemGroup> GetParentItemGroup()
        //{
        //    try
        //    {
        //        using (IDbConnection connection = OpenConnection())
        //        {
        //            string str = "select ItemGRoupName as ParentItemGroupName,ItemGroupID as ParentItemGroupID from ItemGroupMaster";

        //            List<ParentItemGroup> data = connection.Query<ParentItemGroup>(str, new { }, null, true, EnumClass.SQLTimeOut).ToList();
        //            data.Insert(0, new ParentItemGroup { ParentItemGroupID = 0, ParentItemGroupName = "Select" });

        //            if (data == null || data.Count == 0)
        //            {
        //                return new List<ParentItemGroup>();
        //            }
        //            return data;
        //        }
        //    }
        //    catch (Exception _Exception)
        //    {
        //        throw _Exception;
        //    }
        //}
        //public DataTable GetPopupData(string TableName, string Field1, string Field2, string where, string orderBy)
        //{

        //    DataTable dt = EGeneral.ExecuteDataTable("select DISTINCT " + Field1.ToUpper().Replace("DISTINCT", "") + " as " + Field1 + " , " + Field2.Replace("~", "'").Replace("@", "+") + "  from  " + TableName.Replace("~", "'") + " (nolock) Where " + where.Replace("~", "'") + (orderBy == "" ? " " : " Order By " + orderBy));
        //    return dt;

        //}
        //public DataTable GetPopupData(string TableName, string Field1, string Field2, string where, string orderBy, bool multi = true)
        //{
        //    if (orderBy == "")
        //    {
        //        orderBy = "getdate()";
        //    }

        //    DataTable dt = EGeneral.ExecuteDataTable("select DISTINCT ROW_NUMBER() OVER (ORDER BY " + orderBy + ") as ' ', " + Field1.ToUpper().Replace("DISTINCT", "") + " , " + Field2.Replace("~", "'").Replace("@", "+") + "  from  " + TableName.Replace("~", "'") + " (nolock) Where " + where.Replace("~", "'") + (orderBy == "" ? " " : " Order By " + orderBy));
        //    return dt;

        //}


        //public string GetScaler(string query)
        //{
        //    using (IDbConnection connection = OpenConnection())
        //    {
        //        try
        //        {
        //            string data = "";
        //            string sql = query.Replace("~", "'") + " and 1=1";
        //            data = connection.ExecuteScalar(sql, null, null, EnumClass.SQLTimeOut, CommandType.Text).ToString();
        //            if (data == "")
        //            {
        //                return "";
        //            }
        //            return data;

        //        }
        //        catch (Exception ex)
        //        {
        //            return "";
        //        }
        //    }
        //}
        //public UserRight GetUserRight(string ControllerName, int UserID, int isList)
        //{
        //    using (IDbConnection connection = OpenConnection())
        //    {

        //        try
        //        {
        //            UserRight data = new UserRight();
        //            string sql = "ProcGetRightsUserID";
        //            var param = new
        //            {
        //                UserID = UserID,
        //                ControllerName = ControllerName,
        //                isList = isList

        //            };
        //            data = connection.Query<UserRight>(sql, param, null, true, 2000, CommandType.StoredProcedure).ElementAt(0);


        //            return data;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }

        //}

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        //    public List<DropdownBindModel> CommonCascadingWithoutSelect(string TableName, string FieldName_Text, string FieldName_Value, string FieldName_Where, string FieldValue, string WhereCondition, string FieldOrderBy = "", string AsAlias_Text = "", string AsAlias_Value = "", string AsAlias_FieldOrderBy = "")
        //    {
        //        try
        //        {
        //            if (string.IsNullOrEmpty(AsAlias_Text))
        //            {
        //                AsAlias_Text = "Texts";
        //            }
        //            if (string.IsNullOrEmpty(AsAlias_Value))
        //            {
        //                AsAlias_Value = "Value";
        //            }
        //            if (string.IsNullOrEmpty(FieldOrderBy))
        //            {
        //                FieldOrderBy = FieldName_Text;
        //            }
        //            if (!string.IsNullOrEmpty(FieldOrderBy) && string.IsNullOrEmpty(AsAlias_FieldOrderBy))
        //            {
        //                AsAlias_FieldOrderBy = FieldOrderBy;
        //            }

        //            using (IDbConnection connection = OpenConnection())
        //            {
        //                string str = "";
        //                str += " select distinct " + FieldName_Text + " as " + AsAlias_Text + "," + FieldName_Value + " as " + AsAlias_Value + " ";
        //                if (!string.IsNullOrEmpty(FieldOrderBy))
        //                {
        //                    str += ", " + FieldOrderBy + " as " + AsAlias_FieldOrderBy + " ";
        //                }
        //                str += " from " + TableName + " (nolock) ";
        //                str += " Where 1=1 ";
        //                if (!string.IsNullOrEmpty(FieldName_Where) && !string.IsNullOrEmpty(FieldValue))
        //                {
        //                    str += " and " + FieldName_Where + " = '" + FieldValue + "' ";
        //                }
        //                if (!string.IsNullOrEmpty(WhereCondition))
        //                {
        //                    str += " " + WhereCondition + " ";
        //                }
        //                if (!string.IsNullOrEmpty(AsAlias_FieldOrderBy))
        //                {
        //                    str += " Order By " + AsAlias_FieldOrderBy;
        //                }
        //                List<DropdownBindModel> data = new List<DropdownBindModel>();
        //                data = connection.Query<DropdownBindModel>(str, null, null, true, EnumClass.SQLTimeOut).ToList();

        //                if (data == null || data.Count == 0)
        //                {
        //                    return new List<DropdownBindModel>();
        //                }
        //                return data;
        //            }
        //        }
        //        catch (Exception _Exception)
        //        {
        //            throw _Exception;
        //        }
        //    }

        //    public string GetWhatsappScaler(string id, string tablename)
        //    {
        //        using (IDbConnection connection = OpenConnection())
        //        {
        //            try
        //            {
        //                string data = "";
        //                string sql = @"Select  [dbo].[fetch_MobileNo](@id,@tablename)";
        //                var param = new
        //                {
        //                    id = id,
        //                    tablename = tablename
        //                };
        //                data = connection.ExecuteScalar(sql, param, null, EnumClass.SQLTimeOut, CommandType.Text).ToString();
        //                if (data == "")
        //                {
        //                    return "";
        //                }
        //                return data;

        //            }
        //            catch (Exception ex)
        //            {
        //                return "";
        //            }
        //        }
        //    }

        //    public IEnumerable<LookupItem> GetBranchesByUserId(int userId)
        //    {
        //        using (IDbConnection connection = OpenConnection())
        //        {
        //            string sql = @"
        //        SELECT 
        //            pt.BranchID AS Id, 
        //            B1.BranchName AS Name 
        //        FROM UserBranchMaster pt 
        //        LEFT OUTER JOIN BranchMaster B1 ON pt.BranchID = B1.BranchID 
        //        WHERE pt.UserID = @UserID 
        //        AND ISNULL(pt.Active, 0) = 1";

        //            return connection.Query<LookupItem>(sql, new { UserID = userId });
        //        }
        //    }

        //    ~CommonRepository()
        //    {
        //        Con.Close();
        //        Con.Dispose();
        //    }
        //}
    }
    //class
}//namespace
