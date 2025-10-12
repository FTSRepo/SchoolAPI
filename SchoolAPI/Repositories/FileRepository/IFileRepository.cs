using SchoolAPI.Enums;
using SchoolAPI.Models;

namespace SchoolAPI.Repositories.FileRepository
{
    public interface IFileRepository
    {
        Task<int> SaveFileMetadataAsync(FileMetadata file);
        Task<FileMetadata?> GetFileMetadataAsync(int id, int schoolId);
        Task<IEnumerable<FileMetadata>> GetFilesByEntityAsync(int schoolId, int entityId, FileIdentifier fileIdentifier);
        Task<IEnumerable<FileMetadata>> GetExpiredFilesAsync();
        Task DeleteFileRecordAsync(int id);
    }
}
