using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBE.Domain.Elections.AlternateID;
using MBE.Domain.Elections.Models;
using MBE.Domain.Elections.DataAccess;

namespace MBE.Domain.Elections.AlternateID
{
    public interface IBenefitElectionAlternateIDSelector
    {
        List<BenefitElectionAlternateID> SelectBenefitElectionAlternateID(int planTypeID, int parentUserID, AlternateIDTypes alternateID);
    }

    public class BenefitElectionAlternateIDSelector : IBenefitElectionAlternateIDSelector
    {
        private IBenefitElectionRepository m_benefitElectionRepository;
        public BenefitElectionAlternateIDSelector(IBenefitElectionRepository benefitElectionRepository)
        {
            m_benefitElectionRepository = benefitElectionRepository;
        }
        public  List<BenefitElectionAlternateID> SelectBenefitElectionAlternateID(int planTypeID, int parentUserID, AlternateIDTypes alternateID)
        {
            var planTypes = new List<int>();
            planTypes.Add(planTypeID);
            var benefitElections = m_benefitElectionRepository.SelectBenefitElections(parentUserID, planTypes);
            if (alternateID == AlternateIDTypes.Type1)
            {
                return GetAlternateID1(benefitElections);
            }else
            {
                return GetAlternateID2(benefitElections);
            }
        }

        private List<BenefitElectionAlternateID> GetAlternateID1(List<BenefitElection> benefitElections)
        {
            var benefitElectionAlternateIDs = new List<BenefitElectionAlternateID>();
            foreach (BenefitElection election in benefitElections)
            {
                benefitElectionAlternateIDs.Add(new BenefitElectionAlternateID()
                {
                    UserID = election.UserID,
                    ParentUserID = election.ParentUserID,
                    PlanTypeID = election.PlanTypeID,
                    BenefitStartDate = election.BenefitStartDate,
                    BenefitEndDate = election.BenefitEndDate,
                    AlternateID = election.AlternateID1
                });
            }
            return benefitElectionAlternateIDs;
        }
        private List<BenefitElectionAlternateID> GetAlternateID2(List<BenefitElection> benefitElections)
        {
            var benefitElectionAlternateIDs = new List<BenefitElectionAlternateID>();
            foreach (BenefitElection election in benefitElections)
            {
                benefitElectionAlternateIDs.Add(new BenefitElectionAlternateID()
                {
                    UserID = election.UserID,
                    ParentUserID = election.ParentUserID,
                    PlanTypeID = election.PlanTypeID,
                    BenefitStartDate = election.BenefitStartDate,
                    BenefitEndDate = election.BenefitEndDate,
                    AlternateID = election.AlternateID2
                });
            }
            return benefitElectionAlternateIDs;
        }

    }

    public enum AlternateIDTypes
    {
        Type1 = 1,
        Type2 = 2
    }
}
