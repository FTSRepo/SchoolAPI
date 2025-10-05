using SchoolAPI.Models.Attendance;
using System.Data;
using SchoolAPI.Repositories.AttendanceRepository;
using System.Text.RegularExpressions;
using SchoolAPI.Services.CommonService;
using SchoolAPI.Repositories.CommonRepository;

namespace SchoolAPI.Services.AttendanceService
{
    public class AttendanceService(IAttendanceRepository attendanceRepository, ICommonService commonService, ICommonRepository commonRepository) : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository = attendanceRepository;
        private readonly ICommonService _commons = commonService;
        private readonly ICommonRepository _commonRepository = commonRepository;

        public async Task<List<StaffListRespM>> GetStaffAttendanceAsync(int schoolId)
        {
            List<StaffListRespM> staffLists = new List<StaffListRespM>();
            DataTable dt = await _attendanceRepository.GetStaffAttendanceAsync(schoolId);
            foreach (DataRow dr in dt.Rows)
            {
                staffLists.Add(new StaffListRespM
                {
                    StaffCode = Convert.ToString(dr["StaffCode"]),
                    Name = Convert.ToString(dr["Name"]),
                    Contact = Convert.ToString(dr["Contact1"]),
                    Gender = Convert.ToString(dr["Gender"]),
                    Department = Convert.ToString(dr["Department"])
                });
            }
            return staffLists;
        }
        public async Task<List<StudentListRespM>> GetStudentAttendanceAsync(AttendanceFilter filter)
        {
            List<StudentListRespM> studentListMs = new List<StudentListRespM>();
            DataTable dt = await _attendanceRepository.GetStudentAttendanceAsync(filter.SchoolId, filter.SessionId, filter.ClassId, filter.Sectionid);
            foreach (DataRow dr in dt.Rows)
            {
                studentListMs.Add(new StudentListRespM
                {
                    AdmNo = Convert.ToString(dr["AdmissionNumber"]),
                    StudentId = Convert.ToInt32(dr["StudentId"]),
                    Name = Convert.ToString(dr["Name"]),
                    Gender = Convert.ToString(dr["Gender"]),
                    RollNo = Convert.ToInt32(dr["RollNo"]),
                    FatherName = Convert.ToString(dr["FatherName"])
                });
            }
            return studentListMs;
        }
        public async Task<string> InsertStaffAttendanceAsync(StaffAttendanceRequestM saveAttendanceMaster)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[13] {// new DataColumn("Id", typeof(int)),
                        new DataColumn("SchoolId",typeof(int)),
                        new DataColumn("Name", typeof(string)),
                        new DataColumn("staffcode",typeof(string)),
                        new DataColumn("Gender", typeof(string)),
                        new DataColumn("CurrentDate", typeof(DateTime)),
                        new DataColumn("status", typeof(string)),
                        new DataColumn("Department", typeof(string)),
                        new DataColumn("Remark", typeof(string)),
                        new DataColumn("isactive", typeof(int)),
                        new DataColumn("CreatedBy", typeof(int)),
                        new DataColumn("CreatedOn", typeof(DateTime)),
                        new DataColumn("Modifydby", typeof(int)),
                        new DataColumn("ModifyOn", typeof(DateTime)),
                });

            foreach (StaffAttendanceDetailM attendaceDetails in saveAttendanceMaster.lSaveAttendanceDetails)
            {
                dt.Rows.Add(
                            saveAttendanceMaster.SchoolId,
                            attendaceDetails.Name,
                            attendaceDetails.Staffcode,
                            attendaceDetails.Gender,
                            Convert.ToDateTime(saveAttendanceMaster.Date), //  Attendance date,
                            attendaceDetails.Status,
                            attendaceDetails.Department,
                            attendaceDetails.Remark,
                            1,
                            saveAttendanceMaster.UserId,
                            Convert.ToString(DateTime.Now),
                            saveAttendanceMaster.UserId,
                            Convert.ToString(DateTime.Now)
                            );
            }
            string msg = await _attendanceRepository.InsertStaffAttendanceAsync(dt);
            return msg;

        }
        public async Task<string> InsertStudentAttendanceAsync(StudentAttendanceRequestM saveAttendanceMaster)
        {
            string msg = "";
            string template;
            List<string> smsresponse = new List<string>();
            DataTable dt1 = await _commons.GetTemplateSmsAsync(saveAttendanceMaster.SchoolId, 1);
            template = dt1.Rows[0]["TemplateDesc"].ToString();
            string entityId = dt1.Rows[0]["entityId"].ToString();
            string dlttemplate = dt1.Rows[0]["DltTemplateId"].ToString();

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[16]
                {
                        new DataColumn("SchoolId", typeof(int)),
                        new DataColumn("SessionId", typeof(int)),
                        new DataColumn("Name", typeof(string)),
                        new DataColumn("AdmissionNumber",typeof(string)),
                        new DataColumn("Class",typeof(int)),
                        new DataColumn("Section",typeof(int)),
                        new DataColumn("Gender", typeof(string)),
                        new DataColumn("CurrentDate", typeof(DateTime)),
                        new DataColumn("status", typeof(string)),
                        new DataColumn("Remark", typeof(string)),
                        new DataColumn("isactive", typeof(int)),
                        new DataColumn("CreatedBy", typeof(int)),
                        new DataColumn("CreatedOn", typeof(DateTime)),
                        new DataColumn("Modifydby", typeof(int)),
                        new DataColumn("ModifyOn", typeof(DateTime)),
                        new DataColumn("RollNo",typeof(int)),
            });
            foreach (StudentAttendanceDetailM attendaceDetails in saveAttendanceMaster.lSaveAttendanceDetails)
            {
                dt.Rows.Add(
                            saveAttendanceMaster.SchoolId,
                            saveAttendanceMaster.SessionId,
                            attendaceDetails.Name,
                            attendaceDetails.AdmNo,
                            saveAttendanceMaster.ClassId,
                            saveAttendanceMaster.SectionId,
                            attendaceDetails.Gender,
                            Convert.ToDateTime(saveAttendanceMaster.Date),
                            attendaceDetails.Status,
                            attendaceDetails.Remark,
                            1,
                            saveAttendanceMaster.UserId,
                            Convert.ToString(DateTime.Now),
                            saveAttendanceMaster.UserId,
                            Convert.ToString(DateTime.Now),
                            attendaceDetails.RollNo
                            );
                if (saveAttendanceMaster.SchoolId != 41)
                {
                    if (Convert.ToInt32(attendaceDetails.Status) != 1 || attendaceDetails.Status != "1")
                    {
                        string curDate = Convert.ToString(DateTime.Now).Substring(0, 9);
                        string atenDate = Convert.ToString(Convert.ToDateTime(saveAttendanceMaster.Date)).Substring(0, 9);
                        if (curDate == atenDate)
                        {
                            attendaceDetails.Contact = await _commons.GetStudentMobileByStudentIdAsync(attendaceDetails.StudentID);
                            if (attendaceDetails.Contact != null || attendaceDetails.Contact != "")
                            {
                                if (Regex.Match(attendaceDetails.Contact.Trim(), @"^[6789]\d{9}$").Success)
                                {
                                    string Contact = "91" + attendaceDetails.Contact.Trim();
                                    string sid = attendaceDetails.StudentID.ToString();
                                    //template = template == null ? "Dear Parent, '" + attendaceDetails.Name.ToUpper() + "' is absent today" : template.Replace("@", attendaceDetails.Name.ToUpper());
                                    //template = template.Replace("{#var#}", attendaceDetails.Name.ToUpper());

                                    string SMSCredit =await _commons.GetSMSCreditAsync(saveAttendanceMaster.SchoolId);
                                    if (Convert.ToInt32(SMSCredit) > 0)
                                    {
                                        smsresponse.Add(await _commons.FTSMessanger(template.Replace("{#var#}", attendaceDetails.Name.ToUpper()), Contact, saveAttendanceMaster.SchoolId, 1, sid, entityId, dlttemplate));
                                    }
                                    else
                                    {
                                        msg = "Insufficient SMS Credit - ";
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (Convert.ToInt32(attendaceDetails.Status) != 1 || attendaceDetails.Status != "1")
                    {
                        msg = template.Replace("{#var#}", attendaceDetails.Name.ToUpper());
                        DataTable smsDT = new DataTable();
                        smsDT.Columns.AddRange(new DataColumn[10]
                        {
                            new DataColumn("SchoolId", typeof(int)),
                            new DataColumn("Mobile", typeof(string)),
                            new DataColumn("MessageId",typeof(string)),
                            new DataColumn("MessageTxt", typeof(string)),
                            new DataColumn("StatusCode",typeof(string)),
                            new DataColumn("StatusMessage", typeof(string)),
                            new DataColumn("StatusJobId",typeof(string)),
                            new DataColumn("MessageType", typeof(string)),
                            new DataColumn("date", typeof(DateTime)),
                            new DataColumn("UId", typeof(string))
                        });
                        smsDT.Rows.Add(
                                  saveAttendanceMaster.SchoolId,
                                  "",
                                  "",
                                  "",
                                  "",
                                  msg,
                                  "",
                                  "",
                                  DateTime.Now,
                                  ""
                                );
                       await _commonRepository.InsertSMSLogAsync(smsDT);
                    }
                }
            }
            msg += await _attendanceRepository.InsertStudentAttendanceAsync(dt);
            return msg;
        }
        public async Task<DataTable> GetAttendanceByStudentIdAsync(AttendanceFilter filter)
        {
            DataTable dt = await _attendanceRepository.GetAttendanceByStudentIdAsync(filter);
            return dt;
        }
        public async Task<DataTable> GetAttendanceByStaffIdAsync(AttendanceFilter filter)
        {
            DataTable dt = await _attendanceRepository.GetAttendanceByStaffIdAsync(filter);
            return dt;
        }
        public async Task<DataTable> GetAbsentStudentsListAsync(AttendanceFilter filter)
        {
            DataTable dt = await _attendanceRepository.GetAbsentStudentsListAsync(filter);
            return dt;
        }
    }
}
