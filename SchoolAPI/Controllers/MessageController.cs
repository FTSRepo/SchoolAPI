using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Services.MessageService;

namespace SchoolAPI.Controllers
{
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        [HttpGet]
        [Route("api/GetMessage")]
        public async Task<IActionResult> GetMessages(int schoolId, int studentId)
        {
            return Ok(await _messageService.GetMessageAsync(schoolId, studentId).ConfigureAwait(false));
        }

        [HttpGet]
        [Route("api/GetNews")]
        public async Task<IActionResult> GetNewsAsync(int schoolId)
        {
            return Ok(await _messageService.GetNewsAsync(schoolId).ConfigureAwait(false));
        }
        [HttpGet]
        [Route("api/GetEvents")]
        public async Task<IActionResult> GetEvents(int schoolId)
        {
            return Ok(await _messageService.GetEventsAsync(schoolId).ConfigureAwait(false));
        }
    }
}
