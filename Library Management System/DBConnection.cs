using MySql.Data.MySqlClient;

namespace Library_Management_System
{
    public class DBConnection
    {
        public static MySqlConnection GetConnection()
        {
            string connStr = "server=localhost;user id=root;password=;database=library_db";
            return new MySqlConnection(connStr);
        }
    }
}
