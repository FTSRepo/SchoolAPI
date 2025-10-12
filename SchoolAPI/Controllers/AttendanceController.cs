using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Models.Attendance;
using SchoolAPI.Services.AttendanceService;
using SchoolAPI.Services.CommonService;

namespace SchoolAPI.Controllers
{
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;
        private readonly ICommonService _commons;
        public AttendanceController(IAttendanceService attendanceService, ICommonService commonService)
        {
            _attendanceService = attendanceService;
            _commons = commonService;
        }
        [Route("api/GetStudentsList")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GetStudentsListByClass(AttendanceFilter attendanceFilter)
        {
            var result = await _attendanceService.GetStudentAttendanceAsync(attendanceFilter).ConfigureAwait(false);
            if (result.Count > 0)
            {
                return Ok(new { data = result.OrderBy(x => x.AdmNo).ToList(), status = true });
            }
            else
            {
                return Ok(new { data = "", status = false });
            }
        }

        [Route("api/GetStaffList")]
        [HttpPost]
        public async Task<IActionResult> GetStaffList(int SchoolId)
        {
            var result = await _attendanceService.GetStaffAttendanceAsync(SchoolId);
            if (result == null)
            {
                return Ok(new { data = "", status = false });
            }
            else
            {
                return Ok(new { data = result.OrderBy(x => x.Name).ToList(), status = true });
            }
        }

        [Route("api/SaveStaffAttendance")]
        [HttpPost]
        public async Task<IActionResult> SaveStaffAttendance(StaffAttendanceRequestM saveAttendanceMaster)
        {
            string result = await _attendanceService.InsertStaffAttendanceAsync(saveAttendanceMaster);
            if (result.Contains("Success"))
            {
                return Ok(new { data = result, status = true });
            }
            else
            {
                return Ok(new { data = result, status = false });
            }
        }

        [Route("api/SaveStudentsAttendance")]
        [HttpPost]
        public async Task<IActionResult> SaveStudentsAttendance(StudentAttendanceRequestM saveAttendanceMaster)
        {
            string msg = await _attendanceService.InsertStudentAttendanceAsync(saveAttendanceMaster);
            if (msg.Contains("Success"))
            {
                return Ok(new { data = msg, status = true });
            }
            else
            {
                return Ok(new { data = msg, status = false });
            }
        }

        [Route("api/studentAttendance")]
        [HttpPost]
        public async Task<IActionResult> studentAttendance(AttendanceFilter filter)
        {
            var result = await _attendanceService.GetAttendanceByStudentIdAsync(filter);
            if (result.Rows.Count > 0)
            {
                return Ok(new { data = result, status = true });
            }
            else
            {
                return Ok(new { data = "", status = false });
            }

        }
        [Route("api/StaffAttendance")]
        [HttpPost]
        public async Task<IActionResult> StaffAttendance(AttendanceFilter filter)
        {
            var result = await _attendanceService.GetAttendanceByStaffIdAsync(filter);
            if (result.Rows.Count > 0)
            {
                return Ok(new { data = result, status = true });
            }
            else
            {
                return Ok(new { data = "", status = false });
            }
        }
        [Route("api/getAbsentStudentList")]
        [HttpPost]
        public async Task<IActionResult> getAbsentStudentList(AttendanceFilter filter)
        {
            var result = await _attendanceService.GetAbsentStudentsListAsync(filter);
            if (result.Rows.Count > 0)
            {
                return Ok(new { data = result, status = true });
            }
            else
            {
                return Ok(new { data = "", status = false });
            }
        }
    }
}
