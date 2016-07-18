using System;
using System.Collections.Generic;
using System.Linq;
using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections.AlternateID
{
    public interface IRule7Calculator
    {
        List<UserAlternateID> GetAlternateID(List<BenefitElectionAlternateID> benefitElectionAlternateIDs, List<CoveredUser> coveredUsers, DateTime effectiveDate);
    }
    public class Rule7Calculator : RuleCalculatorBase, IRule7Calculator
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
            if (coveredUser.RelationID == (int)Relation.Employee) return "0";
            if (coveredUser.RelationID == (int)Relation.Spouse) return "1";
            var maxAlternateID = GetMaxAlternateIDDayBeforeEffectiveDate(benefitElectionAlternateIDs, effectiveDate);
            if (String.IsNullOrEmpty(maxAlternateID)) return "2";
            return (int.Parse(maxAlternateID) + 1).ToString();
        }

        private string GetMaxAlternateIDDayBeforeEffectiveDate(List<BenefitElectionAlternateID> benefitElectionAlternateIDs, DateTime effectiveDate)
        {
            var dayBeforeEffectiveDate = effectiveDate.AddDays(-1);
            var alternateIDs = benefitElectionAlternateIDs.FindAll(a => a.BenefitStartDate <= dayBeforeEffectiveDate && a.BenefitEndDate >= dayBeforeEffectiveDate
                                                                           && IsNumeric(a.AlternateID) && int.Parse(a.AlternateID) > 1)
                                                        .OrderByDescending(a => int.Parse(a.AlternateID));
            if (alternateIDs.Count() > 0) return CompareWithPreviousCoveredUserAlternateID(alternateIDs.FirstOrDefault().AlternateID);
            return String.Empty;
        }

        private string CompareWithPreviousCoveredUserAlternateID(string value)
        {
            if (string.IsNullOrEmpty(m_previousCoveredUserAlternateID)) return value;
            if (int.Parse(m_previousCoveredUserAlternateID) > int.Parse(value)) return m_previousCoveredUserAlternateID;
            return value;
        }
        public bool IsNumeric(string value)
        {
            float output;
            return float.TryParse(value, out output);
        }
    }
}
