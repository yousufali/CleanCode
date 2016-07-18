using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections.AlternateID
{
    public interface IRule2Calculator
    {
        List<UserAlternateID> GetAlternateID(List<BenefitElectionAlternateID> benefitElectionAlternateIDs, List<CoveredUser> coveredUsers, DateTime effectiveDate);
    }
    public class Rule2Calculator : RuleCalculatorBase, IRule2Calculator
    {
        private string m_previousCoveredUserAlternateID;
        public List<UserAlternateID> GetAlternateID(List<BenefitElectionAlternateID> benefitElectionAlternateIDs, List<CoveredUser> coveredUsers, DateTime effectiveDate)
        {
            var userAlternateIDs = new List<UserAlternateID>();
            foreach (CoveredUser coveredUser in coveredUsers)
            {
                userAlternateIDs.Add(GetUserAlternateID(benefitElectionAlternateIDs, coveredUser, effectiveDate));
            }
            return userAlternateIDs;
        }

        private UserAlternateID GetUserAlternateID(List<BenefitElectionAlternateID> benefitElectionAlternateIDs, CoveredUser coveredUser, DateTime effectiveDate)
        {
            var userAlternateID = new UserAlternateID() { UserID = coveredUser.UserID };
            var election = GetElectionAsOfEffectiveDateOrDayBeforeEffectiveDate(benefitElectionAlternateIDs, coveredUser.UserID, effectiveDate);
            if (HasDayBeforeElectionAlternateID(election))
            {
                userAlternateID.AlternateID = election.AlternateID;
            }
            else
            {
                userAlternateID.AlternateID = CalculateAlternateID(benefitElectionAlternateIDs, coveredUser, effectiveDate);
            };
            m_previousCoveredUserAlternateID = userAlternateID.AlternateID;
            return userAlternateID;
        }
        private bool HasDayBeforeElectionAlternateID(BenefitElectionAlternateID benefitElectionAlternateID)
        {
            return ((benefitElectionAlternateID != null) && String.IsNullOrEmpty(benefitElectionAlternateID.AlternateID));
        }
        private string CalculateAlternateID(List<BenefitElectionAlternateID> benefitElectionAlternateIDs, CoveredUser coveredUser, DateTime effectiveDate)
        {
            if (coveredUser.RelationID == (int)Relation.Employee) return "A";
            if (coveredUser.RelationID == (int)Relation.Spouse) return "B";
            var maxAlternateID = GetMaxAlternateIDDayBeforeEffectiveDate(benefitElectionAlternateIDs, effectiveDate);
            if (String.IsNullOrEmpty(maxAlternateID)) return "C";
            return GetNextCharacter(maxAlternateID);
        }

        private string GetMaxAlternateIDDayBeforeEffectiveDate(List<BenefitElectionAlternateID> benefitElectionAlternateIDs, DateTime effectiveDate)
        {
            var dayBeforeEffectiveDate = effectiveDate.AddDays(-1);
            var alternateIDs = benefitElectionAlternateIDs.FindAll(a => a.BenefitStartDate <= dayBeforeEffectiveDate && a.BenefitEndDate >= dayBeforeEffectiveDate
                                                        && GetAsciiOfFirstCharacter(a.AlternateID) > GetAsciiOfFirstCharacter("B"))
                                                        .OrderBy(a => GetAsciiOfFirstCharacter(a.AlternateID));
            if (alternateIDs.Count() > 0) return CompareWithPreviousCoveredUserAlternateID(alternateIDs.FirstOrDefault().AlternateID);
            return String.Empty;
        }

        private string CompareWithPreviousCoveredUserAlternateID(string value)
        {
            if (string.IsNullOrEmpty(m_previousCoveredUserAlternateID)) return value;
            var previousCoveredUserAlternateIDAscii = GetAsciiOfFirstCharacter(m_previousCoveredUserAlternateID);
            var maxAlternateIDAscii = GetAsciiOfFirstCharacter(value);
            if (previousCoveredUserAlternateIDAscii > maxAlternateIDAscii) return m_previousCoveredUserAlternateID;
            return value;
        }

        private int GetAsciiOfFirstCharacter(string value)
        {
            char c = value[0];
            return (int)c;
        }
        private string GetNextCharacter(string value)
        {
            var asciiValue = (int)(value[0]);
            return ((char)(asciiValue + 1)).ToString();
        }
    }
}
