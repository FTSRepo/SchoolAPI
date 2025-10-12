using System.Data;
using SchoolAPI.Models.Homework;
using SchoolAPI.Repositories.HomeworkRepository;

namespace SchoolAPI.Services.Homework
    {
    public class HomeworService(IHomeworkRepository homeworkRepository) : IHomeworkService
        {
        private readonly IHomeworkRepository _homeworkRepository = homeworkRepository;
        public async Task<bool> AssignHomeworkAsync(HomeWorkMasterM objHomeWork, List<HomeWorkDetailM> homeWorks)
            {
            DataTable HomeWorkTable = new DataTable();
            DataColumn COLUMN = new DataColumn
                {
                ColumnName = "Question",
                DataType = typeof(string)
                };
            HomeWorkTable.Columns.Add(COLUMN);
            COLUMN = new DataColumn
                {
                ColumnName = "Filename",
                DataType = typeof(string)
                };
            HomeWorkTable.Columns.Add(COLUMN);
            COLUMN = new DataColumn
                {
                ColumnName = "Filepath",
                DataType = typeof(string)
                };
            HomeWorkTable.Columns.Add(COLUMN);
            foreach ( var hwd in homeWorks )
                {
                DataRow DR = HomeWorkTable.NewRow();
                DR [0] = hwd.Question;
                DR [1] = hwd.Filename;
                HomeWorkTable.Rows.Add(DR);
                }

            return await _homeworkRepository.AssignHomeworkAsync(objHomeWork, HomeWorkTable).ConfigureAwait(false);
            }
        public async Task<List<HomeWorkResponseM>> GetHomeWorksAppAsync(int schoolId, int sessionId, int classId = 0, int sectionId = 0, int studentId = 0, int staffId = 0)
            {
            DataSet ds = await _homeworkRepository.GetHomeWorksAppAsync(schoolId, sessionId, classId, sectionId, studentId, staffId).ConfigureAwait(false);

            List<HomeWorkResponseM> listStaffDetail = [];
            if ( ds != null )
                {
                foreach ( DataRow row in ds.Tables [0].Rows )
                    {
                    HomeWorkResponseM oViewStaff = new()
                        {
                        SubjectName = Convert.ToString(row ["subjectname"]),
                        TeacherName = Convert.ToString(row ["StaffName"]),
                        HomeWorkDate = Convert.ToDateTime(Convert.ToString(row ["CreatedOn"])),
                        Question = Convert.ToString(row ["Question"]),
                        Filename = Convert.ToString(row ["FileName"]),
                        };

                    listStaffDetail.Add(oViewStaff);
                    }
                }
            return listStaffDetail;
            }
        }
    }
