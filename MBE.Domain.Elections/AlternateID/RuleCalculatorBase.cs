using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBE.Domain.Elections.AlternateID
{
    public class RuleCalculatorBase
    {
        public BenefitElectionAlternateID GetElectionAsOfEffectiveDateOrDayBeforeEffectiveDate(List<BenefitElectionAlternateID> benefitElectionAlternateIDs, 
                                                                                            int userID, DateTime effectiveDate)
        {
            var benefitElectionAsOfEffectiveDate = benefitElectionAlternateIDs.FirstOrDefault(a => a.BenefitStartDate <= effectiveDate && a.BenefitEndDate >= effectiveDate);
            if (benefitElectionAsOfEffectiveDate != null) return benefitElectionAsOfEffectiveDate;
            var dayBeforeEffectiveDate = effectiveDate.AddDays(-1);
            var benefitElectionAsOfDayBeforeEffectiveDate = benefitElectionAlternateIDs.FirstOrDefault(a => a.BenefitStartDate <= dayBeforeEffectiveDate && a.BenefitEndDate >= dayBeforeEffectiveDate);
            return benefitElectionAsOfDayBeforeEffectiveDate;
        }
    }
}
