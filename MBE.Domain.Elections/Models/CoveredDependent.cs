using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBE.Domain.Elections.Models
{
    public class CoveredDependent
    {
        public int UserID { get; set; }
        public int RelationID { get; set; }
        public bool TaxQualified { get; set; }
        public bool SpouseEquivalent { get; set; }
    }
}
