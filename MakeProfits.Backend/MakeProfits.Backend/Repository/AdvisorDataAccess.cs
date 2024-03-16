using MakeProfits.Backend.Models;
using System.Data.SqlClient;
using System.Data;
using MakeProfits.Backend.Models.AdvisorRequests;
using System.Reflection.PortableExecutable;

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
                            advisor.Rating = reader.GetDecimal(7);
                            advisor.ID = reader.GetGuid(8);

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
                        connection.Close();
                        return null;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to Execure SP_{storedProcedure}, Exception raised in genetal context", "GetAdvisorsInfo");
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
        public Advisor GetAdvisor(Guid AdvisorID)
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
                            advisor.Rating = reader.GetDecimal(7);
                            advisor.ID = reader.GetGuid(8);

                            _logger.LogInformation("Retrieved Advisor with name : {AdvisorName}", advisor.FirstName);
                            reader.Close();
                            connection.Close();
                            return advisor;
                        }
                        else
                        {
                            _logger.LogInformation("No Adviosr found with the given AdvisorID");
                            reader.Close();
                            connection.Close();
                            return null;
                        }
                        
                    }
                    catch (SqlException ex)
                    {
                        _logger.LogError(ex, "Failed to Execute SP_{storedProcedure}, Exception  raised due to DBContext", "GetAdvisorsInfo");
                        connection.Close();
                        return null;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to Execure SP_{storedProcedure}, Exception raised in genetal context", "GetAdvisorsInfo");
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
        public IEnumerable<Advisor> GetClientAdvisors(Guid ClientID)
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
                                advisor.Rating = reader.GetDecimal(7);

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
                            reader.Close();
                            connection.Close();
                            return null;
                        }

                    }
                    catch (SqlException ex)
                    {
                        _logger.LogError(ex, "Failed to Execute SP_{storedProcedure}, Exception  raised due to DBContext", "GetUserAdvisors");
                        connection.Close();
                        return null;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to Execure SP_{storedProcedure}, Exception raised in genetal context", "GetUserAdvisors");
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

        public IEnumerable<AbstractUser> GetAdvisorClients(Guid AdvisorID)
        {
            _logger.LogInformation("Request to retieve the advisors for client with ID : {AdvisorID}", AdvisorID);
            try
            {
                string conn = _configuration.GetConnectionString("DBConnection");
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("GetAdvisorClients", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AdvisorID", AdvisorID);

                    _logger.LogInformation("Created and Parametrized the Comamnd");
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        _logger.LogInformation("Command Executing retrieving results");
                        List<AbstractUser> users = new List<AbstractUser>();
                        AbstractUser user;
                        if (reader.HasRows)
                        {
                            while (reader.Read() && reader.HasRows)
                            {
                                user = new AbstractUser();
                                user.FirstName = reader.GetString(0)??"";
                                user.LastName = reader.GetString(1)??"";
                                user.UserName = user.FirstName + user.LastName;
                                user.AddressLine = reader.GetString(2) ?? "";
                                user.City = reader.GetString(3) ?? "";
                                user.State = reader.GetString(4) ?? "";
                                user.EmailAddress = reader.GetString(5) ?? "";
                                user.PhoneNumber = reader.GetString(6) ?? "";
                                var val = reader.GetDecimal(7);
                                user.funds = val==null ? 0 :  val;
                                user.ID = reader.GetGuid(8);

                                _logger.LogInformation("Retrieved User with name : {UserName}", user.FirstName);
                                users.Add(user);
                            }
                            reader.Close();
                            connection.Close();
                            return users    ;
                        }
                        else
                        {
                            _logger.LogInformation("No Adviosr found for the client");

                            reader.Close();
                            connection.Close();
                            return null;
                        }

                    }
                    catch (SqlException ex)
                    {
                        _logger.LogError(ex, "Failed to Execute SP_{storedProcedure}, Exception  raised due to DBContext", "GetAdvisorClients");
                        connection.Close();
                        return null;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to Execure SP_{storedProcedure}, Exception raised in genetal context", "GetAdvisorClients");
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


        public bool AddCient(AdvisoryRequest advisoryRequest)
        {
            _logger.LogInformation("Intiating the process of Adding client to the Advisor");
            advisoryRequest.RequestBY = "A";
            try
            {
                var connectionstring = _configuration.GetConnectionString("DBConnection");
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                _logger.LogInformation("Connection Established");
                try
                {
                    SqlCommand command = new SqlCommand("adviser_request_client", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@clientID", advisoryRequest.ClientID);
                    command.Parameters.AddWithValue("@advisorID", advisoryRequest.AdvisorID);
                    command.Parameters.AddWithValue("@stratergyID", advisoryRequest.StratergyID);
                    command.Parameters.AddWithValue("@Message", advisoryRequest.Message);
                    _logger.LogInformation("Created and Parametrized  command");

                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read() && reader.GetInt32(0)==1)
                        {

                            reader.Close();
                            conn.Close();
                            return true;
                        }
                        return false;
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Failed to read  result, Exception raised in DB");
                        conn.Close();
                        return false;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Failed to read  result, Exception raised");
                        conn.Close();
                        return true;
                    }
                }
                catch (SqlException ex)
                {
                    _logger.LogError(ex, "Failed to Create  Command, Exception raised in DB");
                    conn.Close();
                    return false;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to Create  Command, Exception raised");
                    conn.Close();
                    return true;
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex,"Failed to Establish Connection, Exception raised in DB");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Failed to Establish Connection, Exception raised");
                return true;
            }
        }

    }
}
