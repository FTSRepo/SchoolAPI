using Microsoft.Data.SqlClient;

namespace SchoolAPI.Infrastructure.Factory
{
    public interface IDbConnectionFactory
    {
        SqlConnection CreateConnection();
    }
}
