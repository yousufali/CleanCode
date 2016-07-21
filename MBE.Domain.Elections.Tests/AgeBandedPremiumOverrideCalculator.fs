namespace MBE.Domain.Elections.Tests
open MBE.Domain.Elections
open MBE.Domain.Elections.Models
open System
open Xunit
open FsUnit.Xunit
open MBE.Domain.Elections.DataAccess
open MBE.Domain.Elections.PremiumOverride

module AgeBandedPremiumOverrideCalculator =

    [<Fact>]
    let ``AgeBanded FlatRate Tier Test``() =
        let tier = new TierCostsAgeBand(TierID = 1, BaseTierID = 1, PremiumOverride = decimal 100.00, FlatRate = true )
        let tierCostsAgeBandRepository = { new ITierCostsAgeBandRepository with member this.SelectTierCostsAgeBand(x) = tier }
        let electionData = new ElectionData (ParentUserID = 1500, PlanID = 1, TierID = 1)

        let actual = (new AgeBandedPremiumOverrideCalculator(tierCostsAgeBandRepository)).GetPremiumOverride(electionData)

        Assert.Equal(decimal 100.00, actual)

    [<Fact>]
    let ``AgeBanded Non-FlatRate Tier With Per=0 Test``() =
        let tier = new TierCostsAgeBand(TierID = 1, BaseTierID = 1, PremiumOverride = decimal 100.00, FlatRate = false, Per = decimal 0 )
        let tierCostsAgeBandRepository = { new ITierCostsAgeBandRepository with member this.SelectTierCostsAgeBand(x) = tier }
        let electionData = new ElectionData (ParentUserID = 1500, PlanID = 1, TierID = 1)

        let actual = (new AgeBandedPremiumOverrideCalculator(tierCostsAgeBandRepository)).GetPremiumOverride(electionData)

        Assert.Equal(decimal 0.00, actual)

    [<Fact>]
    let ``PremiumOverrideCalculator AgeBanded Non-FlatRate Tier With Per > 0 Test``() =
        let tier = new TierCostsAgeBand(TierID = 1, BaseTierID = 1, PremiumOverride = decimal 0.11, FlatRate = false, Per = decimal 100.00 )
        let tierCostsAgeBandRepository = { new ITierCostsAgeBandRepository with member this.SelectTierCostsAgeBand(x) = tier }
        let electionData = new ElectionData (ParentUserID = 1500, PlanID = 1, TierID = 1, Coverage = decimal 8333.00)

        let actual = (new AgeBandedPremiumOverrideCalculator(tierCostsAgeBandRepository)).GetPremiumOverride(electionData)

        Assert.Equal(decimal 9.1663, actual)
