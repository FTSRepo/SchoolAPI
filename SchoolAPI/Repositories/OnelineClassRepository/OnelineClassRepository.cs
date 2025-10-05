using Microsoft.Data.SqlClient;
using SchoolAPI.Infrastructure.Factory;
using SchoolAPI.Models.OnelineClass;
using System.Data;

namespace SchoolAPI.Repositories.OnelineClassRepository
{
    public class OnelineClassRepository(IDbConnectionFactory dbConnectionFactory) : IOnelineClassRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;
        public async Task<DataSet> GetOnlineClassSetupAsync(int schoolId, int sessionId, int staffId, int studentId, string userType)
        {
            using var con = _dbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand("usp_Get_OnlineClassSetup", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@SchoolId", schoolId);
            cmd.Parameters.AddWithValue("@SessionId", sessionId);
            cmd.Parameters.AddWithValue("@studentId", studentId);
            cmd.Parameters.AddWithValue("@StaffId", staffId);
            cmd.Parameters.AddWithValue("@userType", userType);

            await con.OpenAsync();

            var ds = new DataSet();
            using var adapter = new SqlDataAdapter(cmd);

            // SqlDataAdapter.Fill is sync, so wrap in Task.Run
            await Task.Run(() => adapter.Fill(ds));

            return ds;
        }
        public async Task<bool> AddOnlineClassSetupAsync(OnlineClassSetupRequest onlineClassSetupRequest)
        {
            var startTime = onlineClassSetupRequest.starttime.TimeOfDay;
            var endTime = onlineClassSetupRequest.endtime.TimeOfDay;
            using var con = _dbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand("USP_SaveOnlineClass", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@SchoolId", onlineClassSetupRequest.schoolId != 0 ? onlineClassSetupRequest.schoolId : (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@SessionId", onlineClassSetupRequest.SessionId != 0 ? onlineClassSetupRequest.SessionId : (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@classId", onlineClassSetupRequest.classId);
            cmd.Parameters.AddWithValue("@sectionId", onlineClassSetupRequest.sectionId);
            cmd.Parameters.AddWithValue("@staffId", onlineClassSetupRequest.staffId);
            cmd.Parameters.AddWithValue("@subjectId", onlineClassSetupRequest.subjectId);
            cmd.Parameters.AddWithValue("@SessionDate", onlineClassSetupRequest.Sessiondate);
            cmd.Parameters.AddWithValue("@starttime", startTime);
            cmd.Parameters.AddWithValue("@endtime", endTime);
            cmd.Parameters.AddWithValue("@meetingLink", onlineClassSetupRequest.meetingLink);
            await con.OpenAsync();
            int rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }
    }
}