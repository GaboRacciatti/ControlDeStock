using System.Configuration;
using MySql.Data.MySqlClient;

namespace ControlDeStock.DataBase
{
    public static class DbConnection
    {
        public static MySqlConnection GetConnection()
        {
            string connection = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            return new MySqlConnection(connection);
        }
    }
}
