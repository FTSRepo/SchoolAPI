namespace SchoolAPI.Models.Exam
{
    public class GetMessageResponse
    {
        public int Id { get; set; }
        public int Schoolid { get; set; }
        public string imgsignature { get; set; }
        public string Message { get; set; }
        public string ProfileImgDoc { get; set; }
        public string PrincipalName { get; set; }

    }
    public class ExamNameAndDate
    {
        public string ExamName { get; set; }
        public string ExamCode { get; set; }
        public string ResultStartDate { get; set; }
        public string ResultEndDate { get; set; }
    }

    public class GradeRequestModel
    {
        public string StudentIds { get; set; }
        public int SchoolId { get; set; }
        public int SessionId { get; set; }
        public string FileName { get; set; }
        public int ClassId { get; set; }
        public string AdmNo { get; set; }
        public string ExamCode { get; set; }
        public bool IncludeUTMarks { get; set; }
        public string UTExamCode { get; set; }
    }
    public class FileDeleteRequest
    {
        public string FileName { get; set; }
        public string DocName { get; set; }
    }
    public class GetClassDocuments
    {
        public int Id { get; set; }
        public string AddDate { get; set; }
        public string DocumentPublishDate { get; set; }
        public string FileNames { get; set; }
        public string ClassName { get; set; }
        public string SubjectName { get; set; }
    }
    public class GetSchoolToppers
    {
        public int SchoolId { get; set; }
        public int SessionId { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public long Rank { get; set; }
        public string StudentName { get; set; }
        public string ProfileImg { get; set; }
        public decimal Total { get; set; }
    }
    public class ExamMarksEntryResponse
    {
        public int StudentID { get; set; }
        public int RollNo { get; set; }
        public string AdmissionNumber { get; set; }
        public string Name { get; set; }
        public decimal ObtainedMarks { get; set; }
        public decimal NoteBook { get; set; }
        public decimal SubjectEnrichment { get; set; }
        public decimal WrittenMarks { get; set; }
        public decimal OralMarks { get; set; }
        public int Remarks { get; set; }
    }

    public class ExamRemarksEntryResponse
    {
        public int StudentID { get; set; }
        public int RollNo { get; set; }
        public string AdmissionNumber { get; set; }
        public string Name { get; set; }
        public string WorkEducation { get; set; }
        public string ArtEducation { get; set; }
        public string HealthPhysicalEducation { get; set; }
        public string Discipline { get; set; }
        public string Remarks { get; set; }
    }

    public class SubjectM
    {
        public int SubId { get; set; }
        public string SubjectName { get; set; }
        public int MaxMarks { get; set; }
    }

    public class ClassG
    {
        public int SchoolId { get; set; }

        public int ClassGroupId { get; set; }
        public string GroupName { get; set; }
        public string ClassIds { get; set; }
    }

    public class TestMarksEntryResponse
    {
        public int StudentID { get; set; }
        public int RollNo { get; set; }
        public string AdmissionNumber { get; set; }
        public string Name { get; set; }
        public int ObtaindMarks { get; set; }
        public decimal NoteBook { get; set; }
        public decimal SubjectEnrichment { get; set; }
        public decimal WrittenMarks { get; set; }
        public decimal OralMarks { get; set; }
        public int Remarks { get; set; }
    }
    public class ExamMasterResponse
    {
        public string ExamCode { get; set; }
        public string ExamName { get; set; }
        public int PatternId { get; set; }
        public List<EntryPattern> PaternName { get; set; }

    }
    public class EntryPattern
    {
        public string Label { get; set; }
        public string Field { get; set; }
    }
    public class MarksEntryMaster
    {
        public int SchoolId { get; set; }
        public int SessionId { get; set; }
        public string ExamCode { get; set; }
        public int SubjectId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int UserId { get; set; }
        public List<MarksEntryDetail> marksEntryDetails { get; set; }
    }
    public class MarksEntryDetail
    {
        public int StudentId { get; set; }
        public decimal NoteBook { get; set; }
        public decimal SubjectEnrichment { get; set; }
        public decimal ObtainedMarks { get; set; }
        public decimal WrittenMarks { get; set; }
        public decimal OralMarks { get; set; }
        public string Remarks { get; set; }
        public string ArtEducation { get; set; }
        public string WorkEducation { get; set; }
        public string PhysicalEducation { get; set; }
        public string Discipline { get; set; }
        public string ExamName { get; set; }
    }
    public class TestEntryMaster
    {
        public int SchoolId { get; set; }
        public int SessionId { get; set; }
        public string TestCode { get; set; }
        public int SubjectId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int UserId { get; set; }
        public List<TestMarksEntryDetail> marksEntryDetails { get; set; }
    }
    public class TestMarksEntryDetail
    {

        public int StudentId { get; set; }
        public decimal ObtainedMarks { get; set; }
        public string Remarks { get; set; }

    }

    public class APIResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
    }
    public class PrincipalMessageDto
    {
        public int SchoolId { get; set; }

        // Full path for profile image (e.g., "/profile.png")
        public string ProfileImgDoc { get; set; }

        // Principal's message
        public string Message { get; set; }

        // Signature image path
        public string ImgSignature { get; set; }

        // Principal full name
        public string PrincipalName { get; set; }

        // Academic qualification (from StaffAcademicQualification table)
        public string AcademicQualification { get; set; }

        // Professional qualification (from StaffProfessionalQualification table)
        public string ProfessionalQualification { get; set; }
    }
    public class SchoolTopperDto
    {
        public string ClassName { get; set; }          // Class name (e.g., "10th")
        public string SectionName { get; set; }        // Section name (e.g., "A")
        public int Rank { get; set; }                  // Rank (always 1 for toppers, but flexible)
        public string StudentName { get; set; }        // Full student name
        public string ProfileImg { get; set; }         // Profile image (default if not uploaded)
        public int StudentId { get; set; }             // Student unique ID
        public decimal Total { get; set; }             // Total marks scored
    }
    public class ClassDocumentDto
    {
        public int Id { get; set; }                   // Document ID
        public int SubjectId { get; set; }            // Related subject
        public DateTime AddDate { get; set; }         // When added
        public string DocumentPublishDate { get; set; } // Already formatted as dd/MMM/yyyy from SQL
        public string FileNames { get; set; }         // File names (comma-separated if multiple)
        public string ClassName { get; set; }         // Class name
        public int SchoolId { get; set; }             // School ID
        public string SubjectName { get; set; }       // Subject name
    }
    public class StudentExamMarksDto
    {
        public int StudentId { get; set; }              // Student unique ID
        public int RollNo { get; set; }                 // Roll number
        public string AdmissionNumber { get; set; }     // Admission number
        public string Name { get; set; }                // Full student name
        public decimal ObtainedMarks { get; set; }      // Marks obtained
        public decimal NoteBook { get; set; }           // Notebook marks
        public decimal SubjectEnrichment { get; set; }  // Subject enrichment marks
        public decimal WrittenMarks { get; set; }       // Written exam marks
        public decimal OralMarks { get; set; }          // Oral exam marks
        public int Remarks { get; set; }                // Remarks (as int)
    }
    public class StudentExamRemarksDto
    {
        public int StudentId { get; set; }               // Unique student ID
        public int RollNo { get; set; }                  // Roll number
        public string AdmissionNumber { get; set; }      // Admission number
        public string Name { get; set; }                 // Full name
        public string WorkEducation { get; set; }        // Work education grade/remarks
        public string ArtEducation { get; set; }         // Art education grade/remarks
        public string HealthPhysicalEducation { get; set; } // Health & physical education
        public string Discipline { get; set; }           // Discipline grade/remarks
        public string Remarks { get; set; }              // General remarks
    }
    public class ExamMasterByClassIdDto
    {
        public string ExamCode { get; set; }
        public string ExamName { get; set; }
        public int PatternId { get; set; }
        public string PatternName { get; set; }
    }
     
    public class ClassGroupMasterDto
    {
        public int ClassGroupId { get; set; }
        public string GroupName { get; set; }
        public string ClassIds { get; set; }  // CSV from DB, you can split into List<int> if needed
    }
    public class SubjectWithMaxMarksDto
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public decimal MaxMarks { get; set; }
    }
    public class ClassH
    {
        public int TestId { get; set; }
        public string ExamName { get; set; }
        public string ExamCode { get; set; }
    }
    public class TestMarksEntryDto
    {
        public int StudentId { get; set; }
        public int RollNo { get; set; }
        public string AdmissionNumber { get; set; }
        public string Name { get; set; }
        public int ObtainedMarks { get; set; }
        public string Remarks { get; set; }
    }
    public class TestResultResponse
    {
        public int StudentId { get; set; }
        public string FullName { get; set; }
        public string AdmissionNumber { get; set; }
        public int RollNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string ExamName { get; set; }
        public string ExamDate { get; set; }
        public string SubjectName { get; set; }
        public decimal ObtaindMarks { get; set; }
        public int MaxMarks { get; set; }



    }
}
