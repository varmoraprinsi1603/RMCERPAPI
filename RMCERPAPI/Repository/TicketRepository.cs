using Dapper;
using RMCERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RMCERPAPI.Repository
{
    public class TicketRepository : BaseRepository
    {
        // 1. Get Ticket List
        public List<TicketModel> GetTicketList()
        {
            using (IDbConnection connection = OpenConnection())
            {
                string sql = "ProcTicketMaster";

                var param = new
                {
                    PrcType = 1
                };

                return connection.Query<TicketModel>(
                    sql,
                    param,
                    null,
                    true,
                    2000,
                    CommandType.StoredProcedure
                ).ToList();
            }
        }


        // 2. Get Ticket By ID
        public TicketModel GetTicketByID(decimal TicketID)
        {
            using (IDbConnection connection = OpenConnection())
            {
                string sql = "ProcTicketMaster";

                var param = new
                {
                    PrcType = 2,
                    TicketID = TicketID
                };

                return connection.Query<TicketModel>(
                    sql,
                    param,
                    null,
                    true,
                    2000,
                    CommandType.StoredProcedure
                ).FirstOrDefault();
            }
        }


        // 3. Create Ticket
        public decimal AddTicket(TicketModel model)
        {
            using (IDbConnection connection = OpenConnection())
            {
                string sql = "ProcTicketMaster";

                var param = new
                {
                    PrcType = 3,
                    TicketNo = model.TicketNo,
                    Title = model.Title,
                    Description = model.Description,
                    CategoryID = model.CategoryID,
                    Priority = model.Priority,
                    CreatedBy = model.CreatedBy,

                    CompanyName = model.CompanyName,
                    ContactPerson = model.ContactPerson,
                    ContactNo = model.ContactNo,
                    EmailID = model.EmailID,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    Problem = model.Problem,
                    Remarks = model.Remarks,
                    OtherRemarks = model.OtherRemarks,
                    AssignBy = model.AssignBy,
                    AssignByName = model.AssignByName,
                    AssignedToName = model.AssignedToName,
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


        // 4. Assign Ticket
        public TicketModel AssignTicket(
        decimal TicketID,
        decimal AssignedTo,
        decimal AssignBy)
        {
            using (IDbConnection connection = OpenConnection())
            {
                string sql = "ProcTicketMaster";

                var param = new
                {
                    PrcType = 4,
                    TicketID = TicketID,
                    AssignedTo = AssignedTo,
                    AssignBy = AssignBy
                };

                return connection.Query<TicketModel>(
                    sql,
                    param,
                    null,
                    true,
                    2000,
                    CommandType.StoredProcedure
                ).FirstOrDefault();
            }
        }
        // 5. Change Ticket Status
        public TicketModel ChangeStatus(
            decimal TicketID,
            string Status,
            string OldStatus,
            decimal ChangedBy)
        {
            using (IDbConnection connection = OpenConnection())
            {
                string sql = "ProcTicketMaster";

                var param = new
                {
                    PrcType = 5,
                    TicketID = TicketID,
                    Status = Status,
                    OldStatus = OldStatus,
                    ChangedBy = ChangedBy
                };

                return connection.Query<TicketModel>(
                    sql,
                    param,
                    null,
                    true,
                    2000,
                    CommandType.StoredProcedure
                ).FirstOrDefault();
            }
        }


        // 6. Get Ticket History
        public List<dynamic> GetTicketHistory(decimal TicketID)
        {
            using (IDbConnection connection = OpenConnection())
            {
                string sql = "ProcTicketMaster";

                var param = new
                {
                    PrcType = 6,
                    TicketID = TicketID
                };

                return connection.Query(
                    sql,
                    param,
                    null,
                    true,
                    2000,
                    CommandType.StoredProcedure
                ).ToList();
            }
        }


        // 7. Get Categories
        public List<TicketModel> GetCategories()
        {
            using (IDbConnection connection = OpenConnection())
            {
                string sql = "ProcTicketMaster";

                var param = new
                {
                    PrcType = 7
                };

                return connection.Query<TicketModel>(
                    sql,
                    param,
                    null,
                    true,
                    2000,
                    CommandType.StoredProcedure
                ).ToList();
            }
        }


        // 8. Get Support Executives
        public List<UserMasterModel> GetSupportExecutives()
        {
            using (IDbConnection connection = OpenConnection())
            {
                string sql = "ProcTicketMaster";

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

        public TicketModel UpdateTicket(TicketModel model)
        {
            using (IDbConnection connection = OpenConnection())
            {
                string sql = "ProcTicketMaster";

                var param = new
                {
                    PrcType = 9,
                    TicketID = model.TicketID,
                    TicketNo = model.TicketNo,
                    Title = model.Title,
                    Description = model.Description,
                    CategoryID = model.CategoryID,
                    Priority = model.Priority,

                    CompanyName = model.CompanyName,
                    ContactPerson = model.ContactPerson,
                    ContactNo = model.ContactNo,
                    EmailID = model.EmailID,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    Problem = model.Problem,
                    Remarks = model.Remarks,
                    OtherRemarks = model.OtherRemarks,
                    AssignBy = model.AssignBy,
                    AssignedTo = model.AssignedTo,
                    AssignByName = model.AssignByName,
                    AssignedToName = model.AssignedToName,
                };

                return connection.Query<TicketModel>(
                    sql,
                    param,
                    null,
                    true,
                    2000,
                    CommandType.StoredProcedure
                ).FirstOrDefault();
            }
        }


        // 10. Delete Ticket
        public decimal DeleteTicket(decimal TicketID)
        {
            using (IDbConnection connection = OpenConnection())
            {
                string sql = "ProcTicketMaster";

                var param = new
                {
                    PrcType = 10,
                    TicketID = TicketID
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

        // =========================================================
        // Ticket Summary Report Data
        // =========================================================
        public List<dynamic> GetTicketSummaryReport(
            DateTime? FromDate,
            DateTime? ToDate,
            string Status,
            string Priority,
            string AssignedToName)
        {
            using (IDbConnection connection = OpenConnection())
            {
                string sql = "ProcReportTicketSummary";

                var param = new
                {
                    FromDate = FromDate,
                    ToDate = ToDate,
                    Status = Status,
                    Priority = Priority,
                    AssignedToName = AssignedToName
                };

                return connection.Query(
                    sql,
                    param,
                    null,
                    true,
                    2000,
                    CommandType.StoredProcedure
                ).ToList();
            }
        }

        // =========================================================
        // Ticket Performance Report Data
        // =========================================================

    public List<dynamic> GetTicketPerformanceReport(
    DateTime? FromDate,
    DateTime? ToDate,
    string Status,
    string Priority,
    string AssignedToName)
        {
            using (IDbConnection connection = OpenConnection())
            {
                string sql = "ProcReportTicketPerformance";

                var param = new
                {
                    FromDate = FromDate,
                    ToDate = ToDate,
                    Status = Status,
                    Priority = Priority,
                    AssignedToName = AssignedToName
                };

                return connection.Query(
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