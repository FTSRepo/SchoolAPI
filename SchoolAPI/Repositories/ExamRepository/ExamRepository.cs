using Microsoft.Data.SqlClient;
using SchoolAPI.Infrastructure.Factory;
using SchoolAPI.Models.Exam;
using System.Data;

namespace SchoolAPI.Repositories.ExamRepository
{
    public class ExamRepository(IDbConnectionFactory dbConnectionFactory) : IExamRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;
        private async Task<DataTable> ExecuteSelectCommandAsync(string storedProc, SqlParameter[] parameters)
        {
            using var conn = _dbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand(storedProc, conn) { CommandType = CommandType.StoredProcedure };
            if (parameters != null)
                cmd.Parameters.AddRange(parameters);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            var dt = new DataTable();
            dt.Load(reader);
            return dt;
        }

        private async Task<string> ExecuteNonQueryWithOutputAsync(string storedProc, SqlParameter[] parameters, string outputParamName)
        {
            using var conn = _dbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand(storedProc, conn) { CommandType = CommandType.StoredProcedure };
            if (parameters != null)
                cmd.Parameters.AddRange(parameters);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return cmd.Parameters[outputParamName].Value?.ToString() ?? string.Empty;
        }

        // ---------------- Your Methods Converted ----------------

        public async Task<DataTable> GetPrincipalMsgAsync(int schoolId) =>
            await ExecuteSelectCommandAsync("usp_GetPrincipalMSG", new[] { new SqlParameter("@SchoolId", schoolId) });

        public async Task<string> SavePrincipalMsgAsync(int schoolId, string msg)
        {
            var parameters = new[]
            {
                    new SqlParameter("@SchoolId", schoolId),
                new SqlParameter("@Msg", msg),
                new SqlParameter("@msge", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output }
                };
            return await ExecuteNonQueryWithOutputAsync("Usp_SavePrincipalMSG", parameters, "@msge");
        }

        public async Task<DataTable> GetSchoolTopperAsync(int schoolId, int sessionId) =>
            await ExecuteSelectCommandAsync("USP_GetSchoolTopers",
                new[] { new SqlParameter("@SchoolId", schoolId), new SqlParameter("@SessionId", sessionId) });

        public async Task<DataTable> DownloadClassDocumentAsync(int schoolId, int studentId) =>
            await ExecuteSelectCommandAsync("usp_GetClassDoc",
                new[] { new SqlParameter("@SchoolId", schoolId), new SqlParameter("@StudentId", studentId) });

        public async Task<DataTable> GetStudentListForMarksEntryAsync(int schoolId, int sessionId, int classId, int sectionId, string examCode, int subjectId) =>
            await ExecuteSelectCommandAsync("usp_getStudentForExam",
                new[]
                {
                new SqlParameter("@SchoolId", schoolId),
                new SqlParameter("@SessionId", sessionId),
                new SqlParameter("@ClassId", classId),
                new SqlParameter("@SectionId", sectionId),
                new SqlParameter("@ExamCode", examCode),
                new SqlParameter("@SubjectId", subjectId)
                });

        public async Task<DataTable> GetStudentListForRemarksEntryAsync(int schoolId, int sessionId, int classId, int sectionId, string examCode) =>
            await ExecuteSelectCommandAsync("usp_getStudentForExamRemarks",
                new[]
                {
                new SqlParameter("@SchoolId", schoolId),
                new SqlParameter("@SessionId", sessionId),
                new SqlParameter("@ClassId", classId),
                new SqlParameter("@SectionId", sectionId),
                new SqlParameter("@ExamCode", examCode)
                });

        public async Task<DataTable> GetExamNameForMarksEntryByClassIdAsync(int schoolId, int classId) =>
            await ExecuteSelectCommandAsync("usp_getExamMasterByClassId",
                new[] { new SqlParameter("@SchoolId", schoolId), new SqlParameter("@ClassId", classId) });

        public async Task<DataTable> GetClassGroupMasterAsync(int schoolId) =>
            await ExecuteSelectCommandAsync("GetClassGroupMaster", new[] { new SqlParameter("@SchoolId", schoolId) });

        public async Task<DataTable> GetSubjectWithMaxMarksAsync(int schoolId, int sessionId, string examCode, int classGroupId) =>
            await ExecuteSelectCommandAsync("usp_getSubjectWithMaxMarks",
                new[]
                {
                new SqlParameter("@SchoolId", schoolId),
                new SqlParameter("@SessionId", sessionId),
                new SqlParameter("@ExamCode", examCode),
                new SqlParameter("@ClassGroupId", classGroupId)
                });

        public async Task<string> SaveExamTblRemarksEntryAsync(MarksEntryMaster marksEntry, DataTable dt, int userId)
        {
            var parameters = new[]
            {
            new SqlParameter("@SchoolId", marksEntry.SchoolId),
            new SqlParameter("@SessionId", marksEntry.SessionId),
            new SqlParameter("@ExamCode", marksEntry.ExamCode),
            new SqlParameter("@UserId", userId),
            new SqlParameter("@MarksEntryDetails", dt) { SqlDbType = SqlDbType.Structured },
            new SqlParameter("@Msg", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output }
        };
            return await ExecuteNonQueryWithOutputAsync("usp_Save_Exam_tbl_ExamRemarks", parameters, "@Msg");
        }

