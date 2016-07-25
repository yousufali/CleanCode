using System;
using System.Collections.Generic;
using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections.DataAccess
{
    public interface IUserRepository
    {
        Models.User GetUser(int userID);
        List<UserBenefitClass> GetUserBenefitClasses(int userID);
        Employee GetEmployee(int userID);
    }
    public class UserRepository : IUserRepository
    {
        public Models.User GetUser(int userID)
        {
            throw new NotImplementedException();
        }

        public List<UserBenefitClass> GetUserBenefitClasses(int userID)
        {
            throw new NotImplementedException();
        }

        public Employee GetEmployee(int userID)
        {
            throw new NotImplementedException();
        }
    }
}
