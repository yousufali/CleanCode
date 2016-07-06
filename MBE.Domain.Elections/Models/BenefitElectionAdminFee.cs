using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBE.Domain.Elections.Models
{
    public class BenefitElectionAdminFee
    {
        public int AdminFeeID { get; set; }
        public decimal FeeAmount { get; set; }
        public decimal EmployerMonthlyCost { get; set; }
        public decimal EmployeeMonthlyCost { get; set; }
        public decimal Premium { get; set; }
        public decimal PremiumOverride { get; set; }
        public bool IncludeInERCost { get; set; }
        public bool IncludeInEECost { get; set; }
        public bool IncludeInPremium { get; set; }
        public bool IncludeInPremiumOverride { get; set; }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            BenefitElectionAdminFee p = obj as BenefitElectionAdminFee;
            if ((System.Object)p == null)
            {
                return false;
            }
            return (AdminFeeID == p.AdminFeeID) && (FeeAmount == p.FeeAmount) && (EmployerMonthlyCost == p.EmployerMonthlyCost) && (EmployeeMonthlyCost == p.EmployeeMonthlyCost) 
                && (Premium == p.Premium) && (PremiumOverride == p.PremiumOverride) && (IncludeInERCost == p.IncludeInERCost) && (IncludeInEECost == p.IncludeInEECost)
                 && (IncludeInPremium == p.IncludeInPremium) && (IncludeInPremiumOverride == p.IncludeInPremiumOverride);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
