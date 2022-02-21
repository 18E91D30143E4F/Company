using System.Data;
using System.Data.SqlClient;

namespace Data
{
    internal static class DbHelper
    {
        private const string ConnectionString = DbSettings.ConnectionString;

        public static DataTable ExecuteToDataTable(SqlCommand command)
        {
            SqlDataReader reader = null;

            try
            {
                using var connection = new SqlConnection(ConnectionString);
                connection.Open();

                command.Connection = connection;
                reader = command.ExecuteReader();

                var table = new DataTable();
                table.Load(reader);

                return table;
            }
            finally
            {
                reader?.Close();
            }
        }

        public static int ExecuteNoQuery(SqlCommand command)
        {
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(ConnectionString);
                connection.Open();

                command.Connection = connection;
                var number = command.ExecuteNonQuery();

                return number;
            }
            catch
            {
                return 0;
            }
            finally
            {
                connection?.Close();
            }
        }
    }
}