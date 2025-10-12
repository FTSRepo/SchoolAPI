using SchoolAPI.Services.S3Service;

namespace SchoolAPI.Services
    {
    public class FileCleanupService : BackgroundService
        {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<FileCleanupService> _logger;

        public FileCleanupService(IServiceScopeFactory scopeFactory, ILogger<FileCleanupService> logger)
            {
            _scopeFactory = scopeFactory;
            _logger = logger;
            }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
            {
            while ( !stoppingToken.IsCancellationRequested )
                {
                try
                    {
                    using var scope = _scopeFactory.CreateScope();

                    var s3Service = scope.ServiceProvider.GetRequiredService<IS3FileService>();

                    int deleted = await s3Service.DeleteExpiredFilesAsync();

                    if ( deleted > 0 )
                        _logger.LogInformation($"[FileCleanup] Deleted {deleted} expired files at {DateTime.UtcNow}.");
                    }
                catch ( Exception ex )
                    {
                    _logger.LogError(ex, "Error deleting expired files");
                    }

                // Run once every 24 hours
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
                }
            }
        }
    }
