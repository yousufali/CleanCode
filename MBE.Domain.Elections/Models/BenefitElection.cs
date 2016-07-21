using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBE.Domain.Elections.Models
{
    public class BenefitElection
    {
        public int UserID{ get; set; }
        public int ParentUserID { get; set; }
        public int PlanTypeID { get; set; }
        public int PlanID { get; set; }
        public DateTime BenefitStartDate { get; set; }
        public DateTime BenefitEndDate { get; set; }
        public DateTime ElectionStartDate { get; set; }
        public string AlternateID1 { get; set; }
        public string AlternateID2 { get; set; }        
    }
}
