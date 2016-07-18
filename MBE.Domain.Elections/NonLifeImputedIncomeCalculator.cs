using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBE.Domain.Elections.Models;
using MBE.Domain.Elections.DataAccess;

namespace MBE.Domain.Elections
{
    public interface INonLifeImputedIncomeCalculator
    {
        decimal GetImputedIncomeMonthly(SelectedPlanAndTier setupData);
    }
    public class NonLifeImputedIncomeCalculator
    {
        private ITierRepository m_tierRepository;
        private ITierAfterTaxAndImputedIncomeSelector m_tierAfterTaxAndImputedIncomeSelector;
        public NonLifeImputedIncomeCalculator(ITierRepository tierRepository, ITierAfterTaxAndImputedIncomeSelector tierAfterTaxAndImputedIncomeSelector)
        {
            m_tierRepository = tierRepository;
            m_tierAfterTaxAndImputedIncomeSelector = tierAfterTaxAndImputedIncomeSelector;
        }
        public decimal GetImputedIncomeMonthly(SelectedPlanAndTier setupData, List<CoveredDependent> coveredDependents)
        {
            var tierAfterTaxAndImputedIncomeList = m_tierRepository.GetTierAfterTaxAmdImputedIncome(setupData.Tier.TierID, setupData.AgeBanding);
            var tierAfterTaxAndImputedIncome = m_tierAfterTaxAndImputedIncomeSelector.GetEligibleRecord(tierAfterTaxAndImputedIncomeList, coveredDependents);
            return tierAfterTaxAndImputedIncome.ImputedIncome;
        }
    }
}
