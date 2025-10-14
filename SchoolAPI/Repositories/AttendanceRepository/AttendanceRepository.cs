using System.Data;
using Microsoft.Data.SqlClient;
using SchoolAPI.Infrastructure.Factory;
using SchoolAPI.Models.Attendance;

namespace SchoolAPI.Repositories.AttendanceRepository
{
    public class AttendanceRepository(IDbConnectionFactory dbConnectionFactory) : IAttendanceRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;


        public async Task<DataTable> GetStudentAttendanceAsync(int schoolId, int sessionId, int classId, int sectionId)
        {

            var dt = new DataTable();

            using var conn = _dbConnectionFactory.CreateConnection();
            await using var cmd = new SqlCommand("SP_Get_Student_attendance", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddRange(new[]
            {

                new SqlParameter("@SchooId", schoolId),
                new SqlParameter("@SessionId", sessionId),
                new SqlParameter("@Class", classId),
                new SqlParameter("@SectionID", sectionId)

            });

            using SqlDataAdapter adapter = new(cmd);
            await conn.OpenAsync();


            adapter.Fill(dt);

            return dt;
        }
        public async Task<DataTable> GetStaffAttendanceAsync(int schoolId)
        {
            var dt = new DataTable();

            await using var conn = _dbConnectionFactory.CreateConnection();
            await using var cmd = new SqlCommand("SP_GetStaffAttendance", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add(new SqlParameter("@SchoolId", schoolId));

            using var adapter = new SqlDataAdapter(cmd);
            await conn.OpenAsync();

            adapter.Fill(dt);

            return dt;
        }
        public async Task<string> InsertStaffAttendanceAsync(DataTable dt)
        {
            string result = string.Empty;

            await using var conn = _dbConnectionFactory.CreateConnection();
            await using var cmd = new SqlCommand("usp_StaffAttendanceInsert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            // Table-valued parameter
            var tvpParam = new SqlParameter("@StaffAttendance", SqlDbType.Structured)
            {
                TypeName = dt.TableName, // Make sure this matches the SQL TVP type name
                Value = dt
            };
            cmd.Parameters.Add(tvpParam);

            // Output parameter
            var outputParam = new SqlParameter("@Msg", SqlDbType.VarChar, 100)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(outputParam);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            result = outputParam.Value.ToString();
            return result;
        }
        public async Task<string> InsertStudentAttendanceAsync(DataTable dt)
        {
            string result = string.Empty;

            await using var conn = _dbConnectionFactory.CreateConnection();
            await using var cmd = new SqlCommand("usp_StudentAttendanceInsert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            // Table-valued parameter
            var tvpParam = new SqlParameter("@StudentAttendance", SqlDbType.Structured)
            {
                TypeName = dt.TableName, // Ensure this matches the SQL TVP type
                Value = dt
            };
            cmd.Parameters.Add(tvpParam);

            // Output parameter
            var outputParam = new SqlParameter("@Msg", SqlDbType.VarChar, 100)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(outputParam);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            result = outputParam.Value.ToString();
            return result;
        }
        public async Task<DataTable> GetAttendanceByStudentIdAsync(AttendanceFilter filter)
        {
            var dt = new DataTable();

            await using var conn = _dbConnectionFactory.CreateConnection();
            await using var cmd = new SqlCommand("GetAttendanceByStd", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddRange(new[]
            {
                new SqlParameter("@StartDate", filter.Sdate),
                new SqlParameter("@EndDate", filter.Edate),
                new SqlParameter("@SchoolId", filter.SchoolId),
                new SqlParameter("@Sessionid", filter.SessionId),
                new SqlParameter("@StudentId", filter.StudentId)
            });

            using var adapter = new SqlDataAdapter(cmd);
            await Task.Run(() => adapter.Fill(dt)); // Fill is synchronous, so wrap in Task.Run for async

            return dt;
        }
        public async Task<DataTable> GetAttendanceByStaffIdAsync(AttendanceFilter filter)
        {
            var dt = new DataTable();

            await using var conn = _dbConnectionFactory.CreateConnection();
            await using var cmd = new SqlCommand("Get_StaffAttendance", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddRange(new[]
            {
                new SqlParameter("@StartDate", filter.Sdate),
                new SqlParameter("@EndDate", filter.Edate),
                new SqlParameter("@SchoolId", filter.SchoolId),
                new SqlParameter("@UserId", filter.StaffId)
            });

            using var adapter = new SqlDataAdapter(cmd);
            await Task.Run(() => adapter.Fill(dt)); // Fill is synchronous, so wrap in Task.Run for async

            return dt;
        }
        public async Task<DataTable> GetAbsentStudentsListAsync(AttendanceFilter filter)
        {
            var dt = new DataTable();

            await using var conn = _dbConnectionFactory.CreateConnection();
            await using var cmd = new SqlCommand("usp_GetAbsentStudnetsList", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddRange(new[]
            {
                new SqlParameter("@StartDate", filter.Sdate),
                new SqlParameter("@EndDate", filter.Edate),
                new SqlParameter("@SchoolId", filter.SchoolId),
                new SqlParameter("@ClassId", filter.ClassId),
                new SqlParameter("@SectionId", filter.Sectionid)
            });

            using var adapter = new SqlDataAdapter(cmd);
            await Task.Run(() => adapter.Fill(dt)); // Fill is synchronous, so wrap in Task.Run for async

            return dt;
        }


    }
}
