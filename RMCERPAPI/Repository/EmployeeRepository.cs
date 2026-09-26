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
    public class EmployeeRepository : BaseRepository
    {
        //SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        public int AddEmployee(EmployeeModel ModelDatas)
        {
            using (IDbConnection connection = OpenConnection())
            {
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    string sql = "ProcEmployee";
                    var param = new
                    {
                        PrcType = 1,
                        EmployeeID = ModelDatas.EmployeeID,
                        EmployeeName = ModelDatas.EmployeeName,
                        DepartmentID = ModelDatas.DepartmentID,
                        Salary = ModelDatas.Salary,
                        JoiningDate = ModelDatas.JoiningDate,
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

        public int EditEmployee(EmployeeModel ModelDatas)
        {
            using (IDbConnection connection = OpenConnection())
            {
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    string sql = "ProcEmployee";
                    var param = new
                    {
                        PrcType = 2,
                        EmployeeID = ModelDatas.EmployeeID,
                        EmployeeName = ModelDatas.EmployeeName,
                        DepartmentID = ModelDatas.DepartmentID,
                        Salary = ModelDatas.Salary,
                        JoiningDate = ModelDatas.JoiningDate,
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

        public int DeleteEmployee(string id)
        {
            using (IDbConnection connection = OpenConnection())
            {
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    string sql = "ProcEmployee";
                    var param = new
                    {
                        PrcType = 3,
                        EmployeeID = id,
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

        public List<EmployeeModel> GetEmployeeList()
        {
            using (IDbConnection connection = OpenConnection())
            {
                string sql = "Select * From ViewEmployee";

                List<EmployeeModel> data = new List<EmployeeModel>();
                data = connection.Query<EmployeeModel>(sql).ToList();
                if (data == null || data.Count == 0)
                {
                    return new List<EmployeeModel>();
                }
                else
                {
                    return data;
                }
            }
        }

        public EmployeeModel GetDataByID(Decimal ID)
        {
            using (IDbConnection connection = OpenConnection())
            {
                try
                {
                    string sql = "ProcEmployee";
                    var param = new
                    {
                        PrcType = 4,
                        EmployeeID = ID
                    };

                    EmployeeModel data = new EmployeeModel();
                    data = connection.Query<EmployeeModel>(sql, param, null, true, 2000, CommandType.StoredProcedure).ElementAtOrDefault(0);

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
