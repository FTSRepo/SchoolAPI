using System.Data;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SchoolAPI.Models.Registration;
using SchoolAPI.Repositories.RegistrationRepository;
using SchoolAPI.Services.CommonService;
using static Azure.Core.HttpHeader;

namespace SchoolAPI.Services.RegistrationService
{
    public class RegistrationService(IRegistrationRepository registrationRepository, ICommonService commonService) : IRegistrationService
    {
        private readonly IRegistrationRepository _registrationRepository = registrationRepository;
        private readonly ICommonService _commonService = commonService;
        public async Task<List<CategoryM>> BindCategoryDropdownsAsync()
        {
            List<CategoryM> categoryDropdownMs = [];

            DataTable data = await _registrationRepository.BindCategoryDropdownsAsync();

            foreach (DataRow dr in data.Rows)
            {
                CategoryM category = new()
                {
                    CategoryId = Convert.ToInt32(dr["CategoryId"]),
                    CategoryName = Convert.ToString(dr["CategoryName"]),
                };

                categoryDropdownMs.Add(category);
            }
            return categoryDropdownMs;
        }
        public async Task<string> GetNewRegNumberAsync(int schoolId)
        {
            return await _registrationRepository.GetNewRegNumberAsync(schoolId);
        }
        public async Task<List<ReligionM>> BindReligionDropdownsAsync()
        {
            List<ReligionM> religionDropdownMs =[];
            DataTable data = await _registrationRepository.BindReligionDropdownsAsync();
            foreach (DataRow dr in data.Rows)
            {
                ReligionM religion = new()
                {
                    religionId = Convert.ToInt32(dr["ReligionId"]),
                    religionName = Convert.ToString(dr["ReligionName"]),
                };
                religionDropdownMs.Add(religion);
            }
            return religionDropdownMs;
        }
        public async Task<string> SaveRegistrationAsync(StudentRegistrationModel objstudentregistration)
        {
            return await _registrationRepository.SaveRegistrationAsync(objstudentregistration);
        }

        public async Task<string> SendSMS(StudentRegistrationModel registrationModel)
        {
            string result = "";
            if (Regex.Match(registrationModel.fathermobilenumber.Trim(), @"^[6789]\d{9}$").Success)
            {
                if (Convert.ToInt32( await _commonService.GetSMSCreditAsync(registrationModel.Schoolid)) > 0)
                {
                    string template;
                    List<string> smsresponse = new List<string>();
                    DataTable templateDt = await _commonService.GetSMSTemaplateDescAsync(registrationModel.Schoolid, 4);
                    template = templateDt.Rows[0]["TemplateDesc"].ToString();
                    template = template == null ? "R/p Resgistration for your ward-, '" + registrationModel.Firstname + "' is completed successfully with Reg No.'" + registrationModel.Registration + "' on Date:'" + registrationModel.DateofRegistration1.ToString() + "'" : template.Replace("@", registrationModel.Firstname).Replace("#", registrationModel.DateofRegistration1.ToString()).Replace("$", registrationModel.Registration);
                    result = await _commonService.FTSMessanger(template, registrationModel.fathermobilenumber, registrationModel.Schoolid, 4, "");
                }
            }
            return result;
        }
        public async Task<List<StudentRegistrationModelRes>> StudentRegistrationAllRecordAsync(int schoolId, int regno, string? type = null, string requestType = "")
        {
            List<StudentRegistrationModelRes> studentRegistrations = [];
            DataTable dt = await _registrationRepository.StudentRegistrationAllRecordAsync(schoolId, regno, type, requestType);
            foreach(DataRow dr in dt.Rows)
            {
                studentRegistrations.Add(new StudentRegistrationModelRes()
                {
                    Regno = Convert.ToInt32(dr["Regno"]),
                    Registration = Convert.ToString(dr["Registration"]),
                    Date = Convert.ToString(dr["Date"]),
                    Remark = Convert.ToString(dr["Remark"]),
                    Status = Convert.ToInt32(dr["Status"]),
                    Name = Convert.ToString(dr["Name"]),
                    ClassName= Convert.ToString(dr["ClassName"]),
                    Dob = Convert.ToString(dr["DOB"]),
                    Gender = Convert.ToString(dr["Gender"]),
                    FatherMobileNumber = Convert.ToString(dr["FatherMobileNumber"]),
                    FatherName = Convert.ToString(dr["Fname"]),
                    ReciptNo= Convert.ToInt32(dr["ReciptNo"]),
                    RegFee= Convert.ToInt32(dr["RegFee"])
                });

            }
            return studentRegistrations;
        }
    }
}
