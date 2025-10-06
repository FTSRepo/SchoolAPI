using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SchoolAPI.Models.Registration;
using SchoolAPI.Repositories.RegistrationRepository;
using SchoolAPI.Services.RegistrationService;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController(IRegistrationService registrationService, IRegistrationRepository registrationRepository) : ControllerBase
    {
        private readonly IRegistrationService _registrationService = registrationService;
        private readonly IRegistrationRepository _registrationRepository = registrationRepository;

        [HttpGet]
        [Route("GetCategory")]
        public async Task<IActionResult> GetCategory()
        {
            var result = _registrationService.BindCategoryDropdownsAsync();
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound("No categories found.");
            }
        }

        [HttpGet]
        [Route("GetNewRegNumberAsync")]
        public async Task<IActionResult> GetNewRegNumberAsync(int SchoolId)
        {
            string admissionNumber = await _registrationService.GetNewRegNumberAsync(SchoolId);
            if (!string.IsNullOrEmpty(admissionNumber))
            {
                return Ok(admissionNumber);
            }
            else
            {
                return NotFound("Admission number not found.");
            }
        }

        [HttpGet]
        [Route("GetReligion")]
        public async Task<IActionResult> GetReligion()
        {
            List<ReligionM> result = await _registrationService.BindReligionDropdownsAsync();
            if (result != null)
            {
                return Ok(new { religions = result, Status = true });
            }
            else
            {
                return NotFound("No religions found.");
            }
        }

        [HttpGet]
        [Route("GetBloodGroup")]
        public async Task<IActionResult> GetBloodGroup()
        {
            List<BloodGroupDto> result = await _registrationRepository.GetBloodGroupAsync();
            if (result != null)
            {
                return Ok(new { Data = result, Status = true });
            }
            else
            {
                return NotFound("No BloodGroup found.");
            }
        }

        [HttpGet]
        [Route("GetStates")]
        public async Task<IActionResult> GetStates()
        {
            List<StateDto> result = await _registrationRepository.GetStates();
            if (result != null)
            {
                return Ok(new { Data = result, Status = true });
            }
            else
            {
                return NotFound("No BloodGroup found.");
            }
        }

        [HttpPost]
        [Route("saveRegistration")]
        public async Task<IActionResult> SaveRegistration([FromBody] StudentRegistrationModel studentRegistrationModel)
        {
            string result = "", sendSMS = "";

            try
            {
                result = await _registrationService.SaveRegistrationAsync(studentRegistrationModel);
                if (result.Contains("success"))
                {
                    if (studentRegistrationModel.Communication == 2 || studentRegistrationModel.Communication == 3)
                    {
                        sendSMS = await _registrationService.SendSMS(studentRegistrationModel);
                    }
                    if (sendSMS.Contains("success"))
                        return Ok(new { Message = "Data has been saved and SMS sent successfully!!", Status = true });
                    else
                        return Ok(new { Message = "Data has been saved successfully but unable to send SMS", Status = true });
                }
                else
                    return Ok(new { Message = "There are some technical error unable to save data", Status = false });
            }
            catch (Exception Ex)
            {
                return BadRequest(new { Message = Ex.Message, Status = false });
            }
        }

        [HttpGet]
        [Route("GetRegistration")]
        public async Task<IActionResult> GetRegistration(int schoolId)
        {
            var result = await _registrationService.StudentRegistrationAllRecordAsync(schoolId, 0, "All", null);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound("No registrations found.");
            }

        }
    }
}
