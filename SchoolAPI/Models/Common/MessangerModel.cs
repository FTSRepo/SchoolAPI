using System;
using System.Collections.Generic;

namespace SchoolAPI.Models.Common
{
    public class MessageData
    {
        public string Number { get; set; }
        public string MessageId { get; set; }
        public string Message { get; set; }
    }

    public class smsDetails
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string JobId { get; set; }
        public string Balance { get; set; }
        public List<MessageData> MessageData { get; set; }
    }

    public class SMSStatusResponse
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string MessageId { get; set; }
        public List<DeliveryReport> DeliveryReports { get; set; }
    }

    public class DeliveryReport
    {
        public string MessageId { get; set; }
        public string DeliveryStatus { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int DeliveryCount { get; set; }
    }

    public class SendMessengerM
    {
        public string ToEmailID { get; set; }
        public string FromEmailID { get; set; }
        public int SerialNumber { get; set; }
        public string EmailSubject { get; set; }
        public int SchoolId { get; set; }
        public int GroupId { get; set; }
        public int TemplateId { get; set; }
        public string TDesc { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int MemberId { get; set; }
        public int TransactionId { get; set; }
        public string Remarks { get; set; }
        public string Attachement { get; set; }

        public DateTime CreatedBy { get; set; } = new DateTime(1900, 1, 1);
        public DateTime ModifiedBy1 { get; set; } = new DateTime(1900, 1, 1);
        public DateTime CreatedOn { get; set; } = new DateTime(1900, 1, 1);
        public DateTime ModifiedOn1 { get; set; } = new DateTime(1900, 1, 1);
    }

    public class EmailDetail
    {
        public string ToEmailID { get; set; }
        public string FromEmailID { get; set; }
        public string EmailSubject { get; set; }
        public string SchoolName { get; set; }
        public string EmailBody { get; set; }
        public string Remarks { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
    }
}
