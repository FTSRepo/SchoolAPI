using System.Data;
using Microsoft.Data.SqlClient;
using SchoolAPI.Models.Free;

namespace SchoolAPI.Repositories.FeeRepository
{
    public interface IPaymentgatwayRepository
    {
        Task<DataTable> GetPaymentAPIAsync(int SchoolId);
        Task<string> SubmitOnlineFeeAsync(DataTable dt);
        Task<DataSet> CreateOnlinePaymnetOrderAsync(OnlineCreateOrderRequest onlineCreatedOrderRequest);
        Task<DataTable> GetClietKeySecretAsync(int SchoolId);
        Task<string> UpdateOnlinePaymnetOrderAsync(OnlineUpdateOrderRequest onlineUpdateOrderRequest, int SchoolId, int StudentId);
        Task<string> UpdateOnlineOrderStatusAsync(OnlineOrderStatusRequest onlineOrderStatus);
        Task<DataTable> SaveOnlinePaymentOrderAsync(OnlinePaymentOrderRequest onlinePaymentOrderRequest);
        Task<string> SaveOnlinePaymentAsync(OnlinePaymentRequest payment);
    }
}
