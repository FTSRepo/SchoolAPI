namespace SchoolAPI.Enums
    {
    public enum EntityType
        {
        Student = 1,
        Staff = 2,
        School = 3
        }

    public enum FileIdentifier
        {
        StudentProfile = 1,
        StudentDocument = 2,
        StaffProfile = 3,
        StaffDocument = 4,
        SchoolDocument = 5,
        Other = 9
        }

    public enum FileCategory
        {
        General = 0,
        Homework = 1,
        Notice = 2,
        News = 3
        }
    }
