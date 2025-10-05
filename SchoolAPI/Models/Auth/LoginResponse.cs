namespace SchoolAPI.Models.Auth
{ 
    public class LoginResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int SchoolId { get; set; }
        public string DisplayName { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string AssociatLogo { get; set; }
        public int AssociateId { get; set; }
        public string AssociateWeb { get; set; }
        public int Usertypeid { get; set; }
        public string UserType { get; set; }
        public string Logo { get; set; }
        public string RecBGImg { get; set; }
        public string Logop { get; set; }
        public int SessionId { get; set; }
        public string SessionName { get; set; }
        public string AssociatEmail { get; set; }
        public string ErpUrl { get; set; }
        public int StudentId { get; set; }
        public int BoardId { get; set; }
        public string SchEmailId { get; set; }
        public string SchContact { get; set; }
        public string SchContact1 { get; set; }
        public string Schaffiliated { get; set; }
        public string Poweredby { get; set; }
        public string Gender { get; set; }
        public string Married { get; set; }
        public string SchShortName { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public string EmpCode { get; set; }
        public string Dob { get; set; }
        public string Doj { get; set; }
        public List<UserMenu> WebMenu { get; set; }
        public List<MobileUserMenu> MobileMenu { get; set; }
        public List<Student> Students { get; set; }

    }
    public class Student
    {
        public int StudentId { get; set; }
        public string AdmissionNumber { get; set; }
        public string Name { get; set; }
        public string ProfileImg { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public string Logo { get; set; }
        public string Address { get; set; }
    }
}
