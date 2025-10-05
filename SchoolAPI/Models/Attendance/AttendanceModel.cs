namespace SchoolAPI.Models.Attendance
{
    public class AttendanceFilter
    {
        public int SchoolId { get; set; }
        public int SessionId { get; set; }
        public DateTime Sdate { get; set; }
        public DateTime Edate { get; set; }
        public int StudentId { get; set; }
        public int StaffId { get; set; }
        public int ClassId { get; set; }
        public int Sectionid { get; set; }
    }
    public class StudentAttendanceRequestM
    {
        public int SchoolId { get; set; }
        public int SessionId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int UserId { get; set; }
        public string Date { get; set; }
        public List<StudentAttendanceDetailM> lSaveAttendanceDetails { get; set; }
    }
    public class StudentAttendanceDetailM
    {
        public string Name { get; set; }
        //public string StaffCode { get; set; }
        public string Gender { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }
        public string FatherName { get; set; }
        public string AdmNo { get; set; }
        public int RollNo { get; set; }
        public int StudentID { get; set; }
        public string Contact { get; set; }

    }
    public class StaffListRespM
    {
        public string StaffCode { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Gender { get; set; }
        public string Department { get; set; }
        public string CurrentDate { get; set; }
    }
    public class StudentListRespM
    {
        public int StudentId { get; set; }
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string AdmNo { get; set; }
        public string Gender { get; set; }
        public string FatherName { get; set; }

    }
    public class StaffAttendanceRequestM
    {
        public int SchoolId { get; set; }
        public int SessionId { get; set; }
        public int UserId { get; set; }
        public string Date { get; set; }
        public List<StaffAttendanceDetailM> lSaveAttendanceDetails { get; set; }
    }
    public class StaffAttendanceDetailM
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }
        public string Department { get; set; }
        public string Staffcode { get; set; }
        public string Contact { get; set; }
    }
}
