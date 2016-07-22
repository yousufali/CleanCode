﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections.DataAccess
{
    public interface ITierCostRepository
    {
        TierCost SelectTierCost(int tierID);
    }

    public class TierCostRepository : ITierCostRepository
    {
        public TierCost SelectTierCost(int tierID)
        {
            throw new NotImplementedException();
        }
    }


}
