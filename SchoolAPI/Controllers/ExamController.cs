using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchoolAPI.Models.Exam;
using SchoolAPI.Services.ExamManagement;

namespace SchoolAPI.Controllers
    {
    [ApiController]
    public class ExamController(IExamService examService) : ControllerBase
        {
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        private string examAPI = "https://schoolexamapi.friensys.com/";
        private readonly IExamService _examService = examService;

        [Route("api/GetPrincipalMessage")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetPrincipalMessage(int schoolId)
            {
            List<GetMessageResponse> getMessages = await _examService.GetPrincipalMsgAsync(schoolId).ConfigureAwait(false);
            if ( getMessages.Count > 0 )
                {
                return Ok(new { Data = getMessages, Status = true });
                }
            else
                {
                return NotFound(new { Status = false, Message = "No message found." });
                }
            }

        [Route("api/SavePrincipalMessage")]
        [HttpGet]
        public async Task<IActionResult> SavePrincipalMessage(int schoolId, string message)
            {
            string result = await _examService.SavePrincipalMsgAsync(schoolId, message).ConfigureAwait(false);
            if ( result != null && result.Contains("success") )
                {
                return Ok(new { Status = true, Message = result });
                }
            else
                {
                return NotFound(new { Status = false, Message = "Message not saved." });
                }
            }

        [Route("api/GetSchoolToppers")]
        [HttpGet]
        public async Task<IActionResult> GetSchoolToppers(int schoolId, int sessionId)
            {
            List<SchoolTopperDto> result = await _examService.GetSchoolTopperAsync(schoolId, sessionId).ConfigureAwait(false);
            if ( result != null )
                {
                return Ok(new { Data = result, Status = true });
                }
            else
                {
                return NotFound(new { Status = false, Message = "No topper found." });
                }
            }

        [Route("api/GetBooks")]
        [HttpGet]
        public async Task<IActionResult> GetBooks(int schoolId, int studentId)
            {
            List<ClassDocumentDto> result = await _examService.DownloadClassDocumentAsync(schoolId, studentId).ConfigureAwait(false);
            if ( result != null )
                {
                return Ok(new { Data = result, Status = true });
                }
            else
                {
                return NotFound(new { Status = false, Message = "No document found." });
                }
            }

        [Route("api/GetStudentsForMarksEntry")]
        [HttpGet]
        public async Task<IActionResult> GetStudentsForMarksEntry(int schoolId, int sessionId, int classId, int sectionId, int subjectId, string examCode)
            {
            try
                {
                List<StudentExamMarksDto> result = await _examService.GetStudentListForMarksEntryAsync(schoolId, sessionId, classId, sectionId, examCode, subjectId).ConfigureAwait(false);
                if ( result != null )
                    {
                    return Ok(new { Data = result, Status = true });
                    }
                else
                    {
                    return NotFound(new { Status = false, Message = "No student found." });
                    }
                }
            catch ( Exception ex )
                {
                return StatusCode(500, new { Status = false, Message = ex.Message.ToString() });
                }
            }

        [Route("api/GetStudentsForRemaksEntry")]
        [HttpGet]
        public async Task<IActionResult> GetStudentsForRemakrsEntry(int schoolId, int sessionId, int classId, int sectionId, string examCode)
            {
            try
                {
                List<StudentExamRemarksDto> result = await _examService.GetStudentListForRemarksEntryAsync(schoolId, sessionId, classId, sectionId, examCode).ConfigureAwait(false);
                if ( result != null )
                    {
                    return Ok(new { Data = result, Status = true });
                    }
                else
                    {
                    return NotFound(new { Status = false, Message = "No student found." });
                    }
                }
            catch ( Exception ex )
                {
                return StatusCode(500, new { Status = false, Message = ex.Message.ToString() });
                }
            }

        [Route("api/GetExamNameForMarksEntryByClassId")]
        [HttpGet]
        public async Task<IActionResult> GetExamNameForMarksEntryByClassId(int schoolId, int classId)
            {
            try
                {
                List<ExamMasterByClassIdDto> result = await _examService.GetExamNameForMarksEntryByClassIdAsync(schoolId, classId).ConfigureAwait(false);
                if ( result != null )
                    {
                    return Ok(new { Data = result, Status = true });
                    }
                else
                    {
                    return NotFound(new { Status = false, Message = "No exam found." });
                    }
                }
            catch ( Exception ex )
                {
                return StatusCode(500, new { Status = false, Message = ex.Message.ToString() });
                }
            }

        [Route("api/GetClassGroup")]
        [HttpGet]
        public async Task<IActionResult> GetClassGroup(int schoolId)
            {
            try
                {
                List<ClassGroupMasterDto> result = await _examService.GetClassGroupMasterAsync(schoolId).ConfigureAwait(false);
                if ( result != null )
                    {
                    return Ok(new { Data = result, Status = true });
                    }
                else
                    {
                    return NotFound(new { Status = false, Message = "No class group found." });
                    }
                }
            catch ( Exception ex )
                {
                return StatusCode(500, new { Status = false, Message = ex.Message.ToString() });
                }
            }

        [Route("api/GetSubjectWithMaxMarks")]
        [HttpGet]
        public async Task<IActionResult> GetSubjectWithMaxMarks(int schoolId, int sessionId, string examCode, int classGroupId)
            {
            try
                {
                List<SubjectWithMaxMarksDto> result = await _examService.GetSubjectWithMaxMarksAsync(schoolId, sessionId, examCode, classGroupId).ConfigureAwait(false);
                if ( result != null )
                    {
                    return Ok(new { Data = result, Status = true });
                    }
                else
                    {
                    return NotFound(new { Status = false, Message = "No subject found." });
                    }
                }
            catch ( Exception ex )
                {
                return StatusCode(500, new { Status = false, Message = ex.Message.ToString() });
                }
            }

        [Route("api/SaveCoScolasticMarks")]
        [HttpPost]
        public async Task<IActionResult> SaveCoScolasticMarks(MarksEntryMaster marksEntry)
            {
            try
                {
                bool result = await _examService.SaveExamTblRemarksEntryAsync(marksEntry).ConfigureAwait(false);
                if ( result )
                    {
                    return Ok(new { Status = true, Message = "Remarks saved successfully." });
                    }
                else
                    {
                    return NotFound(new { Status = false, Message = "Remarks not saved." });
                    }
                }
            catch ( Exception ex )
                {
                return StatusCode(500, new { Status = false, Message = ex.Message.ToString() });
                }
            }

        [Route("api/SaveScolasticMarks")]
        [HttpPost]
        public async Task<IActionResult> SaveScolasticMarks(MarksEntryMaster marksEntry)
            {
            try
                {
                bool result = await _examService.SaveExamTblMarksEntryAsync(marksEntry).ConfigureAwait(false);
                if ( result )
                    {
                    return Ok(new { Status = true, Message = "Marks saved successfully." });
                    }
                else
                    {
                    return NotFound(new { Status = false, Message = "Marks not saved." });
                    }
                }
            catch ( Exception ex )
                {
                return StatusCode(500, new { Status = false, Message = ex.Message.ToString() });
                }
            }

        [Route("api/GetTestExamName")]
        [HttpGet]
        public async Task<IActionResult> GetTestExamName(int schoolId)
            {
            List<ClassH> dClass = await _examService.GetExamTestCodeAsync(schoolId, 0).ConfigureAwait(false);
            if ( dClass != null )
                {
                return Ok(new { Data = dClass, Status = true });
                }
            else
                {
                return NotFound(new { Status = false, Message = "No test found." });
                }
            }

        [Route("api/GetStudentsForTestExamMarksEntry")]
        [HttpGet]
        public async Task<IActionResult> GetStudentsForTestExamMarksEntry(int schoolId, int sessionId, int classId, int SectionId, string TestCode, int SubjectId)
            {
            try
                {
                List<TestMarksEntryDto> dt = await _examService.GetTestMarksDetailsAsync(schoolId, sessionId, classId, SectionId, TestCode, SubjectId).ConfigureAwait(false);
                if ( dt != null )
                    {
                    return Ok(new { Data = dt, Status = true });
                    }
                else
                    {
                    return NotFound(new { Status = false, Message = "No student found." });
                    }
                }
            catch ( Exception ex )
                {
                return StatusCode(500, new { Status = false, Message = ex.Message.ToString() });
                }
            }

        [Route("api/SaveTestExamMarks")]
        [HttpPost]
        public async Task<IActionResult> SaveTestExamMarks(TestEntryMaster testMarksDetails)
            {
            try
                {
                bool result = await _examService.TblSaveTestMarksEntryAsync(testMarksDetails).ConfigureAwait(false);
                if ( result )
                    {
                    return Ok(new { Status = true, Message = "Test marks saved successfully." });
                    }
                else
                    {
                    return NotFound(new { Status = false, Message = "Test marks not saved." });
                    }

                }
            catch ( Exception ex )
                {
                return StatusCode(500, new { Status = false, Message = ex.Message.ToString() });
                }
            }
        [Route("api/GetStudentTestExamResultByStudentId")]
        [HttpGet]
        public async Task<IActionResult> GetStudentTestExamResultByStudentId(int schoolId, int sessionId, int studentId)
            {
            try
                {
                List<TestResultResponse> dt = await _examService.WebAppGetTestResultAsync(schoolId, sessionId, studentId).ConfigureAwait(false);
                if ( dt != null )
                    {
                    return Ok(new { Data = dt, Status = true });
                    }
                else
                    {
                    return NotFound(new { Status = false, Message = "No result found." });
                    }
                }
            catch ( Exception ex )
                {
                return StatusCode(500, new { Status = false, Message = ex.Message.ToString() });
                }
            }

        [Route("api/GetTestExamResultByTestClassSubject")]
        [HttpGet]
        public async Task<IActionResult> GetTestExamResultByTestClassSubject(int SchoolId, int SessionId, int ClassId, int SectionId, int SubjectId, int TestCode)
            {
            try
                {
                List<TestResultResponse> dt = await _examService.GetTestResultAsync(SchoolId, SessionId, ClassId, SectionId, SubjectId, TestCode).ConfigureAwait(false);
                if ( dt != null )
                    {
                    return Ok(new { Data = dt, Status = true });
                    }
                else
                    {
                    return NotFound(new { Status = false, Message = "No result found." });
                    }
                }
            catch ( Exception ex )
                {
                return StatusCode(500, new { Status = false, Message = ex.Message.ToString() });
                }
            }

        [Route("api/GetResultNameAndDate")]
        [HttpGet]
        public async Task<IActionResult> GetResultNameAndDate(int schoolId, int sessionId)
            {
            try
                {
                var dt = await _examService.GetResultNameAndDateAsync(schoolId, sessionId).ConfigureAwait(false);
                if ( dt != null )
                    {
                    return Ok(new { Data = dt, Status = true });
                    }
                else
                    {
                    return NotFound(new { Status = false, Message = "No result found." });
                    }
                }
            catch ( Exception ex )
                {
                return StatusCode(500, new { Status = false, Message = ex.Message.ToString() });
                }
            }
        [Route("api/CheckStudentDues")]
        [HttpGet]
        public async Task<IActionResult> CheckStudentDues(int schoolId, int sessionId, int studentId)
            {

            try
                {
                bool result = await _examService.GetCheckAllDueClearAsync(schoolId, sessionId, studentId).ConfigureAwait(false);
                if ( result )
                    {
                    return Ok(new { Status = true, Message = "All dues are cleared." });
                    }
                else
                    {
                    return Ok(new { Status = false, Message = "Student has pending dues. Please clear the dues before proceeding." });
                    }
                }
            catch ( Exception ex )
                {
                return StatusCode(500, new { Status = false, Message = ex.Message.ToString() });
                }
            }

        [Route("api/CheckMarksEntryExist")]
        [HttpGet]
        public async Task<IActionResult> CheckMarksEntryExist(int schoolId, int sessionId, int studentId, string examCode)
            {

            try
                {
                bool result = await _examService.GetCheckStudentMarksEntryAsync(schoolId, sessionId, studentId, examCode).ConfigureAwait(false);
                if ( result )
                    {
                    return Ok(new { Status = true, Message = "Marks entry exists for the student." });
                    }
                else
                    {
                    return Ok(new { Status = false, Message = "No marks entry found for the student." });
                    }
                }
            catch ( Exception ex )
                {
                return StatusCode(500, new { Status = false, Message = ex.Message.ToString() });
                }
            }

        [Route("api/DownloadGradeCard")]
        [HttpPost]
        public async Task<APIResponse> DownloadGradCardAsync(GradeRequestModel gradeRequest)
            {
            APIResponse aPIResponse = new();
            try
                {
                HttpResponseMessage response = null;
                using ( var client = new HttpClient() )
                    {
                    client.BaseAddress = new Uri(examAPI);
                    client.Timeout = TimeSpan.FromMinutes(10);
                    response = await client.PostAsJsonAsync("api/DownloadGradeCard", gradeRequest).ConfigureAwait(false);

                    if ( response.IsSuccessStatusCode )
                        {
                        var jsonString = response.Content.ReadAsStringAsync();
                        aPIResponse = JsonConvert.DeserializeObject<APIResponse>(jsonString.Result);

                        }
                    else
                        {
                        aPIResponse.Url = "";
                        aPIResponse.Status = false;
                        aPIResponse.Message = "";
                        }
                    }
                }
            catch ( Exception Ex )
                {
                aPIResponse.Message = Ex.Message.ToString();
                aPIResponse.Status = false;
                }
            return aPIResponse;
            }

        [Route("api/DeleteFiles")]
        [HttpPost]
        public void DeleteFiles(FileDeleteRequest fileDeleteRequest)
            {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(examAPI);
            var response = client.PostAsJsonAsync("api/deleteFiles", fileDeleteRequest).Result;
            }
        }
    }
