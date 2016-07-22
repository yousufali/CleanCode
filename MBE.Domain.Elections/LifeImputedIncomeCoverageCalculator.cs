using System.Collections.Generic;
using System.Linq;
using MBE.Domain.Elections.DataAccess;
using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections
{
    public interface ILifeImputedIncomeCoverageCalculator
    {
        decimal GetImputedIncomeCoverage(ElectionData electionData);
    }
    public class LifeImputedIncomeCoverageCalculator : ILifeImputedIncomeCoverageCalculator
    {
        private readonly IBenefitElectionRepository m_benefitElectionRepository;
        private readonly IPlanTypeRepository m_planTypeRepository;
        private ElectionData m_electionData;
        public LifeImputedIncomeCoverageCalculator(IBenefitElectionRepository benefitElectionRepository, IPlanTypeRepository planTypeRepository)
        {
            m_benefitElectionRepository = benefitElectionRepository;
            m_planTypeRepository = planTypeRepository;
        }

        private const decimal MinimumCoverageWithoutImputedIncome = 50000;

        public decimal GetImputedIncomeCoverage(ElectionData electionData)
        {
            m_electionData = electionData;
            var coverageSum = CalculateTotalCoverageOfImputedIncomePlanTypes();
            return GetImputedIncomeCoverage(coverageSum);
        }

        private decimal CalculateTotalCoverageOfImputedIncomePlanTypes()
        {
            var imputedIncomePlanTypes = GetImputedIncomePlanTypes();
            var benefitElections = m_benefitElectionRepository.SelectBenefitElections(m_electionData.ParentUserID, imputedIncomePlanTypes);
            return benefitElections.Sum(a => a.Coverage);
        }

        private List<int> GetImputedIncomePlanTypes()
        {
            var planTypes = m_planTypeRepository.SelectPlanTypes(m_electionData.ClientID);
            return planTypes.FindAll(a => a.IncludeInLifeImputedIncome).Select(a => a.PlanTypeID).ToList();
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
