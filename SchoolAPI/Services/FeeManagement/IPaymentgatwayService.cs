using System.Data;
using SchoolAPI.Models.Free;

namespace SchoolAPI.Services.FeeManagement
    {
    public interface IPaymentgatewayService
        {
        Task<DataTable> GetPaymentAPIAsync(int SchoolId);
        Task<string> SubmitOnlineFeeAsync(DataTable dt);
        Task<OnlineCreateOrderResponse> CreateOnlinePaymnetOrderAsync(OnlineCreateOrderRequest onlineCreatedOrderRequest);
        Task<APIGatewayCredentials> GetClietKeySecretAsync(int SchoolId);
        Task<string> UpdateOnlinePaymnetOrderAsync(OnlineUpdateOrderRequest onlineUpdateOrderRequest, int SchoolId, int StudentId);
        Task<string> UpdateOnlineOrderStatusAsync(OnlineOrderStatusRequest onlineOrderStatus);
        Task<OnlinePaymentOrderRequest> SaveOnlinePaymentOrderAsync(OnlinePaymentOrderRequest onlinePaymentOrderRequest);
        Task<string> SaveOnlinePaymentAsync(OnlinePaymentRequest payment);
        }
    }
