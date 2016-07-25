using MBE.Domain.Elections.Models;

namespace MBE.Domain.Elections.DataAccess
{
    public interface ITierPrePayValueRepository
    {
        TierPrePayValue GeTierPrePayValue(int tierID);
    }
    public class TierPrePayValueRepository : ITierPrePayValueRepository
    {
        public TierPrePayValue GeTierPrePayValue(int tierID)
        {
            throw new System.NotImplementedException();
        }
    }
}