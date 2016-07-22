using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBE.Domain.Elections.AlternateID;
using MBE.Domain.Elections.DataAccess;
using MBE.Domain.Elections.Models;
using MBE.Domain.Elections.Premium;
using MBE.Domain.Elections.PremiumOverride;
using MBE.Domain.Elections.User;

namespace MBE.Domain.Elections
{
    public class ElectionSaver
    {
        private readonly IAlternateIDCalculator m_alternateIDCalculator;
        private ElectionParameter m_electionParameter;
        private List<UserAlternateID> m_userAlternateIDs;
        private readonly IUserRepository m_userRepository;
        private readonly IPlanRepository m_planRepository;
        private readonly IElectionStartDateCalculator m_electionStartDateCalculator;
        private readonly IPremiumCalculator m_premiumCalculator;
        private IPremiumOverrideCalculator m_premiumOverrideCalculator;
        private IUserRateDiscriminatorCalculator m_userRateDiscriminatorCalculator;

        public ElectionSaver(IAlternateIDCalculator alternateIDCalculator, IUserRepository userRepository, 
            IPlanRepository planRepository, IElectionStartDateCalculator electionStartDateCalculator, IPremiumCalculator premiumCalculator, 
            IPremiumOverrideCalculator premiumOverrideCalculator, IUserRateDiscriminatorCalculator userRateDiscriminatorCalculator)
        {
            m_alternateIDCalculator = alternateIDCalculator;
            m_userRepository = userRepository;
            m_planRepository = planRepository;
            m_electionStartDateCalculator = electionStartDateCalculator;
            m_premiumCalculator = premiumCalculator;
            m_premiumOverrideCalculator = premiumOverrideCalculator;
            m_userRateDiscriminatorCalculator = userRateDiscriminatorCalculator;
        }
        public bool Save(ElectionParameter electionParameter)
        {

            return true;
        }

        private List<BenefitElectionNonFsaRecord> GetElectionRecords(ElectionParameter electionParameter)
        {
            m_electionParameter = electionParameter;
            var plan = m_planRepository.SelectClientBenefitPlan(electionParameter.PlanID);
            var benefitElections = new List<BenefitElectionNonFsaRecord>();
            var nonEoiElectionData = GetNonEoiElectionData(electionParameter);
            foreach (CoveredUser coveredUser in electionParameter.CoveredUsers)
            {
                var be = new BenefitElectionNonFsaRecord();
                be.UserID = coveredUser.UserID;
                be.ParentUserID = electionParameter.UserID;
                be.AlternateID1 = GetUserAlternateID1(coveredUser.UserID);
                be.AlternateID2 = GetUserAlternateID2(coveredUser.UserID);
                be.PlanID = electionParameter.PlanID;
                be.PlanTypeID = electionParameter.PlanTypeID;
                be.BenefitClassID = GetBenefitClassID();
                be.TierID = electionParameter.ElectionAmount.TierID;
                be.BenefitStartDate = electionParameter.EffectiveDate;
                be.BenefitEndDate = plan.TerminationDate;
                be.ElectionStartDate = m_electionStartDateCalculator.GetElectionStartDate(nonEoiElectionData);
                be.Premium = m_premiumCalculator.GetPremium(nonEoiElectionData);
                be.PremiumOverride = m_premiumOverrideCalculator.GetPremiumOverride(nonEoiElectionData);
                be.Contribution = electionParameter.ElectionAmount.EmployeeContribution;
                be.Coverage = electionParameter.ElectionAmount.CoverageAmount;
                be.RateQualifier = m_userRateDiscriminatorCalculator.GetRateDiscriminator(electionParameter.UserID,
                    electionParameter.PlanTypeID);
                be.BenefitPlanYear = plan.EffectiveDate.Year.ToString();
                //be.ImputedIncomeMonthly = imputedIncomeMonthly#TODO
                //be.ImputedIncomePerPay = imputedIncomePerPay#TODO
                //be.AfterTax = afterTax #TODO
                //be.AfterTaxDeductionPerPay = AfterTaxDeductionPerPay #TODO
                be.EOIRequired = electionParameter.EOIRequired;
                be.EOICoverage = electionParameter.EoiElectionAmount.CoverageAmount;
                be.EOIPlan = electionParameter.PlanID.ToString();
                be.EOITier = electionParameter.ElectionAmount.TierID.ToString();
                be.EOIDefaultContribution = electionParameter.EoiElectionAmount.EmployeeContribution;
                //be.EOIDefaultPremium =  Premium #TODO
                be.EOIDefaultCoverage = electionParameter.EoiElectionAmount.CoverageAmount;
                be.PCP1 = coveredUser.Pcp;
                be.PCP2 = coveredUser.Pcp2;
                be.PCP1Seen = coveredUser.PcpSeen;
                be.PCP2Seen = coveredUser.Pcp2Seen;
                //be.PreviousPlanID = previousPlanID #TODO
                //be.PreviousPlanNetwork = previousPlanNetwork  #TODO
                be.ClientDeductionPerPay = electionParameter.ElectionAmount.PerPayCheckDeduction;
                be.SavedUserID = electionParameter.UserID;
                //be.BenefitElectionPayrollScheduleID = payrollScheduleID #TODO
                //be.PayrollExportUniqueID = PayrollExportUniqueID #TODO
                be.EmployerMonthlyContribution = electionParameter.ElectionAmount.EmployerContribution;
                //be.EmployerPerPayContribution = employerPerPayContribution #TODO
                be.BenefitAmount = electionParameter.ElectionAmount.BenefitAmount;
                be.EoiBenefitAmount = electionParameter.EoiElectionAmount.BenefitAmount;
                benefitElections.Add(be);
            }
            return benefitElections;
        }

