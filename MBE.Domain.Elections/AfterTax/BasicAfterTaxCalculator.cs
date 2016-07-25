using MBE.Domain.Elections.DataAccess;
using MBE.Domain.Elections.Models;
using MBE.Domain.Elections.User;

namespace MBE.Domain.Elections.AfterTax
{
    public interface IBasicAfterTaxCalculator
    {
        decimal GetAfterTaxmonthly(ElectionData electionData);
    }
    public class BasicBasicAfterTaxCalculator : IBasicAfterTaxCalculator
    {
        private readonly IPlanRepository m_planRepository;
        private readonly ITierCostRepository m_tierCostRepository;
        private readonly ITierCostsAgeBandRepository m_tierCostsAgeBandRepository;
        private readonly IUserRateDiscriminatorCalculator m_userRateDiscriminatorCalculator;
        public BasicBasicAfterTaxCalculator(IPlanRepository planRepository, ITierCostRepository tierCostRepository, ITierCostsAgeBandRepository tierCostsAgeBandRepository, IUserRateDiscriminatorCalculator userRateDiscriminatorCalculator)
        {
            m_planRepository = planRepository;
            m_tierCostRepository = tierCostRepository;
            m_tierCostsAgeBandRepository = tierCostsAgeBandRepository;
            m_userRateDiscriminatorCalculator = userRateDiscriminatorCalculator;
        }

        public decimal GetAfterTaxmonthly(ElectionData electionData)
        {
            var plan = m_planRepository.SelectClientBenefitPlan(electionData.PlanID);
            if (plan.AgeBanding)
            {
                var tierCostsAgeBand = m_tierCostsAgeBandRepository.SelectTierCostsAgeBand(electionData.TierID);
                return tierCostsAgeBand.AfterTax;
            }
            var rateDiscriminator = m_userRateDiscriminatorCalculator.GetRateDiscriminator(
                electionData.ParentUserID, electionData.PlanTypeID);
            var tierCost = m_tierCostRepository.SelectTierCost(electionData.TierID);
            return tierCost.AfterTax;
        }
    }
}
