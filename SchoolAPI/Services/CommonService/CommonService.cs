using System.Data;
using System.Net;
using System.Text;
using System.Web;
using SchoolAPI.Models.Common;
using SchoolAPI.Repositories.CommonRepository;
using System.Text.Json;

namespace SchoolAPI.Services.CommonService
{
    public class CommonService(ICommonRepository commonRepository) : ICommonService
    {
        private readonly ICommonRepository _commonRepository = commonRepository;
        public async Task<List<ClassM>> GetClassByTeacherAsync(int schoolId, int userId)
        {
            var dt = await _commonRepository.GetClassByTeacherAsync(schoolId, userId).ConfigureAwait(false);
            List<ClassM> classMs = new List<ClassM>();
            foreach (DataRow dr in dt.Rows)
            {
                classMs.Add(new ClassM
                {
                    ClassId = Convert.ToInt32(dr.ItemArray[0]),
                    ClassName = Convert.ToString(dr.ItemArray[1])
                });
            }
            return classMs;
        }
        public async Task<List<SectionM>> GetSectionAsync(int classId, int schoolId, int userId = 0)
        {
            DataTable dt = await _commonRepository.GetSectionAsync(classId, schoolId, userId).ConfigureAwait(false);
            List<SectionM> sectionMs = new List<SectionM>();
            foreach (DataRow dr in dt.Rows)
            {
                sectionMs.Add(new SectionM
                {
                    SectionId = Convert.ToInt32(dr.ItemArray[0]),
                    SectionName = Convert.ToString(dr.ItemArray[1])
                });
            }
            return sectionMs;
        }
        public async Task<List<SubjectM>> GetSubjectListAsync(int schoolId)
        {
            DataTable dt = await _commonRepository.GetSubjectAsync(schoolId).ConfigureAwait(false);
            List<SubjectM> subjects = new List<SubjectM>();
            foreach (DataRow dr in dt.Rows)
            {
                subjects.Add(new SubjectM
                {
                    SubId = Convert.ToInt32(dr.ItemArray[0]),
                    SubjectName = Convert.ToString(dr.ItemArray[1])
                });
            }
            return subjects;
        }
        public async Task<List<Holidays>> GetHolidaysAsync(int schoolId)
        {
            DataTable dt = await _commonRepository.GetHoliDaysAsync(schoolId).ConfigureAwait(false);
            List<Holidays> holidays = new List<Holidays>();
            foreach (DataRow dr in dt.Rows)
            {
                holidays.Add(new Holidays
                {
                    fromDate = Convert.ToString(dr.ItemArray[0]),
                    toDate = Convert.ToString(dr.ItemArray[1]),
                    Remarks = Convert.ToString(dr.ItemArray[2]),
                });
            }
            return holidays;
        }
        public async Task<StaffProfile> GetStaffProfileAsync(int schoolId, int userId)
        {
            DataTable dt = await _commonRepository.GetStaffProfileAsync(schoolId, userId).ConfigureAwait(false);
            StaffProfile staffProfile = new StaffProfile();
            foreach (DataRow dr in dt.Rows)
            {
                staffProfile.StaffName = Convert.ToString(dr["StaffName"]);
                staffProfile.EmpCode = Convert.ToString(dr["EmpCode"]);
                staffProfile.DOB = Convert.ToString(dr["DOB"]);
                staffProfile.DOJ = Convert.ToString(dr["DOJ"]);
                staffProfile.Mobile = Convert.ToString(dr["Mobile"]);
                staffProfile.ProfileImg = Convert.ToString(dr["ProfileImg"]);
            }
            return staffProfile;
        }
        public async Task<StudentProfile> GetStudentProfileAsync(int schoolId, int userId)
        {
            DataTable dt = await _commonRepository.GetStudentProfileAsync(schoolId, userId).ConfigureAwait(false);
            StudentProfile studentProfile = new StudentProfile();
            foreach (DataRow dr in dt.Rows)
            {
                studentProfile.StudentName = Convert.ToString(dr["StudentName"]);
                studentProfile.ClassName = Convert.ToString(dr["ClassName"]);
                studentProfile.SectionName = Convert.ToString(dr["SectionName"]);
                studentProfile.RollNo = Convert.ToInt32(dr["RollNo"]);
                studentProfile.DOB = Convert.ToString(dr["DOB"]);
                studentProfile.AdmNo = Convert.ToString(dr["AdmNo"]);
                studentProfile.Mobile = Convert.ToString(dr["Mobile"]);
                studentProfile.ProfileImg = Convert.ToString(dr["ProfileImg"]);
                studentProfile.FatherName = Convert.ToString(dr["FatherName"]);
                studentProfile.MotherName = Convert.ToString(dr["MotherName"]);
                studentProfile.ClassTeacher = Convert.ToString(dr["ClassTeacher"]);
            }
            return studentProfile;
        }
        public async Task<string> SaveEnquiryAsync(EnquiryM enquiry)
        {
           return await _commonRepository.SaveEnquiryAsync(enquiry).ConfigureAwait(false);
        }
        public async Task<string> UpdateEnqiriesAsync(int enquiryId)
        {
           return await _commonRepository.UpdateEnqiriesAsync(enquiryId).ConfigureAwait(false);
        }
        public async Task<List<StudentBirthday>> GetStudentsBirthdayAsync(int schoolId, int sessionId, int classId, int sectionId)
        {
            DataTable dt = await _commonRepository.GetStudentsBirthdayAsync(schoolId, sessionId, classId, sectionId).ConfigureAwait(false);
            List<StudentBirthday> studentBirthdays = new List<StudentBirthday>();
            foreach (DataRow dr in dt.Rows)
            {
                studentBirthdays.Add(new StudentBirthday()
                {
                    Name = dr["Name"].ToString(),
                    DOB = dr["Dob"].ToString(),
                    profilImg = dr["ProfileImg"].ToString()
                });
            }
            return studentBirthdays;
        }
        public async Task<List<StudentLeaveDto>> GetStudentonLeavetodayAsync(int schoolId, int sessionId, int classId, int sectionId)
        {
            DataTable dt = await _commonRepository.GetStudentonLeavetodayAsync(schoolId, sessionId, classId, sectionId).ConfigureAwait(false);
            List<StudentLeaveDto> studentLeaveDtos = [];
            foreach(DataRow dr in dt.Rows)
            {
                studentLeaveDtos.Add(new StudentLeaveDto
                {
                    Name = dr["Name"].ToString(),
                    ApprovalRemark  = dr["ApprovalRemark"].ToString(),
                     EndDate = Convert.ToDateTime(dr["EndDate"].ToString()),
                     LeaveId = Convert.ToInt32(dr["LeaveType"].ToString()),
                     NoOfDays = Convert.ToInt32(dr["NoOfDays"].ToString()),
                     Remarks = dr["Remarks"].ToString(),
                     RequestedOn = Convert.ToDateTime(dr["RequestedOn"].ToString()),
                     StartDate = Convert.ToDateTime(dr["StartDate"].ToString()),
                     Status = dr["Status"].ToString()
                });

            }
            return studentLeaveDtos;
        }
        public async Task<APPVersionM> GetAPPVersionAsync(int schoolId)
        {
            DataTable dt = await _commonRepository.GetAPPVersionAsync(schoolId).ConfigureAwait(false);
            return new APPVersionM()
            {
                APPURL = dt.Rows[0]["APPURL"].ToString(),
                CurrentVersion = Convert.ToDecimal(dt.Rows[0]["APPVersion"]),
                Message = "New version of this APP is available please update to see all features."
            };
        }
        public async Task<List<StaffLeaveDto>> GetStaffonLeavetodayAsync(int schoolId, int sessionId)
        {
            DataTable dt = await _commonRepository.GetStaffonLeavetodayAsync(schoolId, sessionId).ConfigureAwait(false);
            List<StaffLeaveDto> staffLeaveDtos = [];
            foreach (DataRow dr in dt.Rows)
            {
                staffLeaveDtos.Add(new StaffLeaveDto
                {
                    Name = dr["Name"].ToString(),
                    ApprovalRemark = dr["ApprovalRemark"].ToString(),
                    EndDate = Convert.ToDateTime(dr["EndDate"].ToString()),
                    LeaveId = Convert.ToInt32(dr["LeaveType"].ToString()),
                    NoOfDays = Convert.ToInt32(dr["NoOfDays"].ToString()),
                    Remarks = dr["Remarks"].ToString(),
                    RequestedOn = Convert.ToDateTime(dr["RequestedOn"].ToString()),
                    StartDate = Convert.ToDateTime(dr["StartDate"].ToString()),
                    Status = dr["Status"].ToString()
                });
            }
            return staffLeaveDtos;
        }
        public async Task<List<StudentBirthday>> GetStaffsBirthdayAsync(int schoolId, int sessionId) 
        {
            DataTable dt = await _commonRepository.GetStaffsBirthdayAsync(schoolId, sessionId).ConfigureAwait(false);
            List<StudentBirthday> staffBirthdays = [];
            foreach (DataRow dr in dt.Rows)
            {
                staffBirthdays.Add(new StudentBirthday()
                {
                    Name = dr["Name"].ToString(),
                    DOB = dr["Dob"].ToString(),
                    profilImg = dr["ProfileImg"].ToString()
                });
            }
            return staffBirthdays;
        }
        public async Task<string> SaveStudentDairyAsync(StudentDairyRequest request)
        {
            return await _commonRepository.SaveStudentDairyAsync(request).ConfigureAwait(false);
        }
        public async Task<List<StudentDairyResponse>> GetStudentDiaryAsync(StudentDairyRequest request)
        {
            DataTable dt = await _commonRepository.GetStudentDiaryAsync(request).ConfigureAwait(false);
            List<StudentDairyResponse> enquiries = [];
            foreach (DataRow dr in dt.Rows)
            {
                enquiries.Add(new StudentDairyResponse()
                {
                    StudentName = dr["StudentName"].ToString(),
                    SendDate = dr["adddate"].ToString(),
                    StaffName = dr["StaffName"].ToString(),
                    Message = dr["Message"].ToString(),
                    requestType = dr["requestType"].ToString(),
                    StudentId = Convert.ToInt32(dr["StudentId"].ToString()),
                    StaffId = Convert.ToInt32(dr["StaffId"].ToString())
                });
            }
            return enquiries;
        }
        public async Task<string> SaveNewsorEventsAsync(NewsorEventRequest request)
        {
            return await _commonRepository.SaveNewsorEventsAsync(request).ConfigureAwait(false);
        }
        public async Task<List<NewsorEventResponse>> GetNewsorEventsAsync(int schoolId)
        {
            List<NewsorEventResponse> enquiries = [];
            DataTable dt = await _commonRepository.GetNewsorEventsAsync(schoolId).ConfigureAwait(false);
            foreach (DataRow dr in dt.Rows)
            {
                enquiries.Add(new NewsorEventResponse()
                {
                    Title = dr["Title"].ToString(),
                    ActionDate = dr["NewsOrEventDate"].ToString(),
                    Category = dr["Catagory"].ToString(),
                    Message = dr["Description"].ToString(),
                    IsPublished = Convert.ToBoolean(dr["Publish"].ToString()),
                    Id = Convert.ToInt32(dr["NEId"].ToString()),
                    Organizer = dr["Organizer"].ToString(),
                    Attender = dr["Attender"].ToString()
                });
            }
            return enquiries;

        }
        public async Task<DashboardCollectionSummaryResponse> GetDashboardDataAsync(int schoolId, int sessionId)
        {
            DashboardCollectionSummaryResponse enquiries = new();
            DataTable dt = await _commonRepository.GetDashboardDataAsync(schoolId, sessionId).ConfigureAwait(false);
            foreach (DataRow dr in dt.Rows)
            {
                enquiries.NoofStudentPaid = Convert.ToInt32(dr["totalStudentPaid"]);
                enquiries.TodaysCollection = Convert.ToInt32(dr["TodaysCollection"]);
                enquiries.MonthCollection = Convert.ToInt32(dr["MonthCollection"]);
            }
            return enquiries;

        }
        public async Task<List<SmsTemplateDto>> GetTemplateSmsAsync(int schoolId, int sessionId)
        {
            DataTable dt = await _commonRepository.GetTemplateSmsAsync(schoolId, sessionId).ConfigureAwait(false);
            List<SmsTemplateDto> smsTemplateDtos = [];
            foreach (DataRow dr in dt.Rows)
            {
                smsTemplateDtos.Add(new SmsTemplateDto
                {
                    TemplateId = Convert.ToInt32(dr["TemplateId"]),
                    TemplateName = dr["TemplateName"].ToString(),
                     DltTemplateId = dr["DltTemplateId"].ToString(),
                      EntityId = dr["EntityId"].ToString(),
                       TemplateDesc = dr["TemplateDesc"].ToString()
                });
            }
            return smsTemplateDtos;
        }
        public async Task<TemplateDescDto> GetSMSTemaplateDescAsync(int schoolId, int templateId)
        {
            DataTable dt = await _commonRepository.GetSMSTemaplateDescAsync(schoolId, templateId).ConfigureAwait(false);
            TemplateDescDto templateDescDto = new TemplateDescDto();
            foreach (DataRow dr in dt.Rows)
            {
                templateDescDto.TempDesc = dr["TempDesc"].ToString();
            }
            return templateDescDto;
        }
        public async Task<string> GetSMSCreditAsync(int schoolId)
        {
            return await _commonRepository.GetSMSCreditAsync(schoolId).ConfigureAwait(false);
        }
        public async Task<bool> SaveFirebaseTockenAsync(string tocken, int userId, int userTypeId, int schoolId)
        {
            return await _commonRepository.SaveFirebaseTockenAsync(tocken, userId, userTypeId, schoolId).ConfigureAwait(false);
        }
        public async Task<bool> DeleteFirebaseTockenAsync(string tocken)
        {
            return await _commonRepository.DeleteFirebaseTockenAsync(tocken).ConfigureAwait(false);
        }
        public async Task<PrincipleProfile> PrincipleProfilesAsync(int schoolId)
        {
            DataTable dt = await _commonRepository.PrincipleProfilesAsync(schoolId).ConfigureAwait(false);
            PrincipleProfile staffProfile = new PrincipleProfile();
            foreach (DataRow dr in dt.Rows)
            {
                staffProfile.PrincipleName = Convert.ToString(dr["PrincipleName"]);
                staffProfile.PrincipleMassage = Convert.ToString(dr["PrincipleMassage"]);
                staffProfile.PrincipleImage = Convert.ToString(dr["PrincipleImage"]);
                staffProfile.Degree = Convert.ToString(dr["Degree"]);

            }
            return staffProfile;
        }
        public async Task<StudentRegistrationResponce> SaveRegistrationAsync(StudentRegistrationWeb registration)
        {
            StudentRegistrationResponce student = new StudentRegistrationResponce();
            DataTable dt = await _commonRepository.SaveRegistrationAsync(registration).ConfigureAwait(false);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    student.RegNumber = Convert.ToString(dr["RegNumber"]);
                    student.RegStatus = dr["RegStatus"].ToString();
                    student.Name = Convert.ToString(dr["Name"]);
                    student.FatherName = Convert.ToString(dr["FatherName"]);
                    student.MotherName = Convert.ToString(dr["MotherName"]);
                    student.RegFee = Convert.ToInt32(dr["RegFee"]);
                    student.ApplicationMode = Convert.ToString(dr["ApplicationMode"]);
                    student.Dob = Convert.ToDateTime(dr["Dob"]);
                    student.FMobile = Convert.ToString(dr["FMobile"]);
                    student.Gender = Convert.ToString(dr["Gender"]);
                    student.DateOfReg = Convert.ToDateTime(dr["DateOfReg"]);
                    student.Residence = Convert.ToString(dr["Residence"]);
                    student.City = Convert.ToString(dr["City"]);
                    student.Country = Convert.ToString(dr["Country"]);
                    student.State = Convert.ToString(dr["State"]);
                    student.PinCode = Convert.ToInt32(dr["PinCode"]);
                    student.BloodGroup = Convert.ToString(dr["BloodGroup"]);
                    student.Religion = Convert.ToString(dr["Religion"]);
                    student.Category = Convert.ToString(dr["Category"]);
                }
            }

