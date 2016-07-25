namespace MBE.Domain.Elections.Tests
open MBE.Domain.Elections
open MBE.Domain.Elections.Models
open System
open Xunit
open FsUnit.Xunit
open System.Collections.Generic
open System.Linq
open MBE.Domain.Elections.DataAccess

module LifeImputedIncomeCostCalculatorTests =

    let imputedIncomeCosts = [| new ImputedIncomeCost(Minage = 0, MaxAge = 99, Cost = decimal 0.05) |] |> List
    let imputedIncomeCostsRepository = { new IImputedIncomeCostsRepository with member this.GetImputedIncomeCosts() = imputedIncomeCosts }
    let getLifeImputedIncomeCoverageCalculator imputedIncomeCoverage = { new ILifeImputedIncomeCoverageCalculator with member this.GetImputedIncomeCoverage(x) = decimal imputedIncomeCoverage }
    let userRepository = { new IUserRepository with 
                                                    member this.GetUser(x) = new User(BirthDate = DateTime.Parse "1/1/1980")
                                                    member this.GetUserBenefitClasses(x) = null
                                                    member this.GetEmployee(x) = null }
            
    [<Fact>]
    let ``Imputed Income Monthly Cost is 0 when Imputed Income Coverage is 0``() =
        let selectedPlanAndTier = new SelectedPlanAndTier(ImputedIncomeCosts = imputedIncomeCosts, User = new User(BirthDate = DateTime.Parse "1/1/1980"))
        let lifeImputedIncomeCoverageCalculator = getLifeImputedIncomeCoverageCalculator 0

        let lifeImputedIncomeCalculator = new LifeImputedIncomeCalculator(lifeImputedIncomeCoverageCalculator, userRepository, imputedIncomeCostsRepository)

        let actual = lifeImputedIncomeCalculator.GetImputedIncomeMonthly(new ElectionData())
        let expected = decimal 0
        Assert.Equal(expected, actual)
    

    [<Fact>]
    let ``Imputed Income Monthly Cost greater than 0 when Imputed Income Coverage is greater than 0``() =
        let selectedPlanAndTier = new SelectedPlanAndTier(ImputedIncomeCosts = imputedIncomeCosts, User = new User(BirthDate = DateTime.Parse "1/1/1980"))
        let lifeImputedIncomeCoverageCalculator = getLifeImputedIncomeCoverageCalculator 30000

        let lifeImputedIncomeCalculator = new LifeImputedIncomeCalculator(lifeImputedIncomeCoverageCalculator, userRepository, imputedIncomeCostsRepository)
        let actual = lifeImputedIncomeCalculator.GetImputedIncomeMonthly(new ElectionData(EffectiveDate = DateTime.Parse "1/1/2016"))
        let expected = decimal 1.5
        Assert.Equal(expected, actual)
