using System.Collections.Generic;
using System.Linq;
using MBE.Domain.Elections.DataAccess;
using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections.PreviousPlan
{
    public interface IPreviousPlanCalculator
    {
        Models.PreviousPlan SelectPreviousPlan(ElectionData electionData);
    }
    public class PreviousPlanCalculator : IPreviousPlanCalculator
    {
        private readonly IBenefitElectionRepository m_benefitElectionRepository;

        public PreviousPlanCalculator(IBenefitElectionRepository benefitElectionRepository)
        {
            m_benefitElectionRepository = benefitElectionRepository;
        }

        public Models.PreviousPlan SelectPreviousPlan(ElectionData electionData)
        {
            var planTypes = new List<int>() {electionData.PlanTypeID};
            var benefitElections = m_benefitElectionRepository.SelectBenefitElections(electionData.ParentUserID,
                planTypes);
            var benefitElection =  benefitElections.OrderByDescending(a => a.BenefitEndDate).FirstOrDefault();
            if (benefitElection == null) return new Models.PreviousPlan();
            return new Models.PreviousPlan() {PreviousPlanID = benefitElection.PreviousPlanID, PreviousPlanNetwork  = benefitElection.PreviousPlanNetwork };

        }
    }
}
