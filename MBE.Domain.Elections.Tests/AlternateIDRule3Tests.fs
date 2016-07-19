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

module AlternateIDRule3Tests =

    [<Fact>]
    let ``AlternateId Rule 3 Test when Alternate Id exists for Day before Effective Date Election``() =
        let benefitElectionAlternateIDs = [| new BenefitElectionAlternateID(UserID = 1500, ParentUserID = 1500, PlanTypeID = 1, 
                                                 BenefitStartDate = DateTime.Parse "1/1/2015", BenefitEndDate = DateTime.Parse "12/31/2015", 
                                                 AlternateID = "1");
                                             new BenefitElectionAlternateID(UserID = 1501, ParentUserID = 1500, PlanTypeID = 1, 
                                                 BenefitStartDate = DateTime.Parse "1/1/2015", BenefitEndDate = DateTime.Parse "12/31/2015", 
                                                 AlternateID = "2"); 
                                              new BenefitElectionAlternateID(UserID = 1502, ParentUserID = 1500, PlanTypeID = 1, 
                                                 BenefitStartDate = DateTime.Parse "1/1/2015", BenefitEndDate = DateTime.Parse "12/31/2015", 
                                                 AlternateID = "3"); |] |> List

        let coveredUsers = [| new CoveredUser(UserID = 1500, RelationID = 1);
                              new CoveredUser(UserID = 1501, RelationID = 2);
                              new CoveredUser(UserID = 1502, RelationID = 8) |] |> List
        let rule3Calculator = new Rule3Calculator();

        let expected = [| new UserAlternateID( UserID = 1500, AlternateID = "1");
                          new UserAlternateID( UserID = 1501, AlternateID = "2");
                          new UserAlternateID( UserID = 1502, AlternateID = "3") |]
        let actual = rule3Calculator.GetAlternateID(benefitElectionAlternateIDs, coveredUsers, DateTime.Parse "1/1/2016")

        Assert.Equal(expected, actual)
