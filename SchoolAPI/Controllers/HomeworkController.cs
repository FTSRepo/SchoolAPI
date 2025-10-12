using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Models.Homework;
using SchoolAPI.Repositories.HomeworkRepository;
using SchoolAPI.Services.Homework;
using SchoolAPI.Services.S3Service;

namespace SchoolAPI.Controllers
{ 
    [ApiController]
    public class HomeworkController(IHomeworkService homeworkService, IS3FileService s3FileService, IHomeworkRepository homeworkRepository) : ControllerBase
    {
        private readonly IHomeworkService _homeworkService = homeworkService;
        private readonly IS3FileService _s3Service = s3FileService;
        private readonly IHomeworkRepository _homeworkRepository = homeworkRepository;

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

        [HttpPost("saveanduploadhomework")]
        [Consumes("multipart/form-data")]
        [AllowAnonymous]
        public async Task<IActionResult> UploadHomework([FromForm] HomeworkUploadRequest request)
        {
            string fileUrl = string.Empty;

            if (request.File != null)
            fileUrl = await _s3Service.UploadFileAsync(request.SchoolId, request.File, Enums.FileCategory.Homework);

            int result = await _homeworkRepository.SveHomeWorkAsync(request, fileUrl).ConfigureAwait(false);
            if(request == null || result == 0)
            {
                return BadRequest("Failed to save homework.");
            }

            return Ok(new { Message = "Homework uploaded successfully", fileUrl });
        }

        // ✅ Get Homework List (for Students)
        [HttpGet("getHomeworks")]
        [AllowAnonymous]
        public async Task<IActionResult> GetHomework(int schoolId, string className, string sectionName)
        {
           var result = await _homeworkRepository.GetHomeworkAsync(schoolId, className, sectionName).ConfigureAwait(false);
            if (result == null || result.Count == 0)
            {
                return NotFound("No homework found.");
            }
            return Ok(result);
        }
    }
}