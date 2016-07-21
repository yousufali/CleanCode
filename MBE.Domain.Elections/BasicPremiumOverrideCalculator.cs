using MBE.Domain.Elections.DataAccess;
using MBE.Domain.Elections.Models;
using MBE.Domain.Elections.PremiumOverride;

namespace MBE.Domain.Elections
{
    public interface IBasicPremiumOverrideCalculator
    {
        decimal GetPremiumOverride(ElectionData electionData);
    }
    public class BasicPremiumOverrideCalculator : IBasicPremiumOverrideCalculator
    {
        private readonly IAgeBandedPremiumOverrideCalculator m_ageBandedPremiumOverrideCalculator;
        private readonly INonAgeBandedPremiumOverrideCalculator m_nonAgeBandedPremiumOverrideCalculator;
        private readonly IPlanRepository m_planRepository;
        public BasicPremiumOverrideCalculator(INonAgeBandedPremiumOverrideCalculator nonAgeBandedPremiumOverrideCalculator,
            IAgeBandedPremiumOverrideCalculator ageBandedPremiumOverrideCalculator, IPlanRepository planRepository)
        {
            m_planRepository = planRepository;
            m_nonAgeBandedPremiumOverrideCalculator = nonAgeBandedPremiumOverrideCalculator;
            m_ageBandedPremiumOverrideCalculator = ageBandedPremiumOverrideCalculator;
        }
        public decimal GetPremiumOverride(ElectionData electionData)
        {
            var plan = m_planRepository.SelectClientBenefitPlan(electionData.PlanID);
            if (plan.AgeBanding)
            {
                return m_ageBandedPremiumOverrideCalculator.GetPremiumOverride(electionData);
            }
            else
            {
                return m_nonAgeBandedPremiumOverrideCalculator.GetPremiumOverride(electionData);
            }
        }
    }
}
