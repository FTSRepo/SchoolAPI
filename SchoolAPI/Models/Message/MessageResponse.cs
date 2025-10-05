namespace SchoolAPI.Models.Message
{
    public class MessageResponse
    {
      public int SchoolId { get; set; }
      public string MessageTxt { get; set; }
      public DateTime Date { get; set; }
    }

    public class NewsResponse
    {
        public string Title{ get; set; }                
        public string Description { get; set; }    
        public string NewsOrEventDate { get; set; }                 
        public DateTime CreatedOn { get; set; }
    }
}
