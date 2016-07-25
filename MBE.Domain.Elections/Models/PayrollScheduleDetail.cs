using System;

namespace MBE.Domain.Elections.Models
{
    public class PayrollScheduleDetail
    {
        public DateTime DeductionStartDate { get; set; }
        public DateTime DeductionEndDate { get; set; }
        public DateTime? LastRunDate { get; set; }
        public int NumPrePaysRemaining { get; set; }
    }
    public class TierPrePayValue
    {
        public int TierCostAgeBandID { get; set; }
        public decimal IncrementalCharge { get; set; }
        public decimal PrePayAmount { get; set; }
        public decimal IncrementalChargeEr { get; set; }
        public decimal PrePayAmountEr { get; set; }
        public string PrePayYear { get; set; }
    }

    public class UserPrePayAmountPaid
    {
        public decimal AmountPaid { get; set; }
        public DateTime ThruDate { get; set; }
        public decimal AfterTaxPaid { get; set; }
        public decimal ImputedIncomePaid { get; set; }
    }
}
