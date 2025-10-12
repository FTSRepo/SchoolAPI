using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Enums;
using SchoolAPI.Models.Common;
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
            var result = await _registrationService.BindCategoryDropdownsAsync().ConfigureAwait(false);
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
        
        public async Task<IActionResult> SaveRegistration([FromBody] StudentRegistrationModelReq studentRegistrationModel)
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
        [AllowAnonymous]
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
        [HttpGet]
        [Route("GetRegistrationByRegNo")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRegistrationByRegNo(int schoolId, int regNo)
        {
            var result = await _registrationService.StudentRegistrationByRegNoAsync(schoolId, regNo);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound("No registrations found.");
            }
        }
        [HttpPost]
        [Route("DeleteRegistrationByRegNo")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteRegistrationByRegNo(int schoolId,int regNo)
        {
            bool result = await _registrationRepository.DeleteRegRecordAsync(schoolId, regNo);
            if (result !=null)
            {
                return Ok(new { Message = "Delete Registration Number Successfull", Status = true });
            }
            else
            {
                return BadRequest(new { Message = "Failed to delete registration record", Status = false });
            }
        }

        [HttpPost]
        [Route("UpdateRegistrationByRegNo")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateRegistrationByRegNo(int schoolId, int regNo, Status status, string remark, int userId)
        {
            bool result = await _registrationRepository.UpdateRegistrationStatusAsync(schoolId, regNo, status, remark, userId);
            if (result)
            {
                return Ok(new { Message = "Registration status updated successfully", Status = true });
            }
            else
            {
                return BadRequest(new { Message = "Failed to update registration status", Status = false });
            }
        }

        [HttpPost]
        [Route("SaveEnquiry")]        
        
        public async Task<IActionResult> SaveEnquiry(EnquiryM enquiry)
        {
            bool result = await _registrationRepository.SaveEnquiryAsync(enquiry).ConfigureAwait(false);
            if(result != null)
                return Ok(new { Message = "Enquiry saved successfully", Status = true });
            else
                return Ok(new { Message = "Failed to save enquiry", Status = false });
        }

        [HttpGet]
        [Route("GetEnquiry")]
        
        public async Task<IActionResult> GetEnquiry(int schoolId)
        {
            var result = await _registrationService.GetEnquiriesAsync(schoolId, "online");
            if (result != null)
                return Ok(new { Message = result, Status = true });
            else
                return Ok(new { Status = false });
        }

        [HttpGet]
        [Route("GetEnquiryById")]
        
        public async Task<IActionResult> GetEnquiryById(int schoolId, int enquiryId)
        {
            var result = await _registrationService.GetEnquiryByIdAsync(schoolId, enquiryId);
            if (result != null)
                return Ok(new { Data = result, Status = true });
            else
                return Ok(new { Status = false });
        }

        [HttpPost]
        [Route("UpdateEnquiry")]        
        
        public async Task<IActionResult> UpdateEnquiry(int enquiryId)
        {
           bool result = await _registrationRepository.UpdateEnquiriesAsync(enquiryId).ConfigureAwait(false);
            if (result !=null)
                return Ok(new { Message = "Enquiry updated successfully", Status = true });
            else
                return Ok(new { Message = "Failed to update enquiry", Status = false });
        }

        [HttpPost]
        [Route("DeleteEnquiry")]        
        
        public async Task<IActionResult> DeleteEnquiry(int enquiryId)
        {
            bool result = await _registrationRepository.DeleteOnlineEnquiryAsync(enquiryId).ConfigureAwait(false);
            if(result !=null)
                return Ok(new { Message = "Enquiry deleted successfully", Status = true });
            else
                return Ok(new { Message = "Failed to delete enquiry", Status = false });
        }

    }
}
