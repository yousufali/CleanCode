using System;
using System.Linq;
using MBE.Domain.Elections.Models;
using MBE.Domain.Elections.DataAccess;

namespace MBE.Domain.Elections
{
    public interface ILifeImputedIncomeCalculator
    {
        decimal GetImputedIncomeMonthly(ElectionData electionData);
    }

    public class LifeImputedIncomeCalculator : ILifeImputedIncomeCalculator
    {
        private readonly ILifeImputedIncomeCoverageCalculator m_lifeImputedIncomeCoverageCalculator;
        private readonly IUserRepository m_userRepository;
        private readonly IImputedIncomeCostsRepository m_imputedIncomeCostsRepository;
        private ElectionData m_electionData;
        public LifeImputedIncomeCalculator(ILifeImputedIncomeCoverageCalculator lifeImputedIncomeCoverageCalculator, IUserRepository userRepository, IImputedIncomeCostsRepository imputedIncomeCostsRepository)
        {
            m_lifeImputedIncomeCoverageCalculator = lifeImputedIncomeCoverageCalculator;
            m_userRepository = userRepository;
            m_imputedIncomeCostsRepository = imputedIncomeCostsRepository;
        }

        public decimal GetImputedIncomeMonthly(ElectionData electionData)
        {
            m_electionData = electionData;
            var coverage = m_lifeImputedIncomeCoverageCalculator.GetImputedIncomeCoverage(electionData);
            var imputedIncomeCost = GetImputedIncomeCost();
            var monthlyCost =  CalculateImputedIncomeMonthlyCostForCoverage(imputedIncomeCost, coverage);
            return monthlyCost;
        }

        private decimal CalculateImputedIncomeMonthlyCostForCoverage(ImputedIncomeCost imputedIncomeCost, decimal coverage)
        {
            return (coverage / 1000) * imputedIncomeCost.Cost;
        }

        private  ImputedIncomeCost GetImputedIncomeCost()
        {
            var imputedIncomeCosts = m_imputedIncomeCostsRepository.GetImputedIncomeCosts();
            var user = m_userRepository.GetUser(m_electionData.ParentUserID);
            var age = GetAgeAsEndOfYear(user.BirthDate, m_electionData.EffectiveDate.Year);
            var imputedIncomeCost = imputedIncomeCosts.FirstOrDefault(a => a.Minage <= age && a.MaxAge >= age);
            if (imputedIncomeCost != null) return imputedIncomeCost;
            return new ImputedIncomeCost();
        }

        private int GetAgeAsEndOfYear(DateTime birthDate, int toYear)
        {
            DateTime toDate = DateTime.Parse($"12/31/{toYear}");
            var age = toYear - birthDate.Year;
            if (birthDate > toDate.AddYears(-age)) age--;
            return age;
        }

    }
}
