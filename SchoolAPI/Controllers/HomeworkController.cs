using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Services.Homework;
using SchoolAPI.Models.Homework;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SchoolAPI.Controllers
{ 
    [ApiController]
    public class HomeworkController(IHomeworkService homeworkService) : ControllerBase
    {
        private readonly IHomeworkService _homeworkService = homeworkService;

        [Route("api/SaveHomeWork")]
        [HttpPost]
        public async Task<IActionResult> SaveHomeWork(HomeWorkMasterM objHomeWork)
        {
            try
            {
                if(objHomeWork == null)
                {
                    return NotFound();
                }
                var result = await _homeworkService.AssignHomeworkAsync(objHomeWork, objHomeWork.LstHomeWorkDetailM).ConfigureAwait(false);
                return Ok(new { Message = "Homework assigned successfully", Status = result });
            }
            catch (Exception)
            {
               return Ok(new { Message = "Homework assigned failed", Status = false });
            }
        }

        [Route("api/GetHomeWork")]
        [HttpGet]
        public async Task<IActionResult> GetHomeWork(int schoolId, int SessionId, int ClassId, int SectionId, int StudentId, int StaffId)
        {
            try
            {
                var result = await _homeworkService.GetHomeWorksAppAsync(schoolId, SessionId, ClassId, SectionId, StudentId, StaffId).ConfigureAwait(false);
                if (result == null || result.Count == 0)
                {
                    return NotFound();
                }
                return Ok(new { Message = "Homework fetched successfully", Status = true, Data = result });
            }
            catch (Exception ex)
            {
                return Ok(new { Message = "Homework assigned failed", Status = false });
            }
        }
    }
}
