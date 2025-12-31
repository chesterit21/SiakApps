using System.Data;
using Npgsql;

namespace SiakWebApps.DataAccess
{
    public class BaseRepository
    {
        private readonly string _connectionString;

        public BaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}