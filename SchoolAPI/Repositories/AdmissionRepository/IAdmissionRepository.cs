using SchoolAPI.Models.Admission;

namespace SchoolAPI.Repositories.AdmissionRepository
{
    public interface IAdmissionRepository
    {
        Task<string> AddStudentDetailsAsync(StudentAdmissionM oStudentDetail);
    }
}
