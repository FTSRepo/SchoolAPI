using System;

namespace SchoolAPI.Models.Common
{
    public class ViewStudentM
    {
        // Core properties in use
        public int rollNo { get; set; }
        public int SchoolId { get; set; }
        public int StudentId { get; set; }
        public string AdmissionNumber { get; set; }
        public string Registrationno { get; set; }
        public string Name { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentMiddleName { get; set; }
        public string StudentLastName { get; set; }
        public string FatherName { get; set; }
        public string Contact1 { get; set; }
        public string EmailId { get; set; }
        public string CurrentClass { get; set; }
        public string Admclass { get; set; }
        public string Section { get; set; }
        public string DoAdmissionString { get; set; }
        public string Gender { get; set; }
        public string BloodGroup { get; set; }
        public string DOB { get; set; }

        // Optional / legacy fields (not used but preserved for compatibility)
        public string Birthplace { get; set; }
        public int PreviousDetailID { get; set; }
        public int PreSchoolid { get; set; }
        public int Pstudentid { get; set; }
        public string Schoolname { get; set; }
        public string Board { get; set; }
        public int PClass { get; set; }
        public int Reason { get; set; }

        public int AddressStudentid { get; set; }
        public int AddressStaffId { get; set; }
        public int Addresstype { get; set; }
        public string Addressline1 { get; set; }
        public string Addressline2 { get; set; }
        public int Country { get; set; }
        public int State { get; set; }
        public string District { get; set; }
        public string Postoffice { get; set; }
        public string Area { get; set; }
        public string Pincode { get; set; }
        public string LandMark { get; set; }
    }
}