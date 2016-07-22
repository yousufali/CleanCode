using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections.DataAccess
{
    public interface ITierAfterTaxAndImputedIncomeRepository
    {
        List<TierAfterTaxAndImputedIncome> GetTierAfterTaxAmdImputedIncome(int tierID, int planID);
     }
    public class TierAfterTaxAndImputedIncomeRepository : ITierAfterTaxAndImputedIncomeRepository
    {
        public List<TierAfterTaxAndImputedIncome> GetTierAfterTaxAmdImputedIncome(int tierID, int planID)
        {
            throw new NotImplementedException();
        }
    }
}
