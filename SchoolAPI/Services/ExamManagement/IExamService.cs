using SchoolAPI.Models.Exam;

namespace SchoolAPI.Services.ExamManagement
    {
    public interface IExamService
        {
        Task<List<GetMessageResponse>> GetPrincipalMsgAsync(int schoolId);
        Task<string> SavePrincipalMsgAsync(int schoolId, string msg);

        Task<List<SchoolTopperDto>> GetSchoolTopperAsync(int schoolId, int sessionId);
        Task<List<ClassDocumentDto>> DownloadClassDocumentAsync(int schoolId, int studentId);
        Task<List<StudentExamMarksDto>> GetStudentListForMarksEntryAsync(int schoolId, int sessionId, int classId, int sectionId, string examCode, int subjectId);
        Task<List<StudentExamRemarksDto>> GetStudentListForRemarksEntryAsync(int schoolId, int sessionId, int classId, int sectionId, string examCode);
        Task<List<ExamMasterByClassIdDto>> GetExamNameForMarksEntryByClassIdAsync(int schoolId, int classId);
        Task<List<ClassGroupMasterDto>> GetClassGroupMasterAsync(int schoolId);
        Task<List<SubjectWithMaxMarksDto>> GetSubjectWithMaxMarksAsync(int schoolId, int sessionId, string examCode, int classGroupId);

        Task<bool> SaveExamTblRemarksEntryAsync(MarksEntryMaster marksEntry);
        Task<bool> SaveExamTblMarksEntryAsync(MarksEntryMaster marksEntry);

        Task<List<ClassH>> GetExamTestCodeAsync(int schoolId, int sessionId);
        Task<List<TestMarksEntryDto>> GetTestMarksDetailsAsync(int schoolId, int sessionId, int classId, int sectionId, string testCode, int subjectId);
        Task<bool> TblSaveTestMarksEntryAsync(TestEntryMaster testmarksEntry);

        Task<List<TestResultResponse>> WebAppGetTestResultAsync(int schoolId, int sessionId, int studentId);
        Task<List<TestResultResponse>> GetTestResultAsync(int schoolId, int sessionId, int classId, int sectionId, int subjectId, int testCode);

        Task<List<ExamNameAndDate>> GetResultNameAndDateAsync(int schoolId, int sessionId);
        Task<bool> GetCheckAllDueClearAsync(int schoolId, int sessionId, int studentId);
        Task<bool> GetCheckStudentMarksEntryAsync(int schoolId, int sessionId, int studentId, string examCode);
        }
    }
