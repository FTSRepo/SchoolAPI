namespace SchoolAPI.Models.Free
{
    public class OnlineCreateOrderRequest
    {
        public int SchoolId { get; set; }
        public int SessionId { get; set; }
        public int StudentId { get; set; }
        public string MonthIds { get; set; }
        public int Amount { get; set; }

    }

    public class OnlineOrderStatusRequest
    {
        public string razorpay_order_id { get; set; }
        public string razorpay_payment_id { get; set; }
        public string razorpay_signature { get; set; }
        public int status_code { get; set; }
        public int SchoolId { get; set; }
    }

    public class OnlineUpdateOrderRequest
    {
        public string id { get; set; }
        public string entity { get; set; }
        public int amount { get; set; }
        public int amount_paid { get; set; }
        public int amount_due { get; set; }
        public string currency { get; set; }
        public string receipt { get; set; }
        public string status { get; set; }
        public int attempts { get; set; }
        public string notes { get; set; }
        public string created_at { get; set; }
    }

    public class OnlinePaymentOrderRequest
    {
        public int SchoolId { get; set; }
        public int StudentId { get; set; }
        public string RegistrationNumber { get; set; }
        public string OrderId { get; set; }
        public int OrderAmount { get; set; }
        public string OrderStatus { get; set; }
        public string OrderRemark { get; set; }
        public string PaymentId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
    public class OnlinePaymentRequest
    {
        public string Regno { get; set; }
        public string PaymentId { get; set; }
        public int SchoolId { get; set; }
        public string PaymentStatus { get; set; }
        public string OrderId { get; set; }
    }

    public class FeeDetailsResponse
    {
        public string BillNo { get; set; }
        public int Amount { get; set; }
        public int BalanceAmount { get; set; }
        public string PayMode { get; set; }
        public string PaidDate { get; set; }
        public string PaymentMonth { get; set; }
    }
    public class SubmitFeeReceiptM
    {
        public int StudentId { get; set; }
        public string AdmNo { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public string Father { get; set; }
        public string Mother { get; set; }
        public string RollNo { get; set; }
        public string ContactNo { get; set; }
        public List<PaymentDetails> PaymentDetails { get; set; }
        public OldBalance oldBalance { get; set; }
        public List<MonthName> MonthNames { get; set; }
        public List<SubmitPaymnet> submitPaymnets { get; set; }

    }
    public class OldBalance
    {
        public int FeeHeadId { get; set; }
        public string FeeHeadName { get; set; }
        public int FeeAmount { get; set; }
        public string MonthId { get; set; }
    }
    public class MonthName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Submit { get; set; }
    }
    public class PaymentDetails
    {
        public string BillNo { get; set; }
        public string PayMonth { get; set; }
        public decimal PayAmount { get; set; }
        public decimal PrvBalance { get; set; }
        public decimal Discount { get; set; }
        public decimal Concession { get; set; }
        public decimal PaidAmt { get; set; }
        public decimal CurentBal { get; set; }
        public string PayMode { get; set; }
        public string PmtDate { get; set; }
        public string MonthId { get; set; }
    }
    public class SubmitPaymnet
    {
        public int FeeHeadMasterId { get; set; }
        public string FeeHeadName { get; set; }
        public decimal Amount { get; set; }
        public decimal Concession { get; set; }
        public decimal Disscount { get; set; }
        public decimal Payable { get; set; }
        public decimal Balance { get; set; }

    }
    public class OnlineCreateOrderResponse
    {
        public int SchoolId { get; set; }
        public int StudentId { get; set; }
        public int Amount { get; set; }
        public string ReceiptNo { get; set; }
        public string Currency { get; set; }
        public APIGatewayCredentials gatewayCredentials { get; set; }
    }
    public class APIGatewayCredentials
    {
        public string Key_Id { get; set; }
        public string Key_Secret { get; set; }
    }

    public class TodaysCollectionDto
    {
        public string Name { get; set; }            // Student full name
        public decimal Amount { get; set; }         // Sum of Paid Amount
        public string AddDate { get; set; }         // Date in "dd/MM/yyyy" format
    }
    public class TodaysCollectionReportDto
    {
        public int StudentId { get; set; }
        public int RollNo { get; set; }
        public string AdmissionNumber { get; set; }
        public string Class { get; set; }
        public string MobileNumber { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string BillNo { get; set; }
        public decimal TotalFeeAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal Concession { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal TranAmt { get; set; }
        public decimal BalanceAmount { get; set; }
        public string PaymentMonth { get; set; }
        public bool IsActive { get; set; }
        public string AddedBy { get; set; }
    }
    public class DaywiseCollectionDto
    {
        public decimal Cash { get; set; }
        public decimal Bank { get; set; }
        public decimal Amount { get; set; }
        public string TranDate { get; set; } // "dd/MM/yyyy" or "Total"
    }

}
