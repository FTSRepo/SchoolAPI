using Azure.Core;
using Microsoft.Data.SqlClient;
using SchoolAPI.Infrastructure.Factory;
using SchoolAPI.Models.Homework;
using System.Data;
using System.Data.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SchoolAPI.Repositories.HomeworkRepository
{
    public class HomeworkRepository(IDbConnectionFactory dbConnectionFactory) : IHomeworkRepository
    {
        private readonly IDbConnectionFactory _connectionFactory = dbConnectionFactory;
        public async Task<bool> AssignHomeworkAsync(HomeWorkMasterM objHomeWork, DataTable homeWorkTable)
        {
            using var con = _connectionFactory.CreateConnection();
            using var cmd = new SqlCommand("Sp_SaveHomeWork", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Details", homeWorkTable);
            cmd.Parameters.AddWithValue("@SchoolId", objHomeWork.SchoolId);
            cmd.Parameters.AddWithValue("@StaffID", objHomeWork.StaffId);
            cmd.Parameters.AddWithValue("@Classid", objHomeWork.ClassId);
            cmd.Parameters.AddWithValue("@SectionId", objHomeWork.SectionId);
            cmd.Parameters.AddWithValue("@Subjectid", objHomeWork.SubjectId);
            cmd.Parameters.AddWithValue("@SubDate", objHomeWork.LastSubmissionDate);
            cmd.Parameters.AddWithValue("@SessionId", objHomeWork.SessionId);

            await con.OpenAsync();
            int rows = await cmd.ExecuteNonQueryAsync();

            return rows > 0;
        }
        public async Task<DataSet> GetHomeWorksAppAsync(int schoolId, int sessionId, int classId = 0, int sectionId = 0, int studentId = 0, int staffId = 0)
        {
            using var con = _connectionFactory.CreateConnection();
            using var cmd = new SqlCommand("Sp_GetHomework_app", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@SchoolId", schoolId);
            cmd.Parameters.AddWithValue("@SessionId", sessionId);
            cmd.Parameters.AddWithValue("@ClassId", classId);
            cmd.Parameters.AddWithValue("@SectionId", sectionId);
            cmd.Parameters.AddWithValue("@StudentId", studentId);
            cmd.Parameters.AddWithValue("@StaffId", staffId);

            await con.OpenAsync();
            var ds = new DataSet();
            using var adapter = new SqlDataAdapter(cmd);
            await Task.Run(() => adapter.Fill(ds));

            return ds;
        }

        public async Task<int> SveHomeWorkAsync(HomeworkUploadRequest request, string fileUrl)
        {
            SqlConnection _connection = _connectionFactory.CreateConnection();

            using var con = _connectionFactory.CreateConnection();
            using var cmd = new SqlCommand(@"INSERT INTO Homework (SchoolId, ClassName, SectionName, SubjectName, HomeworkDate, FileUrl, FileName, Description)
                                            OUTPUT INSERTED.Id 
                                            VALUES (@SchoolId, @ClassName, @SectionName, @SubjectName, @HomeworkDate, @FileUrl, @FileName, @Description)", con);

            cmd.Parameters.AddWithValue("@SchoolId", request.SchoolId);
            cmd.Parameters.AddWithValue("@ClassName", request.ClassName);
            cmd.Parameters.AddWithValue("@SectionName", request.SectionName);
            cmd.Parameters.AddWithValue("@SubjectName", request.SubjectName);
            cmd.Parameters.AddWithValue("@HomeworkDate", request.HomeworkDate);
            cmd.Parameters.AddWithValue("@FileUrl", fileUrl);
            cmd.Parameters.AddWithValue("@FileName", request.File.FileName);
            cmd.Parameters.AddWithValue("@Description", request.Description ?? (object)DBNull.Value);
            await con.OpenAsync();
            return (int)await cmd.ExecuteScalarAsync();
        }
        public async Task<List<HomeworkResponse>> GetHomeworkAsync(int schoolId, string className, string sectionName)
        {
            string query = @"
            SELECT top 15 Id, ClassName, SectionName, SubjectName, HomeworkDate, FileUrl, FileName, Description
            FROM Homework
            WHERE SchoolId = @SchoolId AND ClassName = @ClassName AND SectionName = @SectionName
            ORDER BY HomeworkDate DESC"
           ;
            using var _connection = _connectionFactory.CreateConnection();
            var list = new List<HomeworkResponse>();

            using (var cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@SchoolId", schoolId);
                cmd.Parameters.AddWithValue("@ClassName", className);
                cmd.Parameters.AddWithValue("@SectionName", sectionName);

                await _connection.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    list.Add(new HomeworkResponse
                    {
                        Id = (int)reader["Id"],
                        ClassName = reader["ClassName"].ToString(),
                        SectionName = reader["SectionName"].ToString(),
                        SubjectName = reader["SubjectName"].ToString(),
                        HomeworkDate = Convert.ToDateTime(reader["HomeworkDate"]),
                        FileUrl = reader["FileUrl"].ToString(),
                        FileName = reader["FileName"].ToString(),
                        Description = reader["Description"]?.ToString()
                    });
                }
                await _connection.CloseAsync();
            }

            return list;
        }
    }
}
