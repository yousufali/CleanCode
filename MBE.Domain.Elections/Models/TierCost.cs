using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBE.Domain.Elections.Models
{
    public interface ITierBasicInfo
    {
        int TierID { get; set; }
        int BaseTierID { get; set; }
        decimal PremiumOverride { get; set; }
//#TODO : refactor to handle it in a better way
        //Non-Agebanded tier will have FlatRate false
        bool FlatRate { get; set; }
        decimal Per { get; set; }
    }
    public class TierCost : ITierBasicInfo
    {
        public int TierID { get; set; }
        public int BaseTierID { get; set; }
        public decimal PremiumOverride { get; set; }
        public bool FlatRate { get; set; }
        public decimal Per { get; set; }
        public decimal AfterTax { get; set; }
    }
    public class TierCostsAgeBand : TierCost
    {
        public int TierCostsAgeBandID { get; set; }
        public decimal AfterTax { get; set; }
    }
}