            return student;
        }

        public async Task<DataTable> GetDLTDetailsByTemplateNameAsync(int schoolId, string templateName)
        {
            return await _commonRepository.GetDLTDetailsByTemplateNameAsync(schoolId, templateName).ConfigureAwait(false);
        }
        public async Task<List<ViewStudentM>> GetStudentListAsync(int classId, int sectionId, int schoolId, int sessionId)
        {
            List<ViewStudentM> lviewStudentMs = new List<ViewStudentM>();
            DataTable dt = await _commonRepository.GetStudentListAsync(classId, sectionId, schoolId, sessionId).ConfigureAwait(false);
            foreach (DataRow data in  dt.Rows)
            {
                ViewStudentM viewStudentM = new ViewStudentM();
                viewStudentM.StudentFirstName = data["Name"].ToString();
                viewStudentM.AdmissionNumber = data["AdmissionNumber"].ToString();
                viewStudentM.rollNo = Convert.ToInt32(data["RollNo"]);
                viewStudentM.StudentId = Convert.ToInt32(data["StudentId"]);
                lviewStudentMs.Add(viewStudentM);
            }
            return lviewStudentMs;
        }
        public async Task<string?> GetStudentMobileByStudentIdAsync(int studentId)
        {
            return await _commonRepository.GetStudentMobileByStudentIdAsync(studentId).ConfigureAwait(false);
        }
        public async Task<string> FTSMessanger(string msg, string listMobile, int SchoolId, int type, string sid, string entityId = null, string dltTemplateId = null, int languageid = 1)
        {
            string message = "";
            try
            {
                DataTable dt =  await _commonRepository.GetApiDetailAsync(SchoolId).ConfigureAwait(false);
                string _createURL = string.Empty;
                string sResponse = string.Empty;
                int MessgCount = 1;
                if (dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0]["Credit_Value"]) > 0)
                {
                    if (languageid == 1 && !ContainsUnicodeCharacter(msg))
                    {
                        MessgCount = msg.Length > 160 ? ((msg.Length / 160) + (msg.Length % 160 == 0 ? 0 : 1)) : 1;
                        if (dt.Rows[0]["provider"].ToString() == "click")
                        {
                            _createURL = Convert.ToString(dt.Rows[0]["url"]) + Convert.ToString(dt.Rows[0]["Apikey"]) +
                                            "&senderid=" + Convert.ToString(dt.Rows[0]["SenderId"]) +
                                            "&channel=" + Convert.ToString(dt.Rows[0]["Channel"]) +
                                            "&number=" + HttpUtility.UrlEncode(listMobile.TrimEnd(',')) +
                                            "&text=" + HttpUtility.UrlEncode(msg) +
                                            "&route=" + Convert.ToString(dt.Rows[0]["route"]) +
                                            "&Peid=" + entityId +
                                            "&DLTTemplateId=" + dltTemplateId;
                        }
                        else
                        {
                            _createURL = Convert.ToString(dt.Rows[0]["url"]) + Convert.ToString(dt.Rows[0]["Apikey"]) +
                                     "&senderid=" + Convert.ToString(dt.Rows[0]["SenderId"]) +
                                     "&channel=" + Convert.ToString(dt.Rows[0]["unicodechannel"]) +
                                    "&number=" + HttpUtility.UrlEncode(listMobile.TrimEnd(',')) +
                                    "&text=" + HttpUtility.UrlEncode(msg) +
                                    "&route=" + Convert.ToString(dt.Rows[0]["route"]) +
                                    "&EntityId=" + entityId +
                                    "&dlttemplateid=" + dltTemplateId;
                        }
                    }
                    else
                    {
                        MessgCount = msg.Length > 67 ? ((msg.Length / 67) + (msg.Length % 67 == 0 ? 0 : 1)) : 1;
                        if (dt.Rows[0]["provider"].ToString() == "click")
                        {
                            _createURL = Convert.ToString(dt.Rows[0]["url"]) + Convert.ToString(dt.Rows[0]["Apikey"]) +
                                    "&senderid=" + Convert.ToString(dt.Rows[0]["SenderId"]) +
                                    "&channel=" + Convert.ToString(dt.Rows[0]["unicodechannel"]) +
                                    "&number=" + HttpUtility.UrlEncode(listMobile.TrimEnd(',')) +
                                    "&text=" + HttpUtility.UrlEncode(msg) +
                                    "&route=" + Convert.ToString(dt.Rows[0]["route"]) +
                                    "&Peid=" + entityId +
                                    "&DLTTemplateId=" + dltTemplateId;
                        }
                        else
                        {
                            _createURL = Convert.ToString(dt.Rows[0]["url"]) + Convert.ToString(dt.Rows[0]["Apikey"]) +
                                     "&senderid=" + Convert.ToString(dt.Rows[0]["SenderId"]) +
                                     "&channel=" + Convert.ToString(dt.Rows[0]["unicodechannel"]) +
                                    "&number=" + HttpUtility.UrlEncode(listMobile.TrimEnd(',')) +
                                    "&text=" + HttpUtility.UrlEncode(msg) +
                                    "&route=" + Convert.ToString(dt.Rows[0]["route"]) +
                                    "&EntityId=" + entityId +
                                    "&dlttemplateid=" + dltTemplateId;
                        }
                    }
                    sResponse = GetResponse(_createURL);

                    if (sResponse != string.Empty)
                    {
                        smsDetails jobj = JsonSerializer.Deserialize<smsDetails>(sResponse);
                        DeliveryReport jobStatus = new DeliveryReport()
                        {
                            DeliveryStatus = "done",
                            DeliveryCount = MessgCount
                        };
                        /*GetMessageStatusByJobId(jobj.JobId);*/
                        sResponse = SaveSMSLog(msg, jobj, jobStatus, SchoolId, listMobile, type, sid);
                    }
                }
                else
                {
                    message = "Messaging Services not configured or insufficient Message credit";
                }

            }
            catch (Exception ex)
            {
                message = ex.StackTrace.ToString();
            }
            return message;
        }
        public Task<List<ClassM>> GetClassListAsync(int schoolId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<SectionM>> GetSectionListAsync(int schoolId, int classId, int userId)
        {
            throw new NotImplementedException();
        }

        private bool ContainsUnicodeCharacter(string input)
        {
            const int MaxAnsiCode = 255;

            return input.Any(c => c > MaxAnsiCode);
        }
        private string GetResponse(string sURL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sURL);
            request.MaximumAutomaticRedirections = 4;
            request.Credentials = CredentialCache.DefaultCredentials;

            try
            {
                request.ServicePoint.Expect100Continue = true;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string sResponse = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
                return sResponse;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        private string SaveSMSLog(string msg, smsDetails jobj, DeliveryReport jobStatus, int SchoolId, string listMobile, int type, string sid)
        {
            string message = "";
            DataTable smsDT = new DataTable();
            smsDT.Columns.AddRange(new DataColumn[11]
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
                        new DataColumn("UId", typeof(string)),
                        new DataColumn("MessageCount", typeof(int))
            });
            if (jobj.MessageData != null)
            {
                foreach (var s in jobj.MessageData)
                {
                    smsDT.Rows.Add(
                               SchoolId,
                               s.Number == null ? listMobile : s.Number,
                               s.MessageId == null ? "" : s.MessageId,
                               s.Message == null ? msg : s.Message,
                               jobj.ErrorCode,
                               jobStatus.DeliveryStatus,
                               jobj.JobId,
                               type,
                               DateTime.Now,
                               sid,
                               jobStatus.DeliveryCount
                             );
                }
                _commonRepository.UpdateSMSCreditAsync(SchoolId, smsDT.Rows.Count * jobStatus.DeliveryCount);
                message = "Messge Sent Successfully!!";
            }
            else
            {
                smsDT.Rows.Add(
                               SchoolId,
                               listMobile,
                               "",
                               msg,
                               jobj.ErrorCode,
                               jobj.ErrorMessage,
                               jobj.JobId == null ? "" : jobj.JobId,
                               type,
                               DateTime.Now,
                               sid,
                               0
                             );
                message = "Insufficient Messaage Credit!!";
            }
            _commonRepository.InsertSMSLogAsync(smsDT);
            return message;
        }

        Task<DataTable> ICommonService.GetStudentonLeavetodayAsync(int schoolId, int sessionId, int classId, int sectionId)
        {
            throw new NotImplementedException();
        }

        Task<DataTable> ICommonService.GetStaffonLeavetodayAsync(int schoolId, int sessionId)
        {
            throw new NotImplementedException();
        }

        Task<DataTable> ICommonService.GetTemplateSmsAsync(int schoolId, int sessionId)
        {
            throw new NotImplementedException();
        }

        Task<DataTable> ICommonService.GetSMSTemaplateDescAsync(int schoolId, int templateId)
        {
            throw new NotImplementedException();
        }

        public Task<DataTable> GetApiDetailAsync(int schoolId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSMSCreditAsync(int schoolId, int credit)
        {
            throw new NotImplementedException();
        }
    }
}
