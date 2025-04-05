using MySql.Data.MySqlClient;

namespace CumulativeAssignment_01.Models
{
    //Creating a class for database
    public class SchoolDbContext
    {
        // accessing connection details using methods
        private static string User { get { return "root"; } }

        private static string Password { get { return ""; } }

        private static string Database { get { return "schooldb"; } }

        private static string Server { get { return "localhost"; } }

        private static string Port { get { return "3306"; } }

        protected static string ConnectionString
        {
            get
            {
                return "user =" + User + "; password =" + Password + "; database =" + Database + "; server =" + Server + "; port =" + Port + "; convert zero datetime=True";
            }
        }

        public MySqlConnection AccessDatabase()
        {
            return new MySqlConnection(ConnectionString);
        }





    }
}
