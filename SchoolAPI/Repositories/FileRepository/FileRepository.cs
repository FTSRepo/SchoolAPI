using Microsoft.Data.SqlClient;
using SchoolAPI.Enums;
using SchoolAPI.Infrastructure.Factory;
using SchoolAPI.Models;
using SchoolAPI.Repositories.FileRepository;

namespace SchoolAPI.Data
    {
    public class FileRepository(IDbConnectionFactory dbConnectionFactory) : IFileRepository
        {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;
        public async Task<int> SaveFileMetadataAsync(FileMetadata file)
            {
            using var con = _dbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand(@"INSERT INTO FileMetadata 
        (SchoolId, FileIdentifier, EntityType, EntityId, FileCategory, FileName, FileUrl, FileSize, ContentType, UploadedAt, ExpiryDate)
        OUTPUT INSERTED.Id 
        VALUES (@SchoolId, @FileIdentifier, @EntityType, @EntityId, @FileCategory, @FileName, @FileUrl, @FileSize, @ContentType, @UploadedAt, @ExpiryDate)", con);

            cmd.Parameters.AddWithValue("@SchoolId", file.SchoolId);
            cmd.Parameters.AddWithValue("@FileIdentifier", file.FileIdentifier);
            cmd.Parameters.AddWithValue("@EntityType", file.EntityType);
            cmd.Parameters.AddWithValue("@EntityId", file.EntityId);
            cmd.Parameters.AddWithValue("@FileCategory", file.FileCategory);
            cmd.Parameters.AddWithValue("@FileName", file.FileName);
            cmd.Parameters.AddWithValue("@FileUrl", file.FileUrl);
            cmd.Parameters.AddWithValue("@FileSize", file.FileSize);
            cmd.Parameters.AddWithValue("@ContentType", file.ContentType ?? ( object ) DBNull.Value);
            cmd.Parameters.AddWithValue("@UploadedAt", file.UploadedAt);
            cmd.Parameters.AddWithValue("@ExpiryDate", file.ExpiryDate ?? ( object ) DBNull.Value);


            await con.OpenAsync();
            return ( int ) await cmd.ExecuteScalarAsync();
            }

        public async Task<FileMetadata?> GetFileMetadataAsync(int id, int SchoolId)
            {
            using var con = _dbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand("SELECT * FROM FileMetadata WHERE Id=@Id AND SchoolId=@SchoolId", con);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@SchoolIdId", SchoolId);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if ( await reader.ReadAsync() )
                {
                return new FileMetadata
                    {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    SchoolId = reader.GetInt32(reader.GetOrdinal("SchoolId")),
                    FileIdentifier = Enum.Parse<FileIdentifier>(reader ["FileIdentifier"].ToString()),
                    FileCategory = Enum.Parse<FileCategory>(reader ["FileCategory"].ToString()),
                    EntityType = Enum.Parse<EntityType>(reader ["EntityType"].ToString()),
                    EntityId = reader.GetInt32(reader.GetOrdinal("EntityId")),
                    FileName = reader.GetString(reader.GetOrdinal("FileName")),
                    FileUrl = reader.GetString(reader.GetOrdinal("FileUrl")),
                    FileSize = Convert.ToInt64(reader ["FileSize"]),
                    ContentType = reader ["ContentType"].ToString(),
                    UploadedAt = Convert.ToDateTime(reader ["UploadedAt"])
                    };
                }
            return null;
            }

        public async Task<IEnumerable<FileMetadata>> GetFilesByEntityAsync(int SchoolId, int entityId, FileIdentifier fileIdentifier)
            {
            var files = new List<FileMetadata>();
            using var con = _dbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand("SELECT * FROM FileMetadata WHERE SchoolId=@SchoolId AND entityId=@EntityId AND FileIdentifier=@FileIdentifier", con);
            cmd.Parameters.AddWithValue("@SchoolId", SchoolId);
            cmd.Parameters.AddWithValue("@EntityId", entityId);
            cmd.Parameters.AddWithValue("@FileIdentifier", fileIdentifier);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while ( await reader.ReadAsync() )
                {
                files.Add(new FileMetadata
                    {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    SchoolId = reader.GetInt32(reader.GetOrdinal("SchoolId")),
                    FileIdentifier = Enum.Parse<FileIdentifier>(reader ["FileIdentifier"].ToString()),
                    FileCategory = Enum.Parse<FileCategory>(reader ["FileCategory"].ToString()),
                    EntityId = reader.GetInt32(reader.GetOrdinal("EntityId")),
                    FileName = reader.GetString(reader.GetOrdinal("FileName")),
                    FileUrl = reader.GetString(reader.GetOrdinal("FileUrl")),
                    FileSize = Convert.ToInt64(reader ["FileSize"]),
                    ContentType = reader ["ContentType"].ToString(),
                    UploadedAt = Convert.ToDateTime(reader ["UploadedAt"])
                    });
                }
            return files;
            }
        public async Task<IEnumerable<FileMetadata>> GetExpiredFilesAsync()
            {
            var expired = new List<FileMetadata>();
            using var con = _dbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand("SELECT * FROM FileMetadata WHERE ExpiryDate < GETUTCDATE()", con);
            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while ( await reader.ReadAsync() )
                {
                expired.Add(new FileMetadata
                    {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    SchoolId = reader.GetInt32(reader.GetOrdinal("SchoolId")),
                    FileUrl = reader.GetString(reader.GetOrdinal("FileUrl")),
                    FileIdentifier = Enum.Parse<FileIdentifier>(reader ["FileIdentifier"].ToString()),
                    FileCategory = Enum.Parse<FileCategory>(reader ["FileCategory"].ToString()),
                    ExpiryDate = Convert.ToDateTime(reader ["ExpiryDate"])
                    });
                }
            return expired;
            }

        public async Task DeleteFileRecordAsync(int id)
            {
            using var con = _dbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand("DELETE FROM FileMetadata WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            }

        }
    }
