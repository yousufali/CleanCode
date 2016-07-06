using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBE.Domain.Elections.Models
{
    public class SelectedPlanAndTier : ClientBenefitPlan
    {
        public ITierBasicInfo Tier { get; set; }
        public List<AdminFee> AdminFees { get; set; }
        public ClientPlanOrder ClientPlanOrder { get; set; }
    }
}