using System;
using System.Collections.Generic;
using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections.DataAccess
{
    public interface IPayrollFrequencyRepository
    {
        List<PayrollFrequencyPayrollSchedule> SelectPayrollFrequencyPayrollSchedule(int payrollScheduleID);
        List<PayrollPlanTypeFrequency> SelectPayrollPlanTypeFrequency(int payrollFrequencyID);
        List<PayrollFrequency> SelectPayrollFrequency(int payrollFrequencyID);
    }
    public class PayrollFrequencyRepository : IPayrollFrequencyRepository
    {
        public List<PayrollFrequencyPayrollSchedule> SelectPayrollFrequencyPayrollSchedule(int payrollScheduleID)
        {
            throw new NotImplementedException();
        }

        public List<PayrollPlanTypeFrequency> SelectPayrollPlanTypeFrequency(int payrollFrequencyID)
        {
            throw new NotImplementedException();
        }

        public List<PayrollFrequency> SelectPayrollFrequency(int payrollFrequencyID)
        {
            throw new NotImplementedException();
        }
    }
}
