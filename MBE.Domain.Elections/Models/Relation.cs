using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBE.Domain.Elections.Models
{
    public enum Relation
    {
        Employee = 1,
        Spouse = 2,
        DomesticPartner = 4,
        Child = 8,
        ChildofDomesticPartner = 16,
        SurvivingSpouse = 32,
        Father = 64,
        Mother = 128,
        CourtOrderedDependent = 256,
        Beneficiary = 512,
        InactiveSpouse = 1024,
        InactiveChild = 2048,
        InactiveBeneficiary = 4096,
        CivilUnionSpouse = 8192,
        InactiveDomesticPartner = 16384,
        InactiveCivilUnionSpouse = 32768,
        CommonLawSpouse = 65536,
        ExSpouse = 131072,
        InactiveExSpouse = 262144
    }
}
