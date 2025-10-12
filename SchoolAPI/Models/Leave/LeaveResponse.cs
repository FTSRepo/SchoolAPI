namespace SchoolAPI.Models.Leave
    {
    public class LeaveResponse
        {
        public string Name { get; set; }
        public int LeaveId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int NoDays { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public string ApprovalRemark { get; set; }
        public DateTime AddDate { get; set; }
        }
    }
