using SchoolAPI.Models.Common;
using SchoolAPI.Models.Registration;
using System.Data;
using System.Text.RegularExpressions;

namespace SchoolAPI.Services.RegistrationService
{
    public interface IRegistrationService
    {
        Task<List<CategoryM>> BindCategoryDropdownsAsync();
        Task<string> GetNewRegNumberAsync(int schoolId);
        Task<List<ReligionM>> BindReligionDropdownsAsync();
        Task<string> SaveRegistrationAsync(StudentRegistrationModelReq objstudentregistration);
        Task<string> SendSMS(StudentRegistrationModelReq registrationModel);
        Task<List<StudentRegistrationModelRes>> StudentRegistrationAllRecordAsync(int schoolId, int regno, string? type = null, string requestType = "");
        Task<StudentRegistrationModelRes?> StudentRegistrationByRegNoAsync(int schoolId, int regNo );
        Task<List<EnquiryM>> GetEnquiriesAsync(int schoolId, string requestType);
        Task<EnquiryM> GetEnquiryByIdAsync(int schoolId, int enquiryId);
    }
}
