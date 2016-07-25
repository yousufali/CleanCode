using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections.DataAccess
{
    public interface IUserPrePayAmountPaidRepository
    {
        UserPrePayAmountPaid GetUserPrePayAmount(int userID, string planYear);
    }
    public class UserPrePayAmountPaidRepository : IUserPrePayAmountPaidRepository
    {
        public UserPrePayAmountPaid GetUserPrePayAmount(int userID, string planYear)
        {
            throw new System.NotImplementedException();
        }
    }
}