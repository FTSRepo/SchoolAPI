using System.Data;
using Microsoft.Data.SqlClient;
using SchoolAPI.Infrastructure.Factory;
using SchoolAPI.Models.Leave;

namespace SchoolAPI.Repositories.LeaveRepository
    {
    public class LeaveRepository(IDbConnectionFactory dbConnectionFactory) : ILeaveRepository
        {
        private readonly IDbConnectionFactory _connectionFactory = dbConnectionFactory;

        public async Task<string> SaveLeaveRequestAsync(LeaveRequest leave)
            {
            using var con = _connectionFactory.CreateConnection();
            using var cmd = new SqlCommand("Sp_ISaveLeaveRequest", con)
                {
                CommandType = CommandType.StoredProcedure
                };

            cmd.Parameters.AddWithValue("@SchoolId", leave.SchoolId);
            cmd.Parameters.AddWithValue("@UserId", leave.UserId);
            cmd.Parameters.AddWithValue("@UserTypeId", leave.UserTypeId);
            cmd.Parameters.AddWithValue("@SessionId", leave.SessionId);
            cmd.Parameters.AddWithValue("@StartDate", leave.StartDate);
            cmd.Parameters.AddWithValue("@EndDate", leave.EndDate);
            cmd.Parameters.AddWithValue("@Remarks", leave.Remarks);

            var msgParam = new SqlParameter("@Msg", SqlDbType.VarChar, 100)
                {
                Direction = ParameterDirection.Output
                };
            cmd.Parameters.Add(msgParam);

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return msgParam.Value?.ToString();
            }

        public async Task<List<LeaveResponse>> GetLeavesByUserIdAsync(int schoolId, int sessionId, int userId, int userTypeId)
            {
            var results = new List<LeaveResponse>();

            using var con = _connectionFactory.CreateConnection();
            using var cmd = new SqlCommand("Sp_SLeaveRequest", con)
                {
                CommandType = CommandType.StoredProcedure
                };

            cmd.Parameters.AddWithValue("@SchoolId", schoolId);
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@UserTypeId", userTypeId);
            cmd.Parameters.AddWithValue("@SessionId", sessionId);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while ( await reader.ReadAsync() )
                {
                results.Add(new LeaveResponse
                    {
                    LeaveId = reader.GetSafeInt("LeaveId"),
                    StartDate = reader.GetSafeString("StartDate"),
                    EndDate = reader.GetSafeString("EndDate"),
                    Status = reader.GetSafeString("Status"),
                    Remarks = reader.GetSafeString("Remarks")

                    });
                }

            return results;
            }

        public async Task<string> DeleteLeaveByLeaveIdAsync(int leaveId)
            {
            using var con = _connectionFactory.CreateConnection();
            using var cmd = new SqlCommand("Sp_DLeaveRequest", con)
                {
                CommandType = CommandType.StoredProcedure
                };

            cmd.Parameters.AddWithValue("@LeaveId", leaveId);

            var msgParam = new SqlParameter("@Msg", SqlDbType.VarChar, 100)
                {
                Direction = ParameterDirection.Output
                };
            cmd.Parameters.Add(msgParam);

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return msgParam.Value?.ToString();
            }

        public async Task<bool> UpdateLeaveApprovalStatusAsync(LeaveUpdateRequest objM)
            {
            using var con = _connectionFactory.CreateConnection();
            using var cmd = new SqlCommand("Usp_UpdateLeaveAprovelStatus", con)
                {
                CommandType = CommandType.StoredProcedure
                };

            cmd.Parameters.AddWithValue("@SchoolId", objM.SchoolId);
            cmd.Parameters.AddWithValue("@LeaveApId", objM.LeaveApId);
            cmd.Parameters.AddWithValue("@ApprovalRemark", objM.ApprovalRemark);
            cmd.Parameters.AddWithValue("@ApprovalStatus", objM.ApprovalStatus);
            cmd.Parameters.AddWithValue("@UserId", objM.UserId);

            await con.OpenAsync();
            var rows = await cmd.ExecuteNonQueryAsync();

            return rows > 0;
            }
        }
    }
