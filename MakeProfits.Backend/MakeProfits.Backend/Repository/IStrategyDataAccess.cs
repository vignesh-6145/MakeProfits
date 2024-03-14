using MakeProfits.Backend.Models;

namespace MakeProfits.Backend.Repository
{
    public interface IStrategyDataAccess
    {
        Strategy addStrat(Strategy strategy);
        string getinvestname(string table, Guid id);
        void deletestrat(Guid strategyId);
        List<Strategy> showStrat(Guid advisorId);
        void subscription(Guid clientId);
        bool CheckFirstTime(Guid clientId);
    }
}
