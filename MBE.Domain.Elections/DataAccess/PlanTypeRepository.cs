using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections.DataAccess
{
    public interface IPlanTypeRepository
    {
        List<ClientPlanOrder> SelectPlanTypes(int clientID);
    }
    public class PlanTypeRepository : IPlanTypeRepository
    {
        public List<ClientPlanOrder> SelectPlanTypes(int clientID)
        {
            throw new NotImplementedException();
        }
    }
}
