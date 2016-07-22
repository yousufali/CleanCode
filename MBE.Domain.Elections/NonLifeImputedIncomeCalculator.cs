using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections
{
    public interface INonLifeImputedIncomeCalculator
    {
        decimal GetImputedIncomeMonthly(ElectionData electionData);
    }
    public class NonLifeImputedIncomeCalculator : INonLifeImputedIncomeCalculator
    {
        private readonly ITierAfterTaxAndImputedIncomeSelector m_tierAfterTaxAndImputedIncomeSelector;

        public NonLifeImputedIncomeCalculator(ITierAfterTaxAndImputedIncomeSelector tierAfterTaxAndImputedIncomeSelector)
        {
            m_tierAfterTaxAndImputedIncomeSelector = tierAfterTaxAndImputedIncomeSelector;
        }
        public decimal GetImputedIncomeMonthly(ElectionData electionData)
        {
            var tierAfterTaxAndImputedIncome = m_tierAfterTaxAndImputedIncomeSelector.GetEligibleRecord(electionData);
            return tierAfterTaxAndImputedIncome.ImputedIncome;
        }
    }
}
