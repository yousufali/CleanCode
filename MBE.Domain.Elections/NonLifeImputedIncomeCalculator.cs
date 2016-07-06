using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBE.Domain.Elections.Models;
using MBE.Domain.Elections.DataAccess;

namespace MBE.Domain.Elections
{
    public interface INonLifeImputedIncomeCalculator
    {
        TierAfterTaxAndImputedIncome GetImputedIncomeMonthly(SelectedPlanAndTier setupData);
    }
    public class NonLifeImputedIncomeCalculator
    {
        private ITierRepository m_tierRepository;
        private List<CoveredDependent> m_coveredDependents;
        public NonLifeImputedIncomeCalculator(ITierRepository tierRepository)
        {
            m_tierRepository = tierRepository;
        }
        public TierAfterTaxAndImputedIncome GetImputedIncomeMonthly(SelectedPlanAndTier setupData, List<CoveredDependent> coveredDependents)
        {

        }
        private bool IsMinimumChildrenCoveredConditionSatisfied(TierAfterTaxAndImputedIncome t)
        {
            if (t.MinChildrenCovered <= TotalChildrenCovered()) return true;
            return false;
        }

        private bool IsMaximumChildrenCoveredConditionSatisfied(TierAfterTaxAndImputedIncome t)
        {
            if (t.MaxChildrenCovered >= TotalChildrenCovered()) return true;
            return false;
        }

        private bool IsMinimumDomesticPartnerChildrenCoveredConditionSatisfied(TierAfterTaxAndImputedIncome t)
        {
            if (t.MinDPChildrenCovered <= TotalDomesticPartnerChildrenCovered()) return true;
            return false;
        }

        private bool IsMaximumDomesticPartnerChildrenCoveredConditionSatisfied(TierAfterTaxAndImputedIncome t)
        {
            if (t.MaxDPChildrenCovered >= TotalDomesticPartnerChildrenCovered()) return true;
            return false;
        }

        private bool IsSpouseEquivalentCoveredConditionSatisfied(TierAfterTaxAndImputedIncome t)
        {
            if (t.MaxDPChildrenCovered >= TotalDomesticPartnerChildrenCovered()) return true;
            return false;
        }

        private int TotalChildrenCovered()
        {
            return m_coveredDependents.FindAll(a => (a.RelationID == (int)Relation.Child) || (a.RelationID == (int)Relation.CourtOrderedDependent)).Count;
        }
        private int TotalDomesticPartnerChildrenCovered()
        {
            return m_coveredDependents.FindAll(a => (a.RelationID == (int)Relation.ChildofDomesticPartner)).Count;
        }
    }
}
