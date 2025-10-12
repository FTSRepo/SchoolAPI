using System.Data;
using SchoolAPI.Models.Attendance;

namespace SchoolAPI.Services.AttendanceService
    {
    public interface IAttendanceService
        {
        Task<List<StudentListRespM>> GetStudentAttendanceAsync(AttendanceFilter filter);
        Task<List<StaffListRespM>> GetStaffAttendanceAsync(int schoolId);
        Task<string> InsertStaffAttendanceAsync(StaffAttendanceRequestM saveAttendanceMaster);
        Task<string> InsertStudentAttendanceAsync(StudentAttendanceRequestM saveAttendanceMaster);
        Task<DataTable> GetAttendanceByStudentIdAsync(AttendanceFilter filter);
        Task<DataTable> GetAttendanceByStaffIdAsync(AttendanceFilter filter);
        Task<DataTable> GetAbsentStudentsListAsync(AttendanceFilter filter);
        }
    }
