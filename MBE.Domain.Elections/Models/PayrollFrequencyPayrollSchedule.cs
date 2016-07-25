using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBE.Domain.Elections.Models
{
    public class PayrollFrequencyPayrollSchedule
    {
        public int PayrollFrequencyPayrollScheduleID { get; set; }
        public int PayrollFrequencyID { get; set; }
        public int PayrollScheduleID { get; set; }
    }
    public class PayrollPlanTypeFrequency
    {
        public int PayrollFrequencyID { get; set; }
        public int PlanTypeMask { get; set; }
    }

    public class PayrollFrequency
    {
        public int PayrollFrequencyID { get; set; }
        public int PayPeriods { get; set; }
        public int EmployeeEmployerOrBoth { get; set; }
        public bool IsByPlanType { get; set; }
    }
}
