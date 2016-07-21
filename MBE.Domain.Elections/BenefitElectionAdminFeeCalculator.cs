using System.Collections.Generic;
using MBE.Domain.Elections.DataAccess;
using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections
{
    public interface IBenefitElectionAdminFeeCalculator
    {
        List<BenefitElectionAdminFee> GetBenefitElectionAdminFees(ElectionData electionData);
    }

    public class BenefitElectionAdminFeeCalculator : IBenefitElectionAdminFeeCalculator
    {
        private readonly IBasicPremiumOverrideCalculator m_premiumOverrideCalculator;
        private readonly IAdminFeeRepository m_adminFeeRepository;
        private ElectionData m_electionData;
        public BenefitElectionAdminFeeCalculator(IBasicPremiumOverrideCalculator premiumOverrideCalculator, IAdminFeeRepository adminFeeRepository)
        {
            m_premiumOverrideCalculator = premiumOverrideCalculator;
            m_adminFeeRepository = adminFeeRepository;
        }

        public List<BenefitElectionAdminFee> GetBenefitElectionAdminFees(ElectionData electionData)
        {
            m_electionData = electionData;
            var premiumOverride = m_premiumOverrideCalculator.GetPremiumOverride(electionData);
            var adminFees = m_adminFeeRepository.SelectAdminFees(electionData.PlanID, electionData.TierID);
            var benefitElectionAdminFees = new List<BenefitElectionAdminFee>();
            foreach(AdminFee adminFee in adminFees)
            {
                if (adminFee.AdminFeeCalculationTypeID == (int)AdminFeeCalculationType.FlatFee)
                {
                    benefitElectionAdminFees.Add(GetFlatBenefitElectionAdminFee(adminFee));
                }
                else
                {
                    benefitElectionAdminFees.Add(GetNonFlatBenefitElectionAdminFee(adminFee, premiumOverride));
                }
            }
            return benefitElectionAdminFees;
        }

        private BenefitElectionAdminFee GetFlatBenefitElectionAdminFee(AdminFee adminFee)
        {
            var benefitElectionAdminFee = InitializeBenefitElectionAdminFee(adminFee);
            benefitElectionAdminFee.EmployeeMonthlyCost = GetEmployeeCostAdminFee(adminFee, adminFee.Fee);
            benefitElectionAdminFee.EmployerMonthlyCost = GetEmployerCostAdminFee(adminFee, adminFee.Fee);
            benefitElectionAdminFee.Premium = GetPremiumAdminFee(adminFee, adminFee.Fee);
            benefitElectionAdminFee.PremiumOverride = GetPremiumOverrideAdminFee(adminFee, adminFee.Fee);
            return benefitElectionAdminFee;
        }

        private BenefitElectionAdminFee GetNonFlatBenefitElectionAdminFee(AdminFee adminFee, decimal premiumOverride)
        {
            var benefitElectionAdminFee = InitializeBenefitElectionAdminFee(adminFee);
            benefitElectionAdminFee.EmployeeMonthlyCost = GetEmployeeCostAdminFee(adminFee, CalculatePercentageAdminFee(adminFee.Fee, m_electionData.BasicEmployeeMonthlyCost));
            benefitElectionAdminFee.EmployerMonthlyCost = GetEmployerCostAdminFee(adminFee, CalculatePercentageAdminFee(adminFee.Fee, m_electionData.BasicEmployerMonthlyCost));
            benefitElectionAdminFee.Premium = GetPremiumAdminFee(adminFee, CalculatePercentageAdminFee(adminFee.Fee, GetPremium()));
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

        private decimal GetPremium()
        {
            return m_electionData.BasicEmployeeMonthlyCost + m_electionData.BasicEmployerMonthlyCost;
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
