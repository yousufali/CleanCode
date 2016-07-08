using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBE.Domain.Elections.Models
{
    public class CoverageAmountForImputedIncomeCalculation
    {
        public int PlanTypeID { get; set; }
        public DateTime StartDate { get; set; }
        public decimal Coverage { get; set; }

    }
}
