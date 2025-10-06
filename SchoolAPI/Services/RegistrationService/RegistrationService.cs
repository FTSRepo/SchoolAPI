using System.Data;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Identity.Client;
using SchoolAPI.Helper;
using SchoolAPI.Models.Common;
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
        public async Task<List<StudentRegistrationModel>> StudentRegistrationAllRecordAsync(int schoolId, int regno, string? type = null, string requestType = "")
        {
            List<StudentRegistrationModel> studentRegistrations = [];
            DataTable dt = await _registrationRepository.StudentRegistrationAllRecordAsync(schoolId, regno, type, requestType);
            foreach(DataRow dr in dt.Rows)
            {
                studentRegistrations.Add(MapStudent(dr));

            }
            return studentRegistrations;
        }
         
        public async Task<StudentRegistrationModel?> StudentRegistrationByRegNoAsync(int regNo, int schoolId)
        {
            DataTable dt = await _registrationRepository.StudentRegistrationAllRecordAsync(schoolId, regNo, null, "");

            if (dt.Rows.Count == 0)
                return null;

            DataRow dr = dt.Rows[0]; // Only first row, since you're returning a single student

            return MapStudent(dr);
        }

        public async Task<List<EnquiryM>> GetEnquiriesAsync(int schoolId, string requestType)
        {
            List<EnquiryM> enquiries = [];
            DataTable dt = await _registrationRepository.GetEnquiriesAsync(schoolId, requestType).ConfigureAwait(false);
            foreach (DataRow dr in dt.Rows)
            {
                enquiries.Add(mapEnquiry(dr));
            }
            return enquiries;
        }
        public async Task<EnquiryM> GetEnquiryByIdAsync(int schoolId, int enquiryId)
        {
            DataTable dt = await _registrationRepository.GetEnquiryByIdAsync(schoolId, enquiryId).ConfigureAwait(false);
            DataRow dr = dt.Rows[0];
            return mapEnquiry(dr);
        }
        private static StudentRegistrationModel MapStudent(DataRow dr)
        {
            return new StudentRegistrationModel
            {
                AadharNo = dr.GetString("AadharNo"),
                Addressline1ph = dr.GetString("Addressline1ph"),
                AddressLine1S = dr.GetString("AddressLine1S"),
                CategoryName = dr.GetString("CategoryName"),
                AddressLine2S = dr.GetString("AddressLine2S"),
                Addresslineph = dr.GetString("Addresslineph"),
                DateofRegistration1 = dr.GetString("DateofRegistration1"),
                BirthPlace = dr.GetString("BirthPlace"),
                BloodGroup = dr.GetInt("BloodGroup"),
                BloodGroupName = dr.GetString("BloodGroupName"),
                Category = dr.GetInt("Category"),
                cityp = dr.GetString("CityType"),
                Cityph = dr.GetString("CityPh"),
                Class = dr.GetInt("Class"),            // Class is int, use proper column
                ClassName = dr.GetString("ClassName"),
                DateofRegistration = dr.GetDate("DateofRegistration"),
                Dob = dr.GetDate("Dob"),
                EducationQualificationfather = dr.GetString("EducationQualificationfather"),
                EducationQualificationmother = dr.GetString("EducationQualificationmother"),
                FatherMobile1 = dr.GetString("FatherMobile1"),
                fathermobilenumber = dr.GetString("fathermobilenumber"),
                Firstname = dr.GetString("Firstname"),
                Firstnamefather = dr.GetString("Firstnamefather"),
                Firstnamemother = dr.GetString("Firstnamemother"),
                Fname = dr.GetString("Fname"),
                Gender = dr.GetString("Gender"),
                lastName = dr.GetString("lastName"),
                Lastnamefather = dr.GetString("Lastnamefather"),
                LastNamemother = dr.GetString("LastNamemother"),
                MiddleName = dr.GetString("MiddleName"),
                Middlenamefather = dr.GetString("Middlenamefather"),
                Middlenamemother = dr.GetString("Middlenamemother"),
                mobile1 = dr.GetString("mobile1"),
                mobilenop = dr.GetString("MobileNo"),
                ModifiedBy = dr.GetString("ModifiedBy"),
                Name = dr.GetString("Name"),
                Occupation = dr.GetString("Occupation"),
                Occupationmother = dr.GetString("Occupationmother"),
                Presentdistance = dr.GetString("Presentdistance"),
                ProfessionalQualificationfather = dr.GetString("ProfessionalQualificationfather"),
                ProfessionalQualificationmother = dr.GetString("ProfessionalQualificationmother"),
                Remark = dr.GetString("Remark"),
                ReligionName = dr.GetString("ReligionName"),
                status = dr.GetString("status"),
                Sessionid = dr.GetInt("Sessionid"),
                Schoolid = dr.GetInt("Schoolid")
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
