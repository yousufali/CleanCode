using System;
using System.Collections.Generic;
using System.Linq;
using MBE.Domain.Elections.DataAccess;
using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections.SummerPrePay
{
    public interface ISummerPrePayEmployeeDeductionCalculator
    {
        decimal GetEmployeeMonthlyDeduction(ElectionData electionData);
    }
    public class SummerPrePayEmployeeDeductionCalculator : ISummerPrePayEmployeeDeductionCalculator
    {
        private IPayrollScheduleDetailRepository m_payrollScheduleDetailRepository;
        private IUserPrePayAmountPaidRepository m_userPrePayAmountPaidRepository;
        private ITierPrePayValueRepository m_tierPrePayValueRepository;
        private IBenefitElectionRepository m_benefitElectionRepository;
        private IPlanRepository m_planRepository;
        private IUserRepository m_userRepository;
        private ElectionData m_electionData;
        public SummerPrePayEmployeeDeductionCalculator(IPayrollScheduleDetailRepository payrollScheduleDetailRepository, IUserPrePayAmountPaidRepository userPrePayAmountPaidRepository, 
                                                        ITierPrePayValueRepository tierPrePayValueRepository, IPlanRepository planRepository, IBenefitElectionRepository benefitElectionRepository, IUserRepository userRepository)
        {
            m_payrollScheduleDetailRepository = payrollScheduleDetailRepository;
            m_userPrePayAmountPaidRepository = userPrePayAmountPaidRepository;
            m_tierPrePayValueRepository = tierPrePayValueRepository;
            m_planRepository = planRepository;
            m_benefitElectionRepository = benefitElectionRepository;
            m_userRepository = userRepository;
        }

        public decimal GetEmployeeMonthlyDeduction(ElectionData electionData)
        {
            m_electionData = electionData;
            var plan = m_planRepository.SelectClientBenefitPlan(electionData.PlanID);
            if (!plan.PrePayPlan) return 0;
            return GetSummerPrePayClientDeductionPerPay();
        }

        private decimal GetSummerPrePayClientDeductionPerPay()
        {
            var planTypes = new List<int> {m_electionData.PlanTypeID};
            var employee = m_userRepository.GetEmployee(m_electionData.ParentUserID);
            var benefitElections = m_benefitElectionRepository.SelectBenefitElections(m_electionData.ParentUserID,
                planTypes).FindAll(a => a.BenefitStartDate >= m_electionData.EffectiveDate);
            var plan = m_planRepository.SelectClientBenefitPlan(m_electionData.PlanID);
            var selectedTierPrePayValue = m_tierPrePayValueRepository.GeTierPrePayValue(m_electionData.TierID);            

            var payrollScheduleDetails =
                m_payrollScheduleDetailRepository.SelectPayrollScheduleDetails(employee.PayrollScheduleID, plan.EffectiveDate);
            var numPrePaidPeriodsRemaining = GetPrePaidPeriodsRemaining(payrollScheduleDetails);

            var amountPaid = GetSummerPrePayAmountPaid(benefitElections, payrollScheduleDetails, m_electionData.EffectiveDate);
            var amountScheduled = GetSummerPrePayAmountScheduled(selectedTierPrePayValue, numPrePaidPeriodsRemaining);
            var balanceDue = GetSummerPrePayBalanceDue(selectedTierPrePayValue, amountPaid, amountScheduled);
            return balanceDue / numPrePaidPeriodsRemaining;
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

        private decimal GetSummerPrePayAmountPaid(IEnumerable<BenefitElection> benefitElections, IEnumerable<PayrollScheduleDetail> payrollScheduleDetails, 
            DateTime effectiveDate)
        {
            decimal total = 0;
            foreach (var election in benefitElections)
            {
                var tier = m_tierPrePayValueRepository.GeTierPrePayValue(election.TierID);
                total += GetSummerPrePayPaidForBenefitElectionSegment(election, payrollScheduleDetails, tier);
            }
            return total;
        }

        private decimal GetSummerPrePayPaidForBenefitElectionSegment(BenefitElection election, IEnumerable<PayrollScheduleDetail> payrollScheduleDetails, TierPrePayValue tierPrePayValue)
        {
            var payPeriodsForSegment = GetPrePayPeriodsForElectionSegment(election, payrollScheduleDetails);
            var summerPrePayPaidFromClientDeductionPerPay = payPeriodsForSegment * election.ClientDeductionPerPay;
            var summerPrePayPaidFromTierPrePayValue = GetSummerPrePayPaidFromTierPrePayValue(tierPrePayValue, payPeriodsForSegment);
            return summerPrePayPaidFromClientDeductionPerPay + summerPrePayPaidFromTierPrePayValue;
        }

        private int GetPrePayPeriodsForElectionSegment(BenefitElection election, IEnumerable<PayrollScheduleDetail> payrollScheduleDetails)
        {
            var prePayPeriodsAtStartDate = GetPayPeriodsRemainingAtBenefitStartDate(election, payrollScheduleDetails);
            var prePayPeriodsAtEndDate = GetPayPeriodsRemainingAtBenefitEndDate(election, payrollScheduleDetails);
            return prePayPeriodsAtStartDate - prePayPeriodsAtEndDate;
        }

        private int GetPayPeriodsRemainingAtBenefitStartDate(BenefitElection benefitElection, IEnumerable<PayrollScheduleDetail> payrollScheduleDetails)
        {
            var row = payrollScheduleDetails.Where(psd => psd.DeductionEndDate >= benefitElection.BenefitStartDate
                        && (psd.LastRunDate > benefitElection.DateCreated || !psd.LastRunDate.HasValue))
                        .OrderByDescending(psd => psd.NumPrePaysRemaining).FirstOrDefault();
            if (row != null) return row.NumPrePaysRemaining;
            return 0;
        }
        private int GetPayPeriodsRemainingAtBenefitEndDate(BenefitElection benefitElection, IEnumerable<PayrollScheduleDetail> payrollScheduleDetails)
        {
            var effectiveEndDate = benefitElection.BenefitEndDate < m_electionData.EffectiveDate ? benefitElection.BenefitEndDate : m_electionData.EffectiveDate;
            var row = payrollScheduleDetails.Where(psd => psd.DeductionEndDate >= effectiveEndDate
                        && (psd.LastRunDate > benefitElection.DateUpdated || !psd.LastRunDate.HasValue))
                        .OrderByDescending(psd => psd.NumPrePaysRemaining).FirstOrDefault();
            if (row != null) return row.NumPrePaysRemaining;
            return 0;
        }
        private decimal GetSummerPrePayPaidFromTierPrePayValue(TierPrePayValue tierPrePayValue, int payPeriodsSegment)
        {
            return tierPrePayValue.IncrementalCharge * payPeriodsSegment;
        }
        private decimal GetSummerPrePayAmountScheduled(TierPrePayValue tierPrePayValue, int numPrePayPeriodsRemaining)
        {
            return tierPrePayValue.IncrementalCharge * numPrePayPeriodsRemaining;
        }
        private decimal GetSummerPrePayBalanceDue(TierPrePayValue tierPrePayValue, decimal amountPaid, decimal amountScheduled)
        {
            return tierPrePayValue.PrePayAmount - amountPaid - amountScheduled;
        }
        //public decimal GetSummerPrePayPaidForBenefitElectionSegment(BenefitElection election, IEnumerable<PayrollScheduleDetail> payrollScheduleDetails,
        //   DateTime effectiveDate, TierPrePayValue tierPrePayValue)
        //{
        //    var payPeriodsForSegment = GetPrePayPeriodsForElectionSegment(election, payrollScheduleDetails, effectiveDate);
        //    var summerPrePayPaidFromClientDeductionPerPay = payPeriodsForSegment * election.ClientDeductionPerPay;
        //    var summerPrePayPaidFromTierPrePayValue = GetSummerPrePayPaidFromTierPrePayValue(tierPrePayValue, payPeriodsForSegment);
        //    return summerPrePayPaidFromClientDeductionPerPay + summerPrePayPaidFromTierPrePayValue;
        //}
    }
}
