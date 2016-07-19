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

module AlternateIDRule2Tests =

    [<Fact>]
    let ``AlternateId Rule 2 Test when Alternate Id exists for Day before Effective Date Election``() =
        let benefitElectionAlternateIDs = [| new BenefitElectionAlternateID(UserID = 1500, ParentUserID = 1500, PlanTypeID = 1, 
                                                 BenefitStartDate = DateTime.Parse "1/1/2015", BenefitEndDate = DateTime.Parse "12/31/2015", 
                                                 AlternateID = "A");
                                             new BenefitElectionAlternateID(UserID = 1501, ParentUserID = 1500, PlanTypeID = 1, 
                                                 BenefitStartDate = DateTime.Parse "1/1/2015", BenefitEndDate = DateTime.Parse "12/31/2015", 
                                                 AlternateID = "B"); 
                                              new BenefitElectionAlternateID(UserID = 1502, ParentUserID = 1500, PlanTypeID = 1, 
                                                 BenefitStartDate = DateTime.Parse "1/1/2015", BenefitEndDate = DateTime.Parse "12/31/2015", 
                                                 AlternateID = "C"); |] |> List

        let coveredUsers = [| new CoveredUser(UserID = 1500, RelationID = 1);
                              new CoveredUser(UserID = 1501, RelationID = 2);
                              new CoveredUser(UserID = 1502, RelationID = 8) |] |> List
        let rule2Calculator = new Rule2Calculator();

        let expected = [| new UserAlternateID( UserID = 1500, AlternateID = "A");
                          new UserAlternateID( UserID = 1501, AlternateID = "B");
                          new UserAlternateID( UserID = 1502, AlternateID = "C") |]
        let actual = rule2Calculator.GetAlternateID(benefitElectionAlternateIDs, coveredUsers, DateTime.Parse "1/1/2016")

        Assert.Equal(expected, actual)

    [<Fact>]
    let ``AlternateId Rule 2 Test when New Dependent added to coverage``() =
        let benefitElectionAlternateIDs = [| new BenefitElectionAlternateID(UserID = 1500, ParentUserID = 1500, PlanTypeID = 1, 
                                                 BenefitStartDate = DateTime.Parse "1/1/2015", BenefitEndDate = DateTime.Parse "12/31/2015", 
                                                 AlternateID = "A");
                                             new BenefitElectionAlternateID(UserID = 1501, ParentUserID = 1500, PlanTypeID = 1, 
                                                 BenefitStartDate = DateTime.Parse "1/1/2015", BenefitEndDate = DateTime.Parse "12/31/2015", 
                                                 AlternateID = "B"); 
                                              new BenefitElectionAlternateID(UserID = 1502, ParentUserID = 1500, PlanTypeID = 1, 
                                                 BenefitStartDate = DateTime.Parse "1/1/2015", BenefitEndDate = DateTime.Parse "12/31/2015", 
                                                 AlternateID = "C"); |] |> List

        let coveredUsers = [| new CoveredUser(UserID = 1500, RelationID = 1);
                              new CoveredUser(UserID = 1501, RelationID = 2);
                              new CoveredUser(UserID = 1502, RelationID = 8);
                              new CoveredUser(UserID = 1503, RelationID = 8) |] |> List
        let rule2Calculator = new Rule2Calculator();

        let expected = [| new UserAlternateID( UserID = 1500, AlternateID = "A");
                          new UserAlternateID( UserID = 1501, AlternateID = "B");
                          new UserAlternateID( UserID = 1502, AlternateID = "C");
                          new UserAlternateID( UserID = 1503, AlternateID = "D") |]
        let actual = rule2Calculator.GetAlternateID(benefitElectionAlternateIDs, coveredUsers, DateTime.Parse "1/1/2016")

        Assert.Equal(expected, actual)

    [<Fact>]
    let ``AlternateId Rule 2 Test when a dependent dropped and New Dependent added to coverage``() =
        let benefitElectionAlternateIDs = [| new BenefitElectionAlternateID(UserID = 1500, ParentUserID = 1500, PlanTypeID = 1, 
                                                 BenefitStartDate = DateTime.Parse "1/1/2015", BenefitEndDate = DateTime.Parse "12/31/2015", 
                                                 AlternateID = "A");
                                             new BenefitElectionAlternateID(UserID = 1501, ParentUserID = 1500, PlanTypeID = 1, 
                                                 BenefitStartDate = DateTime.Parse "1/1/2015", BenefitEndDate = DateTime.Parse "12/31/2015", 
                                                 AlternateID = "B"); 
                                              new BenefitElectionAlternateID(UserID = 1502, ParentUserID = 1500, PlanTypeID = 1, 
                                                 BenefitStartDate = DateTime.Parse "1/1/2015", BenefitEndDate = DateTime.Parse "12/31/2015", 
                                                 AlternateID = "C"); |] |> List

        let coveredUsers = [| new CoveredUser(UserID = 1500, RelationID = 1);
                              new CoveredUser(UserID = 1501, RelationID = 2);
                              new CoveredUser(UserID = 1503, RelationID = 8) |] |> List
        let rule2Calculator = new Rule2Calculator();

        let expected = [| new UserAlternateID( UserID = 1500, AlternateID = "A");
                          new UserAlternateID( UserID = 1501, AlternateID = "B");
                          new UserAlternateID( UserID = 1503, AlternateID = "D") |]
        let actual = rule2Calculator.GetAlternateID(benefitElectionAlternateIDs, coveredUsers, DateTime.Parse "1/1/2016")

        Assert.Equal(expected, actual)

    [<Fact>]
    let ``AlternateId Rule 2 Test when enrolled first time by user``() =
        let benefitElectionAlternateIDs = [| |] |> List

        let coveredUsers = [| new CoveredUser(UserID = 1500, RelationID = 1);
                              new CoveredUser(UserID = 1501, RelationID = 2);
                              new CoveredUser(UserID = 1503, RelationID = 8) |] |> List
        let rule2Calculator = new Rule2Calculator();

        let expected = [| new UserAlternateID( UserID = 1500, AlternateID = "A");
                          new UserAlternateID( UserID = 1501, AlternateID = "B");
                          new UserAlternateID( UserID = 1503, AlternateID = "C") |]
        let actual = rule2Calculator.GetAlternateID(benefitElectionAlternateIDs, coveredUsers, DateTime.Parse "1/1/2016")

        Assert.Equal(expected, actual)
   