using MBE.Domain.Elections.DataAccess;

namespace MBE.Domain.Elections.PayPeriod
{
    public interface IEmployerPayPeriodsPerAnnumCalculator
    {
        int GetPayPeriodsPerAnnum(int userID, int planTypeID);
    }
    public class EmployerPayPeriodsPerAnnumCalculator : IEmployerPayPeriodsPerAnnumCalculator
    {
        private readonly IUserRepository m_userRepository;
        private readonly IPayrollFrequencyCalculator m_payrollFrequencyCalculator;
        public EmployerPayPeriodsPerAnnumCalculator(IUserRepository userRepository, IPayrollFrequencyCalculator payrollFrequencyCalculator)
        {
            m_userRepository = userRepository;
            m_payrollFrequencyCalculator = payrollFrequencyCalculator;
        }

        public int GetPayPeriodsPerAnnum(int userID, int planTypeID)
        {
            var employee = m_userRepository.GetEmployee(userID);
            var payrollFrequency = m_payrollFrequencyCalculator.GetPayrollFrequency(employee.PayrollScheduleID,
                planTypeID);
            if (payrollFrequency != null) return payrollFrequency.PayPeriods;
            return employee.PayPeriodsPerAnnum;
        }
    }
}
