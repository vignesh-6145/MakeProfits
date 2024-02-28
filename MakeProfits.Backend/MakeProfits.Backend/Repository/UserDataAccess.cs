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
                using (SqlCommand command = new SqlCommand("RegisterUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parameters
                    command.Parameters.AddWithValue("@UserName", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@UserId", user.Id);


                    command.ExecuteNonQuery();
                }
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
                            return true;
                        }
                        return false;
                    }

                }
            }
        }

    }
}
