using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBE.Domain.Elections.CreditQuestion;

namespace MBE.Domain.Elections.Premium
{
    public class AdditionalPremiumCalculator
    {
        private IBenefitElectionAdminFeeCalculator m_benefitElectionAdminFeeCalculator;
        private IUserCreditAmountCalculator m_userCreditAmountCalculator;
        public AdditionalPremiumCalculator(IBenefitElectionAdminFeeCalculator benefitElectionAdminFeeCalculator, IUserCreditAmountCalculator userCreditAmountCalculator )
        {
            m_benefitElectionAdminFeeCalculator = benefitElectionAdminFeeCalculator;
            m_userCreditAmountCalculator = userCreditAmountCalculator;
        }
        public decimal Get()
        {
            return 0;
            //var benefitElectionAdminFee = m_benefitElectionAdminFeeCalculator.GetBenefitElectionAdminFees()
        }
    }
}
