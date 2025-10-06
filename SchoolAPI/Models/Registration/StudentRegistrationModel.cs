namespace SchoolAPI.Models.Registration
{
    public class StudentRegistrationModelReq
    {
        public string? Registration { get; set; }
        public DateTime DateofRegistration { get; set; }
        public int Class { get; set; }
        public string? Firstname { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateTime Dob { get; set; }
        public string? BirthPlace { get; set; }
        public int RadioValue { get; set; }
        public string? AddressLine1S { get; set; }
        public string? AddressLine2S { get; set; }
        public int CountryS { get; set; }
        public string? Statep { get; set; }
        public string? Cityp { get; set; }
        public int Pincodep { get; set; }
        public string? FatherMobileNumber { get; set; }
        public string? MobileNo { get; set; }
        public string? PresentDistance { get; set; }
        public string? AddressLine1Ph { get; set; }
        public string? AddressLinePh { get; set; }
        public int CountryPh { get; set; }
        public int StatePh { get; set; }
        public string? CityPh { get; set; }
        public int PinCodePh { get; set; }
        public int Distance { get; set; }
        public int Communication { get; set; } = 1; // 1-Post, 2-SMS, 3-Both

        // Father details
        public string? FirstNameFather { get; set; }
        public string? MiddleNameFather { get; set; }
        public string? LastNameFather { get; set; }
        public string? EducationQualificationFather { get; set; }
        public string? ProfessionalQualificationFather { get; set; }
        public string? OccupationFather { get; set; }

        // Mother details
        public string? FirstNameMother { get; set; }
        public string? MiddleNameMother { get; set; }
        public string? LastNameMother { get; set; }
        public string? EducationQualificationMother { get; set; }
        public string? ProfessionalQualificationMother { get; set; }
        public string? OccupationMother { get; set; }

        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public int SchoolId { get; set; }
        public int SessionId { get; set; }

        public string? FatherMobileNo1 { get; set; }
        public string? Mobile1 { get; set; }
        public string? AadharNo { get; set; }
        public string? RegFee { get; set; }   
        public int BloodGroup { get; set; }
        public int Category { get; set; }
        public int Religion { get; set; }
        public int SiblingsStudentId { get; set; }

        // OUTPUT param from SP
        public string? Msg { get; set; }
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
