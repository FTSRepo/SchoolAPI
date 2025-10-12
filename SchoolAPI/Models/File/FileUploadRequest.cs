using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Models.File
{
    public class FileUploadRequest
    {
        [Required]
        public IFormFile File { get; set; }

        public string Description { get; set; }
    }
}
