using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Enums;
using SchoolAPI.Models;
using SchoolAPI.Repositories.FileRepository;
using SchoolAPI.Services.S3Service;

namespace SchoolAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController(IS3FileService s3Service, IFileRepository repository) : ControllerBase
    {
        private readonly IS3FileService _s3Service = s3Service;
        private readonly IFileRepository _repository = repository;

        public class FileUploadRequest
        {
            [Required]
            public IFormFile File { get; set; }

            public string Description { get; set; }
        }

        [Route("upload")]
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadFile(
            [FromForm] FileUploadRequest file,
            [FromForm] int schoolId,
            [FromForm] int entityId,
            [FromForm] FileIdentifier fileIdentifier,
            [FromForm] FileCategory fileCategory)
        {
            if (file == null || file.File.Length == 0)
                return BadRequest("File is required.");

            var fileUrl = await _s3Service.UploadFileAsync(schoolId, file.File, fileCategory);

            DateTime? dateTime = null;
            var expiryDate = (fileCategory is FileCategory.Homework or
                  FileCategory.Notice or
                  FileCategory.News)
                 ? DateTime.UtcNow.AddDays(30)
                 : dateTime;

            var metadata = new FileMetadata
            {
                SchoolId = schoolId,
                FileIdentifier = fileIdentifier,
                FileCategory = fileCategory,
                EntityId = entityId,
                FileName = file.File.FileName,
                FileUrl = fileUrl,
                FileSize = file.File.Length,
                ContentType = file.File.ContentType,
                UploadedAt = DateTime.UtcNow,
                ExpiryDate = expiryDate
            };

            var id = await _repository.SaveFileMetadataAsync(metadata);
            metadata.Id = id;

            return Ok(metadata);
        }

        [Route(("{schoolId}/{entityId}/{fileIdentifier}"))]
        [HttpGet]
        public async Task<IActionResult> GetFiles(int schoolId, int entityId, string fileIdentifier)
        {
            var files = await _repository.GetFilesByEntityAsync(schoolId, entityId, fileIdentifier);
            return Ok(files);
        }
    }
}
