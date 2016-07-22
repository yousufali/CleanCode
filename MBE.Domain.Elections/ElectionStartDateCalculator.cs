using System;
using System.Collections.Generic;
using System.Linq;
using  MBE.Domain.Elections.DataAccess;
using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections
{
    public interface IElectionStartDateCalculator
    {
        DateTime GetElectionStartDate(ElectionData electionData);
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

        public DateTime GetElectionStartDate(ElectionData electionData)
        {
            var planTypes = new List<int> {electionData.PlanTypeID};
            var elections = m_benefitElectionRepository.SelectBenefitElections(electionData.ParentUserID, planTypes);
            var dayBeforeEffectiveDate = electionData.EffectiveDate.AddDays(-1);
            var electionDayBeforeEffectiveDate =
                elections.FirstOrDefault(
                    a => a.BenefitStartDate <= dayBeforeEffectiveDate && a.BenefitEndDate >= dayBeforeEffectiveDate);
            if (electionDayBeforeEffectiveDate == null) return electionData.EffectiveDate;
            return !IsPlanWaive(electionDayBeforeEffectiveDate.PlanID) ? electionDayBeforeEffectiveDate.ElectionStartDate : electionData.EffectiveDate;
        }

        private bool IsPlanWaive(int planID)
        {
            var plan = m_planRepository.SelectClientBenefitPlan(planID);
            return plan.WaivePlan;
        }
    }
}
