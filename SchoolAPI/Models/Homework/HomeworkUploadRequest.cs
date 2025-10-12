using System.ComponentModel.DataAnnotations;

public class HomeworkUploadRequest
    {
    [Required] public int SchoolId { get; set; }
    [Required] public string ClassName { get; set; }
    [Required] public string SectionName { get; set; }
    [Required] public string SubjectName { get; set; }
    [Required] public DateTime HomeworkDate { get; set; }
    public string Description { get; set; }

    [Required] public IFormFile File { get; set; }
    }

public class HomeworkResponse
    {
    public int Id { get; set; }
    public string ClassName { get; set; }
    public string SectionName { get; set; }
    public string SubjectName { get; set; }
    public DateTime HomeworkDate { get; set; }
    public string FileUrl { get; set; }
    public string FileName { get; set; }
    public string Description { get; set; }
    }
