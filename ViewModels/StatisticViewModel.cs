using Reports.Models;
using System.Linq;

namespace Reports.ViewModels
{
    public class StatisticViewModel
    {
        public string? FullName { get; set; }
        public string? GuardID { get; set; }
        public int Count { get; set; }
        public int Reported { get; set; }
        public int Paid { get; set; }
        public decimal Total { get; set; }

        public StatisticViewModel(Guard guard)
        {
            FullName = guard.FullName; 
            GuardID = guard.GuardID;
            Count = guard.Reports.Count;
            Reported = guard.Reports.Where(r => r.IsReported).Count();
            Paid = guard.Reports.Where(r => r.IsPaid).Count();
            Total = guard.Reports.Sum(r => r.Total);
        }
    }
}