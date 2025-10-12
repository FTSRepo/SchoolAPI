using System.Data;
using SchoolAPI.Models.Free;
using SchoolAPI.Repositories.FeeRepository;

namespace SchoolAPI.Services.FeeManagement
    {
    public class PaymentgatwayService(IPaymentgatwayRepository paymentgatwayRepository) : IPaymentgatewayService
        {
        private readonly IPaymentgatwayRepository _paymentgatwayRepository = paymentgatwayRepository;
        public async Task<OnlineCreateOrderResponse> CreateOnlinePaymnetOrderAsync(OnlineCreateOrderRequest onlineCreatedOrderRequest)
            {
            OnlineCreateOrderResponse onlineCreateOrderResponse = new();
            DataSet ds = await _paymentgatwayRepository.CreateOnlinePaymnetOrderAsync(onlineCreatedOrderRequest).ConfigureAwait(false);

            if ( ds != null )
                {
                if ( ds.Tables [0].Rows.Count > 0 )
                    {
                    foreach ( DataRow dr in ds.Tables [0].Rows )
                        {
                        APIGatewayCredentials aPIGatewayCredentials = new APIGatewayCredentials();
                        aPIGatewayCredentials.Key_Id = Convert.ToString(dr ["Key_Id"]);
                        aPIGatewayCredentials.Key_Secret = Convert.ToString(dr ["Key_Secret"]);
                        onlineCreateOrderResponse.gatewayCredentials = aPIGatewayCredentials;
                        }
                    }
                if ( ds.Tables [1].Rows.Count > 0 )
                    {
                    foreach ( DataRow dr in ds.Tables [1].Rows )
                        {
                        onlineCreateOrderResponse.Currency = Convert.ToString(dr ["Currency"]);
                        onlineCreateOrderResponse.Amount = Convert.ToInt32(dr ["Amount"]);
                        onlineCreateOrderResponse.ReceiptNo = Convert.ToString(dr ["ReceiptNo"]);
                        }
                    }
                }
            return onlineCreateOrderResponse;
            }

        public async Task<APIGatewayCredentials> GetClietKeySecretAsync(int SchoolId)
            {
            APIGatewayCredentials aPIGatewayCredentials = new APIGatewayCredentials();
            DataTable ds = await _paymentgatwayRepository.GetClietKeySecretAsync(SchoolId).ConfigureAwait(false);
            if ( ds != null )
                {
                if ( ds.Rows.Count > 0 )
                    {
                    foreach ( DataRow dr in ds.Rows )
                        {
                        aPIGatewayCredentials.Key_Id = Convert.ToString(dr ["Key_Id"]);
                        aPIGatewayCredentials.Key_Secret = Convert.ToString(dr ["Key_Secret"]);
                        }
                    }
                }
            return aPIGatewayCredentials;
            }

        public async Task<string> SubmitOnlineFeeAsync(DataTable dt)
            {
            return await _paymentgatwayRepository.SubmitOnlineFeeAsync(dt).ConfigureAwait(false);
            }
        public async Task<DataTable> GetPaymentAPIAsync(int SchoolId)
            {
            return await _paymentgatwayRepository.GetPaymentAPIAsync(SchoolId).ConfigureAwait(false);
            }
        public async Task<string> UpdateOnlinePaymnetOrderAsync(OnlineUpdateOrderRequest onlineUpdateOrderRequest, int SchoolId, int StudentId)
            {
            return await _paymentgatwayRepository.UpdateOnlinePaymnetOrderAsync(onlineUpdateOrderRequest, SchoolId, StudentId).ConfigureAwait(false);
            }
        public async Task<string> UpdateOnlineOrderStatusAsync(OnlineOrderStatusRequest onlineOrderStatus)
            {
            return await _paymentgatwayRepository.UpdateOnlineOrderStatusAsync(onlineOrderStatus).ConfigureAwait(false);
            }
        public async Task<OnlinePaymentOrderRequest> SaveOnlinePaymentOrderAsync(OnlinePaymentOrderRequest onlinePaymentOrderRequest)
            {
            DataTable dt = await _paymentgatwayRepository.SaveOnlinePaymentOrderAsync(onlinePaymentOrderRequest).ConfigureAwait(false);
            OnlinePaymentOrderRequest onlinePaymentOrder = new();
            if (
                dt != null && dt.Rows.Count > 0 )
                {
                onlinePaymentOrder.SchoolId = Convert.ToInt32(dt.Rows [0] ["SchoolId"]);
                onlinePaymentOrder.StudentId = Convert.ToInt32(dt.Rows [0] ["StudentId"]);
                onlinePaymentOrder.RegistrationNumber = Convert.ToString(dt.Rows [0] ["RegistrationNumber"]);
                onlinePaymentOrder.OrderId = Convert.ToString(dt.Rows [0] ["OrderId"]);
                onlinePaymentOrder.OrderAmount = Convert.ToInt32(dt.Rows [0] ["OrderAmount"]);
                onlinePaymentOrder.OrderStatus = Convert.ToString(dt.Rows [0] ["OrderStatus"]);
                onlinePaymentOrder.OrderRemark = Convert.ToString(dt.Rows [0] ["OrderRemark"]);
                onlinePaymentOrder.PaymentId = Convert.ToString(dt.Rows [0] ["PaymentId"]);
                onlinePaymentOrder.CreatedOn = Convert.ToDateTime(dt.Rows [0] ["CreatedOn"]);
                onlinePaymentOrder.ModifiedOn = Convert.ToDateTime(dt.Rows [0] ["ModifiedOn"]);
                }
            return onlinePaymentOrder;
            }
        public async Task<string> SaveOnlinePaymentAsync(OnlinePaymentRequest payment)
            {
            return await _paymentgatwayRepository.SaveOnlinePaymentAsync(payment).ConfigureAwait(false);
            }
        public void Dispose()
            {
            GC.SuppressFinalize(this);
            }
        }
    }
