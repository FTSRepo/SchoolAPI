using System.Data;
using SchoolAPI.Models.Common;

namespace SchoolAPI.Services.CommonService
{
    public interface ICommonService
    {
        Task<List<ClassM>> GetClassByTeacherAsync(int schoolId, int userId);
        Task<List<SectionM>> GetSectionAsync(int classId, int schoolId, int userId = 0);
        Task<List<SubjectM>> GetSubjectListAsync(int schoolId);
        Task<List<Holidays>> GetHolidaysAsync(int schoolId);
        Task<StaffProfile> GetStaffProfileAsync(int schoolId, int userId);
        Task<StudentProfile> GetStudentProfileAsync(int schoolId, int userId);
        Task<string> SaveEnquiryAsync(EnquiryM enquiry);
        Task<string> UpdateEnqiriesAsync(int enquiryId);
        Task<List<StudentBirthday>> GetStudentsBirthdayAsync(int schoolId, int sessionId, int classId, int sectionId);
        Task<DataTable> GetStudentonLeavetodayAsync(int schoolId, int sessionId, int classId, int sectionId);
        Task<APPVersionM> GetAPPVersionAsync(int schoolId);
        Task<DataTable> GetStaffonLeavetodayAsync(int schoolId, int sessionId);
        Task<List<StudentBirthday>> GetStaffsBirthdayAsync(int schoolId, int sessionId);
        Task<string> SaveStudentDairyAsync(StudentDairyRequest request);
        Task<List<StudentDairyResponse>> GetStudentDiaryAsync(StudentDairyRequest request);
        Task<string> SaveNewsorEventsAsync(NewsorEventRequest request);
        Task<List<NewsorEventResponse>> GetNewsorEventsAsync(int schoolId);
        Task<DashboardCollectionSummaryResponse> GetDashboardDataAsync(int schoolId, int sessionId);
        Task<DataTable> GetTemplateSmsAsync(int schoolId, int sessionId);
        Task<DataTable> GetSMSTemaplateDescAsync(int schoolId, int templateId);
        Task<string> GetSMSCreditAsync(int schoolId);
        Task<bool> SaveFirebaseTockenAsync(string tocken, int userId, int userTypeId, int schoolId);
        Task<bool> DeleteFirebaseTockenAsync(string tocken);
        Task<PrincipleProfile> PrincipleProfilesAsync(int schoolId);
        Task<StudentRegistrationResponce> SaveRegistrationAsync(StudentRegistrationWeb registration);
        Task<DataTable> GetDLTDetailsByTemplateNameAsync(int schoolId, string templateName);
        Task<List<ViewStudentM>> GetStudentListAsync(int classId, int sectionId, int schoolId, int sessionId);
        Task<string?> GetStudentMobileByStudentIdAsync(int studentId);
        Task<DataTable> GetApiDetailAsync(int schoolId);
        Task UpdateSMSCreditAsync(int schoolId, int credit);
        Task<string> FTSMessanger(string msg, string listMobile, int SchoolId, int type, string sid, string entityId = null, string dltTemplateId = null, int languageid = 1);
    }
}
