using SchoolAPI.Models.Message;

namespace SchoolAPI.Repositories.MessageRepository
{
    public interface IMessageRepository
    {
        Task<List<MessageResponse>> GetMessageAsync(int schoolId, int studentId);
        Task<List<NewsResponse>> GetNewsAsync(int schoolId);
        Task<List<NewsResponse>> GetEventsAsync(int schoolId);
    }
}
