using Dapper;
using RMCERPAPI.Models;
using System.Data;
using System.Linq;

namespace RMCERPAPI.Repository
{
    public class DashboardRepository : BaseRepository
    {
        public DashboardModel GetDashboard()
        {
            using (IDbConnection connection = OpenConnection())
            {
                string sql = "ProcTicketDashboard";

                return connection.Query<DashboardModel>(
                    sql,
                    null,
                    null,
                    true,
                    2000,
                    CommandType.StoredProcedure
                ).FirstOrDefault();
            }
        }
    }
}