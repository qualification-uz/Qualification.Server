using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualification.Service.DTOs.Insight
{
    public class ApplicationStatusInsightDto
    {
        public int SentCount { get; set; }
        public int PendingCount { get; set; }
        public int WaitingPaymentCount { get; set; }
        public int SuccessPaymentCount { get; set; }
        public int ApprovedPaymentCount { get; set; }
        public int ScheduledTestCount { get; set; }
        public int StartedTestCount { get; set; }
        public int CanceledCount { get; set; }
        public int CompletedCount { get; set; }
    }
}
