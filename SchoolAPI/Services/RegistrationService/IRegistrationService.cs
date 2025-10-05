using System.Data;
using SchoolAPI.Models.Registration;

namespace SchoolAPI.Services.RegistrationService
{
    public interface IRegistrationService
    {
        Task<List<CategoryM>> BindCategoryDropdownsAsync();
        Task<string> GetNewRegNumberAsync(int schoolId);
        Task<List<ReligionM>> BindReligionDropdownsAsync();
        Task<string> SaveRegistrationAsync(StudentRegistrationModel objstudentregistration);
        Task<string> SendSMS(StudentRegistrationModel registrationModel);
        Task<List<StudentRegistrationModel>> StudentRegistrationAllRecordAsync(int schoolId, int regno, string? type = null, string requestType = "");
    }
}
