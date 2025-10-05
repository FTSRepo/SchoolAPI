namespace SchoolAPI.Models.OnelineClass
{
    public class OnlineClassSetupResponse
    {
        public int SetupId { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string Name { get; set; }
        public string SubjectName { get; set; }
        public string Sessiondate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string MeetingLink { get; set; }
    }

    public class OnlineClassSetupRequest
    {
        public int schoolId { get; set; }
        public int SessionId { get; set; }
        public int classId { get; set; }
        public int sectionId { get; set; }
        public int staffId { get; set; }
        public int subjectId { get; set; }
        public DateTime Sessiondate { get; set; }
        public DateTime starttime { get; set; }
        public DateTime endtime { get; set; }
        public string meetingLink { get; set; }
    }
}