        public async Task<string> SaveExamTblMarksEntryAsync(MarksEntryMaster marksEntry, DataTable dt, int userId)
        {
            var parameters = new[]
            {
            new SqlParameter("@SchoolId", marksEntry.SchoolId),
            new SqlParameter("@SessionId", marksEntry.SessionId),
            new SqlParameter("@ExamCode", marksEntry.ExamCode),
            new SqlParameter("@SubjectId", marksEntry.SubjectId),
            new SqlParameter("@ClassId", marksEntry.ClassId),
            new SqlParameter("@SectionId", marksEntry.SectionId),
            new SqlParameter("@UserId", userId),
            new SqlParameter("@MarksEntryDetails", dt) { SqlDbType = SqlDbType.Structured },
            new SqlParameter("@Msg", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output }
        };
            return await ExecuteNonQueryWithOutputAsync("usp_Exam_saveMarksEntry", parameters, "@Msg");
        }

        public async Task<DataTable> GetExamTestCodeAsync(int schoolId, int sessionId) =>
            await ExecuteSelectCommandAsync("Usp_GetTest_ExamMaster",
                new[] { new SqlParameter("@SchoolId", schoolId), new SqlParameter("@SessionId", sessionId) });

        public async Task<DataTable> GetTestMarksDetailsAsync(int schoolId, int sessionId, int classId, int sectionId, string testCode, int subjectId) =>
            await ExecuteSelectCommandAsync("Usp_GetTestMarksEntry",
                new[]
                {
                new SqlParameter("@SchoolId", schoolId),
                new SqlParameter("@SessionId", sessionId),
                new SqlParameter("@ClassId", classId),
                new SqlParameter("@SectionId", sectionId),
                new SqlParameter("@TestCode", testCode),
                new SqlParameter("@SubjectId", subjectId)
                });

        public async Task<string> TblSaveTestMarksEntryAsync(TestEntryMaster testmarksEntry, DataTable dt, int userId)
        {
            var parameters = new[]
            {
            new SqlParameter("@SchoolId", testmarksEntry.SchoolId),
            new SqlParameter("@SessionId", testmarksEntry.SessionId),
            new SqlParameter("@TestCode", testmarksEntry.TestCode),
            new SqlParameter("@SubjectId", testmarksEntry.SubjectId),
            new SqlParameter("@ClassId", testmarksEntry.ClassId),
            new SqlParameter("@SectionId", testmarksEntry.SectionId),
            new SqlParameter("@UserId", userId),
            new SqlParameter("@testmarksEntry", dt) { SqlDbType = SqlDbType.Structured },
            new SqlParameter("@Msg", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output }
        };
            return await ExecuteNonQueryWithOutputAsync("Usp_SaveTestMarksEntry", parameters, "@Msg");
        }

        public async Task<DataTable> WebAppGetTestResultAsync(int schoolId, int sessionId, int studentId) =>
            await ExecuteSelectCommandAsync("Usp_GetTestResultByStudentId",
                new[] { new SqlParameter("@SchoolId", schoolId), new SqlParameter("@SessionId", sessionId), new SqlParameter("@StudentId", studentId) });

        public async Task<DataTable> GetTestResultAsync(int schoolId, int sessionId, int classId, int sectionId, int subjectId, int testCode) =>
            await ExecuteSelectCommandAsync("Usp_GetTestResult",
                new[]
                {
                new SqlParameter("@SchoolId", schoolId),
                new SqlParameter("@SessionId", sessionId),
                new SqlParameter("@ClassId", classId),
                new SqlParameter("@SectionId", sectionId),
                new SqlParameter("@SubjectId", subjectId),
                new SqlParameter("@TestCode", testCode)
                });

        public async Task<DataTable> GetResultNameAndDateAsync(int schoolId, int sessionId) =>
            await ExecuteSelectCommandAsync("usp_getexamtodownloadgradcard",
                new[] { new SqlParameter("@schoolId", schoolId), new SqlParameter("@sessionId", sessionId) });

        public async Task<DataTable> GetCheckAllDueClearAsync(int schoolId, int sessionId, int studentId) =>
            await ExecuteSelectCommandAsync("USP_GetCheckAllDueClear",
                new[]
                {
                new SqlParameter("@schoolId", schoolId),
                new SqlParameter("@sessionId", sessionId),
                new SqlParameter("@studentID", studentId)
                });

        public async Task<DataTable> GetCheckStudentMarksEntryAsync(int schoolId, int sessionId, int studentId, string examCode) =>
            await ExecuteSelectCommandAsync("USP_GetCheckStudentMarksEntry",
                new[]
                {
                new SqlParameter("@schoolId", schoolId),
                new SqlParameter("@sessionId", sessionId),
                new SqlParameter("@studentID", studentId),
                new SqlParameter("@examCode", examCode)
                });
    }
}
