using System;
using System.Collections.Generic;
using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections.User
{
    public interface ICoveredDependentSelector
    {
        List<CoveredDependent> SelectCoveredDependents(int parentUserId, List<CoveredUser> coveredUsers);
    }

    public class CoveredDependentSelector : ICoveredDependentSelector
    {
        public List<CoveredDependent> SelectCoveredDependents(int parentUserId, List<CoveredUser> coveredUsers)
        {
            throw new NotImplementedException();
        }
    }
}
