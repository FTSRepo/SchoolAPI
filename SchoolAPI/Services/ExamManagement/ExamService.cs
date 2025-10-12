using System.Data;
using System.Reflection.Metadata.Ecma335;
using SchoolAPI.Models.Exam;
using SchoolAPI.Repositories.ExamRepository;

namespace SchoolAPI.Services.ExamManagement
{
    public class ExamService : IExamService
    {
        private readonly IExamRepository _examRepository;
        public ExamService(IExamRepository examRepository)
        {
            _examRepository = examRepository;
        }
        public async Task<List<GetMessageResponse>> GetPrincipalMsgAsync(int schoolId)
        {
            DataTable dt = await _examRepository.GetPrincipalMsgAsync(schoolId);
            List<GetMessageResponse> principalMessages = [];
            foreach (DataRow dr in dt.Rows)
            {
                principalMessages.Add(new GetMessageResponse
                {
                    ProfileImgDoc = dr["ProfileImgDoc"].ToString(),
                    imgsignature  = dr["imgsignature"].ToString(),
                    Message = dr["Message"].ToString(),
                    PrincipalName = dr["PrincipalName"].ToString(),
                    Schoolid = Convert.ToInt32(dr["Schoolid"]),
                });
            }
            return principalMessages;
        }
        public async Task<string> SavePrincipalMsgAsync(int schoolId, string msg)
        {
            return await _examRepository.SavePrincipalMsgAsync(schoolId, msg);
        }
        public async Task<List<SchoolTopperDto>> GetSchoolTopperAsync(int schoolId, int sessionId)
        {
            DataTable dt = await _examRepository.GetSchoolTopperAsync(schoolId, sessionId);
            List<SchoolTopperDto> schoolToppers = [];
            foreach (DataRow dr in dt.Rows)
            {
                schoolToppers.Add(new SchoolTopperDto
                {
                    StudentId = Convert.ToInt32(dr["StudentId"]),
                    StudentName = dr["StudentName"].ToString(),
                    ClassName = dr["ClassName"].ToString(),
                    SectionName = dr["SectionName"].ToString(),
                    Total = Convert.ToDecimal(dr["Total"]),
                    Rank = Convert.ToInt32(dr["Rank"]),
                    ProfileImg = dr["ProfileImg"].ToString(),
                });
            }
            return schoolToppers;
        }
        public async Task<List<ClassDocumentDto>> DownloadClassDocumentAsync(int schoolId, int studentId)
        {
            DataTable dt = await _examRepository.DownloadClassDocumentAsync(schoolId, studentId);
            List<ClassDocumentDto> classDocuments = [];
            foreach (DataRow dr in dt.Rows)
            {
                classDocuments.Add(new ClassDocumentDto
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    SubjectId = Convert.ToInt32(dr["SubjectId"].ToString()),
                    SubjectName = dr["SubjectName"].ToString(),
                    ClassName = dr["ClassName"].ToString(),
                    DocumentPublishDate = dr["DocumentPublishDate"].ToString(),
                    FileNames = dr["FileNames"].ToString(),
                    SchoolId = Convert.ToInt32(dr["SchoolId"]),
                    AddDate = Convert.ToDateTime(dr["AddDate"]),

                });
            }
            return classDocuments;
        }
        public async Task<List<StudentExamMarksDto>> GetStudentListForMarksEntryAsync(int schoolId, int sessionId, int classId, int sectionId, string examCode, int subjectId)
        {
            DataTable dt = await _examRepository.GetStudentListForMarksEntryAsync(schoolId, sessionId, classId, sectionId, examCode, subjectId);
            List<StudentExamMarksDto> studentExamMarks = [];
            foreach (DataRow dr in dt.Rows)
            {
                studentExamMarks.Add(new StudentExamMarksDto
                {
                     StudentId = Convert.ToInt32(dr["StudentId"]),
                     Name = dr["Name"].ToString(),
                     RollNo = Convert.ToInt32(dr["RollNo"].ToString()),
                     AdmissionNumber = dr["AdmissionNumber"].ToString(),
                     NoteBook = Convert.ToInt32(dr["NoteBook"]),
                     ObtainedMarks = Convert.ToDecimal(dr["ObtainedMarks"].ToString()),
                     OralMarks = Convert.ToDecimal(dr["OralMarks"]),
                     SubjectEnrichment = Convert.ToDecimal(dr["SubjectEnrichment"]),
                     WrittenMarks =  Convert.ToDecimal(dr["WrittenMarks"]),
                     Remarks = Convert.ToInt32(dr["Remarks"]),
                });
            }
            return studentExamMarks;
        }
        public async Task<List<StudentExamRemarksDto>> GetStudentListForRemarksEntryAsync(int schoolId, int sessionId, int classId, int sectionId, string examCode)
        {
            DataTable dt = await _examRepository.GetStudentListForRemarksEntryAsync(schoolId, sessionId, classId, sectionId, examCode);
            List<StudentExamRemarksDto> studentExamRemarks = [];
            foreach (DataRow dr in dt.Rows)
            {
                studentExamRemarks.Add(new StudentExamRemarksDto
                {
                    StudentId = Convert.ToInt32(dr["StudentId"]),
                    Name = dr["Name"].ToString(),
                    RollNo = Convert.ToInt32(dr["RollNo"].ToString()),
                    AdmissionNumber = dr["AdmissionNumber"].ToString(),
                    Discipline = dr["Discipline"].ToString(),
                    ArtEducation = dr["ArtEducation"].ToString(),
                    HealthPhysicalEducation = dr["HealthPhysicalEducation"].ToString(),
                    WorkEducation = dr["WorkEducation"].ToString(),
                    Remarks = dr["Remarks"].ToString(),
                });
            }
            return studentExamRemarks;
        }
        public async Task<List<ExamMasterByClassIdDto>> GetExamNameForMarksEntryByClassIdAsync(int schoolId, int classId)
        {
            DataTable dt = await _examRepository.GetExamNameForMarksEntryByClassIdAsync(schoolId, classId);
            List<ExamMasterByClassIdDto> examMasters = [];
            foreach (DataRow dr in dt.Rows)
            {
                examMasters.Add(new ExamMasterByClassIdDto
                {
                    ExamCode = dr["ExamCode"].ToString(),
                    ExamName = dr["ExamName"].ToString(),
                    PatternId = Convert.ToInt32(dr["PatternId"]),
                    PatternName = dr["PatternName"].ToString(),
                });
            }
            return examMasters;
        }
        public async Task<List<ClassGroupMasterDto>> GetClassGroupMasterAsync(int schoolId)
        {
            DataTable dt = await _examRepository.GetClassGroupMasterAsync(schoolId);
            List<ClassGroupMasterDto> classGroups = [];
            foreach (DataRow dr in dt.Rows)
            {
                classGroups.Add(new ClassGroupMasterDto
                {
                    ClassGroupId = Convert.ToInt32(dr["ClassGroupId"]),
                    GroupName = dr["GroupName"].ToString(),
                    ClassIds = dr["ClassIds"].ToString(),
                });
            }
            return classGroups;
        }
        public async Task<List<SubjectWithMaxMarksDto>> GetSubjectWithMaxMarksAsync(int schoolId, int sessionId, string examCode, int classGroupId)
        {
            DataTable dt = await _examRepository.GetSubjectWithMaxMarksAsync(schoolId, sessionId, examCode, classGroupId);
            List<SubjectWithMaxMarksDto> subjects = [];
            foreach (DataRow dr in dt.Rows)
            {
                subjects.Add(new SubjectWithMaxMarksDto
                {
                    SubjectId = Convert.ToInt32(dr["SubjectId"]),
                    SubjectName = dr["SubjectName"].ToString(),
                    MaxMarks = Convert.ToDecimal(dr["MaxMarks"]),
                });
            }
            return subjects;
        }
        public async Task<bool> SaveExamTblRemarksEntryAsync(MarksEntryMaster marksEntry)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[6]
            {
                new DataColumn("StudentId", typeof(int)),
                new DataColumn("WorkEducation", typeof(string)),
                new DataColumn("ArtEducation", typeof(string)),
                new DataColumn("PhysicalEducation", typeof(string)),
                new DataColumn("Discipline", typeof(string)),
                new DataColumn("Remarks", typeof(string))
            });
            foreach (MarksEntryDetail obj in marksEntry.marksEntryDetails)
            {
                dt.Rows.Add(
                  Convert.ToInt32(obj.StudentId),
                  Convert.ToString(obj.WorkEducation),
                  Convert.ToString(obj.ArtEducation),
                  Convert.ToString(obj.PhysicalEducation),
                  Convert.ToString(obj.Discipline),
                  Convert.ToString(obj.Remarks)
                 );
            }
            var result = await _examRepository.SaveExamTblRemarksEntryAsync(marksEntry, dt, marksEntry.UserId).ConfigureAwait(false);
            if (result != null && result.Contains("successfully"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> SaveExamTblMarksEntryAsync(MarksEntryMaster marksEntry)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[7]
            {
                new DataColumn("StudentId", typeof(int)),
                new DataColumn("NoteBook", typeof(decimal)),
                new DataColumn("SubjectEnrchMent", typeof(decimal)),
                new DataColumn("ObtaindMarks", typeof(decimal)),
                new DataColumn("WritnMarks", typeof(decimal)),
                new DataColumn("OralMarks", typeof(decimal)),
                new DataColumn("Remarks", typeof(string))
            });
            foreach (MarksEntryDetail obj in marksEntry.marksEntryDetails)
            {
                dt.Rows.Add(
                  Convert.ToInt32(obj.StudentId),
                  Convert.ToDecimal(obj.NoteBook),
                  Convert.ToDecimal(obj.SubjectEnrichment),
                  Convert.ToDecimal(obj.ObtainedMarks),
                  Convert.ToDecimal(obj.WrittenMarks),
                  Convert.ToDecimal(obj.OralMarks),
                  Convert.ToString(obj.Remarks)
                 );
            }
            var result = await _examRepository.SaveExamTblMarksEntryAsync(marksEntry, dt, marksEntry.UserId).ConfigureAwait(false);
            if (result != null && result.Contains("successfully"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<ClassH>> GetExamTestCodeAsync(int schoolId, int sessionId)
        {
            DataTable dt = await _examRepository.GetExamTestCodeAsync(schoolId, sessionId).ConfigureAwait(false);
            List<ClassH> classHs = [];
            foreach (DataRow dr in dt.Rows)
            {
                classHs.Add(new ClassH
                {
                    ExamCode = dr["ExamCode"].ToString(),
                    ExamName = dr["ExamName"].ToString(),
                });
            }
            return classHs;
        }

        public async Task<List<TestMarksEntryDto>> GetTestMarksDetailsAsync(int schoolId, int sessionId, int classId, int sectionId, string testCode, int subjectId)
        {
            DataTable dt = await _examRepository.GetTestMarksDetailsAsync(schoolId, sessionId, classId, sectionId, testCode, subjectId).ConfigureAwait(false);
            List<TestMarksEntryDto> testMarksEntries = [];
            foreach(DataRow dr in dt.Rows)
            {
                testMarksEntries.Add(new TestMarksEntryDto
                {
                    StudentId = Convert.ToInt32(dr["StudentId"]),
                    Name = dr["Name"].ToString(),
                    RollNo = Convert.ToInt32(dr["RollNo"].ToString()),
                    AdmissionNumber = dr["AdmissionNumber"].ToString(),
                    ObtainedMarks = Convert.ToInt32(dr["ObtainedMarks"].ToString()),
                    Remarks = dr["Remarks"].ToString(),
                });
            }
            return testMarksEntries;
        }

        public async Task<bool> TblSaveTestMarksEntryAsync(TestEntryMaster testmarksEntry)
        {

            DataTable dt = new();
            dt.Columns.AddRange(
            [
                new("StudentId", typeof(int)),
                new("ObtaindMarks", typeof(decimal)),
                new("Remarks", typeof(string))
            ]);

            foreach (TestMarksEntryDetail obj in testmarksEntry.marksEntryDetails)
            {
                dt.Rows.Add(
                                Convert.ToInt32(obj.StudentId),
                                Convert.ToDecimal(obj.ObtainedMarks),
                                Convert.ToString(obj.Remarks)
                                );
            } 
            var result = await _examRepository.TblSaveTestMarksEntryAsync(testmarksEntry, dt, testmarksEntry.UserId).ConfigureAwait(false);
            if (result != null && result.Contains("successfully"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<TestResultResponse>> WebAppGetTestResultAsync(int schoolId, int sessionId, int studentId)
        {
            DataTable dt = await _examRepository.WebAppGetTestResultAsync(schoolId, sessionId, studentId).ConfigureAwait(false);
            List<TestResultResponse> testResults = [];
            foreach (DataRow dr in dt.Rows)
            {
                testResults.Add(new TestResultResponse
                {
                    StudentId = Convert.ToInt32(dr["StudentId"]),
                    ClassName = dr["ClassName"].ToString(),
                    RollNo = Convert.ToInt32(dr["RollNo"].ToString()),
                    AdmissionNumber = dr["AdmissionNumber"].ToString(),
                    ExamName = dr["ExamName"].ToString(),
                    MaxMarks = Convert.ToInt32(dr["MaxMarks"].ToString()),
                    FullName = dr["FullName"].ToString(),
                    ObtaindMarks = Convert.ToInt32(dr["ObtaindMarks"].ToString()),
                    ExamDate = dr["ExamDate"].ToString(),
                    SectionName = dr["SectionName"].ToString(),
                    SubjectName = dr["SubjectName"].ToString(),
                });
            }
            return testResults;
        }

        public async Task<List<TestResultResponse>> GetTestResultAsync(int schoolId, int sessionId, int classId, int sectionId, int subjectId, int testCode)
        {
            DataTable dt = await _examRepository.GetTestResultAsync(schoolId, sessionId, classId, sectionId, subjectId, testCode).ConfigureAwait(false);
           List<TestResultResponse> testResults = [];
            foreach (DataRow dr in dt.Rows)
            {
                testResults.Add(new TestResultResponse
                {
                    StudentId = Convert.ToInt32(dr["StudentId"]),
                    ClassName  = dr["ClassName"].ToString(),
                    RollNo = Convert.ToInt32(dr["RollNo"].ToString()),
                    AdmissionNumber = dr["AdmissionNumber"].ToString(),
                     ExamName  = dr["ExamName"].ToString(),
                    MaxMarks = Convert.ToInt32(dr["MaxMarks"].ToString()),
                    FullName = dr["FullName"].ToString(),
                     ObtaindMarks = Convert.ToInt32(dr["ObtaindMarks"].ToString()),
                    ExamDate = dr["ExamDate"].ToString(),
                    SectionName = dr["SectionName"].ToString(),
                    SubjectName = dr["SubjectName"].ToString(),
                });
            }
            return testResults;
        }

        public async Task<List<ExamNameAndDate>> GetResultNameAndDateAsync(int schoolId, int sessionId)
        {
            DataTable dt = await _examRepository.GetResultNameAndDateAsync(schoolId, sessionId).ConfigureAwait(false);
            List<ExamNameAndDate> examNamesAndDates = [];
            foreach (DataRow dr in dt.Rows)
            {
                examNamesAndDates.Add(new ExamNameAndDate
                {
                    ExamCode = dr["ExamCode"].ToString(),
                    ExamName = dr["ExamName"].ToString(),
                    ResultEndDate = dr["EndDate"].ToString(),
                    ResultStartDate = dr["StartDate"].ToString(),
                });
            }
            return examNamesAndDates;
        }

        public async Task<bool> GetCheckAllDueClearAsync(int schoolId, int sessionId, int studentId)
        {
            DataTable dt = await _examRepository.GetCheckAllDueClearAsync(schoolId, sessionId, studentId).ConfigureAwait(false);

            if (dt.Rows.Count == 0 || dt.Rows[0]["MonthId"] == DBNull.Value)
                return false;

            // Paid months list banayein
            List<string> paidMonths = Convert.ToString(dt.Rows[0]["MonthId"]).Split(',').Distinct().ToList();

            // Calendar month to academic month mapping
            int calendarMonth = DateTime.Now.Month;

            // Convert calendar month to academic month number
            // (April = 1, May = 2, ..., March = 12)
            int academicMonth;
            if (calendarMonth >= 4) // April to December
                academicMonth = calendarMonth - 3;
            else // Jan, Feb, March
                academicMonth = calendarMonth + 9;

            // Check if all months from 1 to (academicMonth - 1) are paid
            for (int month = 1; month < academicMonth; month++)
            {
                if (!paidMonths.Contains(month.ToString()))
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<bool> GetCheckStudentMarksEntryAsync(int schoolId, int sessionId, int studentId, string examCode)
        {
            DataTable dt = await _examRepository.GetCheckStudentMarksEntryAsync(schoolId, sessionId, studentId, examCode).ConfigureAwait(false);
            int MarksEntry = Convert.ToInt32(dt.Rows[0][0]);
            if (MarksEntry > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
