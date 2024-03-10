
using MakeProfits.Backend.Models;
using MakeProfits.Backend.Models.Stock;
using System.Data;
using System.Data.SqlClient;

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
                                    return false;
                                }
                                return true;
                            }
                            else
                            {
                                _logger.LogInformation("{ticker_symbol} was not stored in DB", TickSymbol);
                                return false;
                            }
                        }
                        else
                        {
                            _logger.LogInformation("{ticker_symbol} was not stored in DB", TickSymbol);
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
                                return true;
                            }
                            else
                            {
                                _logger.LogInformation("{ticker_symbol} was not stored in DB", TickSymbol);
                                return false;
                            }
                        }
                        else
                        {
                            _logger.LogInformation("{ticker_symbol} was not stored in DB", TickSymbol);
                            return false;
                        }

                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation(ex, "Something went wrong while executing command");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "");
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
                                return true;
                            }
                            else
                            {
                                _logger.LogInformation("{ticker_symbol} was not stored in DB", TickSymbol);
                                return false;
                            }
                        }
                        else
                        {
                            _logger.LogInformation("{ticker_symbol} was not stored in DB", TickSymbol);
                            return false;
                        }

                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Something went wrong while executing command");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "");
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
                            return security;
                        }
                        else
                        {
                            _logger.LogInformation("No SecurityProdile Record found");
                            return null;
                        }
                    }
                    catch (SqlException ex)
                    {
                        _logger.LogError(ex, "Failed to Execute SP_{storedProcedure}, Exception  raised due to DBContext", "retrieve_stock_info");
                        return null;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to Execure SP_{storedProcedure}, Exception raised in genetal context", "retrieve_stock_info");
                        return null;
                    }

                }
                catch (SqlException ex)
                {
                    _logger.LogError(ex, "Failed to Establish a command, Exception  raised due to DBContext");
                    return null;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to Establish a command, Exception raised in genetal context");
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
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to Create & Execute Command : {SP_procedureName}", "insert_stock");
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
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to Create & Execute Command : {SP_procedureName}", "insert_stock_balance_sheet");
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
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to Create & Execute Command : {SP_procedureName}", "insert_stock_income_statement");
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
                            return incomeStatement;
                        }
                        else
                        {
                            _logger.LogInformation("No income-statement Record found");
                            return null;
                        }
                    }
                    catch (SqlException ex)
                    {
                        _logger.LogError(ex, "Failed to Execute SP_{storedProcedure}, Exception  raised due to DBContext", "insert_stock_income_statement");
                        return null;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to Execure SP_{storedProcedure}, Exception raised in genetal context", "insert_stock_income_statement");
                        return null;
                    }

                }
                catch (SqlException ex)
                {
                    _logger.LogError(ex, "Failed to Establish a command, Exception  raised due to DBContext");
                    return null;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to Establish a command, Exception raised in genetal context");
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
                            return balanceSheet;
                        }
                        else
                        {
                            _logger.LogInformation("No income-statement Record found");
                            return null;
                        }
                    }
                    catch (SqlException ex)
                    {
                        _logger.LogError(ex, "Failed to Execute SP_{storedProcedure}, Exception  raised due to DBContext", "retrieve_stock_balance_sheet");
                        return null;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to Execure SP_{storedProcedure}, Exception raised in genetal context", "retrieve_stock_balance_sheet");
                        return null;
                    }

                }
                catch (SqlException ex)
                {
                    _logger.LogError(ex, "Failed to Establish a command, Exception  raised due to DBContext");
                    return null;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to Establish a command, Exception raised in genetal context");
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
                                return true;
                            else
                                return false;
                        }
                        else
                        {
                            _logger.LogInformation("Failed to read reaults");
                            return false;
                        }
                    }
                    catch (SqlException ex)
                    {
                        _logger.LogError(ex, "Failed to Execute SP_{storedProcedure}, Exception  raised due to DBContext", "optin_stratergy");
                        return false;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to Execure SP_{storedProcedure}, Exception raised in genetal context", "optin_stratergy");
                        return false;
                    }

                }
                catch (SqlException ex)
                {
                    _logger.LogError(ex, "Failed to Establish a command, Exception  raised due to DBContext");
                    return false;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to Establish a command, Exception raised in genetal context");
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
    }
}
