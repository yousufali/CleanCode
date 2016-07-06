namespace MBE.Domain.Elections.Tests
open MBE.Domain.Elections
open MBE.Domain.Elections.Models
open System
open Xunit
open FsUnit.Xunit

module PremiumOverrideCalculatorTests =
    
    [<Fact>]
    let ``PremiumOverrideCalculator Non-AgeBanded Test``() =
        let selectedPlanAndTier = (new SelectedPlanAndTier( PlanID = 1, AgeBanding = false, 
                                                    Tier = new TierCost(TierID = 1, BaseTierID = 1, PremiumOverride = decimal 100.00 )))
        let tierAmountField = new TierAmountFields( TierID = 1)

        let actual = (new PremiumOverrideCalculator()).GetPremiumOverride(tierAmountField, selectedPlanAndTier)

        Assert.Equal(decimal 100.00, actual)

    [<Fact>]
    let ``PremiumOverrideCalculator AgeBanded FlatRate Tier Test``() =

        let selectedPlanAndTier = (new SelectedPlanAndTier( PlanID = 1, AgeBanding = true, 
                                                    Tier = new TierCost(TierID = 1, BaseTierID = 1, PremiumOverride = decimal 100.00, FlatRate = true )))
        let tierAmountField = new TierAmountFields( TierID = 1)

        let actual = (new PremiumOverrideCalculator()).GetPremiumOverride(tierAmountField, selectedPlanAndTier)

        Assert.Equal(decimal 100.00, actual)

    [<Fact>]
    let ``PremiumOverrideCalculator AgeBanded Non-FlatRate Tier With Per=0 Test``() =

        let selectedPlanAndTier = (new SelectedPlanAndTier( PlanID = 1, AgeBanding = true, 
                                                    Tier = new TierCost(TierID = 1, BaseTierID = 1, PremiumOverride = decimal 100.00, FlatRate = false, Per = decimal 0 )))
        let tierAmountField = new TierAmountFields( TierID = 1, CoverageAmount = decimal 100000)

        let actual = (new PremiumOverrideCalculator()).GetPremiumOverride(tierAmountField, selectedPlanAndTier)

        Assert.Equal(decimal 0.00, actual)

    [<Fact>]
    let ``PremiumOverrideCalculator AgeBanded Non-FlatRate Tier With Per > 0 Test``() =

        let selectedPlanAndTier = (new SelectedPlanAndTier( PlanID = 1, AgeBanding = true, 
                                                    Tier = new TierCost(TierID = 1, BaseTierID = 1, PremiumOverride = decimal 0.11, FlatRate = false, Per = decimal 100.00 )))
        let tierAmountField = new TierAmountFields( TierID = 1, CoverageAmount = decimal 8333.00)

        let actual = (new PremiumOverrideCalculator()).GetPremiumOverride(tierAmountField, selectedPlanAndTier)

        Assert.Equal(decimal 9.1663, actual)