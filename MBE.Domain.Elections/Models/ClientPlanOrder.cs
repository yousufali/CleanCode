using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBE.Domain.Elections.Models
{
    public class ClientPlanOrder
    {
        public int ClientID { get; set; }
        public int PlanTypeID { get; set; }
        public bool LifeImputedIncomeHolder { get; set; }
        public bool IncludeInLifeImputedIncome { get; set; }
    }
}
