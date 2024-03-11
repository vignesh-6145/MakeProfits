
using MakeProfits.Backend.Models;
using MakeProfits.Backend.Models.Investments.Bonds;
using MakeProfits.Backend.Models.Investments.MutualFunds;
using MakeProfits.Backend.Models.Investments.Stocks;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace MakeProfits.Backend.Repository
{
    public class InvestmentDataAccess : IInvestmentDataAccess
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<InvestmentDataAccess> _logger;
        public InvestmentDataAccess(IConfiguration configuration, ILogger<InvestmentDataAccess> logger) {
            this._configuration = configuration;
            this._logger = logger;
        }
        public bool CheckSecurity(string TickSymbol)
        {
            _logger.LogInformation("Checking if the Stock was previously read or not");
            try
            {
                string connectionString = Convert.ToString(_configuration.GetConnectionString("DBConnection"));
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                _logger.LogInformation("Connection Established");
                try
                {
                    SqlCommand command = new SqlCommand("check_for_stock",connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ticker_symbol",TickSymbol);
                    _logger.LogInformation("Created Command and parametrized Command");

                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        if(reader.Read())
                        {
                            int status = reader.GetInt32(0);
                            DateTime lastUpdatedOn =  reader.GetDateTime(1);
                            if ( status == 1 )
                            {
                                _logger.LogInformation("{ticker_symbol} was already stored in DB", TickSymbol);

                                TimeSpan updateDiff = DateTime.UtcNow.Subtract(lastUpdatedOn);
                                if(updateDiff.Days > 1)
                                {
                                    _logger.LogInformation("The price of  {ticker_symbol} haven't updated, have to make a API Call", TickSymbol);
                                    reader.Close();
                                    connection.Close();
                                    return false;
                                }
                                reader.Close();
                                connection.Close();
                                return true;
                            }
                            else
                            {
                                _logger.LogInformation("{ticker_symbol} was not stored in DB", TickSymbol);
                                reader.Close();
                                connection.Close();
                                return false;
                            }
                        }
                        else
                        {
                            _logger.LogInformation("{ticker_symbol} was not stored in DB", TickSymbol);
                            reader.Close();
                            connection.Close();
                            return false;
                        }

                    }
                    catch (Exception ex) {
                        _logger.LogInformation(ex,"Something went wrong while executing command");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    connection.Close();
                    _logger.LogError(ex,"");
                    return false;
                }
            }
            catch(SqlException ex) {
                _logger.LogError(ex,"Something went wrong while establishing connection");
                return false;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,"Something went wrong please check");
                return false;
            }

        }

        public bool CheckSecurityBalanceSheetInfo(string TickSymbol, int year)
        {
            _logger.LogInformation("Checking if the Balance sheet of {ticker_symbol} is available for the {year}",TickSymbol,year);
            try
            {
                string connectionString = Convert.ToString(_configuration.GetConnectionString("DBConnection"));
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                _logger.LogInformation("Connection Established");
                try
                {
                    SqlCommand command = new SqlCommand("check_for_stock_balance_sheet", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ticker_symbol", TickSymbol);
                    command.Parameters.AddWithValue("@year",year);
                    _logger.LogInformation("Created Command and parametrized Command");

                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            if (reader.GetInt32(0) == 1)
                            {
                                _logger.LogInformation("{ticker_symbol} was already stored in DB", TickSymbol);
                                reader.Close();
                                connection.Close();
                                return true;
                            }
                            else
                            {
                                _logger.LogInformation("{ticker_symbol} was not stored in DB", TickSymbol);
                                reader.Close();
                                connection.Close();
                                return false;
                            }
                        }
                        else
                        {
                            _logger.LogInformation("{ticker_symbol} was not stored in DB", TickSymbol);
                            reader.Close();
                            connection.Close();
                            return false;
                        }

                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation(ex, "Something went wrong while executing command");
                        connection.Close();
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "");
                    connection.Close();
                    return false;
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Something went wrong while establishing connection");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong please check");
                return false;
            }
        }

        public bool CheckSecurityInvestmentStatementtInfo(string TickSymbol, int year)
        {
            _logger.LogInformation("Checking if the Income sheet of {ticker_symbol} is available for the {year}", TickSymbol, year);
            try
            {
                string connectionString = Convert.ToString(_configuration.GetConnectionString("DBConnection"));
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                _logger.LogInformation("Connection Established");
                try
                {
                    SqlCommand command = new SqlCommand("check_for_stock_income_statement", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ticker_symbol", TickSymbol);
                    command.Parameters.AddWithValue("@year", year);
                    _logger.LogInformation("Created Command and parametrized Command");

                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read() && reader.HasRows)
                        {
                            if (reader.GetInt32(0) == 1)
                            {
                                _logger.LogInformation("{ticker_symbol} was already stored in DB", TickSymbol);
                                reader.Close();
                                connection.Close();
                                return true;
                            }
                            else
                            {
                                _logger.LogInformation("{ticker_symbol} was not stored in DB", TickSymbol);
                                reader.Close();
                                connection.Close();
                                return false;
                            }
                        }
                        else
                        {
                            _logger.LogInformation("{ticker_symbol} was not stored in DB", TickSymbol);
                            reader.Close();
                            connection.Close();
                            return false;
                        }

                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Something went wrong while executing command");
                        connection.Close();
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "");
                    connection.Close();
                    return false;
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Something went wrong while establishing connection");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong please check");
                return false;
            }
        }
        public StockDTO RetrieveSecurityProfile(string tickSymbol)
        {
            _logger.LogInformation("Request to retieve {ticker_symbol} Profile", tickSymbol);
            try
            {
                string conn = _configuration.GetConnectionString("DBConnection");
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("retrieve_stock_info",connection);
                    command.CommandType = CommandType.StoredProcedure;

                    _logger.LogInformation("Created and Parametrized the Comamnd");
                    command.Parameters.AddWithValue("@ticker_symbol",tickSymbol);
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        _logger.LogInformation("Command Executing retrieving results");
                        if(reader.Read() && reader.HasRows) {
                            StockDTO security = new StockDTO();
                            security.TickerSymbol = reader.GetString(0);
                            security.Cik = reader.GetString(1);
                            security.OrganizationName = reader.GetString(2);
                            security.Price = reader.GetDecimal(3);
                            //security.UpdatedOn = reader.GetDateTime(4);
                            _logger.LogInformation("Results Retrieved");
                            reader.Close();
                            connection.Close();
                            return security;
                        }
                        else
                        {
                            _logger.LogInformation("No SecurityProdile Record found");
                            reader.Close();
                            connection.Close();
                            return null;
                        }
                    }
                    catch (SqlException ex)
                    {
                        _logger.LogError(ex, "Failed to Execute SP_{storedProcedure}, Exception  raised due to DBContext", "retrieve_stock_info");
                        connection.Close();
                        return null;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to Execure SP_{storedProcedure}, Exception raised in genetal context", "retrieve_stock_info");
                        connection.Close();
                        return null;
                    }

                }
                catch (SqlException ex)
                {
                    _logger.LogError(ex, "Failed to Establish a command, Exception  raised due to DBContext");
                    connection.Close();
                    return null;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to Establish a command, Exception raised in genetal context");
                    connection.Close();
                    return null;
                }
                return null;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Failed to Establish a connection, Exception  raised due to DBContext");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to Establish a connection, Exception raised in genetal context");
                return null;
            }
        }
        public bool InsertSecurity(Stock security)
        {
            _logger.LogInformation("Inserting the security of {ticker_symbol}",security.TickerSymbol);
            try
            {
                string connectionString = Convert.ToString(_configuration.GetConnectionString("DBConnection"));
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                _logger.LogInformation("Connection Established");
                try
                {
                    SqlCommand command = new SqlCommand("insert_stock", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ticker_symbol", security.TickerSymbol);
                    command.Parameters.AddWithValue("@cik", security.Cik);
                    command.Parameters.AddWithValue("@stock_name", security.OrganizationName);
                    command.Parameters.AddWithValue("@updated_on",security.UpdatedOn);
                    command.Parameters.AddWithValue("@price", security.Price);
                    _logger.LogInformation("Created Command and parametrized Command");
                    
                    command.ExecuteNonQuery();
                    _logger.LogInformation("Successfully Inserted record into the DB");
                    connection.Close();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to Create & Execute Command : {SP_procedureName}", "insert_stock");
                    connection.Close();
                    return false;
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Something went wrong while establishing connection");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong please check");
                return false;
            }
            return false;
        }

        public bool InsertSecurityBalanceSheetInfo(StockBalanceSheet securityBalanceSheet)
        {
            _logger.LogInformation("Inserting the security-balance-sheet info of {ticker_symbol}", securityBalanceSheet.TickerSymbol);
            try
            {
                string connectionString = Convert.ToString(_configuration.GetConnectionString("DBConnection"));
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                _logger.LogInformation("Connection Established");
                try
                {
                    SqlCommand command = new SqlCommand("insert_stock_balance_sheet", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ticker_symbol", securityBalanceSheet.TickerSymbol);
                    command.Parameters.AddWithValue("@year", securityBalanceSheet.year);
                    command.Parameters.AddWithValue("@total_assets", securityBalanceSheet.TotalAssets);
                    command.Parameters.AddWithValue("@total_stock_holders_equity", securityBalanceSheet.TotalStockholdersEquity);
                    _logger.LogInformation("Created Command and parametrized Command");

                    command.ExecuteNonQuery();
                    _logger.LogInformation("Successfully Inserted record into the DB");
                    connection.Close();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to Create & Execute Command : {SP_procedureName}", "insert_stock_balance_sheet");
                    connection.Close();
                    return false;
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Something went wrong while establishing connection");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong please check");
                return false;
            }
            return false;
        }
        public bool InsertSecurityIncomeStatementInfo(StockIncomeStatement securityIncomeStatement)
        {
            _logger.LogInformation("Inserting the security-inestments-statement info of {TickSymbol}", securityIncomeStatement.TickerSymbol);
            try
            {
                string connectionString = Convert.ToString(_configuration.GetConnectionString("DBConnection"));
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                _logger.LogInformation("Connection Established");
                try
                {
                    SqlCommand command = new SqlCommand("insert_stock_income_statement", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ticker_symbol", securityIncomeStatement.TickerSymbol);
                    command.Parameters.AddWithValue("@year", securityIncomeStatement.year);
                    command.Parameters.AddWithValue("@net_income", securityIncomeStatement.NetIncome);
                    command.Parameters.AddWithValue("@revenue", securityIncomeStatement.Revenue);
                    _logger.LogInformation("Created Command and parametrized Command");

                    command.ExecuteNonQuery();
                    _logger.LogInformation("Successfully Inserted record into the DB");
                    connection.Close();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to Create & Execute Command : {SP_procedureName}", "insert_stock_income_statement");
                    connection.Close();
                    return false;
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Something went wrong while establishing connection");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong please check");
                return false;
            }
            return false;
        }

        public StockIncomeStatement RetieveSecurityIncomeStatement(string tickSymbol, int year)
        {
            _logger.LogInformation("Request to retieve {ticker_symbol} Income-Statement for the {year}",tickSymbol,year);
            try
            {
                string conn = _configuration.GetConnectionString("DBConnection");
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("retrieve_stock_income_statement", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    _logger.LogInformation("Created and Parametrized the Comamnd");
                    command.Parameters.AddWithValue("@ticker_symbol", tickSymbol);
                    command.Parameters.AddWithValue("@year", year);
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        _logger.LogInformation("Command Executing retrieving results");
                        if (reader.Read() && reader.HasRows)
                        {   
                            StockIncomeStatement incomeStatement = new StockIncomeStatement();
                            incomeStatement.TickerSymbol = reader.GetString(0);
                            incomeStatement.year = reader.GetInt32(1);
                            incomeStatement.NetIncome = reader.GetDecimal(2);
                            incomeStatement.Revenue = reader.GetDecimal(3);
                            //security.UpdatedOn = reader.GetDateTime(4);
                            _logger.LogInformation("Retrieved Income-Statement");
                            reader.Close();
                            connection.Close();
                            return incomeStatement;
                        }
                        else
                        {
                            _logger.LogInformation("No income-statement Record found");
                            reader.Close();
                            connection.Close();
                            return null;
                        }
                    }
                    catch (SqlException ex)
                    {
                        _logger.LogError(ex, "Failed to Execute SP_{storedProcedure}, Exception  raised due to DBContext", "insert_stock_income_statement");
                        connection.Close();
                        return null;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to Execure SP_{storedProcedure}, Exception raised in genetal context", "insert_stock_income_statement");
                        connection.Close();
                        return null;
                    }

                }
                catch (SqlException ex)
                {
                    _logger.LogError(ex, "Failed to Establish a command, Exception  raised due to DBContext");
                    connection.Close();
                    return null;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to Establish a command, Exception raised in genetal context");
                    connection.Close();
                    return null;
                }
                return null;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Failed to Establish a connection, Exception  raised due to DBContext");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to Establish a connection, Exception raised in genetal context");
                return null;
            }
        }
        public StockBalanceSheet RetrieveSecurityBalanceSheet(string tickSymbol, int year)
        {
            _logger.LogInformation("Request to retieve {ticker_symbol} Balance-Sheet for the {year}", tickSymbol, year);
            try
            {
                string conn = _configuration.GetConnectionString("DBConnection");
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("retrieve_stock_balance_sheet", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    _logger.LogInformation("Created and Parametrized the Comamnd");
                    command.Parameters.AddWithValue("@ticker_symbol", tickSymbol);
                    command.Parameters.AddWithValue("@year", year);
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        _logger.LogInformation("Command Executing retrieving results");
                        if (reader.Read() && reader.HasRows)
                        {
                            StockBalanceSheet balanceSheet = new StockBalanceSheet();
                            balanceSheet.TickerSymbol = reader.GetString(0);
                            balanceSheet.year = reader.GetInt32(1);
                            balanceSheet.TotalAssets = reader.GetDecimal(2);
                            balanceSheet.TotalStockholdersEquity = reader.GetDecimal(3);
                            //security.UpdatedOn = reader.GetDateTime(4);
                            _logger.LogInformation("Retrieved Balance-Sheet");
                            reader.Close();
                            connection.Close();
                            return balanceSheet;
                        }
                        else
                        {
                            _logger.LogInformation("No income-statement Record found");
                            reader.Close();
                            connection.Close();
                            return null;
                        }
                    }
                    catch (SqlException ex)
                    {
                        _logger.LogError(ex, "Failed to Execute SP_{storedProcedure}, Exception  raised due to DBContext", "retrieve_stock_balance_sheet");
                        connection.Close();
                        return null;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to Execure SP_{storedProcedure}, Exception raised in genetal context", "retrieve_stock_balance_sheet");
                        connection.Close();
                        return null;
                    }

                }
                catch (SqlException ex)
                {
                    _logger.LogError(ex, "Failed to Establish a command, Exception  raised due to DBContext");
                    connection.Close();
                    return null;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to Establish a command, Exception raised in genetal context");
                    connection.Close();
                    return null;
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Failed to Establish a connection, Exception  raised due to DBContext");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to Establish a connection, Exception raised in genetal context");
                return null;
            }
        }

        public bool OptinStratergy(InvestmentStratergy investmentStratergy)
        {
            _logger.LogInformation("Request to opt-in {StratergyID} for client {clientID}",investmentStratergy.StratergyID,investmentStratergy.ClientID);
            try
            {
                string conn = _configuration.GetConnectionString("DBConnection");
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("optin_stratergy", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    _logger.LogInformation("Created and Parametrized the Comamnd");
                    command.Parameters.AddWithValue("@clientID",investmentStratergy.ClientID);
                    command.Parameters.AddWithValue("@advisorID",investmentStratergy.AdvisorID);
                    command.Parameters.AddWithValue("@stratergyID", investmentStratergy.StratergyID);
                    command.Parameters.AddWithValue("@amount",investmentStratergy.amount);
                    command.Parameters.AddWithValue("@newStatus", investmentStratergy.status);
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        _logger.LogInformation("Command Executing retrieving results");
                        if (reader.Read() && reader.HasRows)
                        {
                            if (reader.GetInt32(0) == 1)
                            {
                                reader.Close();
                                connection.Close();
                                return true;
                            }
                            else
                            {
                                reader.Close();
                                connection.Close();
                                return false;
                            }

                        }
                        else
                        {
                            _logger.LogInformation("Failed to read reaults");

                            reader.Close();
                            connection.Close();
                            return false;
                        }
                    }
                    catch (SqlException ex)
                    {
                        _logger.LogError(ex, "Failed to Execute SP_{storedProcedure}, Exception  raised due to DBContext", "optin_stratergy");
                        connection.Close();
                        return false;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to Execure SP_{storedProcedure}, Exception raised in genetal context", "optin_stratergy");
                        connection.Close();
                        return false;
                    }

                }
                catch (SqlException ex)
                {
                    _logger.LogError(ex, "Failed to Establish a command, Exception  raised due to DBContext");
                    connection.Close();
                    return false;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to Establish a command, Exception raised in genetal context");
                    connection.Close();
                    return false;
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Failed to Establish a connection, Exception  raised due to DBContext");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to Establish a connection, Exception raised in genetal context");
                return false;
            }
        }
        public bool OptoutStratergy(InvestmentStratergy investmentStratergy)
        {
            _logger.LogInformation("Request to opt-out {StratergyID} for client {clientID}", investmentStratergy.StratergyID, investmentStratergy.ClientID);
            try
            {
                string conn = _configuration.GetConnectionString("DBConnection");
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("optout_stratergy", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    _logger.LogInformation("Created and Parametrized the Comamnd");
                    command.Parameters.AddWithValue("@clientID", investmentStratergy.ClientID);
                    command.Parameters.AddWithValue("@advisorID", investmentStratergy.AdvisorID);
                    command.Parameters.AddWithValue("@stratergyID", investmentStratergy.StratergyID);
                    command.Parameters.AddWithValue("@newStatus", investmentStratergy.status=false);
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        _logger.LogInformation("Command Executing retrieving results");
                        if (reader.Read() && reader.HasRows)
                        {
                            if (reader.GetInt32(0) == 1)
                            {
                                reader.Close();
                                connection.Close();
                                return true;
                            }
                            else
                            {
                                reader.Close();
                                connection.Close();
                                return false;
                            }
                        }
                        else
                        {
                            _logger.LogInformation("Failed to read reaults");
                            reader.Close();
                            connection.Close();
                            return false;
                        }
                    }
                    catch (SqlException ex)
                    {
                        _logger.LogError(ex, "Failed to Execute SP_{storedProcedure}, Exception  raised due to DBContext", "optout_stratergy");
                        connection.Close();
                        return false;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to Execure SP_{storedProcedure}, Exception raised in genetal context", "optout_stratergy");
                        connection.Close();
                        return false;
                    }

                }
                catch (SqlException ex)
                {
                    _logger.LogError(ex, "Failed to Establish a command, Exception  raised due to DBContext");
                    connection.Close();
                    return false;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to Establish a command, Exception raised in genetal context");
                    connection.Close();
                    return false;
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Failed to Establish a connection, Exception  raised due to DBContext");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to Establish a connection, Exception raised in genetal context");
                return false;
            }
        }

        public Portfolio GetUserProtfolio(int UserID,string InvestmentType="All")
        {
            _logger.LogInformation("Retrieving Portfolio of the user");
            Portfolio userPortfolio = new Portfolio();
            if (InvestmentType == "Stock" || InvestmentType=="All")
            {
                _logger.LogInformation("Retrieving Stock Portfolio of the user");
                userPortfolio.Stocks = RetrieveStockInvestments(UserID);
                userPortfolio.StocksInvestmentValue = userPortfolio.Stocks.Sum(stockInvestment => stockInvestment.InvestmentValue);
                userPortfolio.TotalInvestmentValue += userPortfolio.StocksInvestmentValue;

            }
            if (InvestmentType == "Bond" || InvestmentType == "All")
            {
                _logger.LogInformation("Retrieving Bond Portfolio of the user");
                userPortfolio.Bonds = RetrieveBondInvestments(UserID);
                userPortfolio.BondInvestmentValue = userPortfolio.Bonds.Sum(bond => bond.InvestmentValue);
                userPortfolio.TotalInvestmentValue += userPortfolio.BondInvestmentValue;
            }
            if (InvestmentType == "MutualFund" || InvestmentType == "All")
            {
                _logger.LogInformation("Retrieving MutualFund Portfolio of the user");
                userPortfolio.MutualFunds = RetrieveMutualFundInvestments(UserID);
                userPortfolio.MutualInvestmentValue = userPortfolio.MutualFunds.Sum(mf => mf.InvestmentValue);
                userPortfolio.MutualInvestmentValue += userPortfolio.MutualInvestmentValue;
            }

            //calculating the percentages
            userPortfolio.StockInvestmentPercentage = userPortfolio.StocksInvestmentValue / userPortfolio.TotalInvestmentValue;
            userPortfolio.BondInvestmentPercentage = userPortfolio.BondInvestmentValue / userPortfolio.TotalInvestmentValue;
            userPortfolio.MutualFundInvestmentPercentage = userPortfolio.MutualInvestmentValue / userPortfolio.TotalInvestmentValue;

            return userPortfolio;
        }

        public List<StockInvestment> RetrieveStockInvestments(int ClientID)
        {
            _logger.LogInformation("Request to retrieve all the StockInvestments of {ClinetID}",ClientID);
            try
            {
                string conn = _configuration.GetConnectionString("DBConnection");
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("retrieve_stock_investments", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@clientID", ClientID);
                    _logger.LogInformation("Created and Parametrized the Comamnd");
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        _logger.LogInformation("Command Executing retrieving results");
                        List<StockInvestment> StockInvestments = new List<StockInvestment>();
                        StockInvestment StockInvestment;
                        if(reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                StockInvestment = new StockInvestment();
                                StockInvestment.ClientID = ClientID;
                                StockInvestment.StratergyID = reader.GetInt32(0);
                                StockInvestment.StockID = reader.GetInt32(1);
                                StockInvestment.StockName = reader.GetString(2);
                                StockInvestment.StockPrice = reader.GetDecimal(3);
                                StockInvestment.StockQuantity = reader.GetInt32(4);
                                StockInvestment.InvestmentValue = reader.GetDecimal(5);

                                _logger.LogInformation("Read Stock Information of {StockName} for Client {ClientID}", StockInvestment.StockName, StockInvestment.ClientID);
                                StockInvestments.Add(StockInvestment);
                            }
                        }
                        else {
                            _logger.LogInformation("User has not Stocks under his portfolio");
                        }
                        reader.Close();
                        connection.Close();
                        return StockInvestments;
                    }
                    catch (SqlException ex)
                    {
                        _logger.LogError(ex, "Failed to Execute SP_{storedProcedure}, Exception  raised due to DBContext", "retrieve_stock_investments");
                        connection.Close();
                        return null;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to Execure SP_{storedProcedure}, Exception raised in genetal context", "retrieve_stock_investments");
                        connection.Close();
                        return null;
                    }

                }
                catch (SqlException ex)
                {
                    _logger.LogError(ex, "Failed to Establish a command, Exception  raised due to DBContext");
                    connection.Close();
                    return null;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to Establish a command, Exception raised in genetal context");
                    connection.Close();
                    return null;
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Failed to Establish a connection, Exception  raised due to DBContext");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to Establish a connection, Exception raised in genetal context");
                return null;
            }

        }

        public List<BondInvestment> RetrieveBondInvestments(int ClientID)
        {
            _logger.LogInformation("Request to retrieve all the StockInvestments of {ClinetID}", ClientID);
            try
            {
                string conn = _configuration.GetConnectionString("DBConnection");
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("retrieve_bond_investments", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@clientID", ClientID);
                    _logger.LogInformation("Created and Parametrized the Comamnd");
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        _logger.LogInformation("Command Executing retrieving results");
                        List<BondInvestment> BondInvestments = new List<BondInvestment>();
                        BondInvestment BondInvestment;
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                BondInvestment = new BondInvestment();
                                BondInvestment.ClientID = ClientID;
                                BondInvestment.StratergyID = reader.GetInt32(0);
                                BondInvestment.BondID = reader.GetInt32(1);
                                BondInvestment.BondName = reader.GetString(2);
                                BondInvestment.BondPrice = reader.GetDecimal(3);
                                BondInvestment.BondQuantity = reader.GetInt32(4);
                                BondInvestment.InvestmentValue = reader.GetDecimal(5);

                                _logger.LogInformation("Read Stock Information of {BondName} for Client {ClientID}", BondInvestment.BondName, BondInvestment.ClientID);
                                BondInvestments.Add(BondInvestment);
                            }
                        }
                        else
                        {
                            _logger.LogInformation("User has not Stocks under his portfolio");
                        }
                        reader.Close();
                        connection.Close();
                        return BondInvestments;
                    }
                    catch (SqlException ex)
                    {
                        _logger.LogError(ex, "Failed to Execute SP_{storedProcedure}, Exception  raised due to DBContext", "retrieve_bond_investments");
                        connection.Close();
                        return null;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to Execure SP_{storedProcedure}, Exception raised in genetal context", "retrieve_bond_investments");
                        connection.Close();
                        return null;
                    }

                }
                catch (SqlException ex)
                {
                    _logger.LogError(ex, "Failed to Establish a command, Exception  raised due to DBContext");
                    connection.Close();
                    return null;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to Establish a command, Exception raised in genetal context");
                    connection.Close();
                    return null;
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Failed to Establish a connection, Exception  raised due to DBContext");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to Establish a connection, Exception raised in genetal context");
                return null;
            }

        }
        public List<MutualFundInvestment> RetrieveMutualFundInvestments(int ClientID)
        {
            _logger.LogInformation("Request to retrieve all the StockInvestments of {ClinetID}", ClientID);
            try
            {
                string conn = _configuration.GetConnectionString("DBConnection");
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("retrieve_mutualfund_investments", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@clientID", ClientID);
                    _logger.LogInformation("Created and Parametrized the Comamnd");
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        _logger.LogInformation("Command Executing retrieving results");
                        List<MutualFundInvestment> MutualFundInvestments = new List<MutualFundInvestment>();
                        MutualFundInvestment MutualFundInvestment;
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                MutualFundInvestment = new MutualFundInvestment();
                                MutualFundInvestment.ClientID = ClientID;
                                MutualFundInvestment.StratergyID = reader.GetInt32(0);
                                MutualFundInvestment.MutualFundID = reader.GetInt32(1);
                                MutualFundInvestment.MutualFundName = reader.GetString(2);
                                MutualFundInvestment.MutualFundPrice = reader.GetDecimal(3);
                                MutualFundInvestment.MutualFundQuantity = reader.GetInt32(4);
                                MutualFundInvestment.InvestmentValue = MutualFundInvestment.MutualFundPrice * MutualFundInvestment.MutualFundQuantity;

                                _logger.LogInformation("Read Stock Information of {MutualFundName} for Client {ClientID}", MutualFundInvestment.MutualFundName, MutualFundInvestment.ClientID);
                                MutualFundInvestments.Add(MutualFundInvestment);
                            }
                        }
                        else
                        {
                            _logger.LogInformation("User has not Stocks under his portfolio");
                        }
                        reader.Close();
                        connection.Close();
                        return MutualFundInvestments;
                    }
                    catch (SqlException ex)
                    {
                        _logger.LogError(ex, "Failed to Execute SP_{storedProcedure}, Exception  raised due to DBContext", "retrieve_mutualfund_investments");
                        connection.Close();
                        return null;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to Execure SP_{storedProcedure}, Exception raised in genetal context", "retrieve_mutualfund_investments");
                        connection.Close();
                        return null;
                    }

                }
                catch (SqlException ex)
                {
                    _logger.LogError(ex, "Failed to Establish a command, Exception  raised due to DBContext");
                    connection.Close();
                    return null;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to Establish a command, Exception raised in genetal context");
                    connection.Close();
                    return null;
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Failed to Establish a connection, Exception  raised due to DBContext");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to Establish a connection, Exception raised in genetal context");
                return null;
            }

        }

    }
}
