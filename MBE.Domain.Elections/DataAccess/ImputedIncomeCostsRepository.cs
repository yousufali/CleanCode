using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections.DataAccess
{
    public interface IImputedIncomeCostsRepository
    {
        List<ImputedIncomeCost> GetImputedIncomeCosts();
    }
    public class ImputedIncomeCostsRepository : IImputedIncomeCostsRepository
    {
        public List<ImputedIncomeCost> GetImputedIncomeCosts()
        {
            throw new NotImplementedException();
        }
    }
}
