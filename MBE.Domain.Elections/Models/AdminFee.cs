using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBE.Domain.Elections.Models
{
    public enum AdminFeeCalculationType
    {
        FlatFee = 1,
        Percentage = 2
    }
    public class AdminFee
    {
        public int AdminFeeID { get; set; }
        public int PlanID { get; set; }
        public int TierID { get; set; }
        public int AdminFeeTypeID { get; set; }
        public int AdminFeeCalculationTypeID { get; set; }
        public decimal Fee { get; set; }
        public bool IncludeInERCost { get; set; }
        public bool IncludeInEECost { get; set; }
        public bool IncludeInPremium { get; set; }
        public bool IncludeInPremiumOverride { get; set; }
    }
}
