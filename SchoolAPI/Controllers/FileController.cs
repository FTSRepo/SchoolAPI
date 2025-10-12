using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Enums;
using SchoolAPI.Models;
using SchoolAPI.Models.File;
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

        [Route("upload")]
        [HttpPost]
        [Consumes("multipart/form-data")]
        [AllowAnonymous]
        public async Task<IActionResult> UploadFile(
            [FromForm] FileUploadRequest file,
            [FromForm] int schoolId,
            [FromForm] EntityType entityType,
            [FromForm] int entityId,
            [FromForm] FileIdentifier fileIdentifier
            )
            {
            if ( file == null || file.File.Length == 0 )
                return BadRequest("File is required.");

            var fileUrl = await _s3Service.UploadFileAsync(schoolId, file.File, FileCategory.General);

            DateTime? dateTime = null;

            var metadata = new FileMetadata
                {
                SchoolId = schoolId,
                FileIdentifier = fileIdentifier,
                FileCategory = FileCategory.General,
                EntityType = entityType,
                EntityId = entityId,
                FileName = file.File.FileName,
                FileUrl = fileUrl,
                FileSize = file.File.Length,
                ContentType = file.File.ContentType,
                UploadedAt = DateTime.UtcNow,
                ExpiryDate = dateTime
                };

            var id = await _repository.SaveFileMetadataAsync(metadata);
            metadata.Id = id;

            return Ok(metadata);
            }

        [Route(( "{schoolId}/{entityId}/{fileIdentifier}" ))]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetFiles(int schoolId, int entityId, FileIdentifier fileIdentifier)
            {
            var files = await _repository.GetFilesByEntityAsync(schoolId, entityId, fileIdentifier);
            return Ok(files);
            }
        }
    }
