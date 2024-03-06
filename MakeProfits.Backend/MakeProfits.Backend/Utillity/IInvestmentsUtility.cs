using MakeProfits.Backend.Models;

namespace MakeProfits.Backend.Utillity
{
    public interface IInvestmentsUtility
    {
        Task<AbstractInvestmentInfo> GetInvestmentInfoAsync(string tickSymbol);
    }
}
