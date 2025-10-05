namespace SchoolAPI.Models.Homework
{
    public class HomeWorkMasterM
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public int SessionId { get; set; }
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public DateTime LastSubmissionDate { get; set; }
        public string LastSubmitDate { get; set; }
        public List<HomeWorkDetailM> LstHomeWorkDetailM { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string StudentIds { get; set; }

    }


    public class HomeWorkDetailM
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Filename { get; set; }
        public byte File { get; set; }
        public string Filepath { get; set; }

    }
    public class HomeWorkResponseM
    {
        public string SubjectName { get; set; }
        public DateTime HomeWorkDate { get; set; }
        public string TeacherName { get; set; }
        public string Question { get; set; }
        public string Filename { get; set; }
    }
}
