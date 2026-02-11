namespace SchoolAPI.Models.Admission
{
    public class StudentAdmissionM
    {
        // School & Session Info
        public int SchoolId { get; set; }
        public int SessionId { get; set; }
        public string SessionName { get; set; }

        // Admission Info
        public string AdmissionNo { get; set; }
        public string RollNo { get; set; }
        public DateTime? AddmissionDate { get; set; }
        public int AdmissionType { get; set; }
        public int CurrentClass { get; set; }
        public int CurrentSection { get; set; }
        public int Concession { get; set; } // QuotaID
        public int? OptionalFeeHeadId { get; set; }

        // Personal Info
        public string StudentFirstName { get; set; }
        public string StudentMiddleName { get; set; }
        public string StudentLastName { get; set; }
        public DateTime? DOB { get; set; }
        public string Birthplace { get; set; }
        public string Gender { get; set; }
        public string BloodGroup { get; set; }
        public string Nationality { get; set; }
        public int Category { get; set; }
        public int Religion { get; set; }

        // Identification
        public string AadharNumber { get; set; }
        public string BirthCertificateNumber { get; set; }
        public string PermanentEducationNumber { get; set; }
        public string eShikshaId { get; set; }

        // Family Info
        public string fatehrName { get; set; }  // (Typo preserved as per your code)
        public string motherName { get; set; }
        public string mobileNo { get; set; }

        // Address Info
        public string Address { get; set; }
        public string Address2 { get; set; }

        // Sibling Info
        public int? SiblingsStudentId { get; set; }

        // School Details
        public string prvSchoolname { get; set; }
        public string Reason { get; set; }
        public string pClass { get; set; }
        public int? HouseId { get; set; }

        // Output
        public string StudentIds { get; set; }
    }
}
