using System.Data;
using SchoolAPI.Models.OnelineClass;
using SchoolAPI.Repositories.OnelineClassRepository;

namespace SchoolAPI.Services.OnelineClassService
{
    public class OnelineClassService(IOnelineClassRepository onelineClassRepository) : IOnelineClassService
    {
        private readonly IOnelineClassRepository _onlieClassRepository = onelineClassRepository;
        public async Task<List<OnlineClassSetupResponse>> GetOnlineClassSetupAsync(int schoolId, int sessionId, int staffId, int studentId, string userType)
        {
            DataSet ds = await _onlieClassRepository.GetOnlineClassSetupAsync(schoolId, sessionId, staffId, studentId, userType).ConfigureAwait(false);
            List<OnlineClassSetupResponse> listStaffDetail = [];
            if (ds != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    OnlineClassSetupResponse oViewStaff = new()
                    {
                        SubjectName = Convert.ToString(row["subjectname"]),
                        Name = Convert.ToString(row["Name"]),
                        ClassName = Convert.ToString(row["ClassName"]),
                        SectionName = Convert.ToString(row["SectionName"]),
                        Sessiondate = Convert.ToString(row["Sessiondate"]),
                        StartTime = Convert.ToString(row["StartTime"]),
                        EndTime = Convert.ToString(row["EndTime"]),
                        MeetingLink = Convert.ToString(row["MeetingLink"]),
                    };

                    listStaffDetail.Add(oViewStaff);
                }
            }
            return listStaffDetail;
        }
        public async Task<bool> AddOnlineClassSetupAsync(OnlineClassSetupRequest onlineClassSetupRequest)
        {
            return await _onlieClassRepository.AddOnlineClassSetupAsync(onlineClassSetupRequest).ConfigureAwait(false);
        }
    }
}
