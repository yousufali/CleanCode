using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBE.Domain.Elections.Models;
using MBE.Domain.Elections.DataAccess;

namespace MBE.Domain.Elections
{
    public interface ILifeImputedIncomeCalculator
    {
        decimal GetImputedIncomeAmount(List<CoverageAmountForImputedIncomeCalculation> coverageAmounts);
    }

    public class LifeImputedIncomeCalculator
    {
        private ITierRepository m_tierRepository;
        private ILifeImputedIncomeCoverageCalculator m_lifeImputedIncomeCoverageCalculator;

        public LifeImputedIncomeCalculator(ITierRepository tierRepository, ILifeImputedIncomeCoverageCalculator lifeImputedIncomeCoverageCalculator)
        {
            m_tierRepository = tierRepository;
            m_lifeImputedIncomeCoverageCalculator = lifeImputedIncomeCoverageCalculator;
        }

        public decimal GetImputedIncomeAmount(List<CoverageAmountForImputedIncomeCalculation> coverageAmounts)
        {
            throw new NotImplementedException();
        }
    }
}
