
using System.Data;
using Microsoft.Data.SqlClient;
using SchoolAPI.Infrastructure.Factory;
using SchoolAPI.Models.Auth;

namespace SchoolAPI.Repositories.AuthRepository
    {
    public class AuthRepository(IDbConnectionFactory dbConnectionFactory) : IAuthRepository
        {
        private readonly IDbConnectionFactory _connectionFactory = dbConnectionFactory;

        public async Task<LoginResponse> Login(string username, string password)
            {
            LoginResponse loginResponse = new();
            List<Student> students = [];

            try
                {
                using SqlConnection con = _connectionFactory.CreateConnection();
                using SqlCommand cmd = new("sp_SLoginMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", username);
                cmd.Parameters.AddWithValue("@Password", password);
                await con.OpenAsync();
                using SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
                // ---- First Result Set: Login Info ----
                if ( await reader.ReadAsync() )
                    {
                    loginResponse.UserId = reader.GetSafeInt("UserId");
                    loginResponse.UserName = reader.GetSafeString("UserName");
                    loginResponse.SchoolId = reader.GetSafeInt("SchoolId");
                    loginResponse.DisplayName = reader.GetSafeString("DisplayName");
                    loginResponse.Address = reader.GetSafeString("Address");
                    loginResponse.Website = reader.GetSafeString("Website");
                    loginResponse.AssociatLogo = reader.GetSafeString("AssociatLogo");
                    loginResponse.AssociateId = reader.GetSafeInt("AssociateId");
                    loginResponse.AssociateWeb = reader.GetSafeString("AssociateWeb");
                    loginResponse.Usertypeid = reader.GetSafeInt("Usertypeid");
                    loginResponse.Logo = reader.GetSafeString("Logo");
                    loginResponse.RecBGImg = reader.GetSafeString("RecBGImg");
                    loginResponse.Logop = reader.GetSafeString("Logop");
                    loginResponse.SessionId = reader.GetSafeInt("SessionId");
                    loginResponse.SessionName = reader.GetSafeString("SessionName");
                    loginResponse.AssociatEmail = reader.GetSafeString("AssociatEmail");
                    loginResponse.ErpUrl = reader.GetSafeString("ErpUrl");


                    }

                // ---- Second Result Set: Student Info ----
                if ( await reader.NextResultAsync() )
                    {
                    while ( await reader.ReadAsync() )
                        {
                        students.Add(new Student
                            {
                            AdmissionNumber = reader.GetSafeString("AdmissionNumber"),
                            StudentId = reader.GetSafeInt("StudentId"),
                            Name = reader.GetSafeString("Name"),
                            ProfileImg = reader.GetSafeString("ProfileImg"),
                            ClassName = reader.GetSafeString("ClassName"),
                            SectionName = reader.GetSafeString("SectionName"),
                            UserName = reader.GetSafeString("Name"),
                            Gender = reader.GetSafeString("Gender"),
                            Logo = loginResponse.Logo,       // picked from first result
                            Address = loginResponse.Address, // picked from first result
                            ClassId = reader.GetSafeInt("ClassId"),
                            SectionId = reader.GetSafeInt("SectionId")
                            });
                        }
                    loginResponse.Students = students;
                    }
                }
            catch ( Exception ex )
                {
                throw new Exception("An error occurred while logging in.", ex);
                }
            return loginResponse;
            }
        public async Task<List<MobileUserMenu>> GetMobileUserMenuAsync(int schoolId, int roleId)
            {
            var lstMenus = new List<MobileUserMenu>();

            try
                {
                using SqlConnection con = _connectionFactory.CreateConnection();
                using SqlCommand cmd = new("USP_GetMobileMenu_ByUserType", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameters
                cmd.Parameters.AddWithValue("@schoolId", schoolId);
                cmd.Parameters.AddWithValue("@UserTypeId", roleId);

                await con.OpenAsync();

                using SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while ( await reader.ReadAsync() )
                    {
                    var menu = new MobileUserMenu
                        {
                        MenuId = reader.GetSafeInt("MenuId"),
                        MenuDesc = reader.GetSafeString("menuDesc"),
                        Href = reader.GetSafeString("href"),
                        Isactive = reader ["Isactive"] != DBNull.Value && !string.IsNullOrWhiteSpace(reader ["Isactive"].ToString()) ? Convert.ToBoolean(reader ["Isactive"]) : false

                        };
                    lstMenus.Add(menu);
                    }
                }
            catch ( Exception ex )
                {
                // Optionally log the error here
                throw new Exception("Error fetching mobile user menu.", ex);
                }

            return lstMenus;
            }

        public async Task SaveRefreshToken(int userId, string token, DateTime expires)
            {
            using var con = _connectionFactory.CreateConnection();
            string sql = "INSERT INTO RefreshTokens(UserId, Token, Expires, IsRevoked) VALUES(@UserId, @Token, @Expires, 0)";
            using var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@Token", token);
            cmd.Parameters.AddWithValue("@Expires", expires);
            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            }

        public async Task<RefreshToken> GetRefreshToken(string token)
            {
            RefreshToken refreshToken = new();
            using var con = _connectionFactory.CreateConnection();
            var sql = "SELECT * FROM RefreshTokens WHERE Token=@Token";
            using var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Token", token);
            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if ( await reader.ReadAsync() )
                {
                refreshToken.Id = Convert.ToInt32(reader ["Id"]);
                refreshToken.UserId = Convert.ToInt32(reader ["UserId"]);
                refreshToken.Token = reader ["Token"].ToString();
                refreshToken.Expires = Convert.ToDateTime(reader ["Expires"]);
                refreshToken.IsRevoked = Convert.ToBoolean(reader ["IsRevoked"]);
                refreshToken.CreatedAt = Convert.ToDateTime(reader ["CreatedAt"]);
                }
            return refreshToken;
            }

        public async Task RevokeRefreshToken(string token)
            {
            using var con = _connectionFactory.CreateConnection();
            var sql = "UPDATE RefreshTokens SET IsRevoked=1 WHERE Token=@Token";
            using var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Token", token);
            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            }

        public async Task<LoginResponse> GetUserById(int userId)
            {
            LoginResponse loginResponse = new();
            using var con = _connectionFactory.CreateConnection();
            var sql = "select um.UserId, um.UserTypeId, lm.UserName from LoginMaster lm join UserMaster um on lm.UserId = um.Id WHERE um.userId=@userId";
            using var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@userId", userId);
            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if ( await reader.ReadAsync() )
                {
                loginResponse.UserId = Convert.ToInt32(reader ["UserId"]);
                loginResponse.UserName = Convert.ToString(reader ["UserName"]);
                loginResponse.Usertypeid = Convert.ToInt32(reader ["UserTypeId"]);
                }
            return loginResponse;
            }

        public async Task<string> ChangePasswordAsync(string userName, string password)
            {
            string resultMessage = string.Empty;

            try
                {
                using var con = _connectionFactory.CreateConnection();
                using var cmd = new SqlCommand("usp_uLoginMaster", con)
                    {
                    CommandType = CommandType.StoredProcedure
                    };

                // Input parameters
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@password", password);

                // Output parameter
                var outputParam = new SqlParameter("@Msg", SqlDbType.VarChar, 100)
                    {
                    Direction = ParameterDirection.Output
                    };
                cmd.Parameters.Add(outputParam);

                await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                // Read output parameter value
                resultMessage = outputParam.Value?.ToString();
                }
            catch ( Exception ex )
                {
                // Optional: log exception
                throw new Exception("Error changing password.", ex);
                }

            return resultMessage;
            }
        public async Task<List<UserMenu>> GetUserMenuAsync(int userId, int schoolId, int roleId)
            {
            var lstMenus = new List<UserMenu>();

            try
                {
                using var conn = _connectionFactory.CreateConnection();
                using var cmd = new SqlCommand("USP_GetUserMenu", ( SqlConnection ) conn)
                    {
                    CommandType = CommandType.StoredProcedure
                    };

                cmd.Parameters.AddRange(new SqlParameter []
                {
            new SqlParameter("@schoolId", schoolId),
            new SqlParameter("@UserTypeId", roleId)
                });

                await conn.OpenAsync();

                using var reader = await cmd.ExecuteReaderAsync();

                while ( await reader.ReadAsync() )
                    {
                    var user = new UserMenu
                        {
                        MenuId = reader ["Menu_id"] != DBNull.Value ? Convert.ToInt32(reader ["Menu_id"]) : 0,
                        ParentMenuId = reader ["ParentMenu_id"] != DBNull.Value ? Convert.ToInt32(reader ["ParentMenu_id"]) : 0,
                        MenuDesc = reader ["menu"] != DBNull.Value ? reader ["menu"].ToString() : string.Empty,
                        MenuUrl = reader ["MenuUrl"] != DBNull.Value ? reader ["MenuUrl"].ToString() : string.Empty,
                        MenuOrder = reader ["menuorder"] != DBNull.Value ? Convert.ToInt32(reader ["menuorder"]) : 0,
                        MenuIcon = reader ["MenuICon"] != DBNull.Value ? reader ["MenuICon"].ToString() : string.Empty
                        };

                    lstMenus.Add(user);
                    }
                }
            catch ( Exception ex )
                {
                // You can log the exception here
                lstMenus = null;
                throw; // rethrow preserving stack trace
                }

            return lstMenus;
            }

        }
    }