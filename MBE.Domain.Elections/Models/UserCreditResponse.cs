using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBE.Domain.Elections.Models
{
    public class UserCreditResponse
    {
        public int UserID { get; set; }
        public int PlanTypeMask { get; set; }
        public decimal PremiumAmount { get; set; }
        public decimal EmployeeAmount { get; set; }
        public decimal EmployerAmount { get; set; }
        public int RelationTypeMask { get; set; }
        public int TierMask { get; set; }
    }
}
