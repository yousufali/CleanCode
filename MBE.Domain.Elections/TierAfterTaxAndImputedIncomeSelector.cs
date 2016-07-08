using System.Collections.Generic;
using System.Linq;
using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections
{
    public interface ITierAfterTaxAndImputedIncomeSelector
    {
        TierAfterTaxAndImputedIncome GetEligibleRecord(List<TierAfterTaxAndImputedIncome> tierAfterTaxAndImputedIncomeList, List<CoveredDependent> coveredDependents);
    }

    public class TierAfterTaxAndImputedIncomeSelector : ITierAfterTaxAndImputedIncomeSelector
    {
        private const int CountNotUsedFlag = -1;
        private List<CoveredDependent> m_coveredDependents;
        private List<TierAfterTaxAndImputedIncome> m_tierAfterTaxAndImputedIncomeList;
        public TierAfterTaxAndImputedIncome GetEligibleRecord(List<TierAfterTaxAndImputedIncome> tierAfterTaxAndImputedIncomeList, List<CoveredDependent> coveredDependents)
        {
            m_coveredDependents = coveredDependents;
            m_tierAfterTaxAndImputedIncomeList = tierAfterTaxAndImputedIncomeList;
            return SelectTierAfterTaxAndImputedIncome();
        }

        private TierAfterTaxAndImputedIncome SelectTierAfterTaxAndImputedIncome()
        {
            var tiers = m_tierAfterTaxAndImputedIncomeList.FindAll(t => IsTierAfterTaxAndImputedIncomeEligible(t));
            if (tiers.Count > 0) return tiers.FirstOrDefault();
            return new TierAfterTaxAndImputedIncome();
        }

        private bool IsTierAfterTaxAndImputedIncomeEligible(TierAfterTaxAndImputedIncome tierAfterTaxAndImputedIncome)
        {
            return IsChildrenCoveredConditionSatisfied(tierAfterTaxAndImputedIncome)
                    && IsDomesticPartnerChildrenCoveredConditionSatisfied(tierAfterTaxAndImputedIncome)
                    && IsTaxQualifiedSpouseEquivalentCoveredConditionSatisfied(tierAfterTaxAndImputedIncome)
                    && IsSpouseEquivalentCoveredConditionSatisfied(tierAfterTaxAndImputedIncome)
                    && IsCoveredCountConditionSatisfied(tierAfterTaxAndImputedIncome);
        }

        private bool IsChildrenCoveredConditionSatisfied(TierAfterTaxAndImputedIncome t)
        {
            return t.MinChildrenCovered <= TotalChildrenCovered() && t.MaxChildrenCovered >= TotalChildrenCovered();
        }

        private bool IsDomesticPartnerChildrenCoveredConditionSatisfied(TierAfterTaxAndImputedIncome t)
        {
            return (t.MinDPChildrenCovered <= TotalDomesticPartnerChildrenCovered()) && (t.MaxDPChildrenCovered >= TotalDomesticPartnerChildrenCovered());
        }

        private bool IsTaxQualifiedSpouseEquivalentCoveredConditionSatisfied(TierAfterTaxAndImputedIncome t)
        {
            if (t.SpouseEquivalentQualifiedCount == CountNotUsedFlag) return true;
            if (t.SpouseEquivalentQualifiedCount >= TotalTaxQualifiedSpouseEquivalentCovered()) return true;
            return false;
        }

        private bool IsSpouseEquivalentCoveredConditionSatisfied(TierAfterTaxAndImputedIncome t)
        {
            if (t.SpouseEquivalentCoveredCount == CountNotUsedFlag) return true;
            if (t.SpouseEquivalentCoveredCount >= TotalSpouseEquivalentCovered()) return true;
            return false;
        }

        private bool IsCoveredCountConditionSatisfied(TierAfterTaxAndImputedIncome t)
        {
            if (t.UseNonAndTaxQualifiedCount)
            {
                return IsNonAndTaxQualifiedCoveredCountSatisfied(t);
            }else
            {
                if (t.UseNonTaxQualifiedCounts)
                {
                    //This code is wrong - it should be calling IsNonTaxQualifiedCoveredCountSatisfied. 
                    //Couldnt change it today as this is how the data is setup and will give different ImputedIncome and After tax amounts from sql function
                    return t.MinTaxQualified <= TotalNonTaxQualifiedDependents() && t.MaxTaxQualified >= TotalNonTaxQualifiedDependents();
                }
                else
                {
                    return IsTaxQualifiedCoveredCountSatisfied(t);
                }
            }
        }

        private bool IsNonAndTaxQualifiedCoveredCountSatisfied(TierAfterTaxAndImputedIncome tierAfterTaxAndImputedIncome)
        {
            return IsNonTaxQualifiedCoveredCountSatisfied(tierAfterTaxAndImputedIncome) && IsTaxQualifiedCoveredCountSatisfied(tierAfterTaxAndImputedIncome);
        }

        private bool IsNonTaxQualifiedCoveredCountSatisfied(TierAfterTaxAndImputedIncome tierAfterTaxAndImputedIncome)
        {
            return tierAfterTaxAndImputedIncome.NonTaxQualifiedMin <= TotalNonTaxQualifiedDependents() && tierAfterTaxAndImputedIncome.NonTaxQualifiedMax >= TotalNonTaxQualifiedDependents();
        }

        private bool IsTaxQualifiedCoveredCountSatisfied(TierAfterTaxAndImputedIncome tierAfterTaxAndImputedIncome)
        {
            return tierAfterTaxAndImputedIncome.MinTaxQualified <= TotalTaxQualifiedDependents() && tierAfterTaxAndImputedIncome.MaxTaxQualified >= TotalTaxQualifiedDependents();
        }

        private int TotalNonTaxQualifiedDependents()
        {
            return m_coveredDependents.FindAll(a => !a.TaxQualified).Count();
        }

        private int TotalTaxQualifiedDependents()
        {
            return m_coveredDependents.FindAll(a => a.TaxQualified).Count();
        }

        private int TotalChildrenCovered()
        {
            return m_coveredDependents.FindAll(a => (a.RelationID == (int)Relation.Child) || (a.RelationID == (int)Relation.CourtOrderedDependent)).Count;
        }
        private int TotalDomesticPartnerChildrenCovered()
        {
            return m_coveredDependents.FindAll(a => (a.RelationID == (int)Relation.ChildofDomesticPartner)).Count;
        }

        private int TotalSpouseEquivalentCovered()
        {
            return m_coveredDependents.FindAll(a => a.SpouseEquivalent).Count;
        }

        private int TotalTaxQualifiedSpouseEquivalentCovered()
        {
            return m_coveredDependents.FindAll(a => a.SpouseEquivalent && a.TaxQualified).Count;
        }
    }
}
