using MBE.Domain.Elections.DataAccess;
using MBE.Domain.Elections.Models;
using MBE.Domain.Elections.Tier;

namespace MBE.Domain.Elections.CreditQuestion
{
    public interface IUserCreditAmountCalculator
    {
        UserCreditAmount GetUserCreditAmount(ElectionData electionData);
    }
    public class UserCreditAmountCalculator : IUserCreditAmountCalculator
    {
        private readonly IUserCreditResponseRepository m_userCreditResponseRepository;
        private readonly IBaseTierCalculator m_baseTierCalculator;

        public UserCreditAmountCalculator(IUserCreditResponseRepository userCreditResponseRepository, IBaseTierCalculator baseTierCalculator)
        {
            m_userCreditResponseRepository = userCreditResponseRepository;
            m_baseTierCalculator = baseTierCalculator;
        }

        public UserCreditAmount GetUserCreditAmount(ElectionData electionData)
        {
            var userCreditAmount = new UserCreditAmount();
            var userCreditResponses = m_userCreditResponseRepository.SelectUserCreditResponses(electionData.ParentUserID, electionData.EffectiveDate);
            var baseTierID = m_baseTierCalculator.GetBaseTierID(electionData.PlanID, electionData.TierID);
            foreach (var userCreditResponse in userCreditResponses.FindAll(a => ((a.PlanTypeMask & electionData.PlanTypeID) == electionData.PlanTypeID ) 
                                                                            && ((a.TierMask & baseTierID) == baseTierID)))
            {
                userCreditAmount.Premium = userCreditAmount.Premium + userCreditResponse.PremiumAmount;
                userCreditAmount.EmployeeMonthlyCost = userCreditAmount.EmployeeMonthlyCost + userCreditResponse.EmployeeAmount;
                userCreditAmount.EmployerMonthlyCost = userCreditAmount.EmployerMonthlyCost + userCreditResponse.EmployerAmount;
            }
            return userCreditAmount;
        }
    }

    public class UserCreditAmount
    {
        public decimal Premium { get; set; }
        public decimal EmployeeMonthlyCost { get; set; }
        public decimal EmployerMonthlyCost { get; set; }
    }
}
