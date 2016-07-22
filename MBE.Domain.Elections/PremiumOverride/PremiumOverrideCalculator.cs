using System.Linq;
using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections.PremiumOverride
{
    public interface IPremiumOverrideCalculator
    {
        decimal GetPremiumOverride(ElectionData electionData);
    }
    public class PremiumOverrideCalculator : IPremiumOverrideCalculator
    {
        private readonly IBasicPremiumOverrideCalculator m_basicPremiumOverrideCalculator;
        private readonly IBenefitElectionAdminFeeCalculator m_benefitElectionAdminFeeCalculator;

        public PremiumOverrideCalculator(IBasicPremiumOverrideCalculator basicPremiumOverrideCalculator, IBenefitElectionAdminFeeCalculator benefitElectionAdminFeeCalculator)
        {
            m_basicPremiumOverrideCalculator = basicPremiumOverrideCalculator;
            m_benefitElectionAdminFeeCalculator = benefitElectionAdminFeeCalculator;
        }

        public decimal GetPremiumOverride(ElectionData electionData)
        {
            var basicPremiumOverride = m_basicPremiumOverrideCalculator.GetPremiumOverride(electionData);
            var benefitElectionAdminFees = m_benefitElectionAdminFeeCalculator.GetBenefitElectionAdminFees(electionData);
            var adminFeePremiumOverride = benefitElectionAdminFees.FindAll(a => a.IncludeInPremiumOverride).Sum(a => a.PremiumOverride);
            return basicPremiumOverride + adminFeePremiumOverride;
        }

    }
}
