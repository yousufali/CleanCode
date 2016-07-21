using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBE.Domain.Elections.Models
{
    public class UserBenefitClass
    {
        public int UserID { get; set; }
        public int BenefitClassID { get; set; }
        public DateTime EffDate { get; set; }
        public DateTime TermDate { get; set; }
    }
}
