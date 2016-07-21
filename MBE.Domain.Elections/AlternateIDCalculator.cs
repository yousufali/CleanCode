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
        List<UserAlternateID> GetAlternateID(List<CoveredUser> coveredUsers, DateTime effectiveDate, int planID, int planTypeID, int parentUserID, AlternateIDTypes alternateIDType);
    }

    public class AlternateIDCalculator : IAlternateIDCalculator
    {
        private IPlanRepository m_planRepository;
        private IBenefitElectionAlternateIDSelector m_benefitElectionAlternateIDSelector;
        private readonly IRule1Calculator m_rule1Calculator;
        private readonly IRule2Calculator m_rule2Calculator;
        private readonly IRule3Calculator m_rule3Calculator;
        private readonly IRule4Calculator m_rule4Calculator;
        private readonly IRule5Calculator m_rule5Calculator;
        private readonly IRule6Calculator m_rule6Calculator;
        private readonly IRule7Calculator m_rule7Calculator;
        public AlternateIDCalculator(IPlanRepository planRepository, IBenefitElectionAlternateIDSelector benefitElectionAlternateIDSelector,
            IRule1Calculator rule1Calculator,IRule2Calculator rule2Calculator, IRule3Calculator rule3Calculator, IRule4Calculator rule4Calculator,
            IRule5Calculator rule5Calculator, IRule6Calculator rule6Calculator, IRule7Calculator rule7Calculator)
        {
            m_planRepository = planRepository;
            m_benefitElectionAlternateIDSelector = benefitElectionAlternateIDSelector;
            m_rule1Calculator = rule1Calculator;
            m_rule2Calculator = rule2Calculator;
            m_rule3Calculator = rule3Calculator;
            m_rule4Calculator = rule4Calculator;
            m_rule5Calculator = rule5Calculator;
            m_rule6Calculator = rule6Calculator;
            m_rule7Calculator = rule7Calculator;
        }

        public List<UserAlternateID> GetAlternateID(List<CoveredUser> coveredUsers, DateTime effectiveDate, int planID, int planTypeID, int parentUserID, AlternateIDTypes alternateIDType)
        {
            var plan = m_planRepository.SelectClientBenefitPlan(planID);
            var ruleID = GetRuleID(plan, alternateIDType);
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

        public int GetRuleID(ClientBenefitPlan plan, AlternateIDTypes alternateIDType)
        {
            if (alternateIDType == AlternateIDTypes.Type1)
            {
                return plan.AlternateIDRuleID;
            }
            else
            {
                return plan.AlternateIDRuleID2;
            }
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
