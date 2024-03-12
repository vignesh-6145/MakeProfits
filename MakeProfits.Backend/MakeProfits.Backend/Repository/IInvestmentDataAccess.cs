using MakeProfits.Backend.Models;
using MakeProfits.Backend.Models.Investments;
using MakeProfits.Backend.Models.Investments.Stocks;

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
        bool OptoutStratergy(InvestmentStratergy investmentStratergy);
        Portfolio GetUserProtfolio(int ClientID, string _="All");
        AdvisorPortfolio GetAdvisorPortfolio(int AdvisorID, string _ = "All");

    }
}
