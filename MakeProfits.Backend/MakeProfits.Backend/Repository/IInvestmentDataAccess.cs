using MakeProfits.Backend.Models.Securities;

namespace MakeProfits.Backend.Repository
{
    public interface IInvestmentDataAccess
    {
        bool CheckSecurity(String TickSymbol);
        bool CheckSecurityBalanceSheetInfo(String TickSymbol, int year);
        bool CheckSecurityInvestmentStatementtInfo(String TickSymbol, int year);
        bool InsertSecurity(Security security);
        bool InsertSecurityBalanceSheetInfo(SecurityBalanceSheet securityBalanceSheet);
        bool InsertSecurityIncomeStatementInfo(SecurityIncomeStatement securityIncomeStatement);
        SecurityDTO RetrieveSecurityProfile(string _);
        SecurityIncomeStatement RetieveSecurityIncomeStatement(string tickSymbol, int year);
        SecurityBalanceSheet RetrieveSecurityBalanceSheet(string tickSymbol, int year);    

    }
}
