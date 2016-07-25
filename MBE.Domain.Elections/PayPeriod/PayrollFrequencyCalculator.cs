using System.Linq;
using MBE.Domain.Elections.DataAccess;
using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections.PayPeriod
{
    public interface IPayrollFrequencyCalculator
    {
        PayrollFrequency GetPayrollFrequency(int payrollScheduleID, int planTypeID);
    }
    public class PayrollFrequencyCalculator : IPayrollFrequencyCalculator
    {
        private readonly IPayrollFrequencyRepository m_payrollFrequencyRepository;
        public PayrollFrequencyCalculator(IPayrollFrequencyRepository payrollFrequencyRepository)
        {
            m_payrollFrequencyRepository = payrollFrequencyRepository;
        }

        public PayrollFrequency GetPayrollFrequency(int payrollScheduleID, int planTypeID)
        {
            var rows = m_payrollFrequencyRepository.SelectPayrollFrequencyPayrollSchedule(payrollScheduleID);
            foreach (PayrollFrequencyPayrollSchedule pfps in rows)
            {
                var payrollPlanTypeFrequencies = m_payrollFrequencyRepository.SelectPayrollPlanTypeFrequency(pfps.PayrollFrequencyID);
                var payrollPlanTypeFrequencyByPlanTypeID = payrollPlanTypeFrequencies.FirstOrDefault(a => (a.PlanTypeMask & planTypeID) == planTypeID);
                if (payrollPlanTypeFrequencyByPlanTypeID != null)
                {
                    return m_payrollFrequencyRepository.SelectPayrollFrequency(payrollPlanTypeFrequencyByPlanTypeID.PayrollFrequencyID).FirstOrDefault();
                }
            }
            return null;
        }
    }
}
