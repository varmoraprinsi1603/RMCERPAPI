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
    public class CustomerRepository : BaseRepository
    {
        //SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        public int AddCustomer(CustomerModel ModelDatas)
        {
            using (IDbConnection connection = OpenConnection())
            {
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    string sql = "ProcCustomer";
                    var param = new
                    {
                        PrcType = 1,
                        CustomerID = ModelDatas.CustomerID,
                        CustomerName = ModelDatas.CustomerName,
                        MobileNo = ModelDatas.MobileNo,
                        Email = ModelDatas.Email,
                        Address = ModelDatas.Address,
                        City = ModelDatas.City,
                        CreatedDate = ModelDatas.CreatedDate,
                        IsActive = ModelDatas.IsActive
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

        public int EditCustomer(CustomerModel ModelDatas)
        {
            using (IDbConnection connection = OpenConnection())
            {
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    string sql = "ProcCustomer";
                    var param = new
                    {
                        PrcType = 2,
                        CustomerID = ModelDatas.CustomerID,
                        CustomerName = ModelDatas.CustomerName,
                        MobileNo = ModelDatas.MobileNo,
                        Email = ModelDatas.Email,
                        Address = ModelDatas.Address,
                        City = ModelDatas.City,
                        CreatedDate = ModelDatas.CreatedDate,
                        IsActive = ModelDatas.IsActive
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

        public int DeleteCustomer(string id)
        {
            using (IDbConnection connection = OpenConnection())
            {
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    string sql = "ProcCustomer";
                    var param = new
                    {
                        PrcType = 3,
                        CustomerID = id,
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

        public List<CustomerModel> GetCustomerList()
        {
            using (IDbConnection connection = OpenConnection())
            {
                string sql = "Select * From ViewCustomer";

                List<CustomerModel> data = new List<CustomerModel>();
                data = connection.Query<CustomerModel>(sql).ToList();
                if (data == null || data.Count == 0)
                {
                    return new List<CustomerModel>();
                }
                else
                {
                    return data;
                }
            }
        }

        public CustomerModel GetDataByID(Decimal ID)
        {
            using (IDbConnection connection = OpenConnection())
            {
                try
                {
                    string sql = "ProcCustomer";
                    var param = new
                    {
                        PrcType = 4,
                        CustomerID = ID
                    };

                    CustomerModel data = new CustomerModel();
                    data = connection.Query<CustomerModel>(sql, param, null, true, 2000, CommandType.StoredProcedure).ElementAtOrDefault(0);

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
