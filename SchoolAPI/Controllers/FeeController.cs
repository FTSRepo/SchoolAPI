using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;
using SchoolAPI.Models.Free;
using SchoolAPI.Services.FeeManagement;

namespace SchoolAPI.Controllers
    {

    [ApiController]
    public class FeeController(IPaymentgatewayService paymentgatewayService, IFeeService feeService) : ControllerBase
        {
        private readonly IPaymentgatewayService _paymentgatewayService = paymentgatewayService;
        private readonly IFeeService _feeService = feeService;
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        [Route("api/BindFeeSumary")]
        [HttpGet]
        public async Task<IActionResult> BindFeeSumary(int schoolId, int studentId, int sessionId)
            {
            List<FeeDetailsResponse> result = await _feeService.GetStudentFeeDetailsByStudentIdAsync(schoolId, studentId, sessionId).ConfigureAwait(false);
            if ( result != null && result.Count > 0 )
                {
                return Ok(new { Status = true, Data = result });
                }
            else
                {
                return Ok(new { Status = false, Data = result });
                }
            }

        [Route("api/BindPaidFeeSumary")]
        [HttpGet]
        public async Task<IActionResult> BindPaidFeeSumary(int schoolId, int studentId, int sessionId)
            {
            SubmitFeeReceiptM result = await _feeService.BindPaidFeeSumaryAsync(schoolId, studentId, sessionId).ConfigureAwait(false);
            if ( result != null )
                {
                return Ok(new { Status = true, Data = result });
                }
            else
                {
                return Ok(new { Status = false, Data = result });
                }
            }

        [Route("api/CalculateFee")]
        [HttpGet]
        public async Task<IActionResult> CalculateFee(int schoolId, int studentId, string monthsIds, int SessionId = 8)
            {
            SubmitFeeReceiptM bindPaidFeeSumaryM = await _feeService.GetStudentsFeeDetailsAsync(schoolId, 0, studentId, monthsIds, SessionId).ConfigureAwait(false);
            if ( bindPaidFeeSumaryM.PaymentDetails.Count > 0 )
                {
                return Ok(new { Status = true, Data = bindPaidFeeSumaryM });
                }
            else
                {
                return Ok(new { Status = false, Data = bindPaidFeeSumaryM });
                }
            }

        [Route("api/CreateOnlineOrder")]
        [HttpPost]
        public async Task<IActionResult> CreateOnlineOrder(OnlineCreateOrderRequest onlineCreateOrderRequest)
            {
            try
                {
                Order dataResult = await _feeService.CreateOnlinePaymentOrderAsync(onlineCreateOrderRequest).ConfigureAwait(false);
                if ( dataResult != null )
                    {
                    return Ok(new { Status = true, Data = dataResult });
                    }
                else
                    {
                    return Ok(new { Status = false, Message = "Order not created please try again" });
                    }
                }
            catch ( Exception Ex )
                {
                return Ok(new { Status = false, Message = Ex.Message.ToLower() });
                }
            }

        [Route("api/GetClientKeySecret")]
        [HttpGet]
        public async Task<IActionResult> GetClientKeySecret(int SchoolId)
            {
            try
                {
                APIGatewayCredentials onlineCreateOrder = new APIGatewayCredentials();
                onlineCreateOrder = await _paymentgatewayService.GetClietKeySecretAsync(SchoolId);
                if ( onlineCreateOrder.Key_Secret != null || onlineCreateOrder.Key_Id != null )
                    {
                    return Ok(new { Status = true, Data = onlineCreateOrder });
                    }
                else
                    {
                    return Ok(new { Status = false, Message = "School is not accepting online payment please submit in school" });
                    }
                }
            catch ( Exception Ex )
                {
                return Ok(new { Status = false, Message = Ex.Message.ToLower() });
                }
            }


        [Route("api/UpdateOnlineOrderStatus")]
        [HttpPost]
        public async Task<IActionResult> UpdateOnlineOrderStatus(OnlineOrderStatusRequest onlineOrderStatus)
            {
            try
                {
                string msg = await _paymentgatewayService.UpdateOnlineOrderStatusAsync(onlineOrderStatus).ConfigureAwait(false);
                if ( msg.Contains("Successfully") )
                    {
                    return Ok(new { Status = true, Message = msg });
                    }
                else
                    {
                    return Ok(new { Status = false, Message = msg });
                    }

                }
            catch ( Exception Ex )
                {
                return Ok(new { Status = false, Message = Ex.Message.ToLower() });
                }
            }

        [Route("api/GetMonthsCollection")]
        [HttpGet]
        public async Task<IActionResult> GetMonthsCollection(int schoolId, int sessionId)
            {

            var result = await _feeService.GetDaywiseFeeDAsync(schoolId, sessionId).ConfigureAwait(false);
            if ( result != null )
                {
                return Ok(new { Status = true, Data = result });
                }
            else
                {
                return Ok(new { Status = false, Data = result });
                }
            }

        [Route("api/GetTodaysCollection")]
        [HttpGet]
        public async Task<IActionResult> GetTodaysCollection(int schoolId, int sessionId)
            {
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            var result = await _feeService.GetTodaysCollectionReportsAsync(schoolId, sessionId, userId: 0, indianTime).ConfigureAwait(false);
            if ( result != null )
                {
                return Ok(new { Status = true, Data = result });
                }
            else
                {
                return Ok(new { Status = false, Data = result });
                }
            }
        [Route("api/GetStudentsPaidFee")]
        [HttpGet]
        public async Task<IActionResult> GetStudentsPaidFee(int schoolId, int sessionId)
            {
            var result = await _feeService.GetStudentsPaidFeeAsync(schoolId, sessionId);
            if ( result != null )
                {
                return Ok(new { Status = true, Data = result });
                }
            else
                {
                return Ok(new { Status = false, Data = result });
                }
            }
        }
    }
