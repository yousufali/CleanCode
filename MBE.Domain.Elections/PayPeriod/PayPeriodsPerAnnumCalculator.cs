using System;

namespace MBE.Domain.Elections.PayPeriod
{
    public interface IPayPeriodsPerAnumCalculator
    {
        int GetEmployeePayPeriodsPerAnnum(int userID, int planTypeID);
        int GetEmployerPayPeriodsPerAnnum(int userID, int planTypeID);
    }
    public class PayPeriodsPerAnnumCalculator : IPayPeriodsPerAnumCalculator
    {
        private readonly IEmployeePayPeriodsPerAnnumCalculator m_employeePayPeriodsPerAnnumCalculator;
        private readonly IEmployerPayPeriodsPerAnnumCalculator m_employerPayPeriodsPerAnnumCalculator;
        public PayPeriodsPerAnnumCalculator(IEmployeePayPeriodsPerAnnumCalculator employeePayPeriodsPerAnnumCalculator, IEmployerPayPeriodsPerAnnumCalculator employerPayPeriodsPerAnnumCalculator)
        {
            m_employeePayPeriodsPerAnnumCalculator = employeePayPeriodsPerAnnumCalculator;
            m_employerPayPeriodsPerAnnumCalculator = employerPayPeriodsPerAnnumCalculator;
        }

        public int GetEmployeePayPeriodsPerAnnum(int userID, int planTypeID)
        {
            return m_employeePayPeriodsPerAnnumCalculator.GetPayPeriodsPerAnnum(userID, planTypeID);
        }

        public int GetEmployerPayPeriodsPerAnnum(int userID, int planTypeID)
        {
            return m_employerPayPeriodsPerAnnumCalculator.GetPayPeriodsPerAnnum(userID, planTypeID);
        }
    }
}
