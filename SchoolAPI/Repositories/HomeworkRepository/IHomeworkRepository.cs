using System.Data;
using SchoolAPI.Models.Homework;

namespace SchoolAPI.Repositories.HomeworkRepository
    {
    public interface IHomeworkRepository
        {
        Task<int> SveHomeWorkAsync(HomeworkUploadRequest request, string fileUrl);
        Task<List<HomeworkResponse>> GetHomeworkAsync(int schoolId, string className, string sectionName);
        Task<bool> AssignHomeworkAsync(HomeWorkMasterM objHomeWork, DataTable homeWorkTable);
        Task<DataSet> GetHomeWorksAppAsync(int schoolId, int sessionId, int classId = 0, int sectionId = 0, int studentId = 0, int staffId = 0);
        }
    }
