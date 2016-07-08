using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBE.Domain.Elections.Models
{
    public class TierAfterTaxAndImputedIncome
    {
        public int TierID { get; set; }
        public bool AgeBanded { get; set; }
        public int MinTaxQualified { get; set; }
        public int MaxTaxQualified { get; set; }
        public decimal AfterTax { get; set; }
        public decimal ImputedIncome { get; set; }
        public bool UseNonTaxQualifiedCounts { get; set; }
        public int SpouseEquivalentQualifiedCount { get; set; }
        public int SpouseEquivalentCoveredCount { get; set; }
        public int MinChildrenCovered { get; set; }
        public int MaxChildrenCovered { get; set; }
        public int MinDPChildrenCovered { get; set; }
        public int MaxDPChildrenCovered { get; set; }
        public bool UseNonAndTaxQualifiedCount { get; set; }
        public int NonTaxQualifiedMin { get; set; }
        public int NonTaxQualifiedMax { get; set; }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            TierAfterTaxAndImputedIncome p = obj as TierAfterTaxAndImputedIncome;
            if ((System.Object)p == null)
            {
                return false;
            }
            return (TierID == p.TierID) && (AgeBanded == p.AgeBanded) && (MinTaxQualified == p.MinTaxQualified) && (MaxTaxQualified == p.MaxTaxQualified)
                && (AfterTax == p.AfterTax) && (ImputedIncome == p.ImputedIncome) && (UseNonTaxQualifiedCounts == p.UseNonTaxQualifiedCounts) && (SpouseEquivalentQualifiedCount == p.SpouseEquivalentQualifiedCount)
                 && (SpouseEquivalentCoveredCount == p.SpouseEquivalentCoveredCount) && (MinChildrenCovered == p.MinChildrenCovered) && (MaxChildrenCovered == p.MaxChildrenCovered)
                  && (MinDPChildrenCovered == p.MinDPChildrenCovered) && (MaxDPChildrenCovered == p.MaxDPChildrenCovered) && (UseNonAndTaxQualifiedCount == p.UseNonAndTaxQualifiedCount) 
                  && (NonTaxQualifiedMin == p.NonTaxQualifiedMin) && (NonTaxQualifiedMax == p.NonTaxQualifiedMax);
            
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
