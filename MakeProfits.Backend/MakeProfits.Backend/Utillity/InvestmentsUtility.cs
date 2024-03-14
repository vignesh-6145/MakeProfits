using MakeProfits.Backend.Models;
using MakeProfits.Backend.Models.Investments.Stocks;
using MakeProfits.Backend.Repository;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Data.SqlClient;

namespace MakeProfits.Backend.Utillity
{
    public class InvestmentsUtility : AbstractAPIUtility,IInvestmentsUtility
    {
        string msgStr;
        private readonly ILogger<InvestmentsUtility> _logger;
        private readonly IInvestmentDataAccess _dataAccess;
        // base uri : https://financialmodelingprep.com/api/v3/
        public InvestmentsUtility(ILogger<InvestmentsUtility> logger, IInvestmentDataAccess dataAccess):base() {
            this._logger = logger;
            this._dataAccess = dataAccess;
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

        public void CaluclateTechnicalIndicators(AbstractInvestmentInfo info)
        {
            decimal Profitability, TechnicalEfficiency, FinancialStructure, ROE;
            int Year;
            foreach (KeyValuePair<int, IncomeStatementInfo> entry in info.IncomeStatements)
            {
                Year = entry.Key;
                Profitability = CalculateProfitability(entry.Value.NetIncome,
                    entry.Value.Revenue);

                TechnicalEfficiency = CalculateTechnicalEfficiency(entry.Value.Revenue // This year Revenue
                    , info.BalanceSheets[entry.Key].TotalAssets  //This year Total Assets
                    , info.BalanceSheets[entry.Key - 1].TotalAssets); // Previous year Total Assets

                FinancialStructure = CalculateFinancialStructure(info.BalanceSheets[entry.Key].TotalAssets
                    , info.BalanceSheets[entry.Key - 1].TotalAssets
                    , info.BalanceSheets[entry.Key].TotalStockholdersEquity
                    , info.BalanceSheets[entry.Key - 1].TotalStockholdersEquity);

                ROE = CalculateReturnOnEquity(Profitability, TechnicalEfficiency, FinancialStructure);

                info.Profitability.Add(Year, Profitability);
                info.TechnicalEfficiency.Add(Year, TechnicalEfficiency);
                info.FinancialStructure.Add(Year, FinancialStructure);
                info.ROE.Add(Year, ROE);
            }
        }

        
        public async Task PersistCompanyProfile(string tickSymbol, string APIKey)
        {
            string CompanyProfile = "profile";
            try
            {
                string msg =await MakeGet($"{CompanyProfile}/{tickSymbol}?apikey={APIKey}");
                JToken CompanyProfileResponse = JArray.Parse(msg)[0];

                Stock security = new Stock();
                security.TickerSymbol = Convert.ToString(CompanyProfileResponse["symbol"]);
                security.Cik = Convert.ToString(CompanyProfileResponse["cik"]);
                security.OrganizationName = Convert.ToString(CompanyProfileResponse["companyName"]);
                security.Price = Convert.ToDecimal(CompanyProfileResponse["price"]);

                _logger.LogInformation("Information of {TickSymbol} was retrieved from API",tickSymbol);

                _dataAccess.InsertSecurity(security);
                _logger.LogInformation("Data was stored into DB");

            }catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while retrieving data for Income statements");
            }
        }
        public async Task PersistIncomeStatements(string tickSymbol, string APIKey){
            string IncomeStatement = "income-statement";
            try
            {
                string msg = await MakeGet($"{IncomeStatement}/{tickSymbol}?apikey={APIKey}");

                JArray IncomeStatementResponse = JArray.Parse(msg);
                int year;
                try
                {

                    StockIncomeStatement incomeStatementInfo = new StockIncomeStatement();
                    foreach (var item in IncomeStatementResponse)
                    {

                        incomeStatementInfo.NetIncome = Convert.ToDecimal(item["netIncome"]);
                        incomeStatementInfo.Revenue = Convert.ToDecimal(item["revenue"]);
                        incomeStatementInfo.year = Convert.ToInt32(item["calendarYear"]);
                        incomeStatementInfo.TickerSymbol = tickSymbol;

                        _logger.LogInformation($"Income-Statement information retreieved for the {incomeStatementInfo.year} NetIncome : {incomeStatementInfo.NetIncome} Revenue : {incomeStatementInfo.Revenue}");

                        if (!_dataAccess.CheckSecurityInvestmentStatementtInfo(tickSymbol, incomeStatementInfo.year))
                        {
                            _dataAccess.InsertSecurityIncomeStatementInfo(incomeStatementInfo);
                            _logger.LogInformation("Data persisted");
                        }
                        else
                        {
                            _logger.LogInformation("Information already exists");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to read income-statement response");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while retrieving data for Income statements");
            }
        }
        public async Task PersistBalanceSheets(string tickSymbol, string APIKey)
        {
            string BalanceSheetAPI = "balance-sheet-statement";
            _logger.LogInformation($"Retrieving Balancesheet information over years");
            //TODO : limit the data for certain years can be done with limit query param, configure from appsettings
            try
            {
                string msg = await MakeGet($"{BalanceSheetAPI}/{tickSymbol}?apikey={APIKey}");

                JArray BalanceSheetResonse = JArray.Parse(msg);
                StockBalanceSheet balanceSheetInfo = new StockBalanceSheet();
                try
                {
                    foreach (var item in BalanceSheetResonse)
                    { 
                        balanceSheetInfo.TotalAssets = Convert.ToDecimal(item["totalAssets"]);
                        balanceSheetInfo.TotalStockholdersEquity = Convert.ToDecimal(item["totalStockholdersEquity"]);
                        balanceSheetInfo.year= Convert.ToInt32(item["calendarYear"]);
                        balanceSheetInfo.TickerSymbol = tickSymbol;
                        _logger.LogInformation($"Balance-sheet information retreieved for the {balanceSheetInfo.year} TotalAssets : {balanceSheetInfo.TotalAssets} TotalStockholdersEquity {balanceSheetInfo.TotalStockholdersEquity}");

                        if (!_dataAccess.CheckSecurityInvestmentStatementtInfo(tickSymbol, balanceSheetInfo.year))
                        {
                            _dataAccess.InsertSecurityBalanceSheetInfo(balanceSheetInfo);
                            _logger.LogInformation("Data persisted");
                        }
                        else
                        {
                            _logger.LogInformation("Information already exists");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to read balance-sheet response");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while retrieving data for balance sheets");
            }
        }
        public async Task RetrieveIncomeStatements(AbstractInvestmentInfo info, string tickSymbol,string APIKey)
        {

            string IncomeStatement = "income-statement";
            try
            {
                string msg = await MakeGet($"{IncomeStatement}/{tickSymbol}?apikey={APIKey}");

                JArray IncomeStatementResponse = JArray.Parse(msg);
                int year, i = 3;

                _logger.LogInformation($"Retrieving Income-Statement information over years");
                try
                {
                    foreach (var item in IncomeStatementResponse)
                    {

                        if (i == 0)
                        {
                            break;
                        }

                        IncomeStatementInfo incomeStatementInfo = new IncomeStatementInfo();
                        incomeStatementInfo.NetIncome = Convert.ToDecimal(item["netIncome"]);
                        incomeStatementInfo.Revenue = Convert.ToDecimal(item["revenue"]);
                        year = Convert.ToInt32(item["calendarYear"]);
                        info.IncomeStatements.Add(year, incomeStatementInfo);
                        i--;
                        _logger.LogInformation($"Income-Statement information retreieved for the {year} NetIncome : {incomeStatementInfo.NetIncome} Revenue : {incomeStatementInfo.Revenue}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to read income-statement response");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while retrieving data for Income statements");
            }
        }
        public async Task RetrieveBalanceSheets(AbstractInvestmentInfo info, string tickSymbol,string APIKey)
        {

            string BalanceSheetAPI = "balance-sheet-statement";
            _logger.LogInformation($"Retrieving Balancesheet information over years");
            //TODO : limit the data for certain years can be done with limit query param, configure from appsettings
            try
            {
                string msg = await MakeGet($"{BalanceSheetAPI}/{tickSymbol}?apikey={APIKey}");

                JArray BalanceSheetResonse = JArray.Parse(msg);

             
                int i = 0;
                int year;
                BalanceSheetInfo balanceSheetInfo = new BalanceSheetInfo();
                try
                {
                    foreach (var item in BalanceSheetResonse)
                    {
                        if (i == 4)
                        {
                            i--;
                            break;
                        }
                        balanceSheetInfo.TotalAssets = Convert.ToDecimal(item["totalAssets"]);
                        balanceSheetInfo.TotalStockholdersEquity = Convert.ToDecimal(item["totalStockholdersEquity"]);
                        year = Convert.ToInt32(item["calendarYear"]);
                        info.BalanceSheets.Add(year, balanceSheetInfo);
                        i++;
                        _logger.LogInformation($"Balance-sheet information retreieved for the {year} TotalAssets : {balanceSheetInfo.TotalAssets} TotalStockholdersEquity {balanceSheetInfo.TotalStockholdersEquity}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to read balance-sheet response");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while retrieving data for balance sheets");
            }

        }

        public decimal CalculateTechnicalEfficiency(decimal CrrYearRevenue,decimal CrrYearTotalAssets, decimal PrevYearTotalAssets)
        {
            return CrrYearRevenue / ((CrrYearTotalAssets + PrevYearTotalAssets) / 2);
        }

        public decimal CalculateFinancialStructure(decimal CrrYearTotalAssets, decimal PrevYearTotalAssets, decimal CrrYearTotalStockholdersEquity, decimal PrevYearTotalStockholdersEquity)
        {
            if((CrrYearTotalStockholdersEquity + PrevYearTotalStockholdersEquity) == 0)
            {
                return 0;
            }
            return ((CrrYearTotalAssets + PrevYearTotalAssets) / 2) /
                ((CrrYearTotalStockholdersEquity+PrevYearTotalStockholdersEquity)/2);
        }
        public decimal CalculateProfitability(decimal NetIncome, decimal Revenue)
        {
            if (Revenue == 0)
            {
                return 0;
            }
            return NetIncome / Revenue;
        }
        public decimal CalculateReturnOnEquity(decimal Profitability, decimal TechnicalEfficiency, decimal FinancialStructure)
        {
            return Profitability * TechnicalEfficiency * FinancialStructure;
        }
        public async Task<AbstractInvestmentInfo> GetInvestmentInfoAsync(string tickSymbol)
        {

            string APIKEY = "k7J5NXEx3Yac2p5UtmG5HkKJn9V92ktP";
            AbstractInvestmentInfo info = new AbstractInvestmentInfo();

            //TODO : Now the data was always been updated by continuoous API CALLs, need to persist the data and retrieve on demand
            // 1. Check if the particular stock informtion was present or not
            if (!_dataAccess.CheckSecurity(tickSymbol))
            {
                //if there was no previous stock presence
                _logger.LogInformation("This stock has no presence, Retrieving Stock Information for {tickSymbol}", tickSymbol);
                await PersistCompanyProfile( tickSymbol, APIKEY);
                await PersistBalanceSheets(tickSymbol, APIKEY);
                await PersistIncomeStatements(tickSymbol, APIKEY);
            }
            
                StockDTO security = _dataAccess.RetrieveSecurityProfile(tickSymbol);
                if(security != null)
                {
                    info.security = security;
                }
                else
                {
                    _logger.LogInformation("No Record Found");
                }
                int years = 3;
                StockIncomeStatement incomeStatement;
                StockBalanceSheet balanceSheet;
                for (int i = 1; i <= years; i++)
                {
                    int crrYear = DateTime.UtcNow.Year - i;
                    if (i == 1)
                    {
                        balanceSheet = _dataAccess.RetrieveSecurityBalanceSheet(tickSymbol, DateTime.UtcNow.Year - years - 1);
                        info.BalanceSheets.Add(DateTime.UtcNow.Year - years - 1, new BalanceSheetInfo() { TotalAssets = balanceSheet.TotalAssets, TotalStockholdersEquity = balanceSheet.TotalStockholdersEquity });
                    }
                    incomeStatement = _dataAccess.RetieveSecurityIncomeStatement(tickSymbol,crrYear);
                    balanceSheet = _dataAccess.RetrieveSecurityBalanceSheet(tickSymbol, crrYear);
                    info.IncomeStatements.Add(crrYear,new IncomeStatementInfo() { NetIncome = incomeStatement.NetIncome,Revenue = incomeStatement.Revenue});
                    info.BalanceSheets.Add(crrYear, new BalanceSheetInfo() { TotalAssets = balanceSheet.TotalAssets, TotalStockholdersEquity = balanceSheet.TotalStockholdersEquity });
                    
                    //info.Profitability.Add(crrYear, CalculateProfitability(incomeStatement.NetIncome, incomeStatement.Revenue));
                    //info.TechnicalEfficiency.Add(crrYear, CalculateTechnicalEfficiency(incomeStatement.Revenue, balanceSheet.TotalAssets, info.BalanceSheets[crrYear - 1].TotalAssets));
                    //info.FinancialStructure.Add(crrYear, CalculateFinancialStructure(balanceSheet.TotalAssets, info.BalanceSheets[crrYear - 1].TotalAssets,balanceSheet.TotalStockholdersEquity, info.BalanceSheets[crrYear - 1].TotalStockholdersEquity));
                    //info.ROE.Add(crrYear,CalculateReturnOnEquity(info.Profitability[crrYear], info.TechnicalEfficiency[crrYear], info.TechnicalEfficiency[crrYear]));

                }
            
            // 2. If Present retreive the information
            // 3. Else Make an API call for the Stock and store them into the DB
            _logger.LogInformation("Retrieving Stock Information for {tickSymbol}", tickSymbol);

            //TODO : retrieve data from appsettings3

            //await RetrieveBalanceSheets(info,tickSymbol,APIKEY);

            //await RetrieveIncomeStatements(info,tickSymbol, APIKEY);


            CaluclateTechnicalIndicators(info);

            return info;


        }
            
        }
}

