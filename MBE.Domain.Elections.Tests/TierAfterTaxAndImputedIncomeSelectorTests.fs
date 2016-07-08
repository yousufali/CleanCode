namespace MBE.Domain.Elections.Tests
open MBE.Domain.Elections
open MBE.Domain.Elections.Models
open System
open Xunit
open FsUnit.Xunit
open System.Collections.Generic
open System.Linq

module TierAfterTaxAndImputedIncomeSelectorTests =
    
    [<Fact>]
    let ``UseNonTaxQualifiedCounts true, UseNonAndTaxQualifiedCount false and 1 taxeligible false dependent Test``() =
        let tierAfterTaxAndImputedIncome = [| new TierAfterTaxAndImputedIncome(TierID = 1, AgeBanded = true, MinTaxQualified = 1, 
                                                MaxTaxQualified = 99, AfterTax = decimal 174.10, ImputedIncome = decimal 522.29, UseNonTaxQualifiedCounts = true, SpouseEquivalentQualifiedCount = 1, 
                                                MinChildrenCovered = 0, MaxChildrenCovered = 99, MinDPChildrenCovered = 0, MaxDPChildrenCovered = 99, UseNonAndTaxQualifiedCount = false,
                                                NonTaxQualifiedMin = 0, NonTaxQualifiedMax = 0) |] |> List;
        let coveredDependent = [| new CoveredDependent(UserID = 1501, RelationID = 8, TaxQualified = false, SpouseEquivalent = false) |] |> List

        let tierAfterTaxAndImputedIncomeSelector = new TierAfterTaxAndImputedIncomeSelector()
        let actual = tierAfterTaxAndImputedIncomeSelector.GetEligibleRecord(tierAfterTaxAndImputedIncome, coveredDependent)
        let expected = new TierAfterTaxAndImputedIncome(TierID = 1, AgeBanded = true, MinTaxQualified = 1, 
                                                MaxTaxQualified = 99, AfterTax = decimal 174.10, ImputedIncome = decimal 522.29, UseNonTaxQualifiedCounts = true, SpouseEquivalentQualifiedCount = 1, 
                                                MinChildrenCovered = 0, MaxChildrenCovered = 99, MinDPChildrenCovered = 0, MaxDPChildrenCovered = 99, UseNonAndTaxQualifiedCount = false,
                                                NonTaxQualifiedMin = 0, NonTaxQualifiedMax = 0)
        Assert.Equal(expected, actual)



    [<Fact>]
    let ``UseNonTaxQualifiedCounts true, UseNonAndTaxQualifiedCount false and 1 taxeligible true dependent Test``() =
        let tierAfterTaxAndImputedIncome = [| new TierAfterTaxAndImputedIncome(TierID = 1, AgeBanded = true, MinTaxQualified = 1, 
                                                MaxTaxQualified = 99, AfterTax = decimal 174.10, ImputedIncome = decimal 522.29, UseNonTaxQualifiedCounts = true, SpouseEquivalentQualifiedCount = 1, 
                                                MinChildrenCovered = 0, MaxChildrenCovered = 99, MinDPChildrenCovered = 0, MaxDPChildrenCovered = 99, UseNonAndTaxQualifiedCount = false,
                                                NonTaxQualifiedMin = 0, NonTaxQualifiedMax = 0) |] |> List;
        let coveredDependent = [| new CoveredDependent(UserID = 1501, RelationID = 8, TaxQualified = true, SpouseEquivalent = false) |] |> List

        let tierAfterTaxAndImputedIncomeSelector = new TierAfterTaxAndImputedIncomeSelector()
        let actual = tierAfterTaxAndImputedIncomeSelector.GetEligibleRecord(tierAfterTaxAndImputedIncome, coveredDependent)
        let expected = new TierAfterTaxAndImputedIncome(TierID = 0, AgeBanded = false, MinTaxQualified = 0, 
                                                MaxTaxQualified = 0, AfterTax = decimal 0, ImputedIncome = decimal 0, UseNonTaxQualifiedCounts = false, SpouseEquivalentQualifiedCount = 0, 
                                                MinChildrenCovered = 0, MaxChildrenCovered = 0, MinDPChildrenCovered = 0, MaxDPChildrenCovered = 0, UseNonAndTaxQualifiedCount = false,
                                                NonTaxQualifiedMin = 0, NonTaxQualifiedMax = 0)
        Assert.Equal(expected, actual)

    [<Fact>]
    let ``UseNonAndTaxQualifiedCount true with 1 taxeligible true and 1 taxeligible false dependent Test``() =
        let tierAfterTaxAndImputedIncome = [| new TierAfterTaxAndImputedIncome(TierID = 1, AgeBanded = true, MinTaxQualified = 1, 
                                                MaxTaxQualified = 99, AfterTax = decimal 174.10, ImputedIncome = decimal 522.29, UseNonTaxQualifiedCounts = false, SpouseEquivalentQualifiedCount = 1, 
                                                MinChildrenCovered = 0, MaxChildrenCovered = 99, MinDPChildrenCovered = 0, MaxDPChildrenCovered = 99, UseNonAndTaxQualifiedCount = true,
                                                NonTaxQualifiedMin = 1, NonTaxQualifiedMax = 99) |] |> List;
        let coveredDependent = [| new CoveredDependent(UserID = 1501, RelationID = 8, TaxQualified = true, SpouseEquivalent = false);
                                  new CoveredDependent(UserID = 1502, RelationID = 8, TaxQualified = false, SpouseEquivalent = false) |] |> List

        let tierAfterTaxAndImputedIncomeSelector = new TierAfterTaxAndImputedIncomeSelector()
        let actual = tierAfterTaxAndImputedIncomeSelector.GetEligibleRecord(tierAfterTaxAndImputedIncome, coveredDependent)
        let expected = new TierAfterTaxAndImputedIncome(TierID = 1, AgeBanded = true, MinTaxQualified = 1, 
                                                MaxTaxQualified = 99, AfterTax = decimal 174.10, ImputedIncome = decimal 522.29, UseNonTaxQualifiedCounts = false, SpouseEquivalentQualifiedCount = 1, 
                                                MinChildrenCovered = 0, MaxChildrenCovered = 99, MinDPChildrenCovered = 0, MaxDPChildrenCovered = 99, UseNonAndTaxQualifiedCount = true,
                                                NonTaxQualifiedMin = 1, NonTaxQualifiedMax = 99)
        Assert.Equal(expected, actual)