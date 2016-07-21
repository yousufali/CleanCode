using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections.DataAccess
{

    public interface IUserCreditResponseRepository
    {
        List<UserCreditResponse> SelectUserCreditResponses(int userID, DateTime effectiveDate);
    }
    public class UserCreditResponseRepository :IUserCreditResponseRepository
    {
        public List<UserCreditResponse> SelectUserCreditResponses(int userID, DateTime effectiveDate)
        {
            throw new NotImplementedException();
        }
    }
}
