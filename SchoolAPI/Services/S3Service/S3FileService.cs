using Amazon.S3;
using Amazon.S3.Model;
using SchoolAPI.Enums;
using SchoolAPI.Repositories.FileRepository;

namespace SchoolAPI.Services.S3Service
    {
    public class S3FileService : IS3FileService
        {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;
        private readonly IConfiguration _config;
        private readonly IFileRepository _repository;

        public S3FileService(IAmazonS3 s3Client, IConfiguration config, IFileRepository repository)
            {
            _s3Client = s3Client;
            _config = config;
            _repository = repository;
            _bucketName = _config ["AWS:BucketName"];
            }

        public async Task<string> UploadFileAsync(int schoolId, IFormFile file, FileCategory category)
            {
            var folder = GetFolderPath(schoolId, category);
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var key = $"{folder}{fileName}";

            using var stream = file.OpenReadStream();

            var request = new PutObjectRequest
                {
                BucketName = _bucketName,
                Key = key,
                InputStream = stream,
                ContentType = file.ContentType,
                CannedACL = S3CannedACL.Private
                };

            await _s3Client.PutObjectAsync(request);

            return $"https://{_bucketName}.s3.amazonaws.com/{key}";
            }
        public async Task DeleteFileAsync(string fileUrl)
            {
            try
                {
                var key = ExtractKeyFromUrl(fileUrl);
                var request = new DeleteObjectRequest
                    {
                    BucketName = _bucketName,
                    Key = key
                    };
                await _s3Client.DeleteObjectAsync(request);
                }
            catch ( Exception ex )
                {
                Console.WriteLine($"Error deleting file: {ex.Message}");
                }
            }

        public async Task<int> DeleteExpiredFilesAsync()
            {
            var expiredFiles = await _repository.GetExpiredFilesAsync();
            int deletedCount = 0;

            foreach ( var file in expiredFiles )
                {
                await DeleteFileAsync(file.FileUrl);
                await _repository.DeleteFileRecordAsync(file.Id);
                deletedCount++;
                }

            return deletedCount;
            }

        private string ExtractKeyFromUrl(string url)
            {
            var baseUrl = $"https://{_bucketName}.s3.amazonaws.com/";
            return url.Replace(baseUrl, "");
            }

        private string GetFolderPath(int schoolId, FileCategory category)
            {
            return category switch
                {
                    FileCategory.General => $"{schoolId}/", // stays in root folder
                    _ => $"{schoolId}/homework/" // others inside subfolders
                    };
            }
        }
    }
