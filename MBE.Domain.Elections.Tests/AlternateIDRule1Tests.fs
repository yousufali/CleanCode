namespace MBE.Domain.Elections.Tests
open MBE.Domain.Elections
open MBE.Domain.Elections.Models
open System
open Xunit
open FsUnit.Xunit
open MBE.Domain.Elections.AlternateID
open MBE.Domain.Elections.DataAccess
open System.Collections.Generic
open System.Linq

module AlternateIDRule1Tests =

    [<Fact>]
    let ``AlternateId Rule 1 Test when Alternate Id exists for Day before Effective Date Election``() =
        let benefitElectionAlternateIDs = [| new BenefitElectionAlternateID(UserID = 1500, ParentUserID = 1500, PlanTypeID = 1, 
                                                 BenefitStartDate = DateTime.Parse "1/1/2015", BenefitEndDate = DateTime.Parse "12/31/2015", 
                                                 AlternateID = "1") |] |> List
        let coveredUsers = [| new CoveredUser(UserID = 1500, RelationID = 1) |] |> List
        let rule1Calculator = new Rule1Calculator();

        let expected = [| new UserAlternateID( UserID = 1500, AlternateID = "1") |]
        let actual = rule1Calculator.GetAlternateID(benefitElectionAlternateIDs, coveredUsers, DateTime.Parse "1/1/2016")

        Assert.Equal(expected, actual)

    [<Fact>]
    let ``AlternateId Rule 1 Test when no election exists day before effective date``() =
        let benefitElectionAlternateIDs = [| new BenefitElectionAlternateID(UserID = 1500, ParentUserID = 1500, PlanTypeID = 1, 
                                                 BenefitStartDate = DateTime.Parse "1/1/2014", BenefitEndDate = DateTime.Parse "12/31/2014", 
                                                 AlternateID = "1") |] |> List
        let coveredUsers = [| new CoveredUser(UserID = 1500, RelationID = 1) |] |> List
        let rule1Calculator = new Rule1Calculator();

        let expected = [| new UserAlternateID( UserID = 1500) |]
        let actual = rule1Calculator.GetAlternateID(benefitElectionAlternateIDs, coveredUsers, DateTime.Parse "1/1/2016")

        Assert.Equal(expected, actual)

    [<Fact>]
    let ``AlternateId Rule 1 Test when no election exists day before effective date but exists as of effective date``() =
        let benefitElectionAlternateIDs = [| new BenefitElectionAlternateID(UserID = 1500, ParentUserID = 1500, PlanTypeID = 1, 
                                                 BenefitStartDate = DateTime.Parse "1/1/2016", BenefitEndDate = DateTime.Parse "12/31/2099", 
                                                 AlternateID = "1") |] |> List
        let coveredUsers = [| new CoveredUser(UserID = 1500, RelationID = 1) |] |> List
        let rule1Calculator = new Rule1Calculator();

        let expected = [| new UserAlternateID( UserID = 1500, AlternateID = "1") |]
        let actual = rule1Calculator.GetAlternateID(benefitElectionAlternateIDs, coveredUsers, DateTime.Parse "1/1/2016")

        Assert.Equal(expected, actual)
