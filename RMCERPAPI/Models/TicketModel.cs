using System;

namespace RMCERPAPI.Models
{
    public class TicketModel
    {
        public TicketModel()
        {
            TicketID = 0;
            TicketNo = "";
            Title = "";
            Description = "";
            CategoryID = 0;
            CategoryName = "";
            Priority = "";
            CreatedDate = DateTime.Now;
            Status = "Open";
            AssignedTo = 0;
            AssignedToName = "";
            CreatedBy = 0;
            CreatedByName = "";
            AssignedDate = null;
            ResolvedDate = null;
            ClosedDate = null;
            CompanyName = "";
            ContactPerson = "";
            ContactNo = "";
            EmailID = "";
            StartDate = null;
            EndDate = null;
            Problem = "";
            Remarks = "";
            OtherRemarks = "";
            AssignBy = 0;
            AssignByName = "";
        }

        public decimal TicketID { get; set; }

        public string TicketNo { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal CategoryID { get; set; }

        // For displaying category name after JOIN
        public string CategoryName { get; set; }

        public string Priority { get; set; }

        public decimal? ChangedBy { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string Status { get; set; }

        public decimal? AssignedTo { get; set; }

        // For displaying assigned employee/user name after JOIN
        public string AssignedToName { get; set; }

        public decimal CreatedBy { get; set; }

        // For displaying creator name after JOIN
        public string CreatedByName { get; set; }

        public DateTime? AssignedDate { get; set; }

        public DateTime? ResolvedDate { get; set; }

        public DateTime? ClosedDate { get; set; }
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNo { get; set; }
        public string EmailID { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string Problem { get; set; }
        public string Remarks { get; set; }
        public string OtherRemarks { get; set; }

        public decimal? AssignBy { get; set; }
        public string AssignByName { get; set; }
    }
}