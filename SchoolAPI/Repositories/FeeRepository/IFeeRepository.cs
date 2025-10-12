using System.Data;
using SchoolAPI.Models.Free;

namespace SchoolAPI.Repositories.FeeRepository
    {
    public interface IFeeRepository
        {
        Task<string> SaveOnlinePaymentAsync(OnlinePaymentRequest payment);
        Task<DataTable> SaveOnlinePaymentOrderAsync(OnlinePaymentOrderRequest onlinePaymentOrderRequest);
        Task<string> UpdateOnlineOrderStatusAsync(OnlineOrderStatusRequest onlineOrderStatus);
        Task<string> UpdateOnlinePaymentOrderAsync(OnlineUpdateOrderRequest onlineUpdateOrderRequest, int schoolId, int studentId);
        Task<DataSet> CreateOnlinePaymentOrderAsync(OnlineCreateOrderRequest onlineCreateOrderRequest);
        Task<DataSet> GetStudentsFeeDetailsAsync(int schoolId, int classId, int studentId, string monthIds, int sessionId);
        Task<DataSet> BindPaidFeeSumaryAsync(int schoolId, int studentId, int sessionId);
        Task<DataTable> GetStudentFeeDetailsByStudentIdAsync(int schoolId, int studentId, int sessionId);
        Task<DataTable> GetDaywiseFeeDAsync(int SchoolId, int SessionId);
        Task<DataTable> GetTodaysCollectionReportsAsync(int SchoolId, int SessionId, int userId, DateTime currentDate);
        Task<DataTable> GetStudentsPaidFeeAsync(int SchoolId, int SessionId);
        }
    }
