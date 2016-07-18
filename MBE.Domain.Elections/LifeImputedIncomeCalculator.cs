using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBE.Domain.Elections.Models;
using MBE.Domain.Elections.DataAccess;

namespace MBE.Domain.Elections
{
    public interface ILifeImputedIncomeCalculator
    {
        decimal GetImputedIncomeMonthly(SelectedPlanAndTier setupData);
    }

    public class LifeImputedIncomeCalculator
    {
        private ILifeImputedIncomeCoverageCalculator m_lifeImputedIncomeCoverageCalculator;

        public LifeImputedIncomeCalculator(ILifeImputedIncomeCoverageCalculator lifeImputedIncomeCoverageCalculator)
        {
            m_lifeImputedIncomeCoverageCalculator = lifeImputedIncomeCoverageCalculator;
        }

        public decimal GetImputedIncomeMonthly(SelectedPlanAndTier setupData, DateTime effectiveDate)
        {
            var coverage = m_lifeImputedIncomeCoverageCalculator.GetImputedIncomeCoverage(setupData.CoverageAmountsForImputedIncome);
            var imputedIncomeCost = GetImputedIncomeCost(setupData, effectiveDate);
            var monthlyCost =  CalculateImputedIncomeMonthlyCostForCoverage(imputedIncomeCost, coverage);
            return monthlyCost;
        }

        private decimal CalculateImputedIncomeMonthlyCostForCoverage(ImputedIncomeCost imputedIncomeCost, decimal coverage)
        {
            return (coverage / 1000) * imputedIncomeCost.Cost;
        }

        private  ImputedIncomeCost GetImputedIncomeCost(SelectedPlanAndTier setupData, DateTime effectiveDate)
        {
            var age = GetAgeAsEndOfYear(setupData.User.BirthDate, effectiveDate.Year);
            var imputedIncomeCost = setupData.ImputedIncomeCosts.FirstOrDefault(a => a.Minage <= age && a.MaxAge >= age);
            if (imputedIncomeCost != null) return imputedIncomeCost;
            return new ImputedIncomeCost();
        }

        private int GetAgeAsEndOfYear(DateTime birthDate, int toYear)
        {
            DateTime toDate = DateTime.Parse(String.Format("12/31/{0}", toYear));
            var age = toYear - birthDate.Year;
            if (birthDate > toDate.AddYears(-age)) age--;
            return age;
        }

    }
}
