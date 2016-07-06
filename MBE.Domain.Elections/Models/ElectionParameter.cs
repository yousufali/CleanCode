using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBE.Domain.Elections.Models
{
    public class ElectionParameter
    {
        public int PlanTypeID { get; private set; }
        public int PlanID { get; private set; }
        public bool EOIRequired { get; private set; }
        public TierAmountFields ElectionAmount { get; private set; }
        public TierAmountFields EoiElectionAmount { get; private set; }
        public string Pcp { get; private set; }
        public int UserID { get; private set; }
        public List<int> DependentUserID { get; private set; }
        public int ClientID { get; private set; }
        public DateTime EffectiveDate { get; private set; }
        public int SavedUserID { get; private set; }
        public bool PcpSeen { get; private set; }
        public string Pcp2 { get; private set; }
        public bool Pcp2Seen { get; private set; }

        public class Builder
        {
            private int planTypeID;
            private int planID;
            private bool eoiRequired;

            private string pcp;
            private int userID;
            private List<int> dependentUserID;
            private int clientID;
            private DateTime effectiveDate;
            private int savedUserID;
            private bool pcpSeen;
            private string pcp2;
            private bool pcp2Seen;
            private TierAmountFields electionAmount;
            private TierAmountFields eoiElectionAmount;
            public Builder()
            {
                electionAmount = new TierAmountFields();
                eoiElectionAmount = new TierAmountFields();
                dependentUserID = new List<int>();
            }
            public Builder WithPlanTypeID(int value) { planTypeID = value; return this; }
            public Builder WithPlanID(int value) { planID = value; return this; }
            public Builder WithEoiRequired(bool value) { eoiRequired = value; return this; }
            public Builder WithPcp(string value) { pcp = value; return this; }
            public Builder WithUserID(int value) { userID = value; return this; }
            public Builder AddCoveredDependent(int value) { this.dependentUserID.Add(value); return this; }
            public Builder WithClientID(int value) { clientID = value; return this; }
            public Builder WithEffectiveDate(DateTime value) { effectiveDate = value; return this; }
            public Builder WithSavedUserID(int value) { savedUserID = value; return this; }
            public Builder WithPcpSeen(bool value) { pcpSeen = value; return this; }
            public Builder WithPcp2(string value) { pcp2 = value; return this; }
            public Builder WithPcp2Seen(bool value) { pcp2Seen = value; return this; }
            public Builder WithTierID(int value)
            {
                if (eoiRequired)
                {
                    eoiElectionAmount.TierID = value;
                }
                else
                {
                    electionAmount.TierID = value;
                }
                return this;
            }
            public Builder WithEmployeeContribution(decimal value)
            {
                if (eoiRequired)
                {
                    eoiElectionAmount.EmployeeContribution = value;
                }
                else
                {
                    electionAmount.EmployeeContribution = value;
                }
                return this;
            }

            public Builder WithEmployerContribution(decimal value)
            {
                if (eoiRequired)
                {
                    eoiElectionAmount.EmployerContribution = value;
                }
                else
                {
                    electionAmount.EmployerContribution = value;
                }
                return this;
            }
            public Builder WithPerPayCheckDeduction(decimal value)
            {
                if (eoiRequired)
                {
                    eoiElectionAmount.PerPayCheckDeduction = value;
                }
                else
                {
                    electionAmount.PerPayCheckDeduction = value;
                }
                return this;
            }
            public Builder WithCoverageAmount(decimal value)
            {
                if (eoiRequired)
                {
                    eoiElectionAmount.CoverageAmount = value;
                }
                else
                {
                    electionAmount.CoverageAmount = value;
                }
                return this;
            }
            public Builder WithBenefitAmount(decimal value)
            {
                if (eoiRequired)
                {
                    eoiElectionAmount.BenefitAmount = value;
                }
                else
                {
                    electionAmount.BenefitAmount = value;
                }
                return this;
            }
            public ElectionParameter Build()
            {
                return new ElectionParameter
                {
                    PlanTypeID = planTypeID,
                    PlanID = planID,
                    EOIRequired = eoiRequired,
                    ElectionAmount = electionAmount,
                    EoiElectionAmount = eoiElectionAmount,
                    Pcp = pcp,
                    UserID = userID,
                    DependentUserID = dependentUserID,
                    ClientID = clientID,
                    EffectiveDate = effectiveDate,
                    SavedUserID = savedUserID,
                    PcpSeen = pcpSeen,
                    Pcp2 = pcp2,
                    Pcp2Seen = pcp2Seen
                };
            }
        }
    }
}
