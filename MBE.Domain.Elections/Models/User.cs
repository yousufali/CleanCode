using System;

namespace MBE.Domain.Elections.Models
{
    public class User
    {
        public int UserID { get; set; }
        public int ClientID { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class Employee
    {
        public int UserID { get; set; }
        public int PayPeriodsPerAnnum { get; set; }
        public int PayrollScheduleID { get; set; }
        public string PayrollExportUniqueID { get; set; }
    }
}
