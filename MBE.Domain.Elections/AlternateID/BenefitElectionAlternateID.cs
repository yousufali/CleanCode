using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBE.Domain.Elections.AlternateID
{
    public class BenefitElectionAlternateID
    {
        public int UserID { get; set; }
        public int ParentUserID { get; set; }
        public int PlanTypeID { get; set; }
        public DateTime BenefitStartDate { get; set; }
        public DateTime BenefitEndDate { get; set; }
        public string AlternateID { get; set; }
    }
}
