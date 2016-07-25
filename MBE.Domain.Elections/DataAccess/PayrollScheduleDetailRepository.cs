using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections.DataAccess
{
    public interface IPayrollScheduleDetailRepository
    {
        List<PayrollScheduleDetail> SelectPayrollScheduleDetails(int payrollScheduleID, DateTime planStartDate);
    }
    public class PayrollScheduleDetailRepository : IPayrollScheduleDetailRepository
    {
        public List<PayrollScheduleDetail> SelectPayrollScheduleDetails(int payrollScheduleID, DateTime planStartDate)
        {
            throw new NotImplementedException();
        }
    }
}
