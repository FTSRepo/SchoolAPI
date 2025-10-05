using System.Data;
using SchoolAPI.Models.Leave;

namespace SchoolAPI.Repositories.LeaveRepository
{
    public interface ILeaveRepository
    {
        public Task<string> SaveLeaveRequestAsync(LeaveRequest leaveRequest);
        public Task<List<LeaveResponse>> GetLeavesByUserIdAsync(int SchoolId, int SessionId, int userId, int UserTypeId);
        public Task<string> DeleteLeaveByLeaveIdAsync(int LeaveId);
        public Task<bool> UpdateLeaveApprovalStatusAsync(LeaveUpdateRequest leaveUpdateRequest);
    }
}
