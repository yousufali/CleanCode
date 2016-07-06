using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBE.Domain.Elections.Models
{
    public class TierAmountFields
    {
        public int TierID { get; set; }
        public decimal EmployeeContribution { get; set; }
        public decimal EmployerContribution { get; set; }
        public decimal PerPayCheckDeduction { get; set; }
        public decimal CoverageAmount { get; set; }
        public decimal BenefitAmount { get; set; }
    }
}
