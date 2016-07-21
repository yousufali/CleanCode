using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBE.Domain.Elections.Models
{
    public class CoveredUser
    {
        public int UserID { get; set; }
        public int RelationID { get; set; }
        public string Pcp { get; private set; }
        public bool PcpSeen { get; private set; }
        public string Pcp2 { get; private set; }
        public bool Pcp2Seen { get; private set; }
    }
}
