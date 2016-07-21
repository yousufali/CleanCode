using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections.DataAccess
{
    public interface IUserRepository
    {
        List<UserBenefitClass> GetUserBenefitClasses(int userID);
    }
    public class UserRepository : IUserRepository
    {
        public List<UserBenefitClass> GetUserBenefitClasses(int userID)
        {
            throw new NotImplementedException();
        }
    }
}
