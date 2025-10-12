using System.Data;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchoolAPI.Models.Common;
using SchoolAPI.Models.Free;
using SchoolAPI.Services.CommonService;
using SchoolAPI.Services.FeeManagement;

[ApiController]
public class CommonController : ControllerBase
    {
    private readonly ICommonService _commns;
    private readonly IPaymentgatewayService _paymentgatewayService;
    private static readonly HttpClient httpClient = new HttpClient();

    public CommonController(ICommonService commns, IPaymentgatewayService paymentgatewayService)
        {
        _commns = commns;
        _paymentgatewayService = paymentgatewayService;
        }


    [HttpPost]
    [Route("api/GetClassList")]
    [AllowAnonymous]
    public async Task<IActionResult> GetClassList(Filters filters)
        {
        var result = await _commns.GetClassByTeacherAsync(filters.SchoolId, filters.UserId);
        if ( result != null )
            return Ok(new { Data = result, Status = true });
        else
            return Ok(new { Status = false });
        }

    [HttpPost]
    [Route("api/GetSectionList")]
    [AllowAnonymous]
    public async Task<IActionResult> GetSectionList(Filters filters)
        {
        var result = await _commns.GetSectionAsync(filters.SchoolId, filters.ClassId, filters.UserId).ConfigureAwait(false);
        if ( result != null )
            {
            return Ok(new { Data = result, Status = true });
            }
        else
            {
            return Ok(new { Status = false });
            }
        }

    [HttpGet]
    [Route("api/GetSubjectList")]
    [AllowAnonymous]
    public async Task<IActionResult> GetSubjectList(int SchoolId)
        {
        var result = await _commns.GetSubjectListAsync(SchoolId).ConfigureAwait(false);
        if ( result.Count > 0 )
            {
            return Ok(new { Data = result, Status = true });
            }
        else
            {
            return Ok(new { Status = false });
            }
        }

    [HttpPost]
    [Route("api/upload")]
    [HttpPost("upload")]
    [AllowAnonymous]
    public async Task<IActionResult> PostFile(List<IFormFile> files)
        {
        if ( files == null || files.Count == 0 )
            return BadRequest("No files uploaded.");

        string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "HomeWork");

        if ( !Directory.Exists(uploadPath) )
            Directory.CreateDirectory(uploadPath);

        var fileNames = new Dictionary<string, string>();

        foreach ( var file in files )
            {
            if ( file.Length > 0 )
                {
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                string newFileName = $"{fileNameWithoutExt}_{Guid.NewGuid()}{extension}";
                string filePath = Path.Combine(uploadPath, newFileName);

                using ( var stream = new FileStream(filePath, FileMode.Create) )
                    {
                    await file.CopyToAsync(stream);
                    }

                fileNames.Add(file.FileName, newFileName);
                }
            }

        return Ok(fileNames);
        }

    [HttpGet]
    [Route("api/GetFile")]
    [HttpGet("download/{fileName}")]
    [AllowAnonymous]
    public IActionResult GetFile(string fileName)
        {
        if ( string.IsNullOrEmpty(fileName) )
            return BadRequest("File name must be provided.");

        string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "HomeWork");
        string filePath = Path.Combine(uploadPath, fileName);

        if ( !System.IO.File.Exists(filePath) )
            return NotFound($"File not found: {fileName}");

        // Get content type
        var contentType = GetContentType(filePath);

        // Return file
        return PhysicalFile(filePath, contentType, fileName);
        }

    // Helper method to get MIME type
    private string GetContentType(string path)
        {
        var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
        if ( !provider.TryGetContentType(path, out var contentType) )
            {
            contentType = "application/octet-stream";
            }
        return contentType;
        }

    [HttpGet]
    [Route("api/GetHolidays")]
    [AllowAnonymous]
    public async Task<IActionResult> GetHolidays(int SchoolId)
        {
        var result = await _commns.GetHolidaysAsync(SchoolId).ConfigureAwait(false);
        if ( result != null )
            {
            return Ok(new { Data = result, Status = true });
            }
        else
            {
            return Ok(new { Status = false });
            }
        }

    [HttpGet]
    [Route("api/GetStaffProfile")]
    [AllowAnonymous]
    public async Task<IActionResult> GetStaffProfile(int SchoolId, int userId)
        {
        var result = await _commns.GetStaffProfileAsync(SchoolId, userId).ConfigureAwait(false);
        if ( result != null )
            {
            return Ok(new { Data = result, Status = true });
            }
        else
            {
            return Ok(new { Status = false });
            }
        }

    [HttpGet]
    [Route("api/GetStudentProfile")]
    [AllowAnonymous]
    public async Task<IActionResult> GetStudentProfile(int SchoolId, int userId)
        {
        var result = await _commns.GetStudentProfileAsync(SchoolId, userId).ConfigureAwait(false);
        if ( result != null )
            {
            return Ok(new { Data = result, Status = true });
            }
        else
            {
            return Ok(new { Status = false });
            }
        }

    [HttpGet]
    [Route("api/GetStudentsBirthday")]
    [AllowAnonymous]
    public async Task<IActionResult> GetStudentsBirthday(int SchoolId, int SessionId, int ClassId, int SectionId)
        {
        List<StudentBirthday> result = await _commns.GetStudentsBirthdayAsync(SchoolId, SessionId, ClassId, SectionId);
        if ( result != null && result.Count > 0 )
            return Ok(new { Data = result, Status = true });
        else
            return Ok(new { Status = false });
        }

    [HttpGet]
    [Route("api/GetStudentOnLeave")]
    [AllowAnonymous]
    public async Task<IActionResult> GetStudentOnLeave(int SchoolId, int SessionId, int ClassId, int SectionId)
        {
        var result = await _commns.GetStudentonLeavetodayAsync(SchoolId, SessionId, ClassId, SectionId);
        if ( result != null )
            return Ok(new { Data = result, Status = true });
        else
            return Ok(new { Status = false });
        }

    [HttpGet]
    [Route("api/GetAPPVersion")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAPPVersion(int SchoolId)
        {
        var result = await _commns.GetAPPVersionAsync(SchoolId);
        if ( result != null )
            return Ok(new { Data = result, Status = true });
        else
            return Ok(new { Status = false });
        }

    [HttpGet]
    [Route("api/GetStaffOnLeave")]
    [AllowAnonymous]
    public async Task<IActionResult> GetStaffOnLeave(int SchoolId, int SessionId)
        {
        var result = await _commns.GetStaffonLeavetodayAsync(SchoolId, SessionId);
        if ( result != null )
            return Ok(new { Data = result, Status = true });
        else
            return Ok(new { Status = false });
        }

    [HttpGet]
    [Route("api/GetStaffBirthDay")]
    [AllowAnonymous]
    public async Task<IActionResult> GetStaffBirthDay(int SchoolId, int SessionId)
        {
        List<StudentBirthday> result = await _commns.GetStaffsBirthdayAsync(SchoolId, SessionId);
        if ( result != null && result.Count > 0 )
            return Ok(new { Data = result, Status = true });
        else
            return Ok(new { Status = false });
        }

    [HttpPost]
    [Route("api/SaveStudentDairy")]
    [AllowAnonymous]
    public async Task<IActionResult> SaveStudentDairy(StudentDairyRequest dairyRequest)
        {
        var result = await _commns.SaveStudentDairyAsync(dairyRequest).ConfigureAwait(false);
        if ( result != null )
            return Ok(new { Message = result, Status = true });
        else
            return Ok(new { Status = false });

        }

    [HttpPost]
    [Route("api/GetStudentDairy")]
    [AllowAnonymous]
    public async Task<IActionResult> GetStudentDairy(StudentDairyRequest enquiry)
        {
        var result = await _commns.GetStudentDiaryAsync(enquiry);
        if ( result != null )
            return Ok(new { Data = result, Status = true });
        else
            return Ok(new { Status = false });
        }

    //Addmin section
    [HttpPost]
    [Route("api/SaveNewsorEvent")]
    [AllowAnonymous]
    public async Task<IActionResult> SaveNewsorEvent(NewsorEventRequest request)
        {
        var result = await _commns.SaveNewsorEventsAsync(request).ConfigureAwait(false);
        if ( result != null )
            return Ok(new { Message = result, Status = true });
        else
            return Ok(new { Status = false });

        }

    [HttpPost]
    [Route("api/GetNewsorEvent")]
    [AllowAnonymous]
    public async Task<IActionResult> GetNewsorEvent(int SchoolId)
        {
        var result = await _commns.GetNewsorEventsAsync(SchoolId);
        if ( result != null )
            return Ok(new { Data = result, Status = true });
        else
            return Ok(new { Status = false });
        }

    [HttpGet]
    [Route("api/GetDashBoardData")]
    [AllowAnonymous]
    public async Task<IActionResult> GetDashBoardData(int SchoolId, int SessionId)
        {
        DashboardCollectionSummaryResponse response = await _commns.GetDashboardDataAsync(SchoolId, SessionId);
        if ( response != null )
            return Ok(new { Data = response, Status = true });
        else
            return Ok(new { Status = false });
        }

    [HttpGet]
    [Route("api/GetCommunicationTemplateName")]
    [AllowAnonymous]
    public async Task<IActionResult> GetCommunicationTemplateName(int SchoolId, int SessionId)
        {
        var response = await _commns.GetDashboardDataAsync(SchoolId, SessionId);
        if ( response != null )
            return Ok(new { Data = response, Status = true });
        else
            return Ok(new { Status = false });
        }

    [HttpGet]
    [Route("api/GetCommunicationTemplateDesc")] //get template desc by templae Id
    [AllowAnonymous]
    public async Task<IActionResult> GetCommunicationTemplateDesc(int SchoolId, int templateId)
        {

        var response = await _commns.GetSMSTemaplateDescAsync(SchoolId, templateId);
        if ( response != null )
            return Ok(new { Data = response, Status = true });
        else
            return Ok(new { Status = false });
        }

    [HttpGet]
    [Route("api/GetSMSCredit")]
    [AllowAnonymous]
    public async Task<IActionResult> GetSMSCredit(int SchoolId, int SessionId)
        {
        string response = await _commns.GetSMSCreditAsync(SchoolId);
        if ( response != null )
            return Ok(new { Data = response, Status = true });
        else
            return Ok(new { Status = false });


        }

    [HttpPost]
    [Route("api/SendSMS")]
    [AllowAnonymous]
    public async Task<IActionResult> SendSMS(int SchoolId, int SessionId, int ClassId, int SectionId, string TemplateName, string TemplateDesc, int language)
        {
        DataTable dt = await _commns.GetDLTDetailsByTemplateNameAsync(SchoolId, TemplateName);
        if ( dt.Rows.Count > 0 )
            {
            string EntityId = dt.Rows [0] ["EntityId"].ToString();
            string TemplateId = dt.Rows [0] ["DltTemplateId"].ToString();
            string lstMobile = "";
            List<ViewStudentM> lstStudentM = await _commns.GetStudentListAsync(ClassId, SectionId, SchoolId, SessionId);
            foreach ( ViewStudentM s in lstStudentM )
                {
                string result = await _commns.GetStudentMobileByStudentIdAsync(s.StudentId).ConfigureAwait(false);
                if ( result.Length == 10 )
                    {
                    char no = result [0];
                    if ( no == '9' || no == '8' || no == '7' || no == '6' )
                        {
                        if ( lstMobile == "" )
                            {
                            lstMobile += "91" + result;
                            }
                        else
                            {
                            lstMobile += "91" + result + ',';
                            }
                        }
                    }
                }
            try
                {

                string response = await _commns.FTSMessanger(TemplateDesc, lstMobile, SchoolId, 3, "", EntityId, TemplateId, language);
                if ( response.Contains("Message Submitted Successfully") )
                    {
                    return Ok(new { Message = "Message Submitted Successfully", Status = true });
                    }
                return Ok(new { Message = response, Status = false });
                }
            catch ( Exception ex )
                {
                return Ok(new { Message = ex.Message, Status = false });
                }
            }
        else
            {
            return Ok(new { Message = "Template not mapped for DLT", Status = false });
            }
        }

    [HttpPost]
    [Route("api/save-notification")]
    [AllowAnonymous]
    public async Task<IActionResult> SaveNotificationNew(NotificationRequest notification)
        {

        try
            {
            // Prepare request payload
            var fcnRequestModel = new pushRequestModel
                {
                to = notification.To,
                body = notification.Message,
                title = notification.Title
                };

            var jsonObj = JsonConvert.SerializeObject(fcnRequestModel);
            var content = new StringContent(jsonObj, Encoding.UTF8, "application/json");

            // Ensure TLS support
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;

            using ( var client = new HttpClient() )
                {
                client.Timeout = TimeSpan.FromMinutes(5); // adjust as needed

                var response = await client.PostAsync("https://exp.host/--/api/v2/push/send", content);

                if ( response.IsSuccessStatusCode )
                    {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    // Log or process the response content as needed
                    return Ok(new { Status = true, Message = "Notification sent successfully", Response = responseContent });
                    }
                else
                    {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    // Log or process the error content as needed
                    return StatusCode(( int ) response.StatusCode, new { Status = false, Message = "Failed to send notification", Error = errorContent });
                    }
                }
            }
        catch ( Exception ex )
            {
            // Log the exception as needed
            return StatusCode(500, new { Status = false, Message = "An error occurred while sending notification", Error = ex.Message });
            }
        }

    [HttpPost]
    [Route("api/saveFirebaseTocken")]
    [AllowAnonymous]
    public async Task<IActionResult> SaveFireBaseTocken(FirebaseRequestModel firebaseRequest)
        {
        try
            {
            var Status = await _commns.SaveFirebaseTockenAsync(firebaseRequest.tocken, firebaseRequest.userId, firebaseRequest.userTypeId, firebaseRequest.SchoolId);
            if ( Status )
                {
                return Ok(new { Status = true, Messaage = "Saved successfully" });
                }
            else
                {
                return StatusCode(500, new { Status = false, Message = "error while saving" });
                }
            }
        catch ( Exception ex )
            {
            return StatusCode(500, new { Error = ex.Message });
            }
        }

    [HttpPost]
    [Route("api/deleteFirebaseTocken")]
    [AllowAnonymous]
    public async Task<IActionResult> delteFireBaseTocken(string tocken)
        {
        bool Status = await _commns.DeleteFirebaseTockenAsync(tocken);
        if ( Status )
            {
            return Ok(new { Status = true, Messaage = "Deleted successfully" });
            }
        else
            {
            return Ok(new { Status = false, Message = "error while Deleting" });
            }
        }

    [HttpGet]
    [Route("api/PrincipleProfiles")]
    [AllowAnonymous]
    public async Task<IActionResult> PrincipleProfiles(int SchoolId)
        {
        var result = await _commns.PrincipleProfilesAsync(SchoolId);
        if ( result != null )
            {
            return Ok(new { Data = result, Status = true });
            }
        else
            {
            return Ok(new { Status = false });
            }
        }

    [Route("api/SaveRegistrationWeb")]
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> SaveRegistration(StudentRegistrationWeb registration)
        {
        OnlineRegistrationResponce responce = new OnlineRegistrationResponce();
        try
            {
            var result = await _commns.SaveRegistrationAsync(registration);

            if ( result != null && !string.IsNullOrEmpty(result.RegNumber) )
                {
                APIGatewayCredentials credentials = await _paymentgatewayService.GetClietKeySecretAsync(registration.SchoolId);

                if ( credentials != null )
                    {
                    responce.ClientKey = credentials.Key_Id;
                    responce.ClientSec = credentials.Key_Secret;
                    PaymentGatewayOrderResponse paymentOrderResponse = await CreateOrderAsync(registration.RegFee, result.RegNumber, responce.ClientKey, responce.ClientSec);

                    var orderRequest = new OnlinePaymentOrderRequest
                        {
                        SchoolId = registration.SchoolId,
                        StudentId = 0,
                        RegistrationNumber = result.RegNumber,
                        OrderId = paymentOrderResponse.Id,
                        OrderAmount = registration.RegFee,
                        OrderStatus = "Pending",
                        OrderRemark = "Registration Fee",
                        PaymentId = "",
                        CreatedOn = DateTime.Now,
                        ModifiedOn = DateTime.Now
                        };

                    OnlinePaymentOrderRequest savedOrder = await _paymentgatewayService.SaveOnlinePaymentOrderAsync(orderRequest);

                    responce.RegistrationNo = result.RegNumber;
                    responce.PaymentGatewayOrderId = paymentOrderResponse.Id;
                    responce.PaymentLink = paymentOrderResponse.ShortUrl;
                    responce.ClientKey = credentials.Key_Id;
                    responce.ClientSec = credentials.Key_Secret;
                    responce.SchoolId = registration.SchoolId;
                    responce.RegFee = registration.RegFee;
                    responce.Name = registration.Name;

                    return Ok(new { Data = responce, Status = true });
                    }
                return Ok(new { Data = responce, Status = false, Message = "School is not accepting online Payment" });
                }
            else
                {
                return Ok(new { Status = false, Message = "Registration failed." });
                }
            }
        catch ( Exception ex )
            {
            return Ok(new { Status = false, Message = ex.Message });
            }
        }
    private async Task<PaymentGatewayOrderResponse> CreateOrderAsync(int registrationFee, string registrationNumber, string keyId, string keySecret)
        {
        var orderResponse = new PaymentGatewayOrderResponse();
        string authHeader = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{keyId}:{keySecret}"));
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authHeader);

        var payload = new
            {
            amount = registrationFee * 100,
            currency = "INR",
            receipt = registrationNumber
            };

        var jsonPayload = JsonConvert.SerializeObject(payload);
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        string apiUrl = "https://api.razorpay.com/v1/orders";

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);

        if ( response.IsSuccessStatusCode )
            {
            string responseBody = await response.Content.ReadAsStringAsync();
            try
                {
                orderResponse = JsonConvert.DeserializeObject<PaymentGatewayOrderResponse>(responseBody);
                }
            catch ( JsonException ex )
                {
                throw new Exception("Error parsing Razorpay order response.", ex);
                }
            }
        else
            {
            string errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Order creation failed: {response.StatusCode} - {errorContent}");
            }

        return orderResponse;
        }

    [Route("api/SaveOnlinePaymentWeb")]
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> SaveOnlinePaymentWeb(SchoolAPI.Models.Free.OnlinePaymentRequest payment)
        {
        var result = _paymentgatewayService.SaveOnlinePaymentAsync(payment);
        if ( result != null )
            return Ok(new { Message = result, Status = true });
        else
            return Ok(new { Status = false });

        }
    }
