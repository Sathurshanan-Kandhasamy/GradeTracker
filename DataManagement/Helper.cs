using System;
using System.Configuration;
using System.Data.SqlClient;

namespace DataManagement
{
    public static class Helper 
    {
        /// <summary>
        /// Retrieves the specified connection string from the app.config file.
        /// </summary>
        /// <param name="name">The name of the connection string being requested.</param>
        /// <returns>The connection string details in a string</returns>
        private static string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        /// <summary>
        /// Builds an sql connection string for an sql server database.
        /// </summary>
        /// <returns>A configured sql connection.</returns>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(GetConnectionString("GradeTracker"));
        }
    }
}

