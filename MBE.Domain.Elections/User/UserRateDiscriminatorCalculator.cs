using System;

namespace MBE.Domain.Elections.User
{
    public interface IUserRateDiscriminatorCalculator
    {
        int GetRateDiscriminator(int userID, int planTypeID);
    }
    public class UserRateDiscriminatorCalculator  : IUserRateDiscriminatorCalculator
    {
        public int GetRateDiscriminator(int userID, int planTypeID)
        {
            throw new NotImplementedException();
        }
    }
}
