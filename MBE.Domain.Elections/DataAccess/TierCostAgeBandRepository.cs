using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections.DataAccess
{
    public interface ITierCostsAgeBandRepository
    {
        TierCostsAgeBand SelectTierCostsAgeBand(int tierID);
    }
    public class TierCostsAgeBandRepository : ITierCostsAgeBandRepository
    {
        public TierCostsAgeBand SelectTierCostsAgeBand(int tierID)
        {
            throw new NotImplementedException();
        }
    }

}
