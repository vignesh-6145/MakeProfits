namespace MakeProfits.Backend.Models
{
    public class AbstractInvestmentInfo
    {
        public string OrganizationName { get; set; }
        public string TickerSymbol { get; set; }
        public string CIK {  get; set; }
        public Dictionary<int,IncomeStatementInfo> IncomeStatements { get; set; } = new Dictionary<int, IncomeStatementInfo>();
        public Dictionary<int, BalanceSheetInfo> BalanceSheets { get; set; } = new Dictionary<int, BalanceSheetInfo>();

        // profitability[i] = IncomeStatements[i].NetIncome/IncomeStatements[i].Revenue
        public Dictionary<int, decimal> Profitability { get; set; } = new Dictionary<int, decimal>();
        //TechnicalEfficiency[i] = IncomeStatement[i].Revenue/((BalanceStatemets[i].TotalAssets + BalanceStatemets[i-1].TotalAssets)/2)
        public Dictionary<int, decimal> TechnicalEfficiency { get; set; } = new Dictionary<int, decimal>();
        // FinancialStructure[i] = (BalanceSheets[i].TotalAssets + BalanceSheets[i-1].TotalAssets)/2
        //  + (BalanceSheets[i].TotalStockholdersEquity + BalanceSheets[i-1].TotalStockholdersEquity)/2
        public Dictionary<int, decimal> FinancialStructure { get; set; } = new Dictionary<int, decimal>();

        // ROE[i] = Profitability[i] + TechnicalEfficiency[i] + FinancialStructure[i]
        public Dictionary<int, decimal> ROE { get; set; } = new Dictionary<int, decimal> ();
    }
}
