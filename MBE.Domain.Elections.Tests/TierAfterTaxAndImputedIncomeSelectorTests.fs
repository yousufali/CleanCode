namespace MBE.Domain.Elections.Tests
open MBE.Domain.Elections
open MBE.Domain.Elections.Models
open System
open Xunit
open FsUnit.Xunit
open System.Collections.Generic
open System.Linq
open MBE.Domain.Elections.User
open MBE.Domain.Elections.DataAccess

module TierAfterTaxAndImputedIncomeSelectorTests =
    
    let electionData = new ElectionData (ParentUserID = 1500, PlanID = 1, TierID = 1)


    let getCoveredDependentSelector coveredDependents = 
        { new ICoveredDependentSelector with member this.SelectCoveredDependents(x, y) = coveredDependents }

    let getTierAfterTaxAndImputedIncomeRepository tierAfterTaxAndImputedIncome = 
        { new ITierAfterTaxAndImputedIncomeRepository with member this.GetTierAfterTaxAmdImputedIncome(x, y) = tierAfterTaxAndImputedIncome }

    [<Fact>]
    let ``UseNonTaxQualifiedCounts true, UseNonAndTaxQualifiedCount false and 1 taxeligible false dependent Test``() =
        let coveredDependentSelector = [| new CoveredDependent(UserID = 1501, RelationID = 8, TaxQualified = false, SpouseEquivalent = false) |] |> List |> getCoveredDependentSelector
        let tierAfterTaxAndImputedIncomeRepository = [| new TierAfterTaxAndImputedIncome(TierID = 1, AgeBanded = true, MinTaxQualified = 1, 
                                                            MaxTaxQualified = 99, AfterTax = decimal 174.10, ImputedIncome = decimal 522.29, UseNonTaxQualifiedCounts = true, SpouseEquivalentQualifiedCount = 1, 
                                                            MinChildrenCovered = 0, MaxChildrenCovered = 99, MinDPChildrenCovered = 0, MaxDPChildrenCovered = 99, UseNonAndTaxQualifiedCount = false,
                                                            NonTaxQualifiedMin = 0, NonTaxQualifiedMax = 0) |] |> List |> getTierAfterTaxAndImputedIncomeRepository

        let tierAfterTaxAndImputedIncomeSelector = new TierAfterTaxAndImputedIncomeSelector(tierAfterTaxAndImputedIncomeRepository, coveredDependentSelector)
        let actual = tierAfterTaxAndImputedIncomeSelector.GetEligibleRecord(electionData)
        let expected = new TierAfterTaxAndImputedIncome(TierID = 1, AgeBanded = true, MinTaxQualified = 1, 
                                                MaxTaxQualified = 99, AfterTax = decimal 174.10, ImputedIncome = decimal 522.29, UseNonTaxQualifiedCounts = true, SpouseEquivalentQualifiedCount = 1, 
                                                MinChildrenCovered = 0, MaxChildrenCovered = 99, MinDPChildrenCovered = 0, MaxDPChildrenCovered = 99, UseNonAndTaxQualifiedCount = false,
                                                NonTaxQualifiedMin = 0, NonTaxQualifiedMax = 0)
        Assert.Equal(expected, actual)



    [<Fact>]
    let ``UseNonTaxQualifiedCounts true, UseNonAndTaxQualifiedCount false and 1 taxeligible true dependent Test``() =
        let coveredDependentSelector = [| new CoveredDependent(UserID = 1501, RelationID = 8, TaxQualified = true, SpouseEquivalent = false) |] |> List |> getCoveredDependentSelector
        let tierAfterTaxAndImputedIncomeRepository = [| new TierAfterTaxAndImputedIncome(TierID = 1, AgeBanded = true, MinTaxQualified = 1, 
                                                            MaxTaxQualified = 99, AfterTax = decimal 174.10, ImputedIncome = decimal 522.29, UseNonTaxQualifiedCounts = true, SpouseEquivalentQualifiedCount = 1, 
                                                            MinChildrenCovered = 0, MaxChildrenCovered = 99, MinDPChildrenCovered = 0, MaxDPChildrenCovered = 99, UseNonAndTaxQualifiedCount = false,
                                                            NonTaxQualifiedMin = 0, NonTaxQualifiedMax = 0) |] |> List |> getTierAfterTaxAndImputedIncomeRepository

        let tierAfterTaxAndImputedIncomeSelector = new TierAfterTaxAndImputedIncomeSelector(tierAfterTaxAndImputedIncomeRepository, coveredDependentSelector)
        let actual = tierAfterTaxAndImputedIncomeSelector.GetEligibleRecord(electionData)

        let expected = new TierAfterTaxAndImputedIncome(TierID = 0, AgeBanded = false, MinTaxQualified = 0, 
                                                MaxTaxQualified = 0, AfterTax = decimal 0, ImputedIncome = decimal 0, UseNonTaxQualifiedCounts = false, SpouseEquivalentQualifiedCount = 0, 
                                                MinChildrenCovered = 0, MaxChildrenCovered = 0, MinDPChildrenCovered = 0, MaxDPChildrenCovered = 0, UseNonAndTaxQualifiedCount = false,
                                                NonTaxQualifiedMin = 0, NonTaxQualifiedMax = 0)
        Assert.Equal(expected, actual)

    [<Fact>]
    let ``UseNonAndTaxQualifiedCount true with 1 taxeligible true and 1 taxeligible false dependent Test``() =

        let coveredDependentSelector = [| new CoveredDependent(UserID = 1501, RelationID = 8, TaxQualified = true, SpouseEquivalent = false);
                                  new CoveredDependent(UserID = 1502, RelationID = 8, TaxQualified = false, SpouseEquivalent = false) |] |> List |> getCoveredDependentSelector

        let tierAfterTaxAndImputedIncomeRepository = [| new TierAfterTaxAndImputedIncome(TierID = 1, AgeBanded = true, MinTaxQualified = 1, 
                                                            MaxTaxQualified = 99, AfterTax = decimal 174.10, ImputedIncome = decimal 522.29, UseNonTaxQualifiedCounts = false, SpouseEquivalentQualifiedCount = 1, 
                                                            MinChildrenCovered = 0, MaxChildrenCovered = 99, MinDPChildrenCovered = 0, MaxDPChildrenCovered = 99, UseNonAndTaxQualifiedCount = true,
                                                            NonTaxQualifiedMin = 1, NonTaxQualifiedMax = 99) |] |> List |> getTierAfterTaxAndImputedIncomeRepository
        
        let tierAfterTaxAndImputedIncomeSelector = new TierAfterTaxAndImputedIncomeSelector(tierAfterTaxAndImputedIncomeRepository, coveredDependentSelector)
        let actual = tierAfterTaxAndImputedIncomeSelector.GetEligibleRecord(electionData)

        let expected = new TierAfterTaxAndImputedIncome(TierID = 1, AgeBanded = true, MinTaxQualified = 1, 
                                                MaxTaxQualified = 99, AfterTax = decimal 174.10, ImputedIncome = decimal 522.29, UseNonTaxQualifiedCounts = false, SpouseEquivalentQualifiedCount = 1, 
                                                MinChildrenCovered = 0, MaxChildrenCovered = 99, MinDPChildrenCovered = 0, MaxDPChildrenCovered = 99, UseNonAndTaxQualifiedCount = true,
                                                NonTaxQualifiedMin = 1, NonTaxQualifiedMax = 99)
        Assert.Equal(expected, actual)