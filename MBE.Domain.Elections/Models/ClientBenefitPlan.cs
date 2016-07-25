using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBE.Domain.Elections.Models
{
    public class ClientBenefitPlan
    {
        public int PlanID { get; set; }
        public bool AgeBanding { get; set; }
        public int AlternateIDRuleID { get; set; }
        public int AlternateIDRuleID2 { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime TerminationDate { get; set; }
        public bool WaivePlan { get; set; }
        public bool PrePayPlan { get; set; }
    }
}