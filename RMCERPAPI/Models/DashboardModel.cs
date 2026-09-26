using System;

namespace RMCERPAPI.Models
{
    public class DashboardModel
    {
        public DashboardModel()
        {
            TotalTickets = 0;
            OpenTickets = 0;
            InProgressTickets = 0;
            ResolvedTickets = 0;
            ClosedTickets = 0;
            HighCriticalTickets = 0;
        }

        public int TotalTickets { get; set; }
        public int OpenTickets { get; set; }
        public int InProgressTickets { get; set; }
        public int ResolvedTickets { get; set; }
        public int ClosedTickets { get; set; }
        public int HighCriticalTickets { get; set; }
    }
}