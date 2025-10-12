using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Models.OnelineClass;
using SchoolAPI.Services.OnelineClassService;

namespace SchoolAPI.Controllers
    {
    [ApiController]
    public class OnlineClassController(IOnelineClassService onelineClassService) : ControllerBase
        {
        private readonly IOnelineClassService _onelineClassService = onelineClassService;

        [Route("api/GetOnlineClass")]
        [HttpGet]
        public async Task<IActionResult> GetOnlineClass(int schoolId, int SessionId, int staffId, int StudentId, string userType)
            {
            try
                {
                var result = _onelineClassService.GetOnlineClassSetupAsync(schoolId, SessionId, staffId, StudentId, userType);
                if ( result == null || result.Result.Count == 0 )
                    {
                    return NotFound();
                    }
                return Ok(new { Message = "Online class fetched successfully", Status = true, Data = result.Result });
                }
            catch ( Exception ex )
                {
                return Ok(new { Message = "Online class fetch failed", Status = false });
                }
            }

        [Route("api/SaveOnlineClass")]
        [HttpPost]
        public async Task<IActionResult> SaveOnlineClass(OnlineClassSetupRequest onlineclasssetup)
            {
            if ( onlineclasssetup == null )
                return NotFound();
            var result = await _onelineClassService.AddOnlineClassSetupAsync(onlineclasssetup);
            return Ok(new { Message = "Online class Saved successfully", Status = result });
            }
        }
    }
