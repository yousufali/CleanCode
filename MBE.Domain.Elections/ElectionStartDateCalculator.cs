using System;
using System.Collections.Generic;
using System.Linq;
using  MBE.Domain.Elections.DataAccess;

namespace MBE.Domain.Elections
{
    public interface IElectionStartDateCalculator
    {
        DateTime GetElectionStartDate(int userID, int planTypeID, DateTime effectiveDate);
    }
    public class ElectionStartDateCalculator : IElectionStartDateCalculator
    {
        private readonly IPlanRepository m_planRepository;
        private readonly IBenefitElectionRepository m_benefitElectionRepository;
        public ElectionStartDateCalculator(IPlanRepository planRepository,
            IBenefitElectionRepository benefitElectionRepository)
        {
            m_planRepository = planRepository;
            m_benefitElectionRepository = benefitElectionRepository;
        }

        public DateTime GetElectionStartDate(int userID, int planTypeID, DateTime effectiveDate)
        {
            var planTypes = new List<int> {planTypeID};
            var elections = m_benefitElectionRepository.SelectBenefitElections(userID, planTypes);
            var dayBeforeEffectiveDate = effectiveDate.AddDays(-1);
            var electionDayBeforeEffectiveDate =
                elections.FirstOrDefault(
                    a => a.BenefitStartDate <= dayBeforeEffectiveDate && a.BenefitEndDate >= dayBeforeEffectiveDate);
            if (electionDayBeforeEffectiveDate == null) return effectiveDate;
            return !IsPlanWaive(electionDayBeforeEffectiveDate.PlanID) ? electionDayBeforeEffectiveDate.ElectionStartDate : effectiveDate;
        }

        private bool IsPlanWaive(int planID)
        {
            var plan = m_planRepository.SelectClientBenefitPlan(planID);
            return plan.WaivePlan;
        }
    }
}
