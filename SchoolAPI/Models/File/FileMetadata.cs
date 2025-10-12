using SchoolAPI.Enums;

namespace SchoolAPI.Models
    {
    public class FileMetadata
        {
        public int Id { get; set; }
        public int SchoolId { get; set; } // school code or tenant key
        public FileIdentifier FileIdentifier { get; set; } // e.g. StudentProfile, StaffDoc, etc.
        public EntityType EntityType { get; set; } // student  or staff  or School
        public int EntityId { get; set; } // studentId or staffId
        public FileCategory FileCategory { get; set; } // e.g. Homework, Notice, News, General
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public long FileSize { get; set; }
        public string ContentType { get; set; }
        public DateTime UploadedAt { get; set; }
        public DateTime? ExpiryDate { get; set; } // null = permanent
        }
    }
