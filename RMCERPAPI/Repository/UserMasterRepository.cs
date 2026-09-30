//using Dapper;
//using RMCERPAPI.Models;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;

//namespace RMCERPAPI.Repository
//{
//    public class UserMasterRepository : BaseRepository
//    {
//        public UserMasterModel Login(string UserName, string Password)
//        {
//            using (IDbConnection connection = OpenConnection())
//            {
//                string sql = "ProcUserMaster";

//                var param = new
//                {
//                    PrcType = 1,
//                    UserName = UserName,
//                    Password = Password
//                };

//                UserMasterModel data =
//                    connection
//                    .Query<UserMasterModel>(
//                        sql,
//                        param,
//                        null,
//                        true,
//                        2000,
//                        CommandType.StoredProcedure
//                    )
//                    .FirstOrDefault();

//                return data;
//            }
//        }
//    }
//}


using Dapper;
using RMCERPAPI.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RMCERPAPI.Repository
{
    public class UserMasterRepository : BaseRepository
    {
        // =========================
        // LOGIN
        // =========================
        public UserMasterModel Login(string UserName, string Password)
        {
            using (IDbConnection connection = OpenConnection())
            {
                string sql = "ProcUserMaster";

                var param = new
                {
                    PrcType = 1,
                    UserName = UserName,
                    Password = Password
                };

                UserMasterModel data =
                    connection.Query<UserMasterModel>(
                        sql,
                        param,
                        null,
                        true,
                        2000,
                        CommandType.StoredProcedure
                    ).FirstOrDefault();

                return data;
            }
        }


        // =========================
        // GET ALL USERS
        // =========================
        public List<UserMasterModel> GetUserList()
        {
            using (IDbConnection connection = OpenConnection())
            {
                string sql = "ProcUserMaster";

                var param = new
                {
                    PrcType = 2
                };

                return connection.Query<UserMasterModel>(
                    sql,
                    param,
                    null,
                    true,
                    2000,
                    CommandType.StoredProcedure
                ).ToList();
            }
        }


        // =========================
        // GET USER BY ID
        // =========================
        public UserMasterModel GetUserByID(decimal UserID)
        {
            using (IDbConnection connection = OpenConnection())
            {
                string sql = "ProcUserMaster";

                var param = new
                {
                    PrcType = 3,
                    UserID = UserID
                };

                return connection.Query<UserMasterModel>(
                    sql,
                    param,
                    null,
                    true,
                    2000,
                    CommandType.StoredProcedure
                ).FirstOrDefault();
            }
        }


        // =========================
        // ADD USER
        // =========================
        public decimal AddUser(UserMasterModel model)
        {
            using (IDbConnection connection = OpenConnection())
            {
                string sql = "ProcUserMaster";

                var param = new
                {
                    PrcType = 4,
                    UserName = model.UserName,
                    Password = model.Password,
                    Name = model.Name,
                    EmailID = model.EmailID,
                    Mobile = model.Mobile,
                    RoleID = model.RoleID,
                    IsActive = model.IsActive
                };

                return connection.ExecuteScalar<decimal>(
                    sql,
                    param,
                    null,
                    2000,
                    CommandType.StoredProcedure
                );
            }
        }


        // =========================
        // UPDATE USER
        // =========================
        public UserMasterModel UpdateUser(UserMasterModel model)
        {
            using (IDbConnection connection = OpenConnection())
            {
                string sql = "ProcUserMaster";

                var param = new
                {
                    PrcType = 5,
                    UserID = model.UserID,
                    UserName = model.UserName,
                    Password = model.Password,
                    Name = model.Name,
                    EmailID = model.EmailID,
                    Mobile = model.Mobile,
                    RoleID = model.RoleID,
                    IsActive = model.IsActive
                };

                return connection.Query<UserMasterModel>(
                    sql,
                    param,
                    null,
                    true,
                    2000,
                    CommandType.StoredProcedure
                ).FirstOrDefault();
            }
        }


        // =========================
        // DEACTIVATE USER
        // =========================
        public UserMasterModel DeactivateUser(decimal UserID)
        {
            using (IDbConnection connection = OpenConnection())
            {
                string sql = "ProcUserMaster";

                var param = new
                {
                    PrcType = 6,
                    UserID = UserID
                };

                return connection.Query<UserMasterModel>(
                    sql,
                    param,
                    null,
                    true,
                    2000,
                    CommandType.StoredProcedure
                ).FirstOrDefault();
            }
        }


        // =========================
        // GET ACTIVE ROLES
        // =========================
        public List<UserMasterModel> GetRoles()
        {
            using (IDbConnection connection = OpenConnection())
            {
                string sql = "ProcUserMaster";

                var param = new
                {
                    PrcType = 7
                };

                return connection.Query<UserMasterModel>(
                    sql,
                    param,
                    null,
                    true,
                    2000,
                    CommandType.StoredProcedure
                ).ToList();
            }
        }

        // =========================
        // GET ACTIVE SUPPORT EXECUTIVES
        // =========================
        public List<UserMasterModel> GetSupportExecutives()
        {
            using (IDbConnection connection = OpenConnection())
            {
                string sql = "ProcUserMaster";

                var param = new
                {
                    PrcType = 8
                };

                return connection.Query<UserMasterModel>(
                    sql,
                    param,
                    null,
                    true,
                    2000,
                    CommandType.StoredProcedure
                ).ToList();
            }
        }
    }
}