        private ElectionData GetNonEoiElectionData(ElectionParameter electionParameter)
        {
            return new ElectionData()
            {
                ParentUserID = electionParameter.UserID,
                EffectiveDate = electionParameter.EffectiveDate,
                PlanTypeID = electionParameter.PlanTypeID,
                PlanID = electionParameter.PlanID,
                TierID = electionParameter.ElectionAmount.TierID,
                CoveredUsers = electionParameter.CoveredUsers,
                BasicEmployeeMonthlyCost = electionParameter.ElectionAmount.EmployeeContribution,
                BasicEmployerMonthlyCost = electionParameter.ElectionAmount.EmployerContribution,
                BasicPremiumCost = electionParameter.ElectionAmount.EmployeeContribution + electionParameter.ElectionAmount.EmployerContribution,
                Coverage = electionParameter.ElectionAmount.CoverageAmount
            };
        }

        private string GetUserAlternateID1(int userID)
        {
            if (m_userAlternateIDs != null)
            {
                m_userAlternateIDs = m_alternateIDCalculator.GetAlternateID(m_electionParameter.CoveredUsers, m_electionParameter.EffectiveDate,
                    m_electionParameter.PlanID, m_electionParameter.PlanTypeID,
                    m_electionParameter.UserID, AlternateIDTypes.Type1);
            }
            var userAlternateID = m_userAlternateIDs.FirstOrDefault(a => a.UserID == userID);
            if (userAlternateID != null) return userAlternateID.AlternateID;
            return  String.Empty;
        }
        private string GetUserAlternateID2(int userID)
        {
            if (m_userAlternateIDs != null)
            {
                m_userAlternateIDs = m_alternateIDCalculator.GetAlternateID(m_electionParameter.CoveredUsers, m_electionParameter.EffectiveDate,
                    m_electionParameter.PlanID, m_electionParameter.PlanTypeID,
                    m_electionParameter.UserID, AlternateIDTypes.Type2);
            }
            var userAlternateID = m_userAlternateIDs.FirstOrDefault(a => a.UserID == userID);
            if (userAlternateID != null) return userAlternateID.AlternateID;
            return String.Empty;
        }

        private int GetBenefitClassID()
        {
            var userBenefitClasses = m_userRepository.GetUserBenefitClasses(m_electionParameter.UserID);
            var userBenefitClassAsOfEffectiveDate =
                userBenefitClasses.FirstOrDefault(
                    a =>
                        a.EffDate <= m_electionParameter.EffectiveDate &&
                        a.TermDate >= m_electionParameter.EffectiveDate);
            if (userBenefitClassAsOfEffectiveDate == null) throw  new Exception("No Benefit Class found");
             return userBenefitClassAsOfEffectiveDate.BenefitClassID;
        }

    }

    public class BenefitElectionNonFsaRecord
    {
        public int UserID { get; set; }
        public int ParentUserID { get; set; }
        public string AlternateID1 { get; set; }
        public string AlternateID2 { get; set; }
        public int PlanID { get; set; }
        public int PlanTypeID { get; set; }
        public int BenefitClassID { get; set; }
        public int TierID { get; set; }
        public DateTime BenefitStartDate { get; set; }
        public DateTime BenefitEndDate { get; set; }
        public DateTime ElectionStartDate { get; set; }
        public decimal Premium { get; set; }
        public decimal PremiumOverride { get; set; }
        public decimal Contribution { get; set; }
        public decimal Coverage { get; set; }
        public int RateQualifier { get; set; }
        public string BenefitPlanYear { get; set; }
        public decimal ImputedIncomeMonthly { get; set; }
        public decimal ImputedIncomePerPay { get; set; }
        public decimal AfterTax { get; set; }
        public decimal AfterTaxDeductionPerPay { get; set; }
        public bool EOIRequired { get; set; }
        public decimal EOICoverage { get; set; }
        public string EOIPlan { get; set; }
        public string EOITier { get; set; }
        public decimal EOIDefaultContribution { get; set; }
        public decimal EOIDefaultPremium { get; set; }
        public decimal EOIDefaultCoverage { get; set; }
        public string PCP1 { get; set; }
        public string PCP2 { get; set; }
        public bool PCP1Seen { get; set; }
        public bool PCP2Seen { get; set; }
        public int PreviousPlanID { get; set; }
        public string PreviousPlanTier { get; set; }
        public string PreviousPlanNetwork { get; set; }
        public decimal ClientDeductionPerPay { get; set; }
        public int SavedUserID { get; set; }
        public int BenefitElectionPayrollScheduleID { get; set; }
        public string PayrollExportUniqueID { get; set; }
        public decimal EmployerMonthlyContribution { get; set; }
        public decimal EmployerPerPayContribution { get; set; }
        public decimal BenefitAmount { get; set; }
        public decimal EoiBenefitAmount { get; set; }
    }
}
