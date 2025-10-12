using SchoolAPI.Models.OnelineClass;

namespace SchoolAPI.Services.OnelineClassService
    {
    public interface IOnelineClassService
        {
        Task<List<OnlineClassSetupResponse>> GetOnlineClassSetupAsync(int schoolId, int sessionId, int staffId, int studentId, string userType);
        Task<bool> AddOnlineClassSetupAsync(OnlineClassSetupRequest onlineClassSetupRequest);
        }
    }
