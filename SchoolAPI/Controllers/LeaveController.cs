using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Models.Leave;
using SchoolAPI.Repositories.LeaveRepository;

namespace SchoolAPI.Controllers
{ 
    [ApiController]
    public class LeaveController(ILeaveRepository leaveRepository) : ControllerBase
    {
        private readonly ILeaveRepository _leqveRepository = leaveRepository;

        [Route("api/SaveLeaves")]
        [HttpPost]
        public async Task<ActionResult> SaveLeaves(LeaveRequest leaveRequest)
        {
            if (leaveRequest == null)
            {
                return BadRequest(new { Status = false, Message = "Details should not be null" });
            }
            else
            {
                var leave = await _leqveRepository.SaveLeaveRequestAsync(leaveRequest).ConfigureAwait(false);
                if (!string.IsNullOrEmpty(leave))
                {
                    return Ok(new { Status = true, Message = leave });
                }
                else
                {
                    return Ok(new { Status = false, Message = "Failed to Save Leave" });
                }
            }
        }
        [Route("api/GetLeaves")]
        [HttpGet]
        public async Task<IActionResult> GetLeaves(int schoolId, int sessionId, int userId, int userType)
        {
            var leaves = await _leqveRepository.GetLeavesByUserIdAsync(schoolId, sessionId, userId, userType).ConfigureAwait(false);
            if (leaves != null && leaves.Count > 0)
            {
                return Ok(new { Status = true, Message = "Leaves Found", Data = leaves });
            }
            else
            {
                return Ok(new { Status = false, Message = "No Leaves Found" });
            }
        }
        [Route("api/DeleteLeaves")]
        [HttpGet]
        public async Task<IActionResult> DeleteLeaves(int leaveId)
        {
            var msg = await _leqveRepository.DeleteLeaveByLeaveIdAsync(leaveId).ConfigureAwait(false);
            if (!string.IsNullOrEmpty(msg))
            {
                return Ok(new { Status = true, Message = msg });
            }
            else
            {
                return Ok(new { Status = false, Message = "Failed to Delete Leave" });
            }
        }

        [Route("api/UpdateLeaveStatus")]
        [HttpPost]
        public async Task<IActionResult> UpdateLeaveStatus(LeaveUpdateRequest leaveM)
        {
            if (leaveM == null)
            {
                return BadRequest(new { Status = false, Message = "Details should not be null" });
            }
            else
            {
                bool msg = await _leqveRepository.UpdateLeaveApprovalStatusAsync(leaveM).ConfigureAwait(false);
                if (msg)
                {
                    return Ok(new { Status = true, Message = "Leave updated successfully" });
                }
                else
                {
                    return Ok(new { Status = false, Message = "Failed to Update Leave Status" });
                }
            }
        }
    }
}
