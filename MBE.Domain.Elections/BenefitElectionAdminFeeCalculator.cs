using System.Collections.Generic;
using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections
{
    public interface IBenefitElectionAdminFeeCalculator
    {
        List<BenefitElectionAdminFee> GetBenefitElectionAdminFees(SelectedPlanAndTier setupData, TierAmountFields tierAmountFields);
    }

    public class BenefitElectionAdminFeeCalculator : IBenefitElectionAdminFeeCalculator
    {
        private IPremiumOverrideCalculator m_premiumOverrideCalculator;
        public BenefitElectionAdminFeeCalculator(IPremiumOverrideCalculator premiumOverrideCalculator)
        {
            m_premiumOverrideCalculator = premiumOverrideCalculator;
        }

        public List<BenefitElectionAdminFee> GetBenefitElectionAdminFees(SelectedPlanAndTier setupData, TierAmountFields tierAmountFields)
        {
            var premiumOverride = m_premiumOverrideCalculator.GetPremiumOverride(tierAmountFields, setupData);
            var benefitElectionAdminFees = new List<BenefitElectionAdminFee>();
            foreach(AdminFee adminFee in setupData.AdminFees)
            {
                if (adminFee.AdminFeeCalculationTypeID == (int)AdminFeeCalculationType.FlatFee)
                {
                    benefitElectionAdminFees.Add(GetFlatBenefitElectionAdminFee(adminFee, premiumOverride));
                }
                else
                {
                    benefitElectionAdminFees.Add(GetNonFlatBenefitElectionAdminFee(adminFee, tierAmountFields, premiumOverride));
                }
            }
            return benefitElectionAdminFees;
        }

        private BenefitElectionAdminFee GetFlatBenefitElectionAdminFee(AdminFee adminFee, decimal premiumOverride)
        {
            var benefitElectionAdminFee = InitializeBenefitElectionAdminFee(adminFee);
            benefitElectionAdminFee.EmployeeMonthlyCost = GetEmployeeCostAdminFee(adminFee, adminFee.Fee);
            benefitElectionAdminFee.EmployerMonthlyCost = GetEmployerCostAdminFee(adminFee, adminFee.Fee);
            benefitElectionAdminFee.Premium = GetPremiumAdminFee(adminFee, adminFee.Fee);
            benefitElectionAdminFee.PremiumOverride = GetPremiumOverrideAdminFee(adminFee, adminFee.Fee);
            return benefitElectionAdminFee;
        }

        private BenefitElectionAdminFee GetNonFlatBenefitElectionAdminFee(AdminFee adminFee, TierAmountFields tierAmountFields, decimal premiumOverride)
        {
            var benefitElectionAdminFee = InitializeBenefitElectionAdminFee(adminFee);
            benefitElectionAdminFee.EmployeeMonthlyCost = GetEmployeeCostAdminFee(adminFee, CalculatePercentageAdminFee(adminFee.Fee, tierAmountFields.EmployeeContribution));
            benefitElectionAdminFee.EmployerMonthlyCost = GetEmployerCostAdminFee(adminFee, CalculatePercentageAdminFee(adminFee.Fee, tierAmountFields.EmployerContribution));
            benefitElectionAdminFee.Premium = GetPremiumAdminFee(adminFee, CalculatePercentageAdminFee(adminFee.Fee, GetPremium(tierAmountFields)));
            benefitElectionAdminFee.PremiumOverride = GetPremiumOverrideAdminFee(adminFee, CalculatePercentageAdminFee(adminFee.Fee, premiumOverride));
            return benefitElectionAdminFee;
        }

        private BenefitElectionAdminFee InitializeBenefitElectionAdminFee(AdminFee adminFee)
        {
            return new BenefitElectionAdminFee()
            {
                AdminFeeID = adminFee.AdminFeeID,
                FeeAmount = adminFee.Fee,
                IncludeInEECost = adminFee.IncludeInEECost,
                IncludeInERCost = adminFee.IncludeInERCost,
                IncludeInPremium = adminFee.IncludeInPremium,
                IncludeInPremiumOverride = adminFee.IncludeInPremiumOverride
            };
        }

        private decimal GetPremium(TierAmountFields tierAmountFields)
        {
            return tierAmountFields.EmployeeContribution + tierAmountFields.EmployerContribution;
        }

        private decimal CalculatePercentageAdminFee(decimal fee, decimal amount)
        {
            return (fee * amount) / 100;
        }

        private decimal GetEmployeeCostAdminFee(AdminFee adminFee, decimal employeeCost)
        {
            if (adminFee.IncludeInEECost) { return employeeCost; }
            return 0;
        }

        private decimal GetEmployerCostAdminFee(AdminFee adminFee, decimal employerCost)
        {
            if (adminFee.IncludeInERCost) return employerCost;
            return 0;
        }

        private decimal GetPremiumAdminFee(AdminFee adminFee, decimal premium)
        {
            if (adminFee.IncludeInPremium) return premium;
            return 0;
        }

        private decimal GetPremiumOverrideAdminFee(AdminFee adminFee, decimal premiumOverride)
        {
            if (adminFee.IncludeInPremiumOverride) return premiumOverride;
            return 0;
        }
    }
}
