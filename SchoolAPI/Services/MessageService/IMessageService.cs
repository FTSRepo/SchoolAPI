using SchoolAPI.Models.Message;

namespace SchoolAPI.Services.MessageService
    {
    public interface IMessageService
        {
        Task<List<MessageResponse>> GetMessageAsync(int schoolId, int studentId);
        Task<List<NewsResponse>> GetNewsAsync(int schoolId);
        Task<List<NewsResponse>> GetEventsAsync(int schoolId);
        }
    }
