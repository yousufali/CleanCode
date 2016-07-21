using MBE.Domain.Elections.DataAccess;

namespace MBE.Domain.Elections.Tier
{
    public interface IBaseTierCalculator
    {
        int GetBaseTierID(int planID, int tierID);
    }

    public  class BaseTierCalculator : IBaseTierCalculator
    {
        private readonly IPlanRepository m_planRepository;
        private readonly ITierCostsAgeBandRepository m_tierCostsAgeBandRepository;
        public BaseTierCalculator(IPlanRepository planRepository, ITierCostsAgeBandRepository tierCostsAgeBandRepository)
        {
            m_planRepository = planRepository;
            m_tierCostsAgeBandRepository = tierCostsAgeBandRepository;
        }
        public int GetBaseTierID(int planID, int tierID)
        {
            var plan = m_planRepository.SelectClientBenefitPlan(planID);
            if (!plan.AgeBanding) return tierID;
            var tierCostAgeBand = m_tierCostsAgeBandRepository.SelectTierCostsAgeBand(tierID);
            return tierCostAgeBand.BaseTierID;
        }
    }
}
