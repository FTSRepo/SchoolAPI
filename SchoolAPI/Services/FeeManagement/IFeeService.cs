using Razorpay.Api;
using SchoolAPI.Models.Free;
using System.Data;

namespace SchoolAPI.Services.FeeManagement
{
    public interface IFeeService
    {
        Task<string> SaveOnlinePaymentAsync(OnlinePaymentRequest payment);
        Task<DataTable> SaveOnlinePaymentOrderAsync(OnlinePaymentOrderRequest onlinePaymentOrderRequest);
        Task<string> UpdateOnlineOrderStatusAsync(OnlineOrderStatusRequest onlineOrderStatus);
        Task<string> UpdateOnlinePaymentOrderAsync(OnlineUpdateOrderRequest onlineUpdateOrderRequest, int schoolId, int studentId);
        Task<SubmitFeeReceiptM> GetStudentsFeeDetailsAsync(int schoolId, int classId, int studentId, string monthIds, int sessionId);
        Task<SubmitFeeReceiptM> BindPaidFeeSumaryAsync(int schoolId, int studentId, int sessionId);
        Task<List<FeeDetailsResponse>> GetStudentFeeDetailsByStudentIdAsync(int schoolId, int studentId, int sessionId);
        Task<Order?> CreateOnlinePaymentOrderAsync(OnlineCreateOrderRequest request);
        Task<List<TodaysCollectionDto>> GetDaywiseFeeDAsync(int SchoolId, int SessionId);
        Task<List<TodaysCollectionReportDto>> GetTodaysCollectionReportsAsync(int SchoolId, int SessionId, int userId, DateTime currentDate);
        Task<List<DaywiseCollectionDto>> GetStudentsPaidFeeAsync(int SchoolId, int SessionId);
    }
}
