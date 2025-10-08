using Microsoft.Data.SqlClient;
using SchoolAPI.Infrastructure.Factory;
using SchoolAPI.Models.Message;
using System.Data;

namespace SchoolAPI.Repositories.MessageRepository
{
    public class MessageRepository(IDbConnectionFactory dbConnectionFactory) : IMessageRepository
    {
        private readonly IDbConnectionFactory _connectionFactory = dbConnectionFactory;
        public async Task<List<MessageResponse>> GetMessageAsync(int schoolId, int studentId)
        {
            List<MessageResponse> messages = [];

            using var con = _connectionFactory.CreateConnection();
            using var cmd = new SqlCommand("usp_GetSMSLogs", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@SchoolId", schoolId);
            cmd.Parameters.AddWithValue("@StudentId", studentId);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                MessageResponse response = new MessageResponse
                {
                    SchoolId = reader["SchoolId"] != DBNull.Value ? Convert.ToInt32(reader["SchoolId"]) : 0,
                    MessageTxt = reader["messageTxt"]?.ToString(),
                    Date = reader["date"] != DBNull.Value ? Convert.ToDateTime(reader["date"]) : DateTime.MinValue,
                };
                messages.Add(response);
            }
            return messages;
        }

        public async Task<List<NewsResponse>> GetNewsAsync(int schoolId)
        { 
            List<NewsResponse> newsResponses = [];

            using var con = _connectionFactory.CreateConnection();
            using var cmd = new SqlCommand("usp_GetNews", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@SchoolId", schoolId);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                NewsResponse response = new NewsResponse
                {
                    Title = reader["Title"] != DBNull.Value ? Convert.ToString(reader["Title"]) : "",
                    Description = reader["Description"]?.ToString(),
                    NewsOrEventDate = reader["NewsOrEventDate"]?.ToString(),
                    CreatedOn = reader["CreatedOn"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedOn"]) : DateTime.MinValue,
                };
                newsResponses.Add(response);
            }
            return newsResponses;            
        }

        public async Task<List<NewsResponse>> GetEventsAsync(int schoolId)
        {
            List<NewsResponse> newsResponses = [];

            using var con = _connectionFactory.CreateConnection();
            using var cmd = new SqlCommand("usp_GetEvents", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@SchoolId", schoolId);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (reader.Read())
            {
                NewsResponse response = new NewsResponse
                {
                    Title = reader["Title"] != DBNull.Value ? Convert.ToString(reader["Title"]) : "",
                    Description = reader["Description"]?.ToString(),
                    NewsOrEventDate = reader["NewsOrEventDate"]?.ToString(),
                    CreatedOn = reader["CreatedOn"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedOn"]) : DateTime.MinValue,
                };
                newsResponses.Add(response);
            }
            return newsResponses;
        }
    }
}
