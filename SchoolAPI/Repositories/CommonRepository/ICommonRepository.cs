using System.Data;
using SchoolAPI.Models.Common;

namespace SchoolAPI.Repositories.CommonRepository
{
    public interface ICommonRepository
    {
        Task<DataTable> GetClassByTeacherAsync(int schoolId, int userId);
        Task<DataTable> GetSectionAsync(int classId, int schoolId, int userId = 0);
        Task<DataTable> GetSubjectAsync(int schoolId);
        Task<DataTable> GetHoliDaysAsync(int schoolId);
        Task<DataTable> GetStaffProfileAsync(int schoolId, int staffId);
        Task<DataTable> GetStudentProfileAsync(int schoolId, int studentId);
        Task<DataTable> GetStudentsBirthdayAsync(int schoolId, int sessionId, int classId, int sectionId);
        Task<DataTable> GetStudentonLeavetodayAsync(int schoolId, int sessionId, int classId, int sectionId);
        Task<DataTable> GetAPPVersionAsync(int schoolId);
        Task<DataTable> GetStaffonLeavetodayAsync(int schoolId, int sessionId);
        Task<DataTable> GetStaffsBirthdayAsync(int schoolId, int sessionId);
        Task<string> SaveStudentDairyAsync(StudentDairyRequest request);
        Task<DataTable> GetStudentDiaryAsync(StudentDairyRequest request);
        Task<string> SaveNewsorEventsAsync(NewsorEventRequest request);
        Task<DataTable> GetNewsorEventsAsync(int schoolId);
        Task<DataTable> GetDashboardDataAsync(int schoolId, int sessionId);
        Task<DataTable> GetTemplateSmsAsync(int schoolId, int sessionId);
        Task<DataTable> GetSMSTemaplateDescAsync(int schoolId, int templateId);
        Task<string> GetSMSCreditAsync(int schoolId);
        Task<bool> SaveFirebaseTockenAsync(string tocken, int userId, int userTypeId, int schoolId);
        Task<bool> DeleteFirebaseTockenAsync(string tocken);
        Task<DataTable> PrincipleProfilesAsync(int schoolId);
        Task<DataTable> SaveRegistrationAsync(StudentRegistrationWeb registration);
        Task<DataTable> GetDLTDetailsByTemplateNameAsync(int schoolId, string templateName);
        Task<DataTable> GetStudentListAsync(int classId, int sectionId, int schoolId, int sessionId);
        Task<string?> GetStudentMobileByStudentIdAsync(int studentId);
        Task<DataTable> GetApiDetailAsync(int schoolId);
        Task UpdateSMSCreditAsync(int schoolId, int credit);
        Task<string> InsertSMSLogAsync(DataTable dt);
    }
}
