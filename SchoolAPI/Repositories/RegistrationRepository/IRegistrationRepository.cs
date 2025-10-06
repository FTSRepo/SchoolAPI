using System.Data;
using SchoolAPI.Models.Common;
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
        Task<bool> DeleteRegRecordAsync(int id, int schoolId);
        Task<bool> UpdateRegistrationStatusAsync(int regNo, int schoolId, string status, string remark, int userId);
        Task<bool> UpdateEnquiriesAsync(int id);
        Task<bool> DeleteOnlineEnquiryAsync(int enquiryId);
        Task<DataTable> GetEnquiriesAsync(int schoolId, string requestType);
        Task<DataTable> GetEnquiryByIdAsync(int schoolId, int enquiryId);
        Task<bool> SaveEnquiryAsync(EnquiryM enquiryM);
    }
}
