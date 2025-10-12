using SchoolAPI.Enums;

namespace SchoolAPI.Services.S3Service
{
    public interface IS3FileService
    {
        Task<string> UploadFileAsync(int schoolId, IFormFile file, FileCategory category);
        Task DeleteFileAsync(string fileUrl);
        Task<int> DeleteExpiredFilesAsync();
    }
}
