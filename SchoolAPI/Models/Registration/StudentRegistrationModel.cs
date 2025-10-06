namespace SchoolAPI.Models.Registration
{
    public class StudentRegistrationModelReq
    {
        public int Communication { get; set; }
        public int RegNo { get; set; }
        public string? Registration { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public int Class { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateTime Dob { get; set; }
        public string? BirthPlace { get; set; }
        public int IsPermanentAddressSame { get; set; }
// Student Address (Same)
public string? AddressLine1S { get; set; }
        public string? AddressLine2S { get; set; }
        public int CountryS { get; set; }
        public int StateP { get; set; }
        public string? CityP { get; set; }
        public int PincodeP { get; set; }

        // Contact Info
        public string? FatherMobileNumber { get; set; }
        public string? MobileNoP { get; set; }
        public string? EmailId { get; set; }

        // Permanent / Physical Address
        public string? AddressLine1Ph { get; set; }
        public string? AddressLinePh { get; set; }
        public int CountryPh { get; set; }
        public int StatePh { get; set; }
        public string? CityPh { get; set; }
        public int PinCodePh { get; set; }
        public int Distance { get; set; }

        // Father Details
        public string? FirstNameFather { get; set; }
        public string? MiddleNameFather { get; set; }
        public string? LastNameFather { get; set; }
        public string? EducationQualificationFather { get; set; }
        public string? ProfessionalQualificationFather { get; set; }
        public string? Occupation { get; set; }

        // Mother Details
        public string? FirstNameMother { get; set; }
        public string? MiddleNameMother { get; set; }
        public string? LastNameMother { get; set; }
        public string? EducationQualificationMother { get; set; }
        public string? ProfessionalQualificationMother { get; set; }
        public string? OccupationMother { get; set; }

        // Audit Info
        public string? CreatedBy { get; set; }

        // School and Session Info
        public int SchoolId { get; set; }
        public int SessionId { get; set; }

        // Additional Contact Info
        public string? FatherMobile1 { get; set; }
        public string? Mobile1 { get; set; }

        // Other Fields
        public int ReciptNo { get; set; }
        public string? AadharNo { get; set; }
        public bool AdmDone { get; set; }
        public int RegFee { get; set; }
        public int BloodGroup { get; set; }
        public int Category { get; set; }
        public int Religion { get; set; }
        public int SiblingsStudentId { get; set; }

}


    public class StudentRegistrationModelRes
    {
        public int Regno { get; set; }
        public string Registration { get; set; }
        public string Date { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string Dob { get; set; }
        public string Gender { get; set; }
        public string FatherMobileNumber { get; set; }
        public string FatherName { get; set; }
        public int ReciptNo { get; set; }
        public int RegFee { get; set; } 

    }
}
