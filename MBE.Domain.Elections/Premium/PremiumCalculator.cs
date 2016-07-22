using System.Linq;
using MBE.Domain.Elections.CreditQuestion;
using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections.Premium
{
    public interface IPremiumCalculator
    {
        decimal GetPremium(ElectionData electionData);
    }
    public class PremiumCalculator
    {
        private readonly IBenefitElectionAdminFeeCalculator m_benefitElectionAdminFeeCalculator;
        private readonly IUserCreditAmountCalculator m_userCreditAmountCalculator;
        public PremiumCalculator(IBenefitElectionAdminFeeCalculator benefitElectionAdminFeeCalculator, IUserCreditAmountCalculator userCreditAmountCalculator )
        {
            m_benefitElectionAdminFeeCalculator = benefitElectionAdminFeeCalculator;
            m_userCreditAmountCalculator = userCreditAmountCalculator;
        }
        public decimal GetPremium(ElectionData electionData)
        {
            var benefitElecitonAdminFees = m_benefitElectionAdminFeeCalculator.GetBenefitElectionAdminFees(electionData);
            var userCreditAmount = m_userCreditAmountCalculator.GetUserCreditAmount(electionData);
            var adminFeePremium = benefitElecitonAdminFees.FindAll(a => a.IncludeInPremium).Sum(a => a.Premium);
            return electionData.BasicPremiumCost + adminFeePremium + userCreditAmount.Premium;
        }

    }
}
