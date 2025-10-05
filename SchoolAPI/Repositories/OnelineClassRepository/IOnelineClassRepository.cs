using System.Data;
using SchoolAPI.Models.OnelineClass;

namespace SchoolAPI.Repositories.OnelineClassRepository
{
    public interface IOnelineClassRepository
    {
        Task<DataSet> GetOnlineClassSetupAsync(int schoolId, int sessionId, int staffId, int studentId, string userType);
        Task<bool> AddOnlineClassSetupAsync(OnlineClassSetupRequest onlineClassSetupRequest);
    }
}
