namespace SchoolAPI.Models.Leave
    {
    public class LeaveUpdateRequest
        {
        public int LeaveApId { get; set; }
        public int SchoolId { get; set; }
        public int UserId { get; set; }
        public string ApprovalStatus { get; set; }
        public string ApprovalRemark { get; set; }
        }
    }
