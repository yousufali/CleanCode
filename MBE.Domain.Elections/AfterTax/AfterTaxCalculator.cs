using MBE.Domain.Elections.Models;
using MBE.Domain.Elections.SummerPrePay;

namespace MBE.Domain.Elections.AfterTax
{
    public interface IAfterTaxCalculator
    {
        decimal GetAfterTax(ElectionData electionData);
    }
    public class AfterTaxCalculator : IAfterTaxCalculator
    {
        private readonly ISummerPrePayAfterTaxCalculator m_summerPrePayAfterTaxCalculator;
        private readonly IBasicAfterTaxCalculator m_basicAfterTaxCalculator;

        public AfterTaxCalculator(ISummerPrePayAfterTaxCalculator summerPrePayAfterTaxCalculator, IBasicAfterTaxCalculator basicAfterTaxCalculator)
        {
            m_summerPrePayAfterTaxCalculator = summerPrePayAfterTaxCalculator;
            m_basicAfterTaxCalculator = basicAfterTaxCalculator;
        }

        public decimal GetAfterTax(ElectionData electionData)
        {
            var basicAfterTax = m_basicAfterTaxCalculator.GetAfterTaxmonthly(electionData);
            var summerPrePayAfterTax = m_summerPrePayAfterTaxCalculator.GetAfterTaxMonthly(electionData);
            return basicAfterTax + summerPrePayAfterTax;
        }
    }
}
