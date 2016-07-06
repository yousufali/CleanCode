namespace MBE.Domain.Elections.Tests
open MBE.Domain.Elections
open MBE.Domain.Elections.Models
open System
open Xunit
open FsUnit.Xunit
open System.Collections.Generic

module PremiumOverrideWithAdminFeeCalculatorTests =

    [<Fact>]
    let ``PremiumOverrideWithAdminFeeCalculator Test``() =

//        let adminFees = seq [ new AdminFee( AdminFeeID = 1, PlanID = 1, TierID = 1, AdminFeeTypeID = 1, AdminFeeCalculationTypeID = 1, Fee = decimal 10.00, IncludeInPremiumOverride = true) ]
        let selectedPlanAndTier = (new SelectedPlanAndTier( PlanID = 1, AgeBanding = true, 
                                                    Tier = new TierCost(TierID = 1, BaseTierID = 1, PremiumOverride = decimal 100.00, FlatRate = true )))
//                                                    AdminFees = new System.Collections.Generic.List<AdminFee>(adminFees)))
        let tierAmountField = new TierAmountFields( TierID = 1)
        let benefitElectionAdminFees = seq [ new BenefitElectionAdminFee(AdminFeeID = 1, FeeAmount = decimal 100.00, PremiumOverride = decimal 10.0, IncludeInPremiumOverride = true )]

        let premiumOverrideCalculator =  { new IPremiumOverrideCalculator with member this.GetPremiumOverride(x, y) = decimal 100.00 }
        let benefitElectionAdminFeeCalculator  = { new IBenefitElectionAdminFeeCalculator 
                                                    with member this.GetBenefitElectionAdminFees(x, y) = new List<BenefitElectionAdminFee>(benefitElectionAdminFees) }

        let premiumOverrideWithAdminFeeCalculator = new PremiumOverrideWithAdminFeeCalculator(premiumOverrideCalculator, benefitElectionAdminFeeCalculator)
        let actual = premiumOverrideWithAdminFeeCalculator.GetPremiumOverride(tierAmountField, selectedPlanAndTier)

        Assert.Equal(decimal 110.00, actual)

