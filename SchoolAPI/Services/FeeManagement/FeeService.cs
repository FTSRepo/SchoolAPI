using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Razorpay.Api;
using SchoolAPI.Infrastructure.Factory;
using SchoolAPI.Models.Auth;
using SchoolAPI.Models.Free;
using SchoolAPI.Repositories.FeeRepository;
using System.Data;
using System.Net;

namespace SchoolAPI.Services.FeeManagement
{
    public class FeeService(IFeeRepository feeRepository, IPaymentgatewayService paymentgatewayService) : IFeeService
    {
        private readonly IFeeRepository _feeRepository = feeRepository;
        private readonly IPaymentgatewayService _paymentgatwyService = paymentgatewayService;
        public async Task<List<FeeDetailsResponse>> GetStudentFeeDetailsByStudentIdAsync(int schoolId, int studentId, int sessionId)
        {
            List<FeeDetailsResponse> feeDetailsResponse = [];
            DataTable ds = await _feeRepository.GetStudentFeeDetailsByStudentIdAsync(schoolId, studentId, sessionId).ConfigureAwait(false);
            foreach (DataRow dr in ds.Rows)
            {
                feeDetailsResponse.Add(new FeeDetailsResponse()
                {
                    Amount = Convert.ToInt32(dr["Amount"]),
                    BalanceAmount = Convert.ToInt32(dr["BalanceAmount"]),
                    BillNo = Convert.ToString(dr["BillNo"]),
                    PaidDate = Convert.ToString(dr["PaidDate"]),
                    PaymentMonth = Convert.ToString(dr["PaymentMonth"]),
                    PayMode = Convert.ToString(dr["PayMode"])
                });
            }
            return feeDetailsResponse;
        }
        public async Task<SubmitFeeReceiptM> BindPaidFeeSumaryAsync(int schoolId, int studentId, int sessionId)
        {
            SubmitFeeReceiptM bindPaidFeeSumaryM = new();
            DataSet ds = await _feeRepository.BindPaidFeeSumaryAsync(schoolId, studentId, sessionId).ConfigureAwait(false);
            if (ds.Tables[0].Rows.Count > 0)
            {
                bindPaidFeeSumaryM.AdmNo = Convert.ToString(ds.Tables[0].Rows[0]["AdmNo"]);
                bindPaidFeeSumaryM.Class = Convert.ToString(ds.Tables[0].Rows[0]["CLASSNAME"]);
                bindPaidFeeSumaryM.Name = Convert.ToString(ds.Tables[0].Rows[0]["STUDENTNAME"]);
                bindPaidFeeSumaryM.Father = Convert.ToString(ds.Tables[0].Rows[0]["FATHERNAME"]);
                bindPaidFeeSumaryM.Mother = Convert.ToString(ds.Tables[0].Rows[0]["MOTHERNAME"]);
                bindPaidFeeSumaryM.ContactNo = Convert.ToString(ds.Tables[0].Rows[0]["CONTACTNO"]);
                bindPaidFeeSumaryM.PaymentDetails = new List<PaymentDetails>();
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    PaymentDetails paymentDetails = new PaymentDetails();
                    paymentDetails.BillNo = Convert.ToString(dr["Billno"]);
                    paymentDetails.PayMonth = Convert.ToString(dr["PaymentMonth"]);
                    paymentDetails.PayAmount = Convert.ToDecimal(dr["Amount"]);
                    paymentDetails.PrvBalance = Convert.ToDecimal(dr["previous"]);
                    paymentDetails.Discount = Convert.ToDecimal(dr["Discount"]);
                    paymentDetails.Concession = Convert.ToDecimal(dr["Concession"]);
                    paymentDetails.PaidAmt = Convert.ToDecimal(dr["PaidAmount"]);
                    paymentDetails.CurentBal = Convert.ToDecimal(dr["BalanceAmount"]);
                    paymentDetails.PayMode = Convert.ToString(dr["Paymode"]);
                    paymentDetails.PmtDate = Convert.ToString(dr["adddate"]);
                    paymentDetails.MonthId = Convert.ToString(dr["months"]);
                    bindPaidFeeSumaryM.PaymentDetails.Add(paymentDetails);
                }

                bindPaidFeeSumaryM.MonthNames = new List<MonthName>();
                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    MonthName month = new MonthName();
                    month.Id = Convert.ToInt32(dr["Id"]);
                    month.Name = Convert.ToString(dr["Name"]);
                    month.Submit = Convert.ToInt16(dr["Submit"]);
                    bindPaidFeeSumaryM.MonthNames.Add(month);
                }

