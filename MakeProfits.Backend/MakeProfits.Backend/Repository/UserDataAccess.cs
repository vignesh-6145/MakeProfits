using MakeProfits.Backend.Models;
using MakeProfits.Models;
using System.Data;
using System.Data.SqlClient;
namespace MakeProfits.Repository
{
    public class UserDataAccess
    {
        private readonly string connectionstring;
          public UserDataAccess(string connectionstring)
          {
            this.connectionstring = connectionstring;
            }
         public void RegisterUser(User user)
        {
            using(SqlConnection connection = new SqlConnection(connectionstring))
            { 
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
                            user.Company = reader.GetString(10);
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
        public bool ValidateUser(String Username,String Password)
        {
            using(SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                using(SqlCommand command = new SqlCommand("ValidateUser",conn))
                {
                    command.CommandType= CommandType.StoredProcedure;
                    SqlParameter pr1=new    SqlParameter("@Username",Username);
                    SqlParameter pr2 = new SqlParameter("@Password", Password);
                    command.Parameters.Add(pr1 );
                    command.Parameters.Add(pr2 );
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            if(reader.GetInt32(0)==1)
                            { return true; }
                            else { return false; }
                        }
                        return false;
                    }

                }
            }
        }

    }
}
