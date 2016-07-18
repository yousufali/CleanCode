using System.Collections.Generic;
using System.Linq;
using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections
{
    public interface ILifeImputedIncomeCoverageCalculator
    {
        decimal GetImputedIncomeCoverage(List<CoverageAmountForImputedIncomeCalculation> coverageAmounts);
    }
    public class LifeImputedIncomeCoverageCalculator : ILifeImputedIncomeCoverageCalculator
    {
        private const decimal MinimumCoverageWithoutImputedIncome = 50000;

        public decimal GetImputedIncomeCoverage(List<CoverageAmountForImputedIncomeCalculation> coverageAmounts)
        {
            var coverageSum = coverageAmounts.Sum(a => a.Coverage);
            return GetImputedIncomeCoverage(coverageSum);
        }

        private decimal GetImputedIncomeCoverage(decimal totalCoverage)
        {
            if (totalCoverage < MinimumCoverageWithoutImputedIncome) {
                return 0;
            } else
            {
                return totalCoverage - MinimumCoverageWithoutImputedIncome;
            }
        }
    }   
}
