using System.Data;
using SchoolAPI.Models.Attendance;

namespace SchoolAPI.Repositories.AttendanceRepository
    {
    public interface IAttendanceRepository
        {
        Task<DataTable> GetStudentAttendanceAsync(int schoolId, int sessionId, int classId, int sectionId);
        Task<DataTable> GetStaffAttendanceAsync(int schoolId);
        Task<string> InsertStaffAttendanceAsync(DataTable dt);
        Task<string> InsertStudentAttendanceAsync(DataTable dt);
        Task<DataTable> GetAttendanceByStudentIdAsync(AttendanceFilter filter);
        Task<DataTable> GetAttendanceByStaffIdAsync(AttendanceFilter filter);
        Task<DataTable> GetAbsentStudentsListAsync(AttendanceFilter filter);
        }
    }
