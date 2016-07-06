namespace MBE.Domain.Elections.Tests
open MBE.Domain.Elections
open MBE.Domain.Elections.Models
open System
open Xunit
open FsUnit.Xunit
open System.Collections.Generic
open System.Linq

module BenefitElectionAdminFeeCalculatorTests =
    
    let tierAmountField = new TierAmountFields( TierID = 1, EmployeeContribution = decimal 120, EmployerContribution = decimal 150, PerPayCheckDeduction = decimal 55.38, CoverageAmount = decimal 0)
    let premiumOverrideCalculator =  { new IPremiumOverrideCalculator with member this.GetPremiumOverride(x, y) = decimal 100.00 }
    
    [<Fact>]
    let ``BenefitElectionAdminFeeCalculator One Flat Admin Fee Test``() =
        let adminFees = seq [ new AdminFee( AdminFeeID = 1, PlanID = 1, TierID = 1, AdminFeeTypeID = 1, AdminFeeCalculationTypeID = 1, Fee = decimal 10.00, IncludeInPremiumOverride = true, 
                                            IncludeInERCost = true, IncludeInEECost = true, IncludeInPremium = true) ] |> List
        let selectedPlanAndTier = (new SelectedPlanAndTier( PlanID = 1, AgeBanding = true, 
                                                    Tier = new TierCost(TierID = 1, BaseTierID = 1, PremiumOverride = decimal 100.00, FlatRate = true ),
                                                    AdminFees = adminFees))
        let benefitElectionAdminFeeCalculator = new BenefitElectionAdminFeeCalculator(premiumOverrideCalculator)
        let actual = benefitElectionAdminFeeCalculator.GetBenefitElectionAdminFees(selectedPlanAndTier, tierAmountField)
        let expected = seq [ new BenefitElectionAdminFee(AdminFeeID = 1, FeeAmount = decimal 10.00, EmployerMonthlyCost = decimal 10.0, EmployeeMonthlyCost = decimal 10.0, Premium = decimal 10.0,
                                        PremiumOverride = decimal 10.0, IncludeInPremiumOverride = true , IncludeInERCost = true, IncludeInEECost = true, IncludeInPremium = true)] |> List

        Assert.Equal<List<BenefitElectionAdminFee>>(expected, actual)


    [<Fact>]
    let ``Admin Fee EmployeeMonthlyCost is 0 if IncludeInEECost false Test``() =
        let adminFees = seq [ new AdminFee( AdminFeeID = 1, PlanID = 1, TierID = 1, AdminFeeTypeID = 1, AdminFeeCalculationTypeID = 1, Fee = decimal 10.00, IncludeInPremiumOverride = true, 
                                            IncludeInERCost = true, IncludeInEECost = false, IncludeInPremium = true) ] |> List
        let selectedPlanAndTier = (new SelectedPlanAndTier( PlanID = 1, AgeBanding = true, 
                                                    Tier = new TierCost(TierID = 1, BaseTierID = 1, PremiumOverride = decimal 100.00, FlatRate = true ),
                                                    AdminFees = adminFees))

        let benefitElectionAdminFeeCalculator = new BenefitElectionAdminFeeCalculator(premiumOverrideCalculator)
        let actual = benefitElectionAdminFeeCalculator.GetBenefitElectionAdminFees(selectedPlanAndTier, tierAmountField)
        let expected = seq [ new BenefitElectionAdminFee(AdminFeeID = 1, FeeAmount = decimal 10.00, EmployerMonthlyCost = decimal 10.0, EmployeeMonthlyCost = decimal 0.0, Premium = decimal 10.0,
                                        PremiumOverride = decimal 10.0, IncludeInPremiumOverride = true , IncludeInERCost = true, IncludeInEECost = false, IncludeInPremium = true)] |> List

        Assert.Equal<List<BenefitElectionAdminFee>>(expected, actual)

    [<Fact>]
    let ``Admin Fee EmployerMonthlyCost is 0 if IncludeInERCost false Test``() =
        let adminFees = seq [ new AdminFee( AdminFeeID = 1, PlanID = 1, TierID = 1, AdminFeeTypeID = 1, AdminFeeCalculationTypeID = 1, Fee = decimal 10.00, IncludeInPremiumOverride = true, 
                                            IncludeInERCost = false, IncludeInEECost = true, IncludeInPremium = true) ] |> List
        let selectedPlanAndTier = (new SelectedPlanAndTier( PlanID = 1, AgeBanding = true, 
                                                    Tier = new TierCost(TierID = 1, BaseTierID = 1, PremiumOverride = decimal 100.00, FlatRate = true ),
                                                    AdminFees = adminFees))

        let benefitElectionAdminFeeCalculator = new BenefitElectionAdminFeeCalculator(premiumOverrideCalculator)
        let actual = benefitElectionAdminFeeCalculator.GetBenefitElectionAdminFees(selectedPlanAndTier, tierAmountField)
        let expected = seq [ new BenefitElectionAdminFee(AdminFeeID = 1, FeeAmount = decimal 10.00, EmployerMonthlyCost = decimal 0.0, EmployeeMonthlyCost = decimal 10.0, Premium = decimal 10.0,
                                        PremiumOverride = decimal 10.0, IncludeInPremiumOverride = true , IncludeInERCost = false, IncludeInEECost = true, IncludeInPremium = true)] |> List

        Assert.Equal<List<BenefitElectionAdminFee>>(expected, actual)


    [<Fact>]
    let ``Admin Fee Premium is 0 if IncludeInPremium false Test``() =
        let adminFees = seq [ new AdminFee( AdminFeeID = 1, PlanID = 1, TierID = 1, AdminFeeTypeID = 1, AdminFeeCalculationTypeID = 1, Fee = decimal 10.00, IncludeInPremiumOverride = true, 
                                            IncludeInERCost = true, IncludeInEECost = true, IncludeInPremium = false) ] |> List
        let selectedPlanAndTier = (new SelectedPlanAndTier( PlanID = 1, AgeBanding = true, 
                                                    Tier = new TierCost(TierID = 1, BaseTierID = 1, PremiumOverride = decimal 100.00, FlatRate = true ),
                                                    AdminFees = adminFees))

        let benefitElectionAdminFeeCalculator = new BenefitElectionAdminFeeCalculator(premiumOverrideCalculator)
        let actual = benefitElectionAdminFeeCalculator.GetBenefitElectionAdminFees(selectedPlanAndTier, tierAmountField)
        let expected = seq [ new BenefitElectionAdminFee(AdminFeeID = 1, FeeAmount = decimal 10.00, EmployerMonthlyCost = decimal 10.0, EmployeeMonthlyCost = decimal 10.0, Premium = decimal 0.0,
                                        PremiumOverride = decimal 10.0, IncludeInPremiumOverride = true , IncludeInERCost = true, IncludeInEECost = true, IncludeInPremium = false)] |> List

        Assert.Equal<List<BenefitElectionAdminFee>>(expected, actual)

    [<Fact>]
    let ``Admin Fee PremiumOverride is 0 if IncludeInPremiumOverride false Test``() =
        let adminFees = seq [ new AdminFee( AdminFeeID = 1, PlanID = 1, TierID = 1, AdminFeeTypeID = 1, AdminFeeCalculationTypeID = 1, Fee = decimal 10.00, IncludeInPremiumOverride = false, 
                                            IncludeInERCost = true, IncludeInEECost = true, IncludeInPremium = true) ] |> List
        let selectedPlanAndTier = (new SelectedPlanAndTier( PlanID = 1, AgeBanding = true, 
                                                    Tier = new TierCost(TierID = 1, BaseTierID = 1, PremiumOverride = decimal 100.00, FlatRate = true ),
                                                    AdminFees = adminFees))

        let benefitElectionAdminFeeCalculator = new BenefitElectionAdminFeeCalculator(premiumOverrideCalculator)
        let actual = benefitElectionAdminFeeCalculator.GetBenefitElectionAdminFees(selectedPlanAndTier, tierAmountField)
        let expected = seq [ new BenefitElectionAdminFee(AdminFeeID = 1, FeeAmount = decimal 10.00, EmployerMonthlyCost = decimal 10.0, EmployeeMonthlyCost = decimal 10.0, Premium = decimal 10.0,
                                        PremiumOverride = decimal 0.0, IncludeInPremiumOverride = false , IncludeInERCost = true, IncludeInEECost = true, IncludeInPremium = true)] |> List

        Assert.Equal<List<BenefitElectionAdminFee>>(expected, actual)

    [<Fact>]
    let ``BenefitElectionAdminFeeCalculator One Percentage Admin Fee Test``() =
        let adminFees = seq [ new AdminFee( AdminFeeID = 1, PlanID = 1, TierID = 1, AdminFeeTypeID = 1, AdminFeeCalculationTypeID = 2, Fee = decimal 2.50, IncludeInPremiumOverride = true, 
                                            IncludeInERCost = true, IncludeInEECost = true, IncludeInPremium = true) ] |> List
        let selectedPlanAndTier = (new SelectedPlanAndTier( PlanID = 1, AgeBanding = true, 
                                                    Tier = new TierCost(TierID = 1, BaseTierID = 1, PremiumOverride = decimal 100.00, FlatRate = true ),
                                                    AdminFees = adminFees))

        let benefitElectionAdminFeeCalculator = new BenefitElectionAdminFeeCalculator(premiumOverrideCalculator)
        let actual = benefitElectionAdminFeeCalculator.GetBenefitElectionAdminFees(selectedPlanAndTier, tierAmountField)
        let expected = seq [ new BenefitElectionAdminFee(AdminFeeID = 1, FeeAmount = decimal 2.5, EmployeeMonthlyCost = decimal 3.0, EmployerMonthlyCost = decimal 3.75, Premium = decimal 6.75,
                                        PremiumOverride = decimal 2.5, IncludeInPremiumOverride = true , IncludeInERCost = true, IncludeInEECost = true, IncludeInPremium = true)] |> List

        Assert.Equal<List<BenefitElectionAdminFee>>(expected, actual)

    [<Fact>]
    let ``BenefitElectionAdminFeeCalculator One Flat and One Percentage Admin Fee Test``() =
        let adminFees = seq [ new AdminFee( AdminFeeID = 1, PlanID = 1, TierID = 1, AdminFeeTypeID = 1, AdminFeeCalculationTypeID = 1, Fee = decimal 10.00, IncludeInPremiumOverride = true, IncludeInERCost = true, IncludeInEECost = true, IncludeInPremium = true);
                              new AdminFee( AdminFeeID = 1, PlanID = 1, TierID = 1, AdminFeeTypeID = 1, AdminFeeCalculationTypeID = 2, Fee = decimal 2.50, IncludeInPremiumOverride = true, IncludeInERCost = true, IncludeInEECost = true, IncludeInPremium = true) ] |> List
        let selectedPlanAndTier = (new SelectedPlanAndTier( PlanID = 1, AgeBanding = true, 
                                                    Tier = new TierCost(TierID = 1, BaseTierID = 1, PremiumOverride = decimal 100.00, FlatRate = true ),
                                                    AdminFees = adminFees))

        let benefitElectionAdminFeeCalculator = new BenefitElectionAdminFeeCalculator(premiumOverrideCalculator)
        let actual = benefitElectionAdminFeeCalculator.GetBenefitElectionAdminFees(selectedPlanAndTier, tierAmountField)
        let expected = seq [ new BenefitElectionAdminFee(AdminFeeID = 1, FeeAmount = decimal 10.00, EmployerMonthlyCost = decimal 10.0, EmployeeMonthlyCost = decimal 10.0, Premium = decimal 10.0,
                                        PremiumOverride = decimal 10.0, IncludeInPremiumOverride = true , IncludeInERCost = true, IncludeInEECost = true, IncludeInPremium = true);
                             new BenefitElectionAdminFee(AdminFeeID = 1, FeeAmount = decimal 2.5, EmployeeMonthlyCost = decimal 3.0, EmployerMonthlyCost = decimal 3.75, Premium = decimal 6.75,
                                        PremiumOverride = decimal 2.5, IncludeInPremiumOverride = true , IncludeInERCost = true, IncludeInEECost = true, IncludeInPremium = true)] |> List

        Assert.Equal<List<BenefitElectionAdminFee>>(expected, actual)