using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Models.Admission;
using SchoolAPI.Repositories.AdmissionRepository;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdmissionController : ControllerBase
    {
        private readonly IAdmissionRepository _admissionRepository;
        public AdmissionController(IAdmissionRepository admissionRepository)
        {
            _admissionRepository = admissionRepository;
        }

        [HttpGet("GetNewAdmissionNumber/{schoolId}")]
        public IActionResult GetNewAdmissionNumber(int schoolId)
        {
            var newAdmissionNumber = 12345; // Example static number, replace with actual logic
            return Ok(new { AdmissionNumber = newAdmissionNumber });
        }

        [HttpGet("ConcessionQuota/{schoolId}")]
        public async Task<IActionResult> ConcessionQuotaAsync(int schoolId)
        {
            // Logic to get concession quota details
            var concessionDetails = new
            {
                SchoolId = schoolId,
                Quota = 10, // Example static data, replace with actual logic
                Description = "Concession quota details"
            };
            return Ok(concessionDetails);
        }

        [HttpGet("SessionName/{schoolId}")]
        public async Task<IActionResult> SessionName(int schoolId)
        {
            // Logic to get concession quota details
            var concessionDetails = new
            {
                SchoolId = schoolId,
                Quota = 10, // Example static data, replace with actual logic
                Description = "Concession quota details"
            };
            return Ok(concessionDetails);
        }

        [HttpGet("HouseName/{schoolId}")]
        public async Task<IActionResult> HouseName(int schoolId)
        {
            // Logic to get concession quota details
            var concessionDetails = new
            {
                SchoolId = schoolId,
                Quota = 10, // Example static data, replace with actual logic
                Description = "Concession quota details"
            };
            return Ok(concessionDetails);
        }

        [HttpGet("OptionalFeeAllocation/{schoolId}")]
        public async Task<IActionResult> OptionalFeeAllocation(int schoolId)
        {
            // Logic to get concession quota details
            var concessionDetails = new
            {
                SchoolId = schoolId,
                Quota = 10, // Example static data, replace with actual logic
                Description = "Concession quota details"
            };
            return Ok(concessionDetails);
        }

        [HttpGet("GetAdmissionDetails/{studentId}")]
        public async Task<IActionResult> GetAdmissionDetailsAsync(int studentId)
        {
            // Logic to get admission details by ID
            var admissionDetails = new
            {
                studentId = studentId,
                StudentName = "John Doe", // Example static data, replace with actual logic
                Class = "10th Grade",
                AdmissionDate = DateTime.Now.AddMonths(-1)
            };
            return Ok(admissionDetails);
        }

        [HttpPost("SaveStudent")]
        public async Task<IActionResult> SaveStudent(StudentAdmissionM studentAdmissionM)

        {
            var result = await _admissionRepository.AddStudentDetailsAsync(studentAdmissionM).ConfigureAwait(false);

            return Ok(new { Message = "Student saved successfully" });

        }
    }
}