using MakeProfits.Backend.Models;
using MakeProfits.Backend.Models.AdvisorRequests;
using MakeProfits.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
namespace MakeProfits.Repository
{
    public class UserDataAccess
    {
        private readonly string connectionstring;
          public UserDataAccess(string connectionstring)
          {
            this.connectionstring = connectionstring;
            }
        public void RegisterUser(userregister user)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("RegisterUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    Guid userid = Guid.NewGuid();
                    // Parameters
                    command.Parameters.AddWithValue("@role", "client");
                    command.Parameters.AddWithValue("@email", user.email);
                    command.Parameters.AddWithValue("@phno", user.phno);
                    command.Parameters.AddWithValue("@password", user.password);
                    command.Parameters.AddWithValue("@firstname", user.firstname);
                    command.Parameters.AddWithValue("@userid", userid);


                    command.ExecuteNonQuery();
                }
            }
        }
        public UserDTO getUserByUserName(string UserName)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();

                //TODO : Add Log Depicting Connection established and 
                using (SqlCommand command = new SqlCommand("GetUserByName", conn))
                {

                    //TODO : Add Log Depicting Command Created
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", UserName);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // TODO : Add Log depicting command executed
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
                            user.Role = reader.GetString(11);
                            

                            return user;
                        }
                        return null;
                    }
                }

                conn.Close();
            }
        }
        public  UserDTO GetUserById(int UserID)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();

                //TODO : Add Log Depicting Connection established and 
                using (SqlCommand command = new SqlCommand("GetUserByID", conn))
                {

                    //TODO : Add Log Depicting Command Created
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID",UserID);

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        // TODO : Add Log depicting command executed
                        if(reader.Read())
                        {
                            return new UserDTO();
                        }
                        return new UserDTO();
                    }
                }

                conn.Close();
            }
        }
        public string ValidateUser(String Username, String Password)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("ValidateUser", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter pr1 = new SqlParameter("@email", Username);
                    SqlParameter pr2 = new SqlParameter("@password", Password);
                    command.Parameters.Add(pr1);
                    command.Parameters.Add(pr2);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader["role"].ToString();
                        }
                        return "not a user";
                    }

                }
            }
        }
        public string ValidateUserOfGgl(string username)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandType = CommandType.Text;

                    // Use parameterized query to prevent SQL injection
                    command.CommandText = "SELECT role FROM users WHERE email = @email";
                    command.Parameters.AddWithValue("@email", username);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader["role"].ToString();
                        }
                        return "not a user";
                    }
                }
            }
        }
        public bool RequestAdvisory(AdvisoryRequest advisoryRequest)
        {
            advisoryRequest.RequestBY = "C";
            try
            {
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                try
                {
                    SqlCommand command = new SqlCommand("client_request_advisor",conn);
                    command.CommandType= CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@clientID",advisoryRequest.ClientID);
                    command.Parameters.AddWithValue("@advisorID",advisoryRequest.AdvisorID);
                    command.Parameters.AddWithValue("@stratergyID", advisoryRequest.StratergyID);
                    command.Parameters.AddWithValue("@Message",advisoryRequest.Message);
                    Console.WriteLine("Created and Parametrized  command");

                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        
                        if(reader.Read() && reader.HasRows)
                        {
                            Console.WriteLine($"RESULT {reader.GetString(0)}");
                            reader.Close();
                            conn.Close();
                            return true;
                        }
                        reader.Close();
                        conn.Close();
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
                    Console.WriteLine("Failed to Create  Command, Exception raised in DB");
                    conn.Close();
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to Create  Command, Exception raised");
                    conn.Close();
                    return true;
                }
            }
            catch(SqlException ex) {
                Console.WriteLine("Failed to Establish Connection, Exception raised in DB");
                return false;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Failed to Establish Connection, Exception raised");
                return true;
            }
        }
        public IEnumerable<Notification> ReadNotifications(Guid UserID)

        {
            Console.WriteLine("Request to retieve all the advisors");
            try
            {
                //string conn = _configuration.GetConnectionString("DBConnection");
                SqlConnection connection = new SqlConnection(connectionstring);
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("read_notifications", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID",UserID);

                    Console.WriteLine("Created and Parametrized the Comamnd");
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        Console.WriteLine("Command Executing retrieving results");
                        List<Notification> notifications = new List<Notification>();
                        Notification notification;
                        while (reader.Read())
                        {
                            notification = new Notification();
                            notification.FromUserName = reader.GetString(0);
                            notification.FromID = reader.GetGuid(1);
                            notification.ToUserName = reader.GetString(2);
                            notification.ToID = reader.GetGuid(3);
                            notification.Message = reader.GetString(4);

                            //Console.WriteLine("Read Notification Received from {FromName} to {ToName}", notification.FromUserName,notification.ToUserName);
                            Console.WriteLine($"Read Notification Received from {notification.FromUserName} to {notification.ToUserName}");

                            notifications.Add(notification);
                        }
                        //Console.WriteLine("Retrieved {count} records", notifications.Count);
                        Console.WriteLine($"Retrieved {notifications.Count} records" );
                        reader.Close();
                        connection.Close();
                        return notifications;
                    }
                    catch (SqlException ex)
                    {
                       // Console.WriteLine(ex, "Failed to Execute SP_{storedProcedure}, Exception  raised due to DBContext", "GetAdvisorsInfo");
                       Console.WriteLine("Failed to Execute SP_read_notifications, Exception  raised due to DBContext");
                        connection.Close();
                        return null;
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine(ex, "Failed to Execure SP_{storedProcedure}, Exception raised in genetal context", "GetAdvisorsInfo");
                        Console.WriteLine("Failed to Execute SP_read_notifications, Exception  raised due to general context");
                        connection.Close();
                        return null;
                    }

                }
                catch (SqlException ex)
                {
                    //Console.WriteLine(ex, "Failed to Establish a command, Exception  raised due to DBContext");
                    Console.WriteLine( "Failed to Establish a command, Exception  raised due to DBContext");
                    connection.Close();
                    return null;
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex, "Failed to Establish a command, Exception raised in genetal context");
                    Console.WriteLine("Failed to Establish a command, Exception  raised due general context");
                    connection.Close();

                    return null;
                }
            }
            catch (SqlException ex)
            {
                //Console.WriteLine(ex, "Failed to Establish a connection, Exception  raised due to DBContext");
                Console.WriteLine("Failed to Establish a connection, Exception  raised due to DBContext");
                return null;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex, "Failed to Establish a connection, Exception raised in genetal context");
                Console.WriteLine("Failed to Establish a connection, Exception raised in genetal context");
                return null;
            }
        }
    }
}
