using System;
using System.Collections.Generic;
using System.Linq;
using MBE.Domain.Elections.DataAccess;
using MBE.Domain.Elections.Models;
using MBE.Domain.Elections.PayPeriod;

namespace MBE.Domain.Elections.SummerPrePay
{
    public interface ISummerPrePayAfterTaxCalculator
    {
        decimal GetAfterTaxMonthly(ElectionData electionData);
    }
    public class SummerPrePayAfterTaxCalculator : ISummerPrePayAfterTaxCalculator
    {
        private const int MedicalPlanTypeID = 1;
        private ElectionData m_electionData;
        private IBenefitElectionRepository m_benefitElectionRepository;
        private IPayrollScheduleDetailRepository m_payrollScheduleDetailRepository;
        private IUserRepository m_userRepository;
        private IPlanRepository m_planRepository;
        private List<BenefitElection> m_medicalElections;
        private IUserPrePayAmountPaidRepository m_userPrePayAmountPaidRepository;
        private ITierPrePayValueRepository m_tierPrePayValueRepository;
        private IPayPeriodsPerAnumCalculator m_payPeriodsPerAnumCalculator;
        public SummerPrePayAfterTaxCalculator(IBenefitElectionRepository benefitElectionRepository, IPayrollScheduleDetailRepository payrollScheduleDetailRepository, IUserRepository userRepository, IPlanRepository planRepository, IUserPrePayAmountPaidRepository userPrePayAmountPaidRepository, ITierPrePayValueRepository tierPrePayValueRepository, IPayPeriodsPerAnumCalculator payPeriodsPerAnumCalculator)
        {
            m_benefitElectionRepository = benefitElectionRepository;
            m_payrollScheduleDetailRepository = payrollScheduleDetailRepository;
            m_userRepository = userRepository;
            m_planRepository = planRepository;
            m_userPrePayAmountPaidRepository = userPrePayAmountPaidRepository;
            m_tierPrePayValueRepository = tierPrePayValueRepository;
            m_payPeriodsPerAnumCalculator = payPeriodsPerAnumCalculator;
        }

        public decimal GetAfterTaxMonthly(ElectionData electionData)
        {
            var plan = m_planRepository.SelectClientBenefitPlan(electionData.PlanID);
            var employee = m_userRepository.GetEmployee(electionData.ParentUserID);
            var payrollScheduleDetails =
                m_payrollScheduleDetailRepository.SelectPayrollScheduleDetails(employee.PayrollScheduleID,
                    plan.EffectiveDate);
            m_electionData = electionData;
            var medicalPlanTypes = new List<int> { MedicalPlanTypeID };
            m_medicalElections = m_benefitElectionRepository.SelectBenefitElections(m_electionData.ParentUserID,
                medicalPlanTypes);

            var tierPrePayValue = m_tierPrePayValueRepository.GeTierPrePayValue(m_electionData.TierID);
            var userPrePayAmountPaid = m_userPrePayAmountPaidRepository.GetUserPrePayAmount(
                m_electionData.ParentUserID, tierPrePayValue.PrePayYear);
            
            var incrementalAfterTax = GetAfterTaxIncrementalAmount();
            var numPayPeriodsRemainings = GetPrePaidPeriodsRemaining(payrollScheduleDetails);
            var afterTaxScheduled = incrementalAfterTax*numPayPeriodsRemainings;
            var afterTaxAmountDue = GetPrePayAfterTaxDue();

            var balanceAfterTax = afterTaxAmountDue - afterTaxScheduled - userPrePayAmountPaid.AfterTaxPaid;
            var payPeriods = m_payPeriodsPerAnumCalculator.GetEmployeePayPeriodsPerAnnum(electionData.ParentUserID,
                electionData.ParentUserID);
            var afterTaxMonthly =  ((balanceAfterTax/numPayPeriodsRemainings) * (payPeriods )) / (decimal) 12.0;
            return afterTaxMonthly;
        }


        public decimal GetAfterTaxIncrementalAmount()
        {
            
            var medicalElection = m_medicalElections.FirstOrDefault(a => a.PlanTypeID == 1 &&
                                                                       a.BenefitStartDate <=
                                                                       m_electionData.EffectiveDate &&
                                                                       a.BenefitEndDate >= m_electionData.EffectiveDate);
            if (medicalElection == null) return 0;
            var afterTax = medicalElection.AfterTax;
            var afterTaxIncrementalAmount = ((afterTax*12)/21) - ((afterTax*12)/26);
            return afterTaxIncrementalAmount;
        }
        private int GetPrePaidPeriodsRemaining(List<PayrollScheduleDetail> payrollScheduleDetails)
        {
            var recordAsOfEffectiveDate =
                payrollScheduleDetails.FindAll(a => a.DeductionStartDate <= m_electionData.EffectiveDate
                                                           && a.DeductionEndDate >= m_electionData.EffectiveDate &&
                                                           !a.LastRunDate.HasValue).OrderByDescending(a => a.NumPrePaysRemaining).FirstOrDefault();
            if (recordAsOfEffectiveDate != null) return recordAsOfEffectiveDate.NumPrePaysRemaining;

            var recordAfterEffectiveDate =
                payrollScheduleDetails.FindAll(
                    a => a.DeductionEndDate > m_electionData.EffectiveDate && !a.LastRunDate.HasValue)
                    .OrderByDescending(a => a.NumPrePaysRemaining)
                    .FirstOrDefault();
            if (recordAfterEffectiveDate != null) return recordAfterEffectiveDate.NumPrePaysRemaining;
            return 0;
        }

        private decimal GetPrePayAfterTaxDue()
        {
            var medicalElection = m_medicalElections.FirstOrDefault(a => a.PlanTypeID == 1 &&
                                                                       a.BenefitStartDate <=
                                                                       m_electionData.EffectiveDate &&
                                                                       a.BenefitEndDate >= m_electionData.EffectiveDate);
            if (medicalElection == null) return 0;
            return ((medicalElection.AfterTax*12)/26)*5;
        }

    }
}
