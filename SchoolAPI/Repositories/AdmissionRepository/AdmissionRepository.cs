using Microsoft.Data.SqlClient;
using SchoolAPI.Infrastructure.Factory;
using SchoolAPI.Models.Admission;
using System.Data;

namespace SchoolAPI.Repositories.AdmissionRepository
{
    public class AdmissionRepository(DbConnectionFactory dbConnectionFactory) : IAdmissionRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;
        public async Task<string> AddStudentDetailsAsync(StudentAdmissionM oStudentDetail)
        {
            return await Task.Run(() =>
               {
                   SqlParameter[] parameters = new SqlParameter[]
                   {
            new SqlParameter("@schoolId", oStudentDetail.SchoolId),
            new SqlParameter("@sessionid", oStudentDetail.SessionId),
            new SqlParameter("@AdmNo", oStudentDetail.AdmissionNo),
            new SqlParameter("@RollNo", oStudentDetail.RollNo),
            new SqlParameter("@FirstName", oStudentDetail.StudentFirstName),
            new SqlParameter("@MiddleName", oStudentDetail.StudentMiddleName),
            new SqlParameter("@LastName", oStudentDetail.StudentLastName),
            new SqlParameter("@DOB", oStudentDetail.DOB),
            new SqlParameter("@Birthplace", oStudentDetail.Birthplace),
            new SqlParameter("@Gender", oStudentDetail.Gender),
            new SqlParameter("@Bloodgroup", oStudentDetail.BloodGroup),
            new SqlParameter("@Nationality", oStudentDetail.Nationality),
            new SqlParameter("@CategoryID", oStudentDetail.Category),
            new SqlParameter("@ReligionID", oStudentDetail.Religion),
            new SqlParameter("@dateofadmission", oStudentDetail.AddmissionDate),
            new SqlParameter("@AdmissionClassID", oStudentDetail.CurrentClass),
            new SqlParameter("@AdmissionSectionID", oStudentDetail.CurrentSection),
            new SqlParameter("@AdmissionTypeID", oStudentDetail.AdmissionType),
            new SqlParameter("@QuotaID", oStudentDetail.Concession),
            new SqlParameter("@OptionalFeeHeadId", oStudentDetail.OptionalFeeHeadId),
            new SqlParameter("@AadharNumber", oStudentDetail.AadharNumber),
            new SqlParameter("@BirthCertificateNumber", oStudentDetail.BirthCertificateNumber),
            new SqlParameter("@SessionName", oStudentDetail.SessionName),
            new SqlParameter("@SiblingsStudentId", oStudentDetail.SiblingsStudentId),
            new SqlParameter("@PermanentEducationNumber", oStudentDetail.PermanentEducationNumber),
            new SqlParameter("@eShikshaId", oStudentDetail.eShikshaId),
            new SqlParameter("@HouseId", oStudentDetail.HouseId),
            new SqlParameter("@FatherName", oStudentDetail.fatehrName),
            new SqlParameter("@MotherName", oStudentDetail.motherName),
            new SqlParameter("@MobileNo", oStudentDetail.mobileNo),
            new SqlParameter("@Address", oStudentDetail.Address),
            new SqlParameter("@Address2", oStudentDetail.Address2),
            new SqlParameter("@PrvSchoolName", oStudentDetail.prvSchoolname),
            new SqlParameter("@Reason", oStudentDetail.Reason),
            new SqlParameter("@lastClass", oStudentDetail.pClass),
                   };

                   parameters[parameters.Length - 1].Direction = ParameterDirection.Output;
                   string result = string.Empty;

                   using var conn = _dbConnectionFactory.CreateConnection();
                   using var cmd = new SqlCommand("usp_NewAdmission_new", conn)
                   {
                       CommandType = CommandType.StoredProcedure
                   };
                   cmd.Parameters.AddRange(parameters);

                   var outputParam = new SqlParameter("@StudentIds", SqlDbType.VarChar, 100)
                   {
                       Direction = ParameterDirection.Output
                   };
                   cmd.Parameters.Add(outputParam);

                   conn.OpenAsync();
                   cmd.ExecuteNonQueryAsync();

                   result = outputParam.Value.ToString();

                   return result;
               });
        }

    }
}