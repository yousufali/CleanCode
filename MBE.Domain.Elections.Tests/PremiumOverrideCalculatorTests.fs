module PremiumOverrideCalculatorTests
//namespace MBE.Domain.Elections.Tests
//open MBE.Domain.Elections
//open MBE.Domain.Elections.Models
//open System
//open Xunit
//open FsUnit.Xunit
//
//module PremiumOverrideCalculatorTests =
//    
//    [<Fact>]
//    let ``PremiumOverrideCalculator Non-AgeBanded Test``() =
//        let selectedPlanAndTier = (new SelectedPlanAndTier( PlanID = 1, AgeBanding = false, 
//                                                    Tier = new TierCost(TierID = 1, BaseTierID = 1, PremiumOverride = decimal 100.00 )))
//        let tierAmountField = new TierAmountFields( TierID = 1)
//
//        let actual = (new PremiumOverrideCalculator()).GetPremiumOverride(tierAmountField, selectedPlanAndTier)
//
//        Assert.Equal(decimal 100.00, actual)
