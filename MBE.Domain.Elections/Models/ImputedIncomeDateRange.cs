using System;

namespace MBE.Domain.Elections.Models
{
    public class ImputedIncomeDateRange
    {
        public DateTime BenefitStartDate { get; set; }
        public DateTime BenefitEndDate { get; set; }
        public DateTime ImputedIncomeStart { get; set; }
        public DateTime ImputedIncomeEnd { get; set; }
        public override bool Equals(Object obj)
        {
            if (obj is ImputedIncomeDateRange)
            {
                var that = obj as ImputedIncomeDateRange;
                return this.BenefitStartDate == that.BenefitStartDate && this.BenefitEndDate == that.BenefitEndDate
                    && this.ImputedIncomeStart == that.ImputedIncomeStart && this.ImputedIncomeEnd == that.ImputedIncomeEnd;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
