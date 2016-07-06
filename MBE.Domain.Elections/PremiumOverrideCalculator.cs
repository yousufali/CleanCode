using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections
{
    public interface IPremiumOverrideCalculator
    {
        decimal GetPremiumOverride(TierAmountFields tierAmountFields, SelectedPlanAndTier selectedPlanAndTier);
    }
    public class PremiumOverrideCalculator : IPremiumOverrideCalculator
    {
        public decimal GetPremiumOverride(TierAmountFields tierAmountFields, SelectedPlanAndTier selectedPlanAndTier)
        {
            if (selectedPlanAndTier.AgeBanding)
            {
                return GetPremiumOverrideForAgeBandedTier(tierAmountFields, selectedPlanAndTier.Tier);
            }
            else
            {
                return selectedPlanAndTier.Tier.PremiumOverride;
            }
        }

        private decimal GetPremiumOverrideForAgeBandedTier(TierAmountFields tierAmountFields, ITierBasicInfo tier)
        {
            if (tier.FlatRate)
            {
                return tier.PremiumOverride;
            }else
            {
                return GetPremiumOverrideForAgeBandedAndNotFlatRateTier(tierAmountFields, tier);
            }
        }

        private decimal GetPremiumOverrideForAgeBandedAndNotFlatRateTier(TierAmountFields tierAmountFields, ITierBasicInfo tier)
        {
            if (tier.Per > 0)
            {
                return CalculatePremiumOverrideForNotAgeBandedAndNotFlatRateTier(tierAmountFields, tier);
            }
            else
            {
                return 0;
            }
        }

        private decimal CalculatePremiumOverrideForNotAgeBandedAndNotFlatRateTier(TierAmountFields tierAmountFields, ITierBasicInfo tier)
        {
            return (tier.PremiumOverride * tierAmountFields.CoverageAmount) / tier.Per;
        }

    }
}
