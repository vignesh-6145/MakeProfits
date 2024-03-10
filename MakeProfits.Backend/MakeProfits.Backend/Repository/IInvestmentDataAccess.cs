using MakeProfits.Backend.Models;
using MakeProfits.Backend.Models.Stock;

namespace MakeProfits.Backend.Repository
{
    public interface IInvestmentDataAccess
    {
        bool CheckSecurity(String TickSymbol);
        bool CheckSecurityBalanceSheetInfo(String TickSymbol, int year);
        bool CheckSecurityInvestmentStatementtInfo(String TickSymbol, int year);
        bool InsertSecurity(Stock security);
        bool InsertSecurityBalanceSheetInfo(StockBalanceSheet securityBalanceSheet);
        bool InsertSecurityIncomeStatementInfo(StockIncomeStatement securityIncomeStatement);
        StockDTO RetrieveSecurityProfile(string _);
        StockIncomeStatement RetieveSecurityIncomeStatement(string tickSymbol, int year);
        StockBalanceSheet RetrieveSecurityBalanceSheet(string tickSymbol, int year);

        bool OptinStratergy(InvestmentStratergy investmentStratergy);

    }
}
