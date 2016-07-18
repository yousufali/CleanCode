using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBE.Domain.Elections.DataAccess;
using MBE.Domain.Elections.Models;
using MBE.Domain.Elections.AlternateID;

namespace MBE.Domain.Elections
{
    public interface IAlternateIDCalculator
    {

    }

    public class AlternateIDCalculator
    {
        private IPlanRepository m_planRepository;
        private IBenefitElectionAlternateIDSelector m_benefitElectionAlternateIDSelector;
        private IRule1Calculator m_rule1Calculator;
        private IRule2Calculator m_rule2Calculator;
        private IRule3Calculator m_rule3Calculator;
        private IRule4Calculator m_rule4Calculator;
        private IRule5Calculator m_rule5Calculator;
        private IRule6Calculator m_rule6Calculator;
        private IRule7Calculator m_rule7Calculator;
        public AlternateIDCalculator(IPlanRepository planRepository, IBenefitElectionAlternateIDSelector benefitElectionAlternateIDSelector)
        {
            m_planRepository = planRepository;
            m_benefitElectionAlternateIDSelector = benefitElectionAlternateIDSelector;
        }

        public List<UserAlternateID> GetAlternateID(List<CoveredUser> coveredUsers, DateTime effectiveDate, int ruleID, int planTypeID, int parentUserID, AlternateIDTypes alternateIDType)
        {
            var benefitElectionAlternateIDs = m_benefitElectionAlternateIDSelector.SelectBenefitElectionAlternateID(planTypeID, parentUserID, alternateIDType);
            if (ruleID == 1)
            {
                return m_rule1Calculator.GetAlternateID(benefitElectionAlternateIDs, coveredUsers, effectiveDate);
            }
            if (ruleID == 2)
            {
                return m_rule2Calculator.GetAlternateID(benefitElectionAlternateIDs, coveredUsers, effectiveDate);
            }
            if (ruleID == 3)
            {
                return m_rule3Calculator.GetAlternateID(benefitElectionAlternateIDs, coveredUsers, effectiveDate);
            }
            if (ruleID == 4)
            {
                return m_rule4Calculator.GetAlternateID(benefitElectionAlternateIDs, coveredUsers, effectiveDate);
            }
            if (ruleID == 5)
            {
                return m_rule5Calculator.GetAlternateID(benefitElectionAlternateIDs, coveredUsers, effectiveDate);
            }
            if (ruleID == 6)
            {
                return m_rule6Calculator.GetAlternateID(benefitElectionAlternateIDs, coveredUsers, effectiveDate);
            }
            if (ruleID == 7)
            {
                return m_rule7Calculator.GetAlternateID(benefitElectionAlternateIDs, coveredUsers, effectiveDate);
            }
            return GetAlternateIDForNoRule(coveredUsers);

        }

        private List<UserAlternateID> GetAlternateIDForNoRule(List<CoveredUser> coveredUsers)
        {
            var userAlternateIDs = new List<UserAlternateID>();
            foreach(CoveredUser coveredUser in coveredUsers)
            {
                userAlternateIDs.Add(new UserAlternateID() { UserID = coveredUser.UserID });
            }
            return userAlternateIDs;
        }
    }
}
