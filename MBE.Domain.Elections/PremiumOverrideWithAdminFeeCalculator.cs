//using System.Linq;
//using MBE.Domain.Elections.Models;

//namespace MBE.Domain.Elections
//{
//    public class PremiumOverrideWithAdminFeeCalculator : IPremiumOverrideCalculator
//    {
//        private readonly IPremiumOverrideCalculator m_premiumOverrideCalculator;
//        private readonly IBenefitElectionAdminFeeCalculator m_benefitElectionAdminFeeCalculator;
//        public PremiumOverrideWithAdminFeeCalculator(IPremiumOverrideCalculator premiumOverrideCalculator, IBenefitElectionAdminFeeCalculator benefitElectionAdminFeeCalculator)
//        {
//            m_premiumOverrideCalculator = premiumOverrideCalculator;
//            m_benefitElectionAdminFeeCalculator = benefitElectionAdminFeeCalculator;
//        }

//        public decimal GetPremiumOverride(TierAmountFields tierAmountFields, SelectedPlanAndTier selectedPlanAndTier)
//        {
//            var premiumOverride = m_premiumOverrideCalculator.GetPremiumOverride(tierAmountFields, selectedPlanAndTier);
//            var premiumOverrideAdminFee = GetPremiumOverrideAdminFee(tierAmountFields, selectedPlanAndTier);
//            return premiumOverride + premiumOverrideAdminFee;
//        }

//        private decimal GetPremiumOverrideAdminFee(TierAmountFields tierAmountFields, SelectedPlanAndTier selectedPlanAndTier)
//        {
//            var benefitElectionAdminFees = m_benefitElectionAdminFeeCalculator.GetBenefitElectionAdminFees(selectedPlanAndTier, tierAmountFields);
//            return benefitElectionAdminFees.FindAll(a => a.IncludeInPremiumOverride).Sum(fee => fee.PremiumOverride);
//        }
//    }
//}
