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
        public bool ImputedIncome { get; set; }
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
    }
}
