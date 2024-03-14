using MakeProfits.Backend.Models;
using System.Data.SqlClient;
using System.Data;

namespace MakeProfits.Backend.Repository
{
    public class StrategyDataAccess : IStrategyDataAccess
    {
        private readonly string connectionstring;
        private readonly IConfiguration configuration;
        public StrategyDataAccess(IConfiguration configuration)
        {
            this.connectionstring = configuration.GetConnectionString("DBConnection");
        }
        public Strategy addStrat(Strategy strategy)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("createStrategy", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    Guid strategyID = Guid.NewGuid();
                    // Parameters
                    command.Parameters.AddWithValue("@StrategyID", strategyID);
                    command.Parameters.AddWithValue("@AdvisorID", strategy.AdvisorID);
                    command.Parameters.AddWithValue("@StockID", strategy.StockID);
                    command.Parameters.AddWithValue("@MFID", strategy.MFID);
                    command.Parameters.AddWithValue("@BondsID", strategy.BondsID);
                    command.Parameters.AddWithValue("@StockPercentage", strategy.StockPercentage);
                    command.Parameters.AddWithValue("@MFPercentage", strategy.MFPercentage);
                    command.Parameters.AddWithValue("@BondsPercentage", strategy.BondsPercentage);

                    strategy.StrategyID = strategyID;
                    command.ExecuteNonQuery();
                    return strategy;
                }
            }
        }
        public string getinvestname(string table, Guid id)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    //var str = "select " + table + "_name from " + table + "_info where " + table + "ID =" + id.ToString();
                    var str = $"SELECT {table}_name FROM {table}_info WHERE {table}ID = '{id.ToString()}'";
                    Console.WriteLine(str);

                    command.CommandText = str;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Console.WriteLine(reader[0].ToString());
                        return reader[0].ToString();
                    }
                    return "";
                }
            }
        }
        public void deletestrat(Guid strategyId)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "DELETE FROM dbo.strategy_info WHERE strategyID = @strategyId";
                    command.Parameters.AddWithValue("@strategyId", strategyId);
                    command.ExecuteNonQuery();

                }
            }
        }
        public List<Strategy> showStrat(Guid advisorId)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    // Parameters
                    command.CommandText = "SELECT * FROM strategy_info WHERE AdvisorID = @advisorID";
                    command.Parameters.AddWithValue("@advisorID", advisorId);

                    SqlDataReader reader = command.ExecuteReader();
                    List<Strategy> strategies = new List<Strategy>();
                    while (reader.Read())
                    {
                        //Strategy strategy = new Strategy
                        //{
                        //    StrategyID = (Guid)reader["strategyID"],
                        //    AdvisorID = (Guid)reader["advisorID"],
                        //    StockID = (Guid)reader["stockID"],
                        //    MFID = (Guid)reader["mfID"],
                        //    BondsID = (Guid)reader["bondsID"],
                        //    StockPercentage = (decimal)reader["stock_per"],
                        //    MFPercentage = (double)reader["mf_per"],
                        //    BondsPercentage = (double)reader["bonds_per"]
                        //};
                        Strategy strategy = new Strategy();
                        strategy.StrategyID = reader.GetGuid(0);
                        strategy.AdvisorID = reader.GetGuid(1);
                        strategy.StockID = reader.GetGuid(2);
                        strategy.MFID = reader.GetGuid(3);
                        strategy.BondsID = reader.GetGuid(4);
                        strategy.StockPercentage = (double)reader.GetDecimal(5);
                        strategy.MFPercentage = (double)reader.GetDecimal(6);
                        strategy.BondsPercentage = (double)reader.GetDecimal(7);


                        strategies.Add(strategy);
                    }
                    return strategies;
                }
            }
        }
        public void subscription(Guid clientId)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    // Parameters
                    command.CommandText = "UPDATE clientTable SET subscription = 1 WHERE clientid = @clientId";
                    command.Parameters.AddWithValue("@clientId", clientId);
                    command.ExecuteNonQuery();

                }
            }
        }

        public bool CheckFirstTime(Guid clientId)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    // Parameters
                    command.CommandText = "SELECT firsttime FROM clientTable WHERE clientid = @clientId";
                    command.Parameters.AddWithValue("@clientId", clientId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bool cur = (bool)reader["firsttime"];

                            if (cur)
                            {
                                reader.Close();
                                // Update the record
                                using (SqlCommand updateCommand = new SqlCommand())
                                {
                                    updateCommand.Connection = connection;
                                    updateCommand.CommandType = CommandType.Text;
                                    updateCommand.CommandText = "UPDATE clientTable SET firsttime = 0 WHERE clientid = @clientId";
                                    updateCommand.Parameters.AddWithValue("@clientId", clientId);
                                    updateCommand.ExecuteNonQuery();
                                }

                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            // No rows found
                            return false;
                        }
                    }
                }
            }
        }
    }
}
