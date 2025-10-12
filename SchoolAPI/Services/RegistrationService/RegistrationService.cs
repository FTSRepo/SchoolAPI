using System.Data;
using System.Text.RegularExpressions;
using SchoolAPI.Helper;
using SchoolAPI.Models.Common;
using SchoolAPI.Models.Registration;
using SchoolAPI.Repositories.RegistrationRepository;
using SchoolAPI.Services.CommonService;

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

            foreach ( DataRow dr in data.Rows )
                {
                CategoryM category = new()
                    {
                    CategoryId = Convert.ToInt32(dr ["CategoryId"]),
                    CategoryName = Convert.ToString(dr ["CategoryName"]),
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
            List<ReligionM> religionDropdownMs = [];
            DataTable data = await _registrationRepository.BindReligionDropdownsAsync();
            foreach ( DataRow dr in data.Rows )
                {
                ReligionM religion = new()
                    {
                    religionId = Convert.ToInt32(dr ["ReligionId"]),
                    religionName = Convert.ToString(dr ["ReligionName"]),
                    };
                religionDropdownMs.Add(religion);
                }
            return religionDropdownMs;
            }
        public async Task<string> SaveRegistrationAsync(StudentRegistrationModelReq objstudentregistration)
            {
            return await _registrationRepository.SaveRegistrationAsync(objstudentregistration);
            }

        public async Task<string> SendSMS(StudentRegistrationModelReq registrationModel)
            {
            string result = "";
            if ( Regex.Match(registrationModel.FatherMobile1.Trim(), @"^[6789]\d{9}$").Success )
                {
                if ( Convert.ToInt32(await _commonService.GetSMSCreditAsync(registrationModel.SchoolId)) > 0 )
                    {
                    string template;
                    List<string> smsresponse = new List<string>();
                    DataTable templateDt = await _commonService.GetSMSTemaplateDescAsync(registrationModel.SchoolId, 4);
                    template = templateDt.Rows [0] ["TemplateDesc"].ToString();
                    template = template == null ? "R/p Resgistration for your ward-, '" + registrationModel.FirstName + "' is completed successfully with Reg No.'" + registrationModel.Registration + "' on Date:'" + registrationModel.DateOfRegistration.ToString() + "'" : template.Replace("@", registrationModel.FirstName).Replace("#", registrationModel.DateOfRegistration.ToString()).Replace("$", registrationModel.Registration);
                    result = await _commonService.FTSMessanger(template, registrationModel.FatherMobile1, registrationModel.SchoolId, 4, "");
                    }
                }
            return result;
            }
        public async Task<List<StudentRegistrationModelRes>> StudentRegistrationAllRecordAsync(int schoolId, int regno, string? type = null, string requestType = "")
            {
            List<StudentRegistrationModelRes> studentRegistrations = [];
            DataTable dt = await _registrationRepository.StudentRegistrationAllRecordAsync(schoolId, regno, type, requestType);
            foreach ( DataRow dr in dt.Rows )
                {
                studentRegistrations.Add(MapStudent(dr));

                }
            return studentRegistrations;
            }

        public async Task<StudentRegistrationModelRes?> StudentRegistrationByRegNoAsync(int schoolId, int regNo)
            {
            DataTable dt = await _registrationRepository.StudentRegistrationAllRecordAsync(schoolId, regNo, null, null);

            if ( dt.Rows.Count == 0 )
                return null;

            DataRow dr = dt.Rows [0]; // Only first row, since you're returning a single student

            return MapStudent(dr);
            }

        public async Task<List<EnquiryM>> GetEnquiriesAsync(int schoolId, string requestType)
            {
            List<EnquiryM> enquiries = [];
            DataTable dt = await _registrationRepository.GetEnquiriesAsync(schoolId, requestType).ConfigureAwait(false);
            foreach ( DataRow dr in dt.Rows )
                {
                enquiries.Add(mapEnquiry(dr));
                }
            return enquiries;
            }
        public async Task<EnquiryM> GetEnquiryByIdAsync(int schoolId, int enquiryId)
            {
            DataTable dt = await _registrationRepository.GetEnquiryByIdAsync(schoolId, enquiryId).ConfigureAwait(false);
            DataRow dr = dt.Rows [0];
            return mapEnquiry(dr);
            }
        private static StudentRegistrationModelRes MapStudent(DataRow dr)
            {
            return new StudentRegistrationModelRes
                {
                Regno = Convert.ToInt32(dr ["Regno"]),
                Registration = Convert.ToString(dr ["Registration"]),
                Date = Convert.ToString(dr ["Date"]),
                Status = Convert.ToString(dr ["Status"]),
                Name = Convert.ToString(dr ["Name"]),
                ClassName = Convert.ToString(dr ["ClassName"]),
                Dob = Convert.ToString(dr ["DOB"]),
                Gender = Convert.ToString(dr ["Gender"]),
                FatherMobileNumber = Convert.ToString(dr ["FatherMobileNumber"]),
                FatherName = Convert.ToString(dr ["Fname"]),
                ReciptNo = Convert.ToInt32(dr ["ReciptNo"]),
                RegFee = Convert.ToInt32(dr ["RegFee"])
                };
            }
        private static EnquiryM mapEnquiry(DataRow dr)
            {
            return new EnquiryM
                {
                Name = dr.GetString("Name"),
                Contact = dr.GetString("Contact"),
                Email = dr.GetString("Email"),
                Message = dr.GetString("Message"),
                EnquiryType = dr.GetString("RequestType"),
                EnquiryDate = dr.GetString("TranDate"),
                EnquiryId = dr.GetInt("Id")
                };
            }

        }
    }
