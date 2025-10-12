using Microsoft.Data.SqlClient;

namespace SchoolAPI.Infrastructure.Factory
    {
    public class DbConnectionFactory : IDbConnectionFactory
        {
        private readonly string _connectionString;

        public DbConnectionFactory(IConfiguration configuration)
            {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            }

        public SqlConnection CreateConnection()
            {
            return new SqlConnection(_connectionString);
            }
        }
    }
