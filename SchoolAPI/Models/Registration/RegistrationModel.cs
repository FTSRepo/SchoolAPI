namespace SchoolAPI.Models.Registration
    {
    public class CategoryM
        {
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        }

    public class BloodGroupDto
        {
        public int Id { get; set; }
        public string GroupName { get; set; }
        }
    public class StateDto
        {
        public int Id { get; set; }
        public string Name { get; set; }
        }

    public class ReligionM
        {
        public string religionName { get; set; }
        public int religionId { get; set; }
        }
    }
