using MySql.Data.MySqlClient;
namespace CumulativeProject.Models
{
    public class SchoolDbContext
    {
        private static string User { get { return "root"; } }
        private static string Password { get { return ""; } }
        private static string Database { get { return "school"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }
        protected static string ConnectionString
        {
            get
            {
                return "server=" + Server
                       + ";user=" + User
                       + ";database=" + Database
                       + ";port=" + Port
                       + ";password=" + Password
                       + ";convert zero datetime=True";
            }
        }
        /// <summary>
        /// It returns the connection to the database named school
        /// </summary>
        /// <returns>
        /// It returns a MySqlConnection object
        /// </returns>
        /// <example>
        /// private SchoolDbContext CumulativeProject = new BlogDbContext();
        /// MySqlConnection Conn = CumulativeProject.AccessDatabase();
        /// </example>
        public MySqlConnection AccessDatabase()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
