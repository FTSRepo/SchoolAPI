namespace SchoolAPI.Models.Leave
    {
    public class LeaveRequest
        {
        public int LeaveApId { get; set; }
        public int SchoolId { get; set; }
        public int SessionId { get; set; }
        public int UserId { get; set; }
        public int UserTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NoDays { get; set; }
        public string Remarks { get; set; }
        public bool Status { get; set; }
        public string ApprovalStatus { get; set; }
        public string ApprovalRemark { get; set; }
        public DateTime CreateDate { get; set; }
        public string RequesterName { get; set; }
        }
    }
