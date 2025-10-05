using SchoolAPI.Models.Message;
using SchoolAPI.Repositories.MessageRepository;

namespace SchoolAPI.Services.MessageService
{
    public class MessageService(IMessageRepository messageRepository) : IMessageService
    {
        private readonly IMessageRepository _messageRepository = messageRepository;
        
        public async Task<List<MessageResponse>> GetMessageAsync(int schoolId, int studentId)
        {
            return await _messageRepository.GetMessageAsync(schoolId, studentId);
        }
        public async Task<List<NewsResponse>> GetNewsAsync(int schoolId)
        {
            return await _messageRepository.GetNewsAsync(schoolId);
        }
        public async Task<List<NewsResponse>> GetEventsAsync(int schoolId)
        {
            return await _messageRepository.GetEventsAsync(schoolId);
        }
    }
}
