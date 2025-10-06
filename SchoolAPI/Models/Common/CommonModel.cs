namespace SchoolAPI.Models.Common
{
    public class Filters
    {
        public int SchoolId { get; set; }
        public int ClassId { get; set; }
        public int UserId { get; set; }
    }
    public class SectionM
    {
        public int SectionId { get; set; }
        public string SectionName { get; set; }
    }
    public class ClassH
    {
        public int TestId { get; set; }
        public string ExamName { get; set; }
        public string ExamCode { get; set; }
    }
    public class BusM
    {
        public int Id { get; set; }
        public string DriverName { get; set; }
        public string name { get; set; }
        public string StaffCode { get; set; }
    }
    public class BusDriverM
    {
        public string name { get; set; }
        public string VehicleRegNo { get; set; }
    }

    public class ClassM
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int SectionId { get; set; }
        public int StaffId { get; set; }
        public string FirstName { get; set; }
        public string ExamCode { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
    }
    public class ExamMaserM
    {
        public string ExamName { get; set; }
        public string ExamCode { get; set; }
    }

    public class ClassGroupM
    {
        public string GroupName { get; set; }
        public int ClassGroupId { get; set; }
    }

    public class ExamNExamMaster
    {
        public string ExamName { get; set; }
        public string ExamCode { get; set; }
        public string PatternName { get; set; }
        public string PaternCode { get; set; }
    }
    public class SessionRequest
    {
        public int Id { get; set; }
        public string SessionName { get; set; }

    }

    public class ClassG
    {
        public int SchoolId { get; set; }

        public int ClassGroupId { get; set; }
        public string GroupName { get; set; }
        public string ClassIds { get; set; }
    }

    public class AssessmentM
    {
        public int AssessmentId { get; set; }
        public string ExamTypeName { get; set; }
        public int ExamTypeId { get; set; }
    }

    public class SubjectM
    {
        public int SubId { get; set; }
        public string SubjectName { get; set; }
        public int MaxMarks { get; set; }
    }

    public class FAtypeM
    {
        public int faSubId { get; set; }
        public int faTypeId { get; set; }
        public string FAtype { get; set; }
    }
    public class GradeM
    {
        public int gsId { get; set; }
        public string GradeSetName { get; set; }
    }

    public class StudentBirthday
    {
        public string Name { get; set; }
        public string DOB { get; set; }
        public string profilImg { get; set; }

    }
    public class StudentsOnLeave
    {
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Remark { get; set; }
        public int NoOfDays { get; set; }
        public string RequestedOn { get; set; }
        public string ApprovalStatus { get; set; }

    }
    public class Holidays
    {
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string Remarks { get; set; }
    }
    public class StudentProfile
    {
        public string StudentName { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string AdmNo { get; set; }
        public string DOB { get; set; }
        public string ProfileImg { get; set; }
        public string ClassTeacher { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Mobile { get; set; }
    }
    public class StaffProfile
    {
        public string StaffName { get; set; }
        public string EmpCode { get; set; }
        public string DOB { get; set; }
        public string DOJ { get; set; }
        public string Mobile { get; set; }
        public string ProfileImg { get; set; }
    }
    public class EnquiryM
    {
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string EnquiryType { get; set; }
        public int SchoolId { get; set; }
        public string EnquiryDate { get; set; }
        public int EnquiryId { get; set; }
    }

    public class APPVersionM
    {
        public decimal CurrentVersion { get; set; }
        public string APPURL { get; set; }
        public string Message { get; set; }
    }

    public class StudentDairyRequest
    {
        public string StudentId { get; set; }
        public string Message { get; set; }
        public string SendDate { get; set; }
        public int SchoolId { get; set; }
        public int SessionId { get; set; }
        public int StaffId { get; set; }
        public string requestType { get; set; }
    }
    public class StudentDairyResponse
    {
        public int StudentId { get; set; }
        public int StaffId { get; set; }
        public string StudentName { get; set; }
        public string Message { get; set; }
        public string SendDate { get; set; }
        public string StaffName { get; set; }
        public string requestType { get; set; }
    }

    public class NewsorEventResponse
    {
        public int Id { get; set; }
        public string Category { get; set; } //News or Event
        public string ActionDate { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public bool IsPublished { get; set; }
        public string Organizer { get; set; }
        public string Attender { get; set; }
    }
    public class NewsorEventRequest
    {
        public string Category { get; set; } //News or Event
        public string Message { get; set; }
        public string Title { get; set; }
        public int SchoolId { get; set; }
        public int SessionId { get; set; }
        public int UserId { get; set; }
        public string NewsOrEventDate { get; set; }
    }



    public class CommonM
    {
        private int countryId = 0;
        private string countryName = "";
        private string countryAbbriviation = "";

        private bool isActive = false;

        private int stateId = 0;
        private string stateName = "";
        private string stateAbbriviation = "";

        private int cityId = 0;
        private string cityName = "";
        private string cityAbbriviation = "";

        private int pincodeId = 0;
        private string pincodeName = "";

        public int CountryId
        {
            get
            {
                return countryId;
            }

            set
            {
                countryId = value;
            }
        }

        public string CountryName
        {
            get
            {
                return countryName;
            }

            set
            {
                countryName = value;
            }
        }

        public string CountryAbbriviation
        {
            get
            {
                return countryAbbriviation;
            }

            set
            {
                countryAbbriviation = value;
            }
        }

        public bool IsActive
        {
            get
            {
                return isActive;
            }

            set
            {
                isActive = value;
            }
        }

        public int StateId
        {
            get
            {
                return stateId;
            }

            set
            {
                stateId = value;
            }
        }

        public string StateName
        {
            get
            {
                return stateName;
            }

            set
            {
                stateName = value;
            }
        }

        public string StateAbbriviation
        {
            get
            {
                return stateAbbriviation;
            }

            set
            {
                stateAbbriviation = value;
            }
        }

        public int CityId
        {
            get
            {
                return cityId;
            }

            set
            {
                cityId = value;
            }
        }


        public int ClassId
        {
            get
            {
                return cityId;
            }

            set
            {
                ClassId = value;
            }
        }

        public string CityName
        {
            get
            {
                return cityName;
            }

            set
            {
                cityName = value;
            }
        }

        public string CityAbbriviation
        {
            get
            {
                return cityAbbriviation;
            }

            set
            {
                cityAbbriviation = value;
            }
        }

        public int PincodeId
        {
            get
            {
                return pincodeId;
            }

            set
            {
                pincodeId = value;
            }
        }

        public string PincodeName
        {
            get
            {
                return pincodeName;
            }

            set
            {
                pincodeName = value;
            }
        }




    }

    public class pushRequestModel
    {
        public List<string> to { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }

    public class fcnRequestModel
    {
        public List<string> registration_ids { get; set; }
        public Notifications notification { get; set; }
    }
    public class Notifications
    {
        public string body { get; set; }
        public string title { get; set; }
    }

    public class NotificationRequest
    {
        public string UserIds { get; set; }
        public int schoolId { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public List<string> To { get; set; }
        public bool IsStaff { get; set; } = false;
    }

    public class Notification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int usertypeId { get; set; }
        public int schoolId { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public DateTime AddDate { get; set; }
        public bool IsRead { get; set; }
    }

    public class PushNotificationKey
    {

        public int Id { get; set; }
        public string Tocken { get; set; }
        public int UserId { get; set; }
        public int usertypeId { get; set; }
        public int schoolId { get; set; }
        public DateTime createdOn { get; set; }
    }
    public class FirebaseRequestModel
    {
        public string tocken { get; set; }
        public int userId { get; set; }
        public int SchoolId { get; set; }
        public int userTypeId { get; set; }

    }

    public class PrincipleProfile
    {
        public string PrincipleName { get; set; }
        public string PrincipleMassage { get; set; }
        public string PrincipleImage { get; set; }
        public string Degree { get; set; }
    }
    public class ParentDashBoardResponse
    {
        public int PaidAmount { get; set; }
        public int DuesAmount { get; set; }
        public int Absent { get; set; }
        public int Persent { get; set; }
        public int LeaveRequested { get; set; }
        public List<AttendanceGrapshItems> AttendanceGrapshItems { get; set; }

    }

    public class AttendanceGrapshItems
    {
        public string Months { get; set; }
        public int Persent { get; set; }
        public int Absent { get; set; }
    }
    public class DashBoardData
    {
        public int TodaysCollection { get; set; }
        public int MonthCollection { get; set; }
        public int YearCollection { get; set; }
        public int TotalDues { get; set; }
        public int PercentCollection { get; set; }
        public int TodaysExpanse { get; set; }
        public int MonthExpanse { get; set; }
        public int YearExpanse { get; set; }
        public int TotalStudent { get; set; }
        public int AdmThisMonth { get; set; }
        public int AbsentStudent { get; set; }
        public int TotalEmp { get; set; }
        public int NewEmp { get; set; }
        public int AbsentEmp { get; set; }
        public int SMSCredit { get; set; }
        public int PresentStudentPercent { get; set; }
        public int PresentEmpPercent { get; set; }
        public int StaffOnLeave { get; set; }
        public int StudentsOnLeave { get; set; }
        public List<SuppportStaff> suppportStaffs { get; set; }
        public List<Strenght> strenghts { get; set; }
        public List<Income> incomes { get; set; }
        public List<Income> expanses { get; set; }
        public List<Collection> collections { get; set; }

    }
    public class SuppportStaff
    {
        public string Name { get; set; }
        public string Contact { get; set; }
        public string ContactName { get; set; }
        public string Email { get; set; }
        public string ImgPath { get; set; }
    }
    public class Strenght
    {
        public string ClassName { get; set; }
        public int BoysCount { get; set; }
        public int GirlsCount { get; set; }
    }
    public class Income
    {
        public string MonthName { get; set; }
        public decimal Amount { get; set; }
    }
    public class Collection
    {
        public decimal Cash { get; set; }
        public decimal Bank { get; set; }
        public decimal Amount { get; set; }
        public string TranDate { get; set; }
    }
    public class FileDeleteRequest
    {
        public string FileName { get; set; }
        public string DocName { get; set; }
    }

    public class StudentRegistrationWeb
    {
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public string Class { get; set; }
        public string Gender { get; set; }
        public string BloodGroup { get; set; }
        public string Religion { get; set; }
        public string Category { get; set; }
        public string AadharNo { get; set; }
        public string PreviousClass { get; set; }
        public string FatherName { get; set; }
        public string Occupation { get; set; }
        public string FMobile { get; set; }
        public string MotherName { get; set; }
        public string MMobile { get; set; }
        public string Moccupation { get; set; }
        public string Residence { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int PinCode { get; set; }
        public string SchoolDis { get; set; }
        public string RegStatus { get; set; }
        public string ApplicationMode { get; set; }
        public int RegFee { get; set; }
        public int SchoolId { get; set; }
    }

    public class StudentRegistrationResponce
    {
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public string Class { get; set; }
        public string Gender { get; set; }
        public string BloodGroup { get; set; }
        public string Religion { get; set; }
        public string Category { get; set; }
        public string AadharNo { get; set; }
        public string PreviousClass { get; set; }
        public string FatherName { get; set; }
        public string Occupation { get; set; }
        public string FMobile { get; set; }
        public string MotherName { get; set; }
        public string Mmobile { get; set; }
        public string Moccupation { get; set; }
        public string Residence { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int PinCode { get; set; }
        public string SchoolDis { get; set; }
        public string RegStatus { get; set; }
        public string ApplicationMode { get; set; }
        public int RegFee { get; set; }
        public int SchoolId { get; set; }
        public string RegNumber { get; set; }
        public string SchoolName { get; set; }
        public string Contact1 { get; set; }
        public string EmailId { get; set; }
        public string Address1 { get; set; }
        public string SRegNo { get; set; }
        public string UDISE { get; set; }
        public string affiliated { get; set; }
        public DateTime DateOfReg { get; set; }

    }
    public class OnlineRegistrationResponce
    {
        public string RegistrationNo { get; set; }
        public string ClientKey { get; set; }
        public string ClientSec { get; set; }
        public string PaymentGatewayOrderId { get; set; }
        public string PaymentLink { get; set; }
        public string Name { get; set; }
        public int SchoolId { get; set; }
        public int RegFee { get; set; }

    }

    public class PaymentGatewayOrderResponse
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Receipt { get; set; }
        public string ShortUrl { get; set; }
    }

    public class OnlinePaymentRequest
    {
        public string Regno { get; set; }
        public string PaymentId { get; set; }
        public int SchoolId { get; set; }
        public string PaymentStatus { get; set; }
        public string OrderId { get; set; }
    }

    public class ExamNameAndDate
    {
        public string ExamName { get; set; }
        public string ExamCode { get; set; }
        public string ResultStartDate { get; set; }
        public string ResultEndDate { get; set; }
    }

    public class APIResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
    }

    public class StudentLeaveDto
    {
        public int LeaveId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }     // lr.StartDate
        public DateTime EndDate { get; set; }       // lr.EndDate
        public string Remarks { get; set; }         // lr.Remarks
        public string ApprovalRemark { get; set; }  // lr.ApprovalRemark
        public string Status { get; set; }          // lr.Status
        public DateTime RequestedOn { get; set; }   // lr.AddDate
        public int NoOfDays { get; set; }           // NoDays
    }
    public class StaffLeaveDto
    {
        public int LeaveId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }     // lr.StartDate
        public DateTime EndDate { get; set; }       // lr.EndDate
        public string Remarks { get; set; }         // lr.Remarks
        public string Status { get; set; }          // lr.Status
        public string ApprovalRemark { get; set; }  // lr.ApprovalRemark
        public DateTime RequestedOn { get; set; }   // lr.AddDate
        public int NoOfDays { get; set; }           // NoDays
    }
    public class DashboardCollectionResponse
    {
        public string TrandDate { get; set; }
        public int AmountCash { get; set; }
        public int AmountBank { get; set; }
        public int TotalAmount { get; set; }
    }

    public class DashboardCollectionSummaryResponse
    {
        public int MonthCollection { get; set; }
        public int TodaysCollection { get; set; }
        public int NoofStudentPaid { get; set; }
    } 
        public class SmsTemplateDto
        {
            public int TemplateId { get; set; }
            public string TemplateName { get; set; }
            public string TemplateDesc { get; set; }
            public string EntityId { get; set; }      // assuming entityId is VARCHAR in DB
            public string DltTemplateId { get; set; } // assuming DltTemplateId is VARCHAR in DB
        }
    public class TemplateDescDto
    {
        public string TempDesc { get; set; }
    }

}
