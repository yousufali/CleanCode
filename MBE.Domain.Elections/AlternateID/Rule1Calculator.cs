using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBE.Domain.Elections.DataAccess;
using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections.AlternateID
{
    public interface IRule1Calculator
    {
        List<UserAlternateID> GetAlternateID(List<BenefitElectionAlternateID> benefitElectionAlternateIDs, List<CoveredUser> coveredUsers, DateTime effectiveDate);
    }

    public class Rule1Calculator : RuleCalculatorBase, IRule1Calculator
    {
        public List<UserAlternateID> GetAlternateID(List<BenefitElectionAlternateID> benefitElectionAlternateIDs, List<CoveredUser> coveredUsers, DateTime effectiveDate)
        {
            var userAlternateIDs = new List<UserAlternateID>();
            foreach(CoveredUser coveredUser in coveredUsers)
            {
                userAlternateIDs.Add(GetUserAlternateID(benefitElectionAlternateIDs, coveredUser.UserID, effectiveDate));
            }
            return userAlternateIDs;
        }

        private UserAlternateID GetUserAlternateID(List<BenefitElectionAlternateID> benefitElectionAlternateIDs, int userID, DateTime effectiveDate)
        {
            var userAlternateID = new UserAlternateID() { UserID = userID };
            var election = GetElectionAsOfEffectiveDateOrDayBeforeEffectiveDate(benefitElectionAlternateIDs, userID, effectiveDate);
            if (election != null) userAlternateID.AlternateID = election.AlternateID;
            return userAlternateID;
        }
    }
}
