using Microsoft.Data.SqlClient;
using System.Data;

namespace WebTMAIntegration.Data.Helpers
{
    public class SqlClientHelper
    {
        private readonly IConfiguration _configuration;

        public SqlClientHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string GetConnectionString()
        {
            return _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<SqlConnection> GetOpenConnectionAsync()
        {
            SqlConnection conn = new SqlConnection(GetConnectionString());

            await conn.OpenAsync();

            return conn;
        }

        // INSERT UPDATE DELETE
        public async Task<int> ExecuteNonQueryAsync(
            string query, 
            CommandType commandType, 
            SqlParameter[]? parameters = null)
        {
            await using SqlConnection connection =
                await GetOpenConnectionAsync();

            return await ExecuteNonQueryAsync(           
                query: query,
                commandType: commandType,
                parameters: parameters,
                connection: connection
            );
        }

        public async Task<int> ExecuteNonQueryAsync(
            string query,
            CommandType commandType,
            SqlParameter[]? parameters = null,
            SqlConnection? connection = null,
            SqlTransaction? transaction = null)
        {
            bool ownsConnection = false;

            if (connection == null)
            {
                connection = await GetOpenConnectionAsync();
                ownsConnection = true;
            }

            try
            {
                await using SqlCommand cmd =
                    new(query, connection, transaction);

                cmd.CommandType = commandType;

                if (parameters?.Length > 0)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                return await cmd.ExecuteNonQueryAsync();
            }
            finally
            {
                if (ownsConnection)
                {
                    await connection.DisposeAsync();
                }
            }
        }
        /*public async Task<int> ExecuteNonQueryAsync(
            string query,
            CommandType commandType,
            SqlParameter[]? parameters = null,
            SqlConnection? connection = null,
            SqlTransaction? transaction = null)
        {

            bool disposeConnection = connection == null;

            connection ??= await GetOpenConnectionAsync();

            try
            {
                using SqlCommand cmd = new SqlCommand(query, connection, transaction);

                cmd.CommandType = commandType;

                if (parameters != null && parameters.Length > 0)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                return await cmd.ExecuteNonQueryAsync();
            }
            finally
            {
                if (disposeConnection)
                {
                    await connection.DisposeAsync();
                }
            }
        }
        */
        // SELECT
        public async Task<DataTable> ExecuteQueryAsync(
            string query,
            CommandType commandType,
            SqlParameter[]? parameters = null)
        {
            DataTable dt = new DataTable();

            await using SqlConnection conn = await GetOpenConnectionAsync();

            using SqlCommand cmd = new SqlCommand(query, conn);

            cmd.CommandType = commandType;

            if (parameters != null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            dt.Load(reader);

            return dt;
        }

        // SINGLE VALUE
        public async Task<object?> ExecuteScalarAsync(
            string query,
            CommandType commandType,
            SqlParameter[]? parameters = null)
        {
            await using SqlConnection conn =
                await GetOpenConnectionAsync();

            using SqlCommand cmd =
                new SqlCommand(query, conn);

            cmd.CommandType = commandType;

            if (parameters != null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }

            return await cmd.ExecuteScalarAsync();
        }
    }
}