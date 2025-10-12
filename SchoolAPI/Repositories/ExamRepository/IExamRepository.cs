using System.Data;
using SchoolAPI.Models.Exam;

namespace SchoolAPI.Repositories.ExamRepository
    {
    public interface IExamRepository
        {
        Task<DataTable> GetPrincipalMsgAsync(int schoolId);
        Task<string> SavePrincipalMsgAsync(int schoolId, string msg);

        Task<DataTable> GetSchoolTopperAsync(int schoolId, int sessionId);
        Task<DataTable> DownloadClassDocumentAsync(int schoolId, int studentId);
        Task<DataTable> GetStudentListForMarksEntryAsync(int schoolId, int sessionId, int classId, int sectionId, string examCode, int subjectId);
        Task<DataTable> GetStudentListForRemarksEntryAsync(int schoolId, int sessionId, int classId, int sectionId, string examCode);
        Task<DataTable> GetExamNameForMarksEntryByClassIdAsync(int schoolId, int classId);
        Task<DataTable> GetClassGroupMasterAsync(int schoolId);
        Task<DataTable> GetSubjectWithMaxMarksAsync(int schoolId, int sessionId, string examCode, int classGroupId);

        Task<string> SaveExamTblRemarksEntryAsync(MarksEntryMaster marksEntry, DataTable dt, int userId);
        Task<string> SaveExamTblMarksEntryAsync(MarksEntryMaster marksEntry, DataTable dt, int userId);

        Task<DataTable> GetExamTestCodeAsync(int schoolId, int sessionId);
        Task<DataTable> GetTestMarksDetailsAsync(int schoolId, int sessionId, int classId, int sectionId, string testCode, int subjectId);
        Task<string> TblSaveTestMarksEntryAsync(TestEntryMaster testmarksEntry, DataTable dt, int userId);

        Task<DataTable> WebAppGetTestResultAsync(int schoolId, int sessionId, int studentId);
        Task<DataTable> GetTestResultAsync(int schoolId, int sessionId, int classId, int sectionId, int subjectId, int testCode);

        Task<DataTable> GetResultNameAndDateAsync(int schoolId, int sessionId);
        Task<DataTable> GetCheckAllDueClearAsync(int schoolId, int sessionId, int studentId);
        Task<DataTable> GetCheckStudentMarksEntryAsync(int schoolId, int sessionId, int studentId, string examCode);
        }
    }
