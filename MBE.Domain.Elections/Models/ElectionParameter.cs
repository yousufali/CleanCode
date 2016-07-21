using System;
using System.Collections.Generic;

namespace MBE.Domain.Elections.Models
{
    public class ElectionParameter
    {
        public int PlanTypeID { get; private set; }
        public int PlanID { get; private set; }
        public bool EOIRequired { get; private set; }
        public int UserID { get; private set; }
        public int ClientID { get; private set; }
        public DateTime EffectiveDate { get; private set; }
        public int SavedUserID { get; private set; }
        public TierAmountFields ElectionAmount { get; private set; }
        public TierAmountFields EoiElectionAmount { get; private set; }        
        public List<CoveredUser> CoveredUsers { get; private set; }

       

        public class Builder
        {
            private int m_planTypeID;
            private int m_planID;
            private bool m_eoiRequired;
            private int m_userID;
            private int m_clientID;
            private DateTime m_effectiveDate;
            private int m_savedUserID;
            private TierAmountFields m_electionAmount;
            private TierAmountFields m_eoiElectionAmount;
            private List<CoveredUser> m_coveredUsers;
            public Builder()
            {
                m_electionAmount = new TierAmountFields();
                m_eoiElectionAmount = new TierAmountFields();

            }
            public Builder WithPlanTypeID(int value) { m_planTypeID = value; return this; }
            public Builder WithPlanID(int value) { m_planID = value; return this; }
            public Builder WithEoiRequired(bool value) { m_eoiRequired = value; return this; }
            public Builder WithUserID(int value) { m_userID = value; return this; }
            public Builder WithClientID(int value) { m_clientID = value; return this; }
            public Builder WithEffectiveDate(DateTime value) { m_effectiveDate = value; return this; }
            public Builder WithSavedUserID(int value) { m_savedUserID = value; return this; }

            public Builder WithTierID(int value)
            {
                if (m_eoiRequired)
                {
                    m_eoiElectionAmount.TierID = value;
                }
                else
                {
                    m_electionAmount.TierID = value;
                }
                return this;
            }
            public Builder WithEmployeeContribution(decimal value)
            {
                if (m_eoiRequired)
                {
                    m_eoiElectionAmount.EmployeeContribution = value;
                }
                else
                {
                    m_electionAmount.EmployeeContribution = value;
                }
                return this;
            }

            public Builder WithEmployerContribution(decimal value)
            {
                if (m_eoiRequired)
                {
                    m_eoiElectionAmount.EmployerContribution = value;
                }
                else
                {
                    m_electionAmount.EmployerContribution = value;
                }
                return this;
            }
            public Builder WithPerPayCheckDeduction(decimal value)
            {
                if (m_eoiRequired)
                {
                    m_eoiElectionAmount.PerPayCheckDeduction = value;
                }
                else
                {
                    m_electionAmount.PerPayCheckDeduction = value;
                }
                return this;
            }
            public Builder WithCoverageAmount(decimal value)
            {
                if (m_eoiRequired)
                {
                    m_eoiElectionAmount.CoverageAmount = value;
                }
                else
                {
                    m_electionAmount.CoverageAmount = value;
                }
                return this;
            }
            public Builder WithBenefitAmount(decimal value)
            {
                if (m_eoiRequired)
                {
                    m_eoiElectionAmount.BenefitAmount = value;
                }
                else
                {
                    m_electionAmount.BenefitAmount = value;
                }
                return this;
            }
            public Builder WithCoveredUsers (List<CoveredUser> coveredUsers )
            {
                m_coveredUsers = coveredUsers;
                return this;
            }
            public ElectionParameter Build()
            {
                return new ElectionParameter
                {
                    PlanTypeID = m_planTypeID,
                    PlanID = m_planID,
                    EOIRequired = m_eoiRequired,
                    ElectionAmount = m_electionAmount,
                    EoiElectionAmount = m_eoiElectionAmount,
                    UserID = m_userID,
                    ClientID = m_clientID,
                    EffectiveDate = m_effectiveDate,
                    SavedUserID = m_savedUserID,
                    CoveredUsers =  m_coveredUsers
                    
                };
            }
        }
    }
}
