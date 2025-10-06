using Microsoft.Data.SqlClient;
using SchoolAPI.Infrastructure.Factory;
using SchoolAPI.Models.Common;
using System.Data;

namespace SchoolAPI.Repositories.CommonRepository
{
    public class CommonRepository(IDbConnectionFactory dbConnectionFactory) : ICommonRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;
        public async Task<DataTable> GetClassByTeacherAsync(int schoolId, int userId)
        {
            var dt = new DataTable();

            try
            {
                using var conn = _dbConnectionFactory.CreateConnection();
                using var cmd = new SqlCommand("upS_GetClassTeacherId", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddRange(new[]
                {
                    new SqlParameter("@SchoolId", schoolId),
                    new SqlParameter("@UserId", userId)
                });

                await conn.OpenAsync();

                using var reader = await cmd.ExecuteReaderAsync();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                // Log exception as needed
                throw;
            }

            return dt;
        }

        public async Task<DataTable> GetSectionAsync(int classId, int schoolId, int userId = 0)
        {
            var dt = new DataTable();

            try
            {
                using var conn = _dbConnectionFactory.CreateConnection();
                using var cmd = new SqlCommand("upS_GetSectionMaster", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddRange(new[]
                {
            new SqlParameter("@SchoolId", schoolId),
            new SqlParameter("@ClassId", classId),
            new SqlParameter("@UserId", userId)
        });

                await conn.OpenAsync();

                using var reader = await cmd.ExecuteReaderAsync();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                // Log or handle exception
                throw;
            }

            return dt;
        }
        public async Task<DataTable> GetSubjectAsync(int schoolId)
        {
            var dt = new DataTable();

            try
            {
                using var conn = _dbConnectionFactory.CreateConnection();
                using var cmd = new SqlCommand("Sp_AssHomeSub", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add(new SqlParameter("@SchoolId", schoolId));

                await conn.OpenAsync();

                using var reader = await cmd.ExecuteReaderAsync();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                // Log or handle exception
                throw;
            }

            return dt;
        }
        public async Task<DataTable> GetHoliDaysAsync(int schoolId)
        {
            var dt = new DataTable();

            try
            {
                using var conn = _dbConnectionFactory.CreateConnection();
                using var cmd = new SqlCommand("usp_getHolidayList", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add(new SqlParameter("@SchoolId", schoolId));

                await conn.OpenAsync();

                using var reader = await cmd.ExecuteReaderAsync();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                // Log or handle exception
                throw;
            }

            return dt;
        }
        public async Task<DataTable> GetStaffProfileAsync(int schoolId, int staffId)
        {
            var dt = new DataTable();
             
            using (SqlConnection conn = _dbConnectionFactory.CreateConnection())
            {
                using (SqlCommand cmd = new SqlCommand("ups_GetStaffProfile", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@SchoolId", schoolId));
                    cmd.Parameters.Add(new SqlParameter("@StaffId", staffId));

                    await conn.OpenAsync();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        dt.Load(reader);
                    }
                }
            }

            return dt;
        }
        public async Task<DataTable> GetStudentProfileAsync(int schoolId, int studentId)
        {
            var dt = new DataTable(); 
            using (SqlConnection conn = _dbConnectionFactory.CreateConnection())
            {
                using (SqlCommand cmd = new SqlCommand("ups_GetStudentProfile", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@SchoolId", schoolId));
                    cmd.Parameters.Add(new SqlParameter("@StudentId", studentId));

                    await conn.OpenAsync();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        dt.Load(reader);
                    }
                }
            }

            return dt;
        }

        public async Task<DataTable> GetStudentsBirthdayAsync(int schoolId, int sessionId, int classId, int sectionId)
        {
            var dataTable = new DataTable();
            using var conn = _dbConnectionFactory.CreateConnection();
            using (var cmd = new SqlCommand("usp_GetStudentBirthdayToday", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SchoolId", schoolId);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                cmd.Parameters.AddWithValue("@ClassId", classId);
                cmd.Parameters.AddWithValue("@SectionId", sectionId);

                await conn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }
            }

            return dataTable;
        }
        public async Task<DataTable> GetStudentsOnLeaveTodayAsync(int schoolId, int sessionId, int classId, int sectionId)
        {
            var dataTable = new DataTable();
            using (var conn = _dbConnectionFactory.CreateConnection())
            using (var cmd = new SqlCommand("usp_GetStudentsOnLeaveToday", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SchoolId", schoolId);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                cmd.Parameters.AddWithValue("@ClassId", classId);
                cmd.Parameters.AddWithValue("@SectionId", sectionId);

                await conn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }
            }

            return dataTable;
        }
        public async Task<DataTable> GetAPPVersionBySchoolIdAsync(int schoolId)
        {
            var dataTable = new DataTable();
            using (var conn = _dbConnectionFactory.CreateConnection())
            using (var cmd = new SqlCommand("usp_GetAPPVersionBySchoolId", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolId", schoolId);

                await conn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }
            }

            return dataTable;
        }
        public  async Task<DataTable> GetStaffOnLeaveTodayAsync(int schoolId, int sessionId)
        {
            var dataTable = new DataTable();
            using (var conn = _dbConnectionFactory.CreateConnection())
            using (var cmd = new SqlCommand("USP_GetStaffOnLeave", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolId", schoolId);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);

                await conn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }
            }

            return dataTable;
        }
        public async Task<string> SaveStudentDairyAsync(StudentDairyRequest request)
        {
            string result = string.Empty;
            using (var conn = _dbConnectionFactory.CreateConnection())
            using (var cmd = new SqlCommand("usp_SaveStudentDairy", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SchoolId", request.SchoolId);
                cmd.Parameters.AddWithValue("@SessionId", request.SessionId);
                cmd.Parameters.AddWithValue("@StudentId", request.StudentId);
                cmd.Parameters.AddWithValue("@AddedBy", request.StaffId);
                cmd.Parameters.AddWithValue("@Message", request.Message);
                cmd.Parameters.AddWithValue("@requestType", request.requestType);

                // Output parameter
                var outputParam = new SqlParameter("@Msg", SqlDbType.VarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputParam);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                result = outputParam.Value?.ToString();
            }

            return result;
        }
        public async Task<DataTable> GetStudentDiaryAsync(StudentDairyRequest request)
        {
            var dt = new DataTable(); 
            using (var conn = _dbConnectionFactory.CreateConnection())
            using (var cmd = new SqlCommand("usp_GetStudentDairy", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SchoolId", request.SchoolId);
                cmd.Parameters.AddWithValue("@SessionId", request.SessionId);
                cmd.Parameters.AddWithValue("@StudentId", request.StudentId);
                cmd.Parameters.AddWithValue("@StaffId", request.StaffId);

                await conn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dt.Load(reader);
                }
            }

            return dt;
        }
        public async Task<string> SaveNewsorEventsAsync(NewsorEventRequest request)
        { 
            using (var conn = _dbConnectionFactory.CreateConnection())
            using (var cmd = new SqlCommand("Usp_INewsAndEvents", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SchoolId", request.SchoolId);
                cmd.Parameters.AddWithValue("@SessionId", request.SessionId);
                cmd.Parameters.AddWithValue("@Title", request.Title);
                cmd.Parameters.AddWithValue("@Description", request.Message);
                cmd.Parameters.AddWithValue("@Catagory", request.Category);
                cmd.Parameters.AddWithValue("@NewsOrEventDate", request.NewsOrEventDate);
                cmd.Parameters.AddWithValue("@Publish", 1);
                cmd.Parameters.AddWithValue("@UserId", request.UserId);

                await conn.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();

                return rowsAffected > 0 ? "Data saved successfully!" : "There is some technical error!";
            }
        }
        public  async Task<DataTable> GetNewsorEventsAsync(int schoolId)
        { 
            using (var conn = _dbConnectionFactory.CreateConnection())
            using (var cmd = new SqlCommand("uSp_SNewsAndEvents", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolId", schoolId);

                var dt = new DataTable();
                await conn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dt.Load(reader);
                }

                return dt;
            }
        }
        public async Task<DataTable> GetDashboardDataAsync(int schoolId, int sessionId)
        { 
            using (var conn = _dbConnectionFactory.CreateConnection())
            using (var cmd = new SqlCommand("USP_CollectionSumary", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolId", schoolId);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);

                var dt = new DataTable();
                await conn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dt.Load(reader);
                }

                return dt;
            }
        }
        public async Task<DataTable> GetTemplateSmsAsync(int schoolId, int type)
        { 
            using (var conn = _dbConnectionFactory.CreateConnection())
            using (var cmd = new SqlCommand("Sp_GetSMSTemplates", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolId", schoolId);
                cmd.Parameters.AddWithValue("@Type", type);

                var dt = new DataTable();
                await conn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dt.Load(reader);
                }

                return dt;
            }
        }
        public async Task<DataTable> GetSMSTemplateDescAsync(int tempId, int schoolId)
        { 
            using (var conn = _dbConnectionFactory.CreateConnection())
            using (var cmd = new SqlCommand("Sp_GetTemplateDesc", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TemplateId", tempId);
                cmd.Parameters.AddWithValue("@SchoolId", schoolId);

                var dt = new DataTable();
                await conn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dt.Load(reader);
                }

                return dt;
            }
        }

        public async Task<string> GetSMSCreditAsync(int schoolId)
        { 
            using (var conn = _dbConnectionFactory.CreateConnection())
            using (var cmd = new SqlCommand("Sp_GetSMSCredit", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolId", schoolId);

                await conn.OpenAsync();

                var result = await cmd.ExecuteScalarAsync();
                return result?.ToString() ?? string.Empty;
            }
        }
        public async Task<bool> SaveFirebaseTokenAsync(string token, int userId, int userTypeId, int schoolId)
        { 
            using (var conn = _dbConnectionFactory.CreateConnection())
            using (var cmd = new SqlCommand("usp_SavePushnotificationKey", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tocken", token);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@userTypeId", userTypeId);
                cmd.Parameters.AddWithValue("@SchoolId", schoolId);

                await conn.OpenAsync();
                var rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0; // return true if insert/update succeeded
            }
        }
        public async Task<bool> DeleteFirebaseTokenAsync(string token)
        {
            using (var conn = _dbConnectionFactory.CreateConnection())
            using (var cmd = new SqlCommand("usp_DeletePushnotificationKey", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tocken", token);

                await conn.OpenAsync();
                var rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0; // true if deleted
            }
        }
        public async Task<DataTable> GetPrincipalMsgAsync(int schoolId)
        {
            using (var conn = _dbConnectionFactory.CreateConnection())
            using (var cmd = new SqlCommand("ups_GetPrincipleMsg", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolId", schoolId);

                await conn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    var dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
        }
        public async Task<DataTable> GetDLTDetailsByTemplateNameAsync(int schoolId, string templateName)
        {
            using (var conn = _dbConnectionFactory.CreateConnection())
            using (var cmd = new SqlCommand("usp_GetDLTDetailsbyTemplate", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Template", templateName);
                cmd.Parameters.AddWithValue("@SchoolId", schoolId);

                await conn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    var dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
        }
        public async Task<DataTable> SaveRegistrationAsync(StudentRegistrationWeb registration)
        {
            using (var conn = _dbConnectionFactory.CreateConnection())
            using (var cmd = new SqlCommand("usp_SaveRegistrationWeb", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SchoolId", registration.SchoolId);
                cmd.Parameters.AddWithValue("@Name", registration.Name ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Dob", registration.Dob);
                cmd.Parameters.AddWithValue("@Class", registration.Class ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Gender", registration.Gender ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@BloodGroup", registration.BloodGroup ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Religion", registration.Religion ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Category", registration.Category ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@AadharNo", registration.AadharNo ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PreviousClass", registration.PreviousClass ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FatherName", registration.FatherName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Occupation", registration.Occupation ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FMobile", registration.FMobile ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@MotherName", registration.MotherName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@MOccupation", registration.Moccupation ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@MMobile", registration.MMobile ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Residence", registration.Residence ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Country", registration.Country ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@State", registration.State ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@City", registration.City ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PinCode", registration.PinCode);
                cmd.Parameters.AddWithValue("@SchoolDis", registration.SchoolDis ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@RegStatus", registration.RegStatus ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ApplicationMode", registration.ApplicationMode ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@RegFee", registration.RegFee);

                await conn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    var dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
        }
        public async Task<DataTable> GetStudentListAsync(int classId, int sectionId, int schoolId, int sessionId)
        {
            using (var conn = _dbConnectionFactory.CreateConnection())
            using (var cmd = new SqlCommand("SP_GetStudentList", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClassId", classId);
                cmd.Parameters.AddWithValue("@SectionId", sectionId);
                cmd.Parameters.AddWithValue("@SchoolId", schoolId);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);

                await conn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    var dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
        }
        public async Task<string?> GetStudentMobileByStudentIdAsync(int studentId)
        {
            await using var conn = _dbConnectionFactory.CreateConnection();
            await using var cmd = new SqlCommand("Sp_GetStudentMobilebyStudentId", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add(new SqlParameter("@StudentId", SqlDbType.Int) { Value = studentId });

            var outputParam = new SqlParameter("@Msg", SqlDbType.VarChar, 100)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(outputParam);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return outputParam.Value as string;
        }
        public async Task<DataTable> GetApiDetailAsync(int schoolId)
        {
            var dataTable = new DataTable();

            await using var conn = _dbConnectionFactory.CreateConnection();
            await using var cmd = new SqlCommand("SP_GetApiDetail", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add(new SqlParameter("@SchoolId", SqlDbType.Int) { Value = schoolId });

            await conn.OpenAsync();
            await using var reader = await cmd.ExecuteReaderAsync();
            dataTable.Load(reader);

            return dataTable;
        }
        public async Task UpdateSMSCreditAsync(int schoolId, int credit)
        {
            await using var conn = _dbConnectionFactory.CreateConnection();
            await using var cmd = new SqlCommand("Sp_UpdateSMSCredit", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add(new SqlParameter("@SchoolId", SqlDbType.Int) { Value = schoolId });
            cmd.Parameters.Add(new SqlParameter("@Credit", SqlDbType.Int) { Value = credit });

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
        public async Task<string> InsertSMSLogAsync(DataTable dt)
        {
            await using var conn = _dbConnectionFactory.CreateConnection();
            await using var cmd = new SqlCommand("usp_CreateSMSLog", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add(new SqlParameter("@SMSLog", SqlDbType.Structured)
            {
                TypeName = dt.TableName, // Make sure the DataTable has the correct type name
                Value = dt
            });

            var msgParam = new SqlParameter("@Msg", SqlDbType.VarChar, 100)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(msgParam);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return msgParam.Value?.ToString() ?? string.Empty;
        }
         

        public Task<DataTable> GetStudentonLeavetodayAsync(int schoolId, int sessionId, int classId, int sectionId)
        {
            throw new NotImplementedException();
        }

        public Task<DataTable> GetAPPVersionAsync(int schoolId)
        {
            throw new NotImplementedException();
        }

        public Task<DataTable> GetStaffonLeavetodayAsync(int schoolId, int sessionId)
        {
            throw new NotImplementedException();
        }

        public Task<DataTable> GetStaffsBirthdayAsync(int schoolId, int sessionId)
        {
            throw new NotImplementedException();
        }

        public Task<DataTable> GetSMSTemaplateDescAsync(int schoolId, int templateId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveFirebaseTockenAsync(string tocken, int userId, int userTypeId, int schoolId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteFirebaseTockenAsync(string tocken)
        {
            throw new NotImplementedException();
        }

        public Task<DataTable> PrincipleProfilesAsync(int schoolId)
        {
            throw new NotImplementedException();
        }

    }
}
