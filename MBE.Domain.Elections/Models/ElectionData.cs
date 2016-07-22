using System;
using System.Collections.Generic;

namespace MBE.Domain.Elections.Models
{
    public class ElectionData
    {
        public int ClientID { get; set; }
        public int ParentUserID { get; set; }
        public DateTime EffectiveDate { get; set; }
        public int PlanTypeID { get; set; }
        public int PlanID { get; set; }
        public int TierID { get; set; }
        public List<CoveredUser> CoveredUsers { get; set; }
        public decimal BasicEmployeeMonthlyCost { get; set; }
        public decimal BasicEmployerMonthlyCost { get; set; }
        public decimal BasicPremiumCost { get; set; }
        public decimal Coverage { get; set; }
    }
}
