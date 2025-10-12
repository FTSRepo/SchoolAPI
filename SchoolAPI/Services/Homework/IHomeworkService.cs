using SchoolAPI.Models.Homework;

namespace SchoolAPI.Services.Homework
    {
    public interface IHomeworkService
        {
        Task<bool> AssignHomeworkAsync(HomeWorkMasterM objHomeWork, List<HomeWorkDetailM> homeWorkTable);
        Task<List<HomeWorkResponseM>> GetHomeWorksAppAsync(int schoolId, int sessionId, int classId = 0, int sectionId = 0, int studentId = 0, int staffId = 0);
        }
    }
