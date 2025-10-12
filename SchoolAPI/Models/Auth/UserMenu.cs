namespace SchoolAPI.Models.Auth
    {
    public class UserMenu
        {
        public int MenuId { get; set; }
        public int ParentMenuId { get; set; }
        public string MenuDesc { get; set; }
        public string MenuUrl { get; set; }
        public string MenuIcon { get; set; }
        public int? MenuOrder { get; set; }
        public bool Isactive { get; set; }
        public List<UserMenu> List { get; set; }
        }
    }