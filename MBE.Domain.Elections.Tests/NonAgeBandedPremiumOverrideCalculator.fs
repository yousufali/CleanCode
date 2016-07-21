namespace MBE.Domain.Elections.Tests
open MBE.Domain.Elections
open MBE.Domain.Elections.Models
open System
open Xunit
open FsUnit.Xunit
open MBE.Domain.Elections.DataAccess
open MBE.Domain.Elections.PremiumOverride

module NonAgeBandedPremiumOverrideCalculator =

    [<Fact>]
    let ``Premium Override from TierCost is returned Test``() =
        let tier = new TierCost(TierID = 1, BaseTierID = 1, PremiumOverride = decimal 100.00, FlatRate = true )
        let tierCostRepository = { new ITierCostRepository with member this.SelectTierCost(x) = tier }
        let electionData = new ElectionData (ParentUserID = 1500, PlanID = 1, TierID = 1)

        let actual = (new NonAgeBandedPremiumOverrideCalculator(tierCostRepository)).GetPremiumOverride(electionData)

        Assert.Equal(decimal 100.00, actual)