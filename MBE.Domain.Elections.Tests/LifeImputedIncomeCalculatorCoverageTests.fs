namespace MBE.Domain.Elections.Tests
open MBE.Domain.Elections
open MBE.Domain.Elections.Models
open System
open Xunit
open FsUnit.Xunit
open System.Collections.Generic
open System.Linq

module LifeImputedIncomeCalculatorCoverageTests =
    
    [<Fact>]
    let ``Total Coverage Less than 50000 Test``() =
        let coverageAmounts = [| new CoverageAmountForImputedIncomeCalculation(PlanTypeID = 1, StartDate = DateTime.Parse "1/1/2016", Coverage = decimal 10000);
                                 new CoverageAmountForImputedIncomeCalculation(PlanTypeID = 1, StartDate = DateTime.Parse "1/1/2016", Coverage = decimal 10000)|] |> List
        let lifeImputedIncomeCoverageCalculator = new LifeImputedIncomeCoverageCalculator();
        let actual = lifeImputedIncomeCoverageCalculator.GetImputedIncomeCoverage(coverageAmounts)
        let expected = decimal 0
        Assert.Equal(expected, actual)

    [<Fact>]
    let ``Total Coverage greater than 50000 Test``() =
        let coverageAmounts = [| new CoverageAmountForImputedIncomeCalculation(PlanTypeID = 1, StartDate = DateTime.Parse "1/1/2016", Coverage = decimal 35000);
                                 new CoverageAmountForImputedIncomeCalculation(PlanTypeID = 1, StartDate = DateTime.Parse "1/1/2016", Coverage = decimal 35000)|] |> List
        let lifeImputedIncomeCoverageCalculator = new LifeImputedIncomeCoverageCalculator();
        let actual = lifeImputedIncomeCoverageCalculator.GetImputedIncomeCoverage(coverageAmounts)
        let expected = decimal 20000
        Assert.Equal(expected, actual)

