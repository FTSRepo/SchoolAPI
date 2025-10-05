using System.Data;
using SchoolAPI.Models.Registration;

namespace SchoolAPI.Repositories.RegistrationRepository
{
    public interface IRegistrationRepository
    {
        Task<DataTable> BindCategoryDropdownsAsync();
        Task<string> GetNewRegNumberAsync(int schoolId);
        Task<DataTable> BindReligionDropdownsAsync();
        Task<List<BloodGroupDto>> GetBloodGroupAsync();
        Task<List<StateDto>> GetStates();
        Task<string> SaveRegistrationAsync(StudentRegistrationModel objstudentregistration);
        Task<DataTable> StudentRegistrationAllRecordAsync(int schoolId, int regno, string? type = null, string requestType = "");
    }
}
