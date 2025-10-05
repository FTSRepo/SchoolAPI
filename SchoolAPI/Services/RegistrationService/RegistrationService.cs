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
        public async Task<List<StudentRegistrationModel>> StudentRegistrationAllRecordAsync(int schoolId, int regno, string? type = null, string requestType = "")
        {
            List<StudentRegistrationModel> studentRegistrations = [];
            DataTable dt = await _registrationRepository.StudentRegistrationAllRecordAsync(schoolId, regno, type, requestType);
            foreach(DataRow dr in dt.Rows)
            {
                studentRegistrations.Add(new StudentRegistrationModel()
                {
                    AadharNo = Convert.ToString(dr["AadharNo"]),
                    Addressline1ph = Convert.ToString(dr["Addressline1ph"]),
                    AddressLine1S = Convert.ToString(dr["AddressLine1S"]),
                    CategoryName = Convert.ToString(dr["CategoryName"]),
                    AddressLine2S = Convert.ToString(dr["AddressLine2S"]),
                    Addresslineph = Convert.ToString(dr["Addresslineph"]),
                    DateofRegistration1 = Convert.ToString(dr["DateofRegistration1"]),
                    BirthPlace = Convert.ToString(dr["BirthPlace"]),
                    BloodGroup = Convert.ToInt32(dr["BloodGroup"]),
                    BloodGroupName = Convert.ToString(dr["BloodGroupName"]),
                    Category = Convert.ToInt32(dr["Category"]),
                    cityp = Convert.ToString(dr["CityType"]),
                    Cityph = Convert.ToString(dr["CityPh"]),
                    Class = Convert.ToInt32(dr["ClassName"]),
                    ClassName = Convert.ToString(dr["ClassName"]),                   
                    DateofRegistration = Convert.ToDateTime(dr["DateofRegistration"]),
                    Dob = Convert.ToDateTime(dr["Dob"]), // Keeping only one of Dob/DOB/Dob1
                    EducationQualificationfather = Convert.ToString(dr["EducationQualificationfather"]),
                    EducationQualificationmother = Convert.ToString(dr["EducationQualificationmother"]),
                    FatherMobile1 = Convert.ToString(dr["FatherMobile1"]),
                    fathermobilenumber = Convert.ToString(dr["fathermobilenumber"]),
                    Firstname = Convert.ToString(dr["Firstname"]),
                    Firstnamefather = Convert.ToString(dr["Firstnamefather"]),
                    Firstnamemother = Convert.ToString(dr["Firstnamemother"]),
                    Fname = Convert.ToString(dr["Fname"]),
                    Gender = Convert.ToString(dr["Gender"]),
                    lastName = Convert.ToString(dr["lastName"]),
                    Lastnamefather = Convert.ToString(dr["Lastnamefather"]),
                    LastNamemother = Convert.ToString(dr["LastNamemother"]),
                    MiddleName = Convert.ToString(dr["MiddleName"]),
                    Middlenamefather = Convert.ToString(dr["Middlenamefather"]),
                    Middlenamemother = Convert.ToString(dr["Middlenamemother"]),
                    mobile1 = Convert.ToString(dr["mobile1"]),
                    mobilenop = Convert.ToString(dr["MobileNo"]), // Only kept once
                    ModifiedBy = Convert.ToString(dr["ModifiedBy"]),
                    Name = Convert.ToString(dr["Name"]),
                    Occupation = Convert.ToString(dr["Occupation"]),
                    Occupationmother = Convert.ToString(dr["Occupationmother"]),
                    Presentdistance = Convert.ToString(dr["Presentdistance"]),
                    ProfessionalQualificationfather = Convert.ToString(dr["ProfessionalQualificationfather"]),
                    ProfessionalQualificationmother = Convert.ToString(dr["ProfessionalQualificationmother"]),
                    Remark = Convert.ToString(dr["Remark"]),
                    ReligionName = Convert.ToString(dr["ReligionName"]),
                    status = Convert.ToString(dr["status"]),
                    Sessionid = Convert.ToInt32(dr["Sessionid"]),
                    Schoolid = Convert.ToInt32(dr["Schoolid"])
                });

            }
            return studentRegistrations;
        }
    }
}
