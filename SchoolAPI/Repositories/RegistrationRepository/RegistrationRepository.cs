using Microsoft.Data.SqlClient;
using SchoolAPI.Enums;
using SchoolAPI.Infrastructure.Factory;
using SchoolAPI.Models.Common;
using SchoolAPI.Models.Registration;
using System.Data;

namespace SchoolAPI.Repositories.RegistrationRepository
{
    public class RegistrationRepository(IDbConnectionFactory dbConnectionFactory) : IRegistrationRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

        public async Task<DataTable> BindCategoryDropdownsAsync()
        {
            var dt = new DataTable();
            await using var conn = _dbConnectionFactory.CreateConnection();
            await using var cmd = new SqlCommand("SP_GetCategoryList", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            await conn.OpenAsync();

            using var reader = await cmd.ExecuteReaderAsync();
            dt.Load(reader);

            return dt;
        }
        public async Task<string> GetNewRegNumberAsync(int schoolId)
        {
            await using var conn = _dbConnectionFactory.CreateConnection();
            await using var cmd = new SqlCommand("Sp_getNewRegNumber", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@SchoolId", schoolId);

            var outputParam = new SqlParameter("@Msg", SqlDbType.VarChar, 100)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(outputParam);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return outputParam.Value?.ToString() ?? string.Empty;
        }
        public async Task<DataTable> BindReligionDropdownsAsync()
        {
            var dt = new DataTable();

            await using var conn = _dbConnectionFactory.CreateConnection();
            await using var cmd = new SqlCommand("SP_GetReligionList", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            await conn.OpenAsync();

            await using var reader = await cmd.ExecuteReaderAsync();
            dt.Load(reader);

            return dt;
        }
        public async Task<List<BloodGroupDto>> GetBloodGroupAsync()
        {
            var result = new List<BloodGroupDto>();

            await using var conn = _dbConnectionFactory.CreateConnection();
            string sql = "SELECT bloodgroupId AS Id, bloodgroupName AS GroupName FROM BloodGroup";

            await using var cmd = new SqlCommand(sql, conn)
            {
                CommandType = CommandType.Text
            };

            await conn.OpenAsync();
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                result.Add(new BloodGroupDto
                {
                    Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0,
                    GroupName = reader["GroupName"]?.ToString()
                });
            }

            return result;
        }

        public async Task<List<StateDto>> GetStates()
        {
            var result = new List<StateDto>();

            await using var conn = _dbConnectionFactory.CreateConnection();
            string sql = "select Id, Name from State where IsActive = 1";

            await using var cmd = new SqlCommand(sql, conn)
            {
                CommandType = CommandType.Text
            };

            await conn.OpenAsync();
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                result.Add(new StateDto
                {
                    Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0,
                    Name = reader["Name"]?.ToString()
                });
            }

            return result;
        }

        public async Task<string> SaveRegistrationAsync(StudentRegistrationModelReq objstudentregistration)
        {
            string result = string.Empty;

            using (var connection = _dbConnectionFactory.CreateConnection())
            using (var command = new SqlCommand("USP_Student_Registration", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddRange(new[]
                {

                                new SqlParameter("@Registration", objstudentregistration.Registration ?? (object)DBNull.Value),
                                new SqlParameter("@DateofRegistration", objstudentregistration.DateOfRegistration),
                                new SqlParameter("@Class", objstudentregistration.Class),
                                new SqlParameter("@Firstname", objstudentregistration.FirstName ?? (object)DBNull.Value),
                                new SqlParameter("@MiddleName", objstudentregistration.MiddleName ?? (object)DBNull.Value),
                                new SqlParameter("@LastName", objstudentregistration.LastName ?? (object)DBNull.Value),
                                new SqlParameter("@Dob", objstudentregistration.Dob),
                                new SqlParameter("@BirthPlace", objstudentregistration.BirthPlace ?? (object)DBNull.Value),
                                new SqlParameter("@radioValue", objstudentregistration.IsPermanentAddressSame),
                                new SqlParameter("@AddressLine1S", objstudentregistration.AddressLine1S ?? (object)DBNull.Value),
                                new SqlParameter("@AddressLine2S", objstudentregistration.AddressLine2S ?? (object)DBNull.Value),
                                new SqlParameter("@CountryS", objstudentregistration.CountryS),
                                new SqlParameter("@Statep", objstudentregistration.StateP),
                                new SqlParameter("@cityp", objstudentregistration.CityP ?? (object)DBNull.Value),
                                new SqlParameter("@Pincodep", objstudentregistration.PincodeP),
                                new SqlParameter("@fathermobilenumber", objstudentregistration.FatherMobileNumber ?? (object)DBNull.Value),
                                new SqlParameter("@mobilenop", objstudentregistration.MobileNoP ?? (object)DBNull.Value),
                                new SqlParameter("@Presentdistance", objstudentregistration.Distance),
                                new SqlParameter("@Addressline1ph", objstudentregistration.AddressLine1Ph ?? (object)DBNull.Value),
                                new SqlParameter("@Addresslineph", objstudentregistration.AddressLinePh ?? (object)DBNull.Value),
                                new SqlParameter("@Countryph", objstudentregistration.CountryPh),
                                new SqlParameter("@Stateph", objstudentregistration.StatePh),
                                new SqlParameter("@Cityph", objstudentregistration.CityPh ?? (object)DBNull.Value),
                                new SqlParameter("@PinCodeph", objstudentregistration.PinCodePh),
                                new SqlParameter("@Distance", objstudentregistration.Distance),
    
                                // Father details
                                new SqlParameter("@Firstnamefather", objstudentregistration.FirstNameFather ?? (object)DBNull.Value),
                                new SqlParameter("@Middlenamefather", objstudentregistration.MiddleNameFather ?? (object)DBNull.Value),
                                new SqlParameter("@Lastnamefather", objstudentregistration.LastNameFather ?? (object)DBNull.Value),
                                new SqlParameter("@EducationQualificationfather", objstudentregistration.EducationQualificationFather ?? (object)DBNull.Value),
                                new SqlParameter("@ProfessionalQualificationfather", objstudentregistration.ProfessionalQualificationFather ?? (object)DBNull.Value),
                                new SqlParameter("@Occupation", objstudentregistration.Occupation ?? (object)DBNull.Value),

                                // Mother details
                                new SqlParameter("@Firstnamemother", objstudentregistration.FirstNameMother ?? (object)DBNull.Value),
                                new SqlParameter("@Middlenamemother", objstudentregistration.MiddleNameMother ?? (object)DBNull.Value),
                                new SqlParameter("@LastNamemother", objstudentregistration.LastNameMother ?? (object)DBNull.Value),
                                new SqlParameter("@EducationQualificationmother", objstudentregistration.EducationQualificationMother ?? (object)DBNull.Value),
                                new SqlParameter("@ProfessionalQualificationmother", objstudentregistration.ProfessionalQualificationMother ?? (object)DBNull.Value),
                                new SqlParameter("@Occupationmother", objstudentregistration.OccupationMother ?? (object)DBNull.Value),

                                new SqlParameter("@CreatedBY", objstudentregistration.CreatedBy ?? (object)DBNull.Value),
                                new SqlParameter("@schoolid", objstudentregistration.SchoolId),
                                new SqlParameter("@Sessionid", objstudentregistration.SessionId),
                                new SqlParameter("@Fathermobileno1", objstudentregistration.FatherMobile1 ?? (object)DBNull.Value),
                                new SqlParameter("@mobile1", objstudentregistration.Mobile1 ?? (object)DBNull.Value),
                                new SqlParameter("@AadharNo", objstudentregistration.AadharNo ?? (object)DBNull.Value),
                                new SqlParameter("@RegFee", objstudentregistration.RegFee),
                                new SqlParameter("@BloodGroup", objstudentregistration.BloodGroup),
                                new SqlParameter("@Category", objstudentregistration.Category),
                                new SqlParameter("@Religion", objstudentregistration.Religion),
                                new SqlParameter("@SiblingsStudentId", objstudentregistration.SiblingsStudentId),

                                // OUTPUT parameter
                                new SqlParameter("@msg", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output

                                }
                });
                     
                try
                {
                    await connection.OpenAsync();

                    await command.ExecuteNonQueryAsync();

                    result = command.Parameters["@Msg"].Value?.ToString() ?? string.Empty;
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }

            return result;
        }
        public async Task<DataTable> StudentRegistrationAllRecordAsync(int schoolId, int regno, string? type = null, string requestType = null)
        {
            var dataTable = new DataTable();

            using (var connection = _dbConnectionFactory.CreateConnection())
            using (var command = new SqlCommand("USP_Get_Registration_Deatils", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddRange(new[]
                {
                    new SqlParameter("@Schoolid", schoolId),
                    new SqlParameter("@regnumber", regno),
                    new SqlParameter("@type", (object?)type ?? DBNull.Value),
                    new SqlParameter("@requestType", requestType)
                });

                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }
            }

            return dataTable;
        }

        public async Task<bool> DeleteRegRecordAsync(int schoolId, int regNo )
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            using (var command = new SqlCommand("usp_delete_StudentRegRecord", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddRange(new[]
                {
                    new SqlParameter("@Id", regNo),
                    new SqlParameter("@SchoolId", schoolId),
                    new SqlParameter
                    {
                        ParameterName = "@Msg",
                        SqlDbType = SqlDbType.VarChar,
                        Size = 100,
                        Direction = ParameterDirection.Output
                    }
                });
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                string message = command.Parameters["@Msg"].Value?.ToString();
                return !string.IsNullOrEmpty(message) && message.Equals("Success", StringComparison.OrdinalIgnoreCase);
            }
        }

        public async Task<bool> UpdateRegistrationStatusAsync(int schoolId, int regNo, Status status, string remark, int userId)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            using (var command = new SqlCommand("USP_UStudentRegistrationDetails", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddRange(new[]
                {
                    new SqlParameter("@SchoolId", schoolId),
                    new SqlParameter("@RegNo", regNo),
                    new SqlParameter("@Status", status),
                    new SqlParameter("@Remark", remark ?? (object)DBNull.Value),
                    new SqlParameter("@UserId", userId)
                });

                await connection.OpenAsync();
                int rowsAffected = await command.ExecuteNonQueryAsync();

                // return true if the SP updated at least 1 row
                return rowsAffected > 0;
            }
        }
        public async Task<bool> UpdateEnquiriesAsync(int id)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            using (var command = new SqlCommand("usp_UpdateEnquiry", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Id", id));
                var outputParam = new SqlParameter("@Msg", SqlDbType.VarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputParam);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                // success if SP returns "SUCCESS"
                var result = outputParam.Value?.ToString();
                return !string.IsNullOrEmpty(result) &&
                       result.Equals("SUCCESS", StringComparison.OrdinalIgnoreCase);


            }
        }
        public async Task<bool> DeleteOnlineEnquiryAsync(int enquiryId)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            using (var command = new SqlCommand("usp_deleteOnlineEnquiry", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@EnquiryId", enquiryId));

                var outputParam = new SqlParameter("@Msg", SqlDbType.VarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputParam);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                // interpret SP output -> "SUCCESS" = true, else false
                var result = outputParam.Value?.ToString();
                return !string.IsNullOrEmpty(result) &&
                       result.Equals("SUCCESS", StringComparison.OrdinalIgnoreCase);
            }
        }
        public async Task<bool> SaveEnquiryAsync(EnquiryM enquiryM)
        {
            using (SqlConnection conn = _dbConnectionFactory.CreateConnection())
            using (SqlCommand cmd = new SqlCommand("usp_saveEnquiry", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Input parameters
                cmd.Parameters.AddWithValue("@SchoolId", enquiryM.SchoolId);
                cmd.Parameters.AddWithValue("@Name", enquiryM.Name ?? string.Empty);
                cmd.Parameters.AddWithValue("@Contact", enquiryM.Contact ?? string.Empty);
                cmd.Parameters.AddWithValue("@Email", enquiryM.Email ?? string.Empty);
                cmd.Parameters.AddWithValue("@Message", enquiryM.Message ?? string.Empty);
                cmd.Parameters.AddWithValue("@RequestType", enquiryM.EnquiryType ?? string.Empty);

                // Output parameter
                var outputParam = new SqlParameter("@Msg", SqlDbType.VarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputParam);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                var result = outputParam.Value?.ToString();
                return !string.IsNullOrEmpty(result) &&
                       result.Equals("SUCCESS", StringComparison.OrdinalIgnoreCase);
            }
        }
        public async Task<DataTable> GetEnquiriesAsync(int schoolId, string? requestType)
        {
            using var conn = _dbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand("usp_getEnquiries", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@SchoolId", schoolId);
            cmd.Parameters.AddWithValue("@requestType", string.IsNullOrEmpty(requestType) ? DBNull.Value : requestType);

            var dt = new DataTable();

            await conn.OpenAsync();
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                dt.Load(reader);
            }

            return dt;
        }
        public async Task<DataTable> GetEnquiryByIdAsync(int schoolId, int enquiryId)
        {
            using var conn = _dbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand("usp_getEnquiryById", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@SchoolId", schoolId);
            cmd.Parameters.AddWithValue("@enquiryId", enquiryId);

            var dt = new DataTable();

            await conn.OpenAsync();
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                dt.Load(reader);
            }

            return dt;
        }

    }
}