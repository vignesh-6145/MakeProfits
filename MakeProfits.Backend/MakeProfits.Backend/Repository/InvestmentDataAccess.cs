using MakeProfits.Backend.Models.Securities;
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
                    SqlCommand command = new SqlCommand("CheckForSecurity",connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TickerSymbol",TickSymbol);
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
                                _logger.LogInformation("{TickSymbol} was already stored in DB",TickSymbol);

                                TimeSpan updateDiff = DateTime.UtcNow.Subtract(lastUpdatedOn);
                                if(updateDiff.Days > 1)
                                {
                                    _logger.LogInformation("The price of  {TickSymbol} haven't updated, have to make a API Call", TickSymbol);
                                    return false;
                                }
                                return true;
                            }
                            else
                            {
                                _logger.LogInformation("{TickSymbol} was not stored in DB", TickSymbol);
                                return false;
                            }
                        }
                        else
                        {
                            _logger.LogInformation("{TickSymbol} was not stored in DB", TickSymbol);
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
            _logger.LogInformation("Checking if the Balance sheet of {TickSymbol} is available for the {year}",TickSymbol,year);
            try
            {
                string connectionString = Convert.ToString(_configuration.GetConnectionString("DBConnection"));
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                _logger.LogInformation("Connection Established");
                try
                {
                    SqlCommand command = new SqlCommand("CheckForSecurityBalance", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TickerSymbol", TickSymbol);
                    command.Parameters.AddWithValue("@year",year);
                    _logger.LogInformation("Created Command and parametrized Command");

                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            if (reader.GetInt32(0) == 1)
                            {
                                _logger.LogInformation("{TickSymbol} was already stored in DB", TickSymbol);
                                return true;
                            }
                            else
                            {
                                _logger.LogInformation("{TickSymbol} was not stored in DB", TickSymbol);
                                return false;
                            }
                        }
                        else
                        {
                            _logger.LogInformation("{TickSymbol} was not stored in DB", TickSymbol);
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
            _logger.LogInformation("Checking if the Income sheet of {TickSymbol} is available for the {year}", TickSymbol, year);
            try
            {
                string connectionString = Convert.ToString(_configuration.GetConnectionString("DBConnection"));
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                _logger.LogInformation("Connection Established");
                try
                {
                    SqlCommand command = new SqlCommand("CheckForSecurityIncome", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TickerSymbol", TickSymbol);
                    command.Parameters.AddWithValue("@year", year);
                    _logger.LogInformation("Created Command and parametrized Command");

                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read() && reader.HasRows)
                        {
                            if (reader.GetInt32(0) == 1)
                            {
                                _logger.LogInformation("{TickSymbol} was already stored in DB", TickSymbol);
                                return true;
                            }
                            else
                            {
                                _logger.LogInformation("{TickSymbol} was not stored in DB", TickSymbol);
                                return false;
                            }
                        }
                        else
                        {
                            _logger.LogInformation("{TickSymbol} was not stored in DB", TickSymbol);
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
        public SecurityDTO RetrieveSecurityProfile(string tickSymbol)
        {
            _logger.LogInformation("Request to retieve {tickSymbol} Profile",tickSymbol);
            try
            {
                string conn = _configuration.GetConnectionString("DBConnection");
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("RetrieveSecurities",connection);
                    command.CommandType = CommandType.StoredProcedure;

                    _logger.LogInformation("Created and Parametrized the Comamnd");
                    command.Parameters.AddWithValue("@TickerSymbol",tickSymbol);
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        _logger.LogInformation("Command Executing retrieving results");
                        if(reader.Read() && reader.HasRows) {
                            SecurityDTO security = new SecurityDTO();
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
                        _logger.LogError(ex, "Failed to Execute SP_{storedProcedure}, Exception  raised due to DBContext", "RetrieveSecurities");
                        return null;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to Execure SP_{storedProcedure}, Exception raised in genetal context", "RetrieveSecurities");
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
        public bool InsertSecurity(Security security)
        {
            _logger.LogInformation("Inserting the security of {TickSymbol}",security.TickerSymbol);
            try
            {
                string connectionString = Convert.ToString(_configuration.GetConnectionString("DBConnection"));
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                _logger.LogInformation("Connection Established");
                try
                {
                    SqlCommand command = new SqlCommand("InsertSecurity", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TickerSymbol", security.TickerSymbol);
                    command.Parameters.AddWithValue("@cik", security.Cik);
                    command.Parameters.AddWithValue("@OrganizarionName", security.OrganizationName);
                    command.Parameters.AddWithValue("@UpdatedOn",security.UpdatedOn);
                    command.Parameters.AddWithValue("@price", security.Price);
                    _logger.LogInformation("Created Command and parametrized Command");
                    
                    command.ExecuteNonQuery();
                    _logger.LogInformation("Successfully Inserted record into the DB");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to Create & Execute Command : {SP_procedureName}", "InsertSecurity");
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

        public bool InsertSecurityBalanceSheetInfo(SecurityBalanceSheet securityBalanceSheet)
        {
            _logger.LogInformation("Inserting the security-balance-sheet info of {TickSymbol}", securityBalanceSheet.TickerSymbol);
            try
            {
                string connectionString = Convert.ToString(_configuration.GetConnectionString("DBConnection"));
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                _logger.LogInformation("Connection Established");
                try
                {
                    SqlCommand command = new SqlCommand("InsertSecurityBalanceSheetInfo", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TickerSymbol", securityBalanceSheet.TickerSymbol);
                    command.Parameters.AddWithValue("@year", securityBalanceSheet.year);
                    command.Parameters.AddWithValue("@TotalAssets", securityBalanceSheet.TotalAssets);
                    command.Parameters.AddWithValue("@TotalStockholdersEquity", securityBalanceSheet.TotalStockholdersEquity);
                    _logger.LogInformation("Created Command and parametrized Command");

                    command.ExecuteNonQuery();
                    _logger.LogInformation("Successfully Inserted record into the DB");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to Create & Execute Command : {SP_procedureName}", "InsertSecurityBalanceSheetInfo");
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
        public bool InsertSecurityIncomeStatementInfo(SecurityIncomeStatement securityIncomeStatement)
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
                    SqlCommand command = new SqlCommand("InsertSecurityIncomeStatementInfo", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TickerSymbol", securityIncomeStatement.TickerSymbol);
                    command.Parameters.AddWithValue("@year", securityIncomeStatement.year);
                    command.Parameters.AddWithValue("@NetIncome", securityIncomeStatement.NetIncome);
                    command.Parameters.AddWithValue("@Revenue", securityIncomeStatement.Revenue);
                    _logger.LogInformation("Created Command and parametrized Command");

                    command.ExecuteNonQuery();
                    _logger.LogInformation("Successfully Inserted record into the DB");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to Create & Execute Command : {SP_procedureName}", "InsertSecurityIncomeStatementInfo");
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

        public SecurityIncomeStatement RetieveSecurityIncomeStatement(string tickSymbol, int year)
        {
            _logger.LogInformation("Request to retieve {tickSymbol} Income-Statement for the {year}",tickSymbol,year);
            try
            {
                string conn = _configuration.GetConnectionString("DBConnection");
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("RetrieveSecurityIncome", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    _logger.LogInformation("Created and Parametrized the Comamnd");
                    command.Parameters.AddWithValue("@TickerSymbol", tickSymbol);
                    command.Parameters.AddWithValue("@year", year);
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        _logger.LogInformation("Command Executing retrieving results");
                        if (reader.Read() && reader.HasRows)
                        {
                            SecurityIncomeStatement incomeStatement = new SecurityIncomeStatement();
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
                        _logger.LogError(ex, "Failed to Execute SP_{storedProcedure}, Exception  raised due to DBContext", "RetrieveSecurityIncome");
                        return null;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to Execure SP_{storedProcedure}, Exception raised in genetal context", "RetrieveSecurityIncome");
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
        public SecurityBalanceSheet RetrieveSecurityBalanceSheet(string tickSymbol, int year)
        {
            _logger.LogInformation("Request to retieve {tickSymbol} Balance-Sheet for the {year}", tickSymbol, year);
            try
            {
                string conn = _configuration.GetConnectionString("DBConnection");
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("RetrieveSecurityBalance", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    _logger.LogInformation("Created and Parametrized the Comamnd");
                    command.Parameters.AddWithValue("@TickerSymbol", tickSymbol);
                    command.Parameters.AddWithValue("@year", year);
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        _logger.LogInformation("Command Executing retrieving results");
                        if (reader.Read() && reader.HasRows)
                        {
                            SecurityBalanceSheet balanceSheet = new SecurityBalanceSheet();
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
                        _logger.LogError(ex, "Failed to Execute SP_{storedProcedure}, Exception  raised due to DBContext", "RetrieveSecurityBalance");
                        return null;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to Execure SP_{storedProcedure}, Exception raised in genetal context", "RetrieveSecurityBalance");
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
    }
}
