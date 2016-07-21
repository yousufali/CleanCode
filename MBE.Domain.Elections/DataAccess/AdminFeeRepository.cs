using System;
using System.Collections.Generic;
using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections.DataAccess
{
    public interface IAdminFeeRepository
    {
        List<AdminFee> SelectAdminFees(int planID, int tierID);
    }
    public class AdminFeeRepository : IAdminFeeRepository
    {
        public List<AdminFee> SelectAdminFees(int planID, int tierID)
        {
            throw new NotImplementedException();
        }
    }
}
