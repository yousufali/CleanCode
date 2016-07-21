using MBE.Domain.Elections.DataAccess;
using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections.PremiumOverride
{
    public interface INonAgeBandedPremiumOverrideCalculator
    {
        decimal GetPremiumOverride(ElectionData electionData);
    }

    public class NonAgeBandedPremiumOverrideCalculator : INonAgeBandedPremiumOverrideCalculator
    {
        private readonly ITierCostRepository m_tierCostRepository;
        public NonAgeBandedPremiumOverrideCalculator(ITierCostRepository tierCostRepository)
        {
            m_tierCostRepository = tierCostRepository;
        }

        public decimal GetPremiumOverride(ElectionData electionData)
        {
            var tier = m_tierCostRepository.SelectTierCost(electionData.TierID);
            return tier.PremiumOverride;
        }

    }
}
