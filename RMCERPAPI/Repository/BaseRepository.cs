//using Microsoft.Data.SqlClient;
//using Microsoft.Extensions.Configuration;
//using System;

//namespace RMCERPAPI.Repository
//{
//    public class BaseRepository
//    {
//        private readonly IConfiguration _configuration;

//        public BaseRepository(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }

//        public SqlConnection OpenConnection()
//        {
//            string connectionString = _configuration.GetConnectionString("Con");

//            if (string.IsNullOrWhiteSpace(connectionString))
//            {
//                throw new Exception("Connection string 'Con' not found in appsettings.json.");
//            }

//            return new SqlConnection(connectionString);
//        }
//    }
//}


//using RMCERPAPI.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RMCERPAPI.Repository
{
    public class BaseRepository
    {
        //public SqlConnection OpenConnection()
        //{
        //    try
        //    {
        //        string varConnectionString = ConfigurationManager.ConnectionStrings["Con"].ToString();
        //        SqlConnection objSqlConnection = new SqlConnection(varConnectionString);
        //        objSqlConnection.Open();
        //        return objSqlConnection;
        //    }
        //    catch (Exception _Exception)
        //    {
        //        throw _Exception;
        //    }
        //}

        public SqlConnection OpenConnection()
        {
            var config = ConfigurationManager.ConnectionStrings["Con"];

            if (config == null)
            {
                throw new Exception("Connection string 'Con' is NULL.");
            }

            string connectionString = config.ConnectionString;

            var connection = new SqlConnection(connectionString);
            connection.Open();

            return connection;
        }

        ~BaseRepository()
        {
            try
            {
                OpenConnection().Dispose();
            }
            catch (Exception)
            {


            }

        }


    }
}
