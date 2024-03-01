using System.Configuration;
using System.Data.SqlClient;


namespace GestionalePoliziaComunale.Models
{

    public class Connection
    {
        public static SqlConnection GetConn()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            return conn;
        }
    }


}