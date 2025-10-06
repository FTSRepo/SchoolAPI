using Microsoft.Data.SqlClient;
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

        public async Task<string> SaveRegistrationAsync(StudentRegistrationModel objstudentregistration)
        {
            string result = string.Empty;

            using (var connection = _dbConnectionFactory.CreateConnection())
            using (var command = new SqlCommand("USP_Student_Registration", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddRange(new[]
                {
            new SqlParameter("@Registration", objstudentregistration.Registration),
            new SqlParameter("@DateofRegistration", objstudentregistration.DateofRegistration1),
            new SqlParameter("@Class", objstudentregistration.Class),
            new SqlParameter("@Firstname", objstudentregistration.Firstname),
            new SqlParameter("@MiddleName", objstudentregistration.MiddleName),
            new SqlParameter("@lastName", objstudentregistration.lastName),
            new SqlParameter("@Dob", objstudentregistration.Dob1),
            new SqlParameter("@BirthPlace", objstudentregistration.BirthPlace),
            new SqlParameter("@radioValue", objstudentregistration.radioValue),
            new SqlParameter("@AddressLine1S", objstudentregistration.AddressLine1S),
            new SqlParameter("@AddressLine2S", objstudentregistration.AddressLine2S),
            new SqlParameter("@CountryS", objstudentregistration.CountryS),
            new SqlParameter("@Statep", objstudentregistration.Satep),
            new SqlParameter("@cityp", objstudentregistration.cityp),
            new SqlParameter("@Pincodep", objstudentregistration.Pincodep),
            new SqlParameter("@fathermobilenumber", objstudentregistration.fathermobilenumber),
            new SqlParameter("@mobilenop", objstudentregistration.mobilenop),
            new SqlParameter("@Presentdistance", objstudentregistration.Presentdistance),
            new SqlParameter("@Addressline1ph", objstudentregistration.Addressline1ph),
            new SqlParameter("@Addresslineph", objstudentregistration.Addresslineph),
            new SqlParameter("@Countryph", objstudentregistration.Countryph),
            new SqlParameter("@Stateph", objstudentregistration.Stateph),
            new SqlParameter("@Cityph", objstudentregistration.Cityph),
            new SqlParameter("@PinCodeph", objstudentregistration.PinCodeph),
            new SqlParameter("@Distance", objstudentregistration.Distance),
            new SqlParameter("@Firstnamefather", objstudentregistration.Firstnamefather),
            new SqlParameter("@Middlenamefather", objstudentregistration.Middlenamefather),
            new SqlParameter("@Lastnamefather", objstudentregistration.Lastnamefather),
            new SqlParameter("@EducationQualificationfather", objstudentregistration.EducationQualificationfather),
            new SqlParameter("@ProfessionalQualificationfather", objstudentregistration.ProfessionalQualificationfather),
            new SqlParameter("@Occupation", objstudentregistration.Occupation),
            new SqlParameter("@Firstnamemother", objstudentregistration.Firstnamemother),
            new SqlParameter("@Middlenamemother", objstudentregistration.Middlenamemother),
            new SqlParameter("@LastNamemother", objstudentregistration.LastNamemother),
            new SqlParameter("@EducationQualificationmother", objstudentregistration.EducationQualificationmother),
            new SqlParameter("@ProfessionalQualificationmother", objstudentregistration.ProfessionalQualificationmother),
            new SqlParameter("@Occupationmother", objstudentregistration.Occupationmother),
            new SqlParameter("@CreatedBY", objstudentregistration.CreatedBy),
            new SqlParameter("@ModifidBy", objstudentregistration.ModifiedBy),
            new SqlParameter("@schoolid", objstudentregistration.Schoolid),
            new SqlParameter("@Sessionid", objstudentregistration.Sessionid),
            new SqlParameter("@Fathermobileno1", objstudentregistration.FatherMobile1),
            new SqlParameter("@mobile1", objstudentregistration.mobile1),
            new SqlParameter("@AadharNo", objstudentregistration.AadharNo),
            new SqlParameter("@RegFee", objstudentregistration.RegFee),
            new SqlParameter("@BloodGroup", objstudentregistration.BloodGroup),
            new SqlParameter("@Category", objstudentregistration.Category),
            new SqlParameter("@Religion", objstudentregistration.Religion),
            new SqlParameter("@SiblingsStudentId", objstudentregistration.SiblingsStudentId),
            new SqlParameter("@Msg", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output }
        });

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                result = command.Parameters["@Msg"].Value?.ToString() ?? string.Empty;
            }

            return result;
        }
        public async Task<DataTable> StudentRegistrationAllRecordAsync(int schoolId, int regno, string? type = null, string requestType = "")
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

        public async Task<bool> DeleteRegRecordAsync(int id, int schoolId)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            using (var command = new SqlCommand("usp_delete_StudentRegRecord", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddRange(new[]
                {
                    new SqlParameter("@Id", id),
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

        public async Task<bool> UpdateRegistrationStatusAsync(int regNo, int schoolId, string status, string remark, int userId)
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