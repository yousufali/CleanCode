using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBE.Domain.Elections.DataAccess;
using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections
{
    public interface IImputedIncomeCalculator
    {
        decimal GetImputedIncomeMonthly(ElectionData electionData);
    }
    
    public class ImputedIncomeCalculator : IImputedIncomeCalculator
    {
        private readonly ILifeImputedIncomeCalculator m_lifeImputedIncomeCalculator;
        private readonly INonLifeImputedIncomeCalculator m_nonLifeImputedIncomeCalculator;
        private readonly IPlanTypeRepository m_planTypeRepository;
        public ImputedIncomeCalculator(ILifeImputedIncomeCalculator lifeImputedIncomeCalculator, INonLifeImputedIncomeCalculator nonLifeImputedIncomeCalculator, IPlanTypeRepository planTypeRepository)
        {
            m_lifeImputedIncomeCalculator = lifeImputedIncomeCalculator;
            m_nonLifeImputedIncomeCalculator = nonLifeImputedIncomeCalculator;
            m_planTypeRepository = planTypeRepository;
        }
        public decimal GetImputedIncomeMonthly(ElectionData electionData)
        {
            if (IsLifeImputedIncomeHolderPlanType(electionData.ClientID, electionData.PlanTypeID))
            {
                return m_lifeImputedIncomeCalculator.GetImputedIncomeMonthly(electionData);
            }
            else
            {
                return m_nonLifeImputedIncomeCalculator.GetImputedIncomeMonthly(electionData);
            }
        }

        private bool IsLifeImputedIncomeHolderPlanType(int clientID, int planTypeID)
        {
            var planTypes = m_planTypeRepository.SelectPlanTypes(clientID);
            var planType = planTypes.FirstOrDefault(a => a.PlanTypeID == planTypeID);
            if (planType != null) return planType.LifeImputedIncomeHolder;
            return false;
        }
    }
}
