namespace MBE.Domain.Elections.Tests
open Xunit
open FsUnit.Xunit
open MBE.Domain.Elections
open MBE.Domain.Elections.Models
open System

module ElectionSaverTests =
    
    [<Fact>]
    let ``SaveElection Tests``() = 
        let parameter = (new ElectionParameter.Builder())
                            .WithPlanTypeID(1)
                            .WithPlanID(25494)
                            .WithTierID(1)
                            .WithEmployeeContribution(decimal 522)
                            .WithEmployerContribution(decimal 100)
                            .WithPerPayCheckDeduction(decimal 240.92)
                            .WithCoverageAmount(decimal 0)
                            .WithBenefitAmount(decimal 0)
                            .WithEoiRequired(false)
                            .WithPcp(String.Empty)
                            .WithPcpSeen(false)
                            .WithPcp2(String.Empty)
                            .WithPcp2Seen(false)
                            .WithUserID(1500)
                            .WithClientID(1)
                            .WithEffectiveDate(DateTime.Parse "6/1/2016")
                            .WithSavedUserID(1500).Build()

        let actual = (new ElectionSaver()).Save(parameter)
        let expected = true
        Assert.Equal(expected, actual)
        