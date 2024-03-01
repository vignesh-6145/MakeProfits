using MakeProfits.Backend.Models;
using MakeProfits.Backend.Repository;
using MakeProfits.Models;
using System.Data;
using System.Data.SqlClient;
namespace MakeProfits.Repository
{
    public class UserDataAccess :IUserDataAccess
    {
        private readonly string connectionstring;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserDataAccess> _logger;
          //public UserDataAccess(string connectionstring)
          //{
          //  this.connectionstring = connectionstring;
          //}
        public UserDataAccess(IConfiguration configuration, ILogger<UserDataAccess> logger)
        {
            this._logger = logger;
            this._configuration = configuration;
            //this._logger = logger;
            // this.connectionstring = Convert.ToString("Data Source=C1-BPFK114-L\\SQLEXPRESS;Initial Catalog=MakeProfits;Integrated Security=True;");
            this.connectionstring = Convert.ToString(this._configuration.GetConnectionString("DBConnection"));
            Console.WriteLine(this.connectionstring);
        }
        
         public void RegisterUser(User user)
        {
            _logger.LogInformation("Request to Insert new user calling SP_{StoredProcedure}", "InsertUser");
            using(SqlConnection connection = new SqlConnection(connectionstring))
            { 
                _logger.LogInformation("DBConnection Established Successfully");
                connection.Open();
                using (SqlCommand command = new SqlCommand("InsertUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parameters
                    command.Parameters.AddWithValue("@UserID", user.UserID);
                    command.Parameters.AddWithValue("@RoleID",user.RoleID);
                    command.Parameters.AddWithValue("@AddressLine",user.AddressLine);
                    command.Parameters.AddWithValue("@City",user.City);
                    command.Parameters.AddWithValue("@State",user.State);
                    command.Parameters.AddWithValue("@EmailAddress",user.EmailAddress);
                    command.Parameters.AddWithValue("@AdvisorID",user.AdvisorID);
                    command.Parameters.AddWithValue("@AgentID",user.AgentID);
                    command.Parameters.AddWithValue("@FirstName",user.FirstName);
                    command.Parameters.AddWithValue("@LastName",user.LastName);
                    command.Parameters.AddWithValue("@Company",user.Company);

                    command.Parameters.AddWithValue("@UserName",user.UserName);
                    command.Parameters.AddWithValue("@Password",user.Password);
                    command.Parameters.AddWithValue("@PhoneNumber",user.PhoneNumber);

                    _logger.LogInformation("Created and Parametrized a new SQLCommand");

                    command.ExecuteNonQuery();
                    _logger.LogInformation("Executed SP_{StoredProcedure}", "InsertUser");
                    _logger.LogInformation("Request Served by SP_{StoredProcedure}", "InsertUser");
                }
            }
        }
        public UserDTO getUserByUserName(string UserName)
        {

            _logger.LogInformation("Request to Retieve user by calling SP_{StoredProcedure}", "GetUserByName");
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                _logger.LogInformation("Established Connection Successfully");
                //TODO : Add Log Depicting Connection established and 
                using (SqlCommand command = new SqlCommand("GetUserByName", conn))
                {

                    //TODO : Add Log Depicting Command Created
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", UserName);

                    _logger.LogInformation("Created and Parametrized a new SQLCommand");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        _logger.LogInformation("Executed SP_{StoredProcedure}", "GetUserByName");
                        UserDTO user = new UserDTO();
                        if (reader.Read())
                        {
                            user.FirstName = reader.GetString(0);
                            user.LastName = reader.GetString(1);
                            user.UserName = reader.GetString(2);
                            user.PhoneNumber = reader.GetString(3);
                            user.AddressLine = reader.GetString(4);
                            user.City = reader.GetString(5);
                            user.State = reader.GetString(6);
                            user.EmailAddress = reader.GetString(7);
                            user.AdvisorName = reader.GetString(8); 
                            user.AgentName = reader.GetString(9);
                            user.Company = reader.GetString(10);
                            user.Role = reader.GetString(11);
                            _logger.LogInformation("Results Fetched successfully");
                            _logger.LogInformation("Request Served by SP_{StoredProcedure}", "GetUserByName");
                            return user;
                        }
                        _logger.LogInformation("No User with Given UserName : {UserName}",UserName);
                        _logger.LogInformation("Request Served by SP_{StoredProcedure}", "GetUserByName");
                        return null;
                    }
                }
            }
        }
        public  UserDTO GetUserById(int UserID)
        {

            _logger.LogInformation("Request to Retieve user by calling SP_{StoredProcedure}", "GetUserByID");
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();

                _logger.LogInformation("Established Connection Successfully");
                using (SqlCommand command = new SqlCommand("GetUserByID", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID",UserID);

                    _logger.LogInformation("Created and Parametrized a new SQLCommand");
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        _logger.LogInformation("Executed SP_{StoredProcedure}", "GetUserByID");
                        UserDTO user = new UserDTO();
                        if (reader.Read())
                        {
                            user.FirstName = reader.GetString(0);
                            user.LastName = reader.GetString(1);
                            user.UserName = reader.GetString(2);
                            user.PhoneNumber = reader.GetString(3);
                            user.AddressLine = reader.GetString(4);
                            user.City = reader.GetString(5);
                            user.State = reader.GetString(6);
                            user.EmailAddress = reader.GetString(7);
                            user.AdvisorName = reader.GetString(8);
                            user.AgentName = reader.GetString(9);
                            user.Company = reader.GetString(10);
                            user.Role = reader.GetString(11);
                            _logger.LogInformation("Results Fetched successfully");
                            _logger.LogInformation("Request Served by SP_{StoredProcedure}", "GetUserByID");
                            return user;
                        }
                        _logger.LogInformation("No User with Given UserName : {UserID}", UserID);
                        _logger.LogInformation("Request Served by SP_{StoredProcedure}", "GetUserByID");
                        return null;
                    }
                }
            }
        }
        public bool ValidateUser(String Username,String Password)
        {
            _logger.LogInformation("Request to Authenticate User calling SP_{StoredProcedure}", "ValidateUser");
            using(SqlConnection conn = new SqlConnection(connectionstring))
            {
                _logger.LogInformation("Established Connection Successfully");
                conn.Open();
                using(SqlCommand command = new SqlCommand("ValidateUser",conn))
                {
                    command.CommandType= CommandType.StoredProcedure;
                    SqlParameter pr1=new    SqlParameter("@Username",Username);
                    SqlParameter pr2 = new SqlParameter("@Password", Password);
                    command.Parameters.Add(pr1 );
                    command.Parameters.Add(pr2 );

                    _logger.LogInformation("Created and Parametrized a new SQLCommand");
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        _logger.LogInformation("Executed SP_{StoredProcedure}", "ValidateUser");
                        if (reader.Read())
                        {
                            if(reader.GetInt32(0)==1)
                            {
                                _logger.LogInformation("User Authenticated Successfully");
                                _logger.LogInformation("Request Served by SP_{StoredProcedure}", "ValidateUser");
                                return true; 
                            }
                            else {

                                _logger.LogInformation("Please Check your Credentials");
                                _logger.LogInformation("Request Served by SP_{StoredProcedure}", "ValidateUser");
                                return false; 
                            }
                        }
                        _logger.LogInformation("No valid user with given given credentials");
                        _logger.LogInformation("Request Served by SP_{StoredProcedure}", "ValidateUser");
                        return false;
                    }

                }
            }
        }

    }
}
