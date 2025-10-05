using Microsoft.Data.SqlClient;
using SchoolAPI.Infrastructure.Factory;
using SchoolAPI.Models.Homework;
using System.Data;

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

    }
}
