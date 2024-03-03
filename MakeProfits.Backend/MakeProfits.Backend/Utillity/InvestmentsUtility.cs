using MakeProfits.Backend.Models;
using Newtonsoft.Json.Linq;

namespace MakeProfits.Backend.Utillity
{
    public class InvestmentsUtility : AbstractAPIUtility,IInvestmentsUtility
    {
        string msgStr;
        private readonly ILogger<InvestmentsUtility> _logger;
        // base uri : https://financialmodelingprep.com/api/v3/
        public InvestmentsUtility(ILogger<InvestmentsUtility> logger):base() {
            this._logger = logger;
        }


        public override async Task<string> MakeGet(string url)
        {
            _logger.LogInformation("Request to make an {ActionMethod} for resource at {URL} ", "GET",$"{_httpClient.BaseAddress.ToString()}{url}");
            try
            {
                _response = await _httpClient.GetAsync(url);
                msgStr =await _response.Content.ReadAsStringAsync();
                return msgStr;
            }catch (Exception ex)
            {
                _logger.LogError(ex,"unable to make an API Call");

                return "";
            }

        }

        public async Task<AbstractInvestmentInfo> GetInvestmentInfoAsync(string tickSymbol)
        {
            string BalanceSheetAPI = "balance-sheet-statement";
            string IncomeStatement = "income-statement";
            string APIKEY = "k7J5NXEx3Yac2p5UtmG5HkKJn9V92ktP";
            AbstractInvestmentInfo info = new AbstractInvestmentInfo();

            string msg =await  MakeGet($"{BalanceSheetAPI}/{tickSymbol}?apikey={APIKEY}");
            
            JArray BalanceSheetResonse = JArray.Parse( msgStr );

            info.TickerSymbol =Convert.ToString(BalanceSheetResonse[0]["symbol"]);
            info.CIK = Convert.ToString(BalanceSheetResonse[0]["cik"]);

            int i = 0;
            int year;
            BalanceSheetInfo balanceSheetInfo = new BalanceSheetInfo();
            foreach(var item in BalanceSheetResonse)
            {
                if (i == 4)
                {
                    i--;
                    break;
                }
                balanceSheetInfo.TotalAssets = Convert.ToDecimal(item["totalAssets"]);
                balanceSheetInfo.TotalStockholdersEquity = Convert.ToDecimal(item["totalStockholdersEquity"]);
                year = Convert.ToInt32(item["calendarYear"]);
                info.BalanceSheets.Add(year,balanceSheetInfo);
                i++;
                
            }

            msg = await MakeGet($"{IncomeStatement}/{tickSymbol}?apikey={APIKEY}");

            JArray IncomeStatementResponse = JArray.Parse( msgStr );
            IncomeStatementInfo incomeStatementInfo = new IncomeStatementInfo();
            foreach(var item in IncomeStatementResponse) {
                if (i == 0)
                {
                    break;
                }
                incomeStatementInfo.NetIncome = Convert.ToDecimal(item["netIncome"]);
                incomeStatementInfo.Revenue = Convert.ToDecimal(item["revenue"]);
                year = Convert.ToInt32(item["calendarYear"]);
                info.IncomeStatements.Add(year,incomeStatementInfo);
                i--;
            }

            //Caluclating technical indicators
            decimal Profitability, TechnicalEfficiency, FinancialStructure, ROE;
            foreach (KeyValuePair<int,IncomeStatementInfo> entry in info.IncomeStatements)
            {
                Profitability = entry.Value.NetIncome / entry.Value.Revenue;
                TechnicalEfficiency = entry.Value.Revenue / ((info.BalanceSheets[entry.Key].TotalAssets + info.BalanceSheets[entry.Key - 1].TotalAssets) / 2);
                FinancialStructure = ((info.BalanceSheets[entry.Key].TotalAssets + info.BalanceSheets[entry.Key - 1].TotalAssets) / 2)/
                    ((info.BalanceSheets[entry.Key].TotalStockholdersEquity + info.BalanceSheets[entry.Key - 1].TotalStockholdersEquity) / 2);
                ROE = Profitability * TechnicalEfficiency * FinancialStructure;
                info.Profitability.Add(entry.Key,Profitability);
                info.TechnicalEfficiency.Add(entry.Key, TechnicalEfficiency);
                info.FinancialStructure.Add(entry.Key,FinancialStructure);
                info.ROE.Add(entry.Key,ROE);
            }

            return info;



        }
    }
}
