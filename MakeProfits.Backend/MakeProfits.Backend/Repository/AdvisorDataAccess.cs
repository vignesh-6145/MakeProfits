using MakeProfits.Backend.Models;
using MakeProfits.Backend.Models.Securities;
using System.Data.SqlClient;
using System.Data;

namespace MakeProfits.Backend.Repository
{
    public class AdvisorDataAccess : IAdvisorDataAccess
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AdvisorDataAccess> _logger;

        public AdvisorDataAccess(IConfiguration configuration, ILogger<AdvisorDataAccess> logger)
        {
            this._configuration = configuration;
            this._logger = logger;
        }
        public IEnumerable<Advisor> GetAllAdvisors()
        {
            _logger.LogInformation("Request to retieve all the advisors");
            try
            {
                string conn = _configuration.GetConnectionString("DBConnection");
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("GetAdvisorsInfo", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    _logger.LogInformation("Created and Parametrized the Comamnd");
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        _logger.LogInformation("Command Executing retrieving results");
                        List<Advisor> advisors = new List<Advisor>();
                        Advisor advisor;    
                        while (reader.Read())
                        {
                            advisor = new Advisor();
                            advisor.FirstName = reader.GetString(0);
                            advisor.LastName = reader.GetString(1);
                            advisor.UserName = advisor.UserName + advisor.LastName;
                            advisor.AddressLine = reader.GetString(2);
                            advisor.City = reader.GetString(3);
                            advisor.State = reader.GetString(4);
                            advisor.EmailAddress = reader.GetString(5);
                            advisor.PhoneNumber = reader.GetString(6);
                            advisor.Rating = reader.GetInt32(7);

                            _logger.LogInformation("Retrieved Advisor with name : {AdvisorName}",advisor.FirstName);

                            advisors.Add(advisor);
                        }
                        _logger.LogInformation("Retrieved {count} records",advisors.Count);
                        reader.Close();
                        connection.Close();
                        return advisors;
                    }
                    catch (SqlException ex)
                    {
                        _logger.LogError(ex, "Failed to Execute SP_{storedProcedure}, Exception  raised due to DBContext", "GetAdvisorsInfo");
                        return null;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to Execure SP_{storedProcedure}, Exception raised in genetal context", "GetAdvisorsInfo");
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
            return null;
        }
        public Advisor GetAdvisor(int AdvisorID)
        {
            _logger.LogInformation("Request to retieve the advisor with ID : {AdvisorID}",AdvisorID);
            try
            {
                string conn = _configuration.GetConnectionString("DBConnection");
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("GetAdvisorInfo", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AdvisorID", AdvisorID);

                    _logger.LogInformation("Created and Parametrized the Comamnd");
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        _logger.LogInformation("Command Executing retrieving results");
                        Advisor advisor;
                        if (reader.Read() && reader.HasRows)
                        {
                            advisor = new Advisor();
                            advisor.FirstName = reader.GetString(0);
                            advisor.LastName = reader.GetString(1);
                            advisor.UserName = advisor.UserName + advisor.LastName;
                            advisor.AddressLine = reader.GetString(2);
                            advisor.City = reader.GetString(3);
                            advisor.State = reader.GetString(4);
                            advisor.EmailAddress = reader.GetString(5);
                            advisor.PhoneNumber = reader.GetString(6);
                            advisor.Rating = reader.GetInt32(7);

                            _logger.LogInformation("Retrieved Advisor with name : {AdvisorName}", advisor.FirstName);
                            reader.Close();
                            connection.Close();
                            return advisor;
                        }
                        else
                        {
                            _logger.LogInformation("No Adviosr found with the given AdvisorID");
                            return null;
                        }
                        
                    }
                    catch (SqlException ex)
                    {
                        _logger.LogError(ex, "Failed to Execute SP_{storedProcedure}, Exception  raised due to DBContext", "GetAdvisorsInfo");
                        return null;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to Execure SP_{storedProcedure}, Exception raised in genetal context", "GetAdvisorsInfo");
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
            return null;
        }
        public IEnumerable<Advisor> GetClientAdvisors(int ClientID)
        {
            _logger.LogInformation("Request to retieve the advisors for client with ID : {ClientID}", ClientID);
            try
            {
                string conn = _configuration.GetConnectionString("DBConnection");
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("GetUserAdvisors", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ClientID", ClientID);

                    _logger.LogInformation("Created and Parametrized the Comamnd");
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        _logger.LogInformation("Command Executing retrieving results");
                        List<Advisor> advisors = new List<Advisor>();
                        Advisor advisor;
                        if (reader.HasRows)
                        {
                            while (reader.Read() && reader.HasRows)
                            {
                                advisor = new Advisor();
                                advisor.FirstName = reader.GetString(0);
                                advisor.LastName = reader.GetString(1);
                                advisor.UserName = advisor.FirstName + advisor.LastName;
                                advisor.AddressLine = reader.GetString(2);
                                advisor.City = reader.GetString(3);
                                advisor.State = reader.GetString(4);
                                advisor.EmailAddress = reader.GetString(5);
                                advisor.PhoneNumber = reader.GetString(6);
                                advisor.Rating = reader.GetInt32(7);

                                _logger.LogInformation("Retrieved Advisor with name : {AdvisorName}", advisor.FirstName);
                                advisors.Add(advisor);
                            }
                            reader.Close();
                            connection.Close();
                            return advisors;
                        }
                        else
                        {
                            _logger.LogInformation("No Adviosr found for the client");
                            return null;
                        }

                    }
                    catch (SqlException ex)
                    {
                        _logger.LogError(ex, "Failed to Execute SP_{storedProcedure}, Exception  raised due to DBContext", "GetUserAdvisors");
                        return null;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to Execure SP_{storedProcedure}, Exception raised in genetal context", "GetUserAdvisors");
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
            return null;
        }

    }
}
