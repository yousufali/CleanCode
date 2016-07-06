using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections
{
    public interface IImputedIncomeCalculator
    {
        decimal GetImputedIncomeMonthly(SelectedPlanAndTier setupData);
    }
    
    public class ImputedIncomeCalculator : IImputedIncomeCalculator
    {
        private ILifeImputedIncomeCalculator m_lifeImputedIncomeCalculator;
        private INonLifeImputedIncomeCalculator m_nonLifeImputedIncomeCalculator;
        public ImputedIncomeCalculator(ILifeImputedIncomeCalculator lifeImputedIncomeCalculator, INonLifeImputedIncomeCalculator nonLifeImputedIncomeCalculator)
        {
            m_lifeImputedIncomeCalculator = lifeImputedIncomeCalculator;
            m_nonLifeImputedIncomeCalculator = nonLifeImputedIncomeCalculator;
        }
        public decimal GetImputedIncomeMonthly(SelectedPlanAndTier setupData)
        {
            if (setupData.ClientPlanOrder.LifeImputedIncomeHolder)
            {
                return m_lifeImputedIncomeCalculator.GetImputedIncomeMonthly(setupData);
            }
            else
            {
                return m_nonLifeImputedIncomeCalculator.GetImputedIncomeMonthly(setupData);
            }
        }
    }
}
