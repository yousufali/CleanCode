using MBE.Domain.Elections.DataAccess;
using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections.PremiumOverride
{
    public interface IAgeBandedPremiumOverrideCalculator
    {
        decimal GetPremiumOverride(ElectionData electionData);
    }

    public class AgeBandedPremiumOverrideCalculator
    {
        private readonly ITierCostsAgeBandRepository m_tierCostsAgeBandRepository;
        private ElectionData m_electionData;

        public AgeBandedPremiumOverrideCalculator(ITierCostsAgeBandRepository tierCostsAgeBandRepository)
        {
            m_tierCostsAgeBandRepository = tierCostsAgeBandRepository;
        }

        public decimal GetPremiumOverride(ElectionData electionData)
        {
            m_electionData = electionData;
            var tier = m_tierCostsAgeBandRepository.SelectTierCostsAgeBand(electionData.TierID);
            return tier.FlatRate ? tier.PremiumOverride : CalcPremiumOverrideForNotFlatRateTier(tier);
        }

        private decimal CalcPremiumOverrideForNotFlatRateTier(TierCostsAgeBand tier)
        {
            return tier.Per > 0 ? CalcPremiumOverrideForNotAgeBandedAndNotFlatRateTier(tier) : 0;
        }

        private decimal CalcPremiumOverrideForNotAgeBandedAndNotFlatRateTier(TierCostsAgeBand tier)
        {
            return (tier.PremiumOverride * m_electionData.Coverage) / tier.Per;
        }
    }
}
