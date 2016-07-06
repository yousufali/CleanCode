using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections.DataAccess
{
    public interface ITierRepository
    {
        List<TierAfterTaxAndImputedIncome> GetTierAfterTaxAmdImputedIncome(int tierID, bool ageBanding);
     }
    public class TierRepository
    {
    }
}