                bindPaidFeeSumaryM.oldBalance = new OldBalance();
                foreach (DataRow dr in ds.Tables[3].Rows)
                {
                    OldBalance oldBalance = new OldBalance();
                    oldBalance.FeeHeadId = Convert.ToInt32(dr["FeeHeadId"]);
                    oldBalance.FeeHeadName = Convert.ToString(dr["FeeHeadName"]);
                    oldBalance.FeeAmount = Convert.ToInt32(dr["FeeAmount"]);
                    oldBalance.MonthId = Convert.ToString(dr["PaymentMonth"]);
                    bindPaidFeeSumaryM.oldBalance = oldBalance;
                }
            }
            return bindPaidFeeSumaryM;
        }
        public async Task<SubmitFeeReceiptM> GetStudentsFeeDetailsAsync(int schoolId, int classId, int studentId, string monthIds, int sessionId)
        {
            SubmitFeeReceiptM bindPaidFeeSumaryM = new()
            {
                submitPaymnets = []
            };
            DataSet ds = await _feeRepository.GetStudentsFeeDetailsAsync(schoolId, 0, studentId, monthIds, sessionId).ConfigureAwait(false);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                SubmitPaymnet submitPaymnet = new()
                {
                    FeeHeadMasterId = Convert.ToInt32(dr["FeeHeadMasterId"].ToString()),
                    FeeHeadName = dr["Name"].ToString(),
                    Amount = Convert.ToDecimal(dr["FeeAmount"]),
                    Concession = Convert.ToDecimal(dr["Concession"]),
                    Disscount = Convert.ToDecimal(dr["Discount"]),
                    Payable = Convert.ToDecimal(dr["FinalAmt"]),
                    Balance = Convert.ToDecimal(dr["Balance"])
                };
                bindPaidFeeSumaryM.submitPaymnets.Add(submitPaymnet);
            }
            return bindPaidFeeSumaryM;
        }
        public async Task<Order?> CreateOnlinePaymentOrderAsync(OnlineCreateOrderRequest request)
        {
            // Call gateway service
            var createResponse = await _paymentgatwyService
                .CreateOnlinePaymnetOrderAsync(request)
                .ConfigureAwait(false);

            // Validate credentials & receipt
            if (string.IsNullOrWhiteSpace(createResponse?.ReceiptNo) ||
                string.IsNullOrWhiteSpace(createResponse?.gatewayCredentials?.Key_Id))
            {
                return null;
            }

            // Configure TLS (done once globally, ideally in Startup.cs)
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // Create Razorpay client
            var client = new RazorpayClient(
                createResponse.gatewayCredentials.Key_Id,
                createResponse.gatewayCredentials.Key_Secret
            );

            // Prepare order options
            var options = new Dictionary<string, object>
            {
                ["amount"] = createResponse.Amount * 100, // Razorpay expects amount in paise
                ["receipt"] = createResponse.ReceiptNo,
                ["currency"] = createResponse.Currency
            };

            // Create order
            var order = client.Order.Create(options);

            // Map response attributes to strongly typed DTO
            var updateRequest = MapOrderAttributes(order.Attributes);

            // Save in DB
            var status = await _feeRepository
                .UpdateOnlinePaymentOrderAsync(updateRequest, request.SchoolId, request.StudentId)
                .ConfigureAwait(false);

            return status.Contains("Success", StringComparison.OrdinalIgnoreCase) ? order : null;
        }
        public async Task<string> SaveOnlinePaymentAsync(OnlinePaymentRequest payment)
        {
            return await _feeRepository.SaveOnlinePaymentAsync(payment).ConfigureAwait(false);
        }
        public async Task<DataTable> SaveOnlinePaymentOrderAsync(OnlinePaymentOrderRequest onlinePaymentOrderRequest)
        {
            return await _feeRepository.SaveOnlinePaymentOrderAsync(onlinePaymentOrderRequest).ConfigureAwait(false);
        }
        public async Task<string> UpdateOnlineOrderStatusAsync(OnlineOrderStatusRequest onlineOrderStatus)
        {
            return await _feeRepository.UpdateOnlineOrderStatusAsync(onlineOrderStatus).ConfigureAwait(false);
        }
        public async Task<string> UpdateOnlinePaymentOrderAsync(OnlineUpdateOrderRequest onlineUpdateOrderRequest, int schoolId, int studentId)
        {
            return await _feeRepository.UpdateOnlinePaymentOrderAsync(onlineUpdateOrderRequest, schoolId, studentId).ConfigureAwait(false);
        }
        public async Task<List<TodaysCollectionDto>> GetDaywiseFeeDAsync(int SchoolId, int SessionId)
        {
            var result = await _feeRepository.GetDaywiseFeeDAsync(SchoolId, SessionId).ConfigureAwait(false);
            List<TodaysCollectionDto> todaysCollectionDtos = [];
            foreach (DataRow today in result.Rows)
            {
                todaysCollectionDtos.Add(new TodaysCollectionDto
                {
                    AddDate = Convert.ToString(today["AddDate"]),
                    Amount = Convert.ToInt32(today["Amount"]),
                    Name = Convert.ToString(today["Name"])
                });
            }
            return todaysCollectionDtos;
        }
        public async Task<List<TodaysCollectionReportDto>> GetTodaysCollectionReportsAsync(int SchoolId, int SessionId, int userId, DateTime currentDate)
        {
            var result = await _feeRepository.GetTodaysCollectionReportsAsync(SchoolId, SessionId, userId, currentDate).ConfigureAwait(false);
            List<TodaysCollectionReportDto> todaysCollectionReportDtos = [];
            foreach (DataRow today in result.Rows)
            {
                todaysCollectionReportDtos.Add(new TodaysCollectionReportDto
                {
                    AddedBy = Convert.ToString(today["AddedBy"]),
                    AdmissionNumber = Convert.ToString(today["AdmissionNumber"]),
                    BalanceAmount = Convert.ToInt32(today["BalanceAmount"]),
                    Class = Convert.ToString(today["Class"]),
                    Concession = Convert.ToInt32(today["Concession"]),
                    Discount = Convert.ToInt32(today["Discount"]),
                    PaidAmount = Convert.ToInt32(today["PaidAmount"]),
                    RollNo = Convert.ToInt32(today["RollNo"]),
                    StudentId = Convert.ToInt32(today["StudentId"]),
                    FatherName = Convert.ToString(today["FatherName"]),
                    MobileNumber = Convert.ToString(today["MobileNumber"]),
                    TotalFeeAmount = Convert.ToInt32(today["TotalFeeAmount"]),
                    TranAmt = Convert.ToInt32(today["TranAmt"]),
                    IsActive = Convert.ToBoolean(today["IsActive"]),
                    PaymentMonth = Convert.ToString(today["PaymentMonth"]),
                    BillNo = Convert.ToString(today["BillNo"]),
                    Name = Convert.ToString(today["Name"])
                });
            }
            return todaysCollectionReportDtos;
        }
        public async Task<List<DaywiseCollectionDto>> GetStudentsPaidFeeAsync(int SchoolId, int SessionId)
        {
            var result = await _feeRepository.GetStudentsPaidFeeAsync(SchoolId, SessionId).ConfigureAwait(false);
            List<DaywiseCollectionDto> daywiseCollectionDtos = [];
            foreach (DataRow daywise in result.Rows)
            {
                daywiseCollectionDtos.Add(new DaywiseCollectionDto
                {
                    Bank  = Convert.ToDecimal(daywise["Bank"]),
                    Amount = Convert.ToInt32(daywise["Amount"]),
                    Cash = Convert.ToDecimal(daywise["Cash"]),
                    TranDate = Convert.ToString(daywise["TranDate"])
                });
            }
            
            return daywiseCollectionDtos;
        }
        private static OnlineUpdateOrderRequest MapOrderAttributes(Dictionary<string, object> attributes)
        {
            return new OnlineUpdateOrderRequest
            {
                id = attributes["id"]?.ToString(),
                entity = attributes["entity"]?.ToString(),
                amount = Convert.ToInt32(attributes["amount"]),
                amount_paid = Convert.ToInt32(attributes["amount_paid"]),
                amount_due = Convert.ToInt32(attributes["amount_due"]),
                currency = attributes["currency"]?.ToString(),
                receipt = attributes["receipt"]?.ToString(),
                status = attributes["status"]?.ToString(),
                attempts = Convert.ToInt32(attributes["attempts"]),
                created_at = attributes["created_at"]?.ToString()
            };
        }
    }
}
