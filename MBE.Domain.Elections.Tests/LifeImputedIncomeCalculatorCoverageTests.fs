namespace MBE.Domain.Elections.Tests
open MBE.Domain.Elections
open MBE.Domain.Elections.Models
open System
open Xunit
open FsUnit.Xunit
open System.Collections.Generic
open System.Linq
open MBE.Domain.Elections.DataAccess

module LifeImputedIncomeCalculatorCoverageTests =
    
    let getBenefitElectionRepository benefitElections = { new IBenefitElectionRepository with member this.SelectBenefitElections(x, y) = benefitElections }

    let getPlanTypeRepository planTypes = { new IPlanTypeRepository with member this.SelectPlanTypes(x) = planTypes };
    let dtp input = DateTime.Parse input

    [<Fact>]
    let ``Total Coverage Less than 50000 Test``() =    
        let benefitElectionRepository = [| new BenefitElection(UserID = 1500, ParentUserID = 1500, PlanTypeID = 16, BenefitStartDate = dtp "1/1/2016", BenefitEndDate = dtp "12/31/2099", Coverage = decimal 10000);
                                           new BenefitElection(UserID = 1500, ParentUserID = 1500, PlanTypeID = 32, BenefitStartDate = dtp "1/1/2016", BenefitEndDate = dtp "12/31/2099", Coverage = decimal 10000) |]
                                            |> List |> getBenefitElectionRepository
        let planTypeRepository = [| new ClientPlanOrder(PlanTypeID = 16, LifeImputedIncomeHolder = true, IncludeInLifeImputedIncome = true);
                                    new ClientPlanOrder(PlanTypeID = 32, LifeImputedIncomeHolder = true, IncludeInLifeImputedIncome = true)  |]
                                    |> List |> getPlanTypeRepository
        let electionData = new ElectionData (ParentUserID = 1500, PlanID = 1, TierID = 1)

        let lifeImputedIncomeCoverageCalculator = new LifeImputedIncomeCoverageCalculator(benefitElectionRepository, planTypeRepository);
        let actual = lifeImputedIncomeCoverageCalculator.GetImputedIncomeCoverage(electionData)
        Assert.Equal(decimal 0, actual)

    [<Fact>]
    let ``Total Coverage greater than 50000 Test``() =
        let benefitElectionRepository = [| new BenefitElection(UserID = 1500, ParentUserID = 1500, PlanTypeID = 16, BenefitStartDate = dtp "1/1/2016", 
                                                                        BenefitEndDate = dtp "12/31/2099", Coverage = decimal 35000);
                                           new BenefitElection(UserID = 1500, ParentUserID = 1500, PlanTypeID = 32, BenefitStartDate = dtp "1/1/2016", 
                                                                        BenefitEndDate = dtp "12/31/2099", Coverage = decimal 35000) |]
                                            |> List |> getBenefitElectionRepository
        let planTypeRepository = [| new ClientPlanOrder(PlanTypeID = 16, LifeImputedIncomeHolder = true, IncludeInLifeImputedIncome = true);
                                    new ClientPlanOrder(PlanTypeID = 32, LifeImputedIncomeHolder = true, IncludeInLifeImputedIncome = true)  |]
                                    |> List |> getPlanTypeRepository
        let electionData = new ElectionData (ParentUserID = 1500, PlanID = 1, TierID = 1)

        let lifeImputedIncomeCoverageCalculator = new LifeImputedIncomeCoverageCalculator(benefitElectionRepository, planTypeRepository);
        let actual = lifeImputedIncomeCoverageCalculator.GetImputedIncomeCoverage(electionData)
        Assert.Equal(decimal 20000, actual)


