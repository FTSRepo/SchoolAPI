using Microsoft.Data.SqlClient;
using SchoolAPI.Infrastructure.Factory;
using SchoolAPI.Models.Free;
using System.Data;

namespace SchoolAPI.Repositories.FeeRepository
{
    public class FeeRepository(IDbConnectionFactory dbConnectionFactory) : IFeeRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;
        public async Task<DataTable> GetStudentFeeDetailsByStudentIdAsync(int schoolId, int studentId, int sessionId)
        {
            using var con = _dbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand("usp_API_GetStudetnFeeDetaisl", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@SchoolId", schoolId);
            cmd.Parameters.AddWithValue("@StudentId", studentId);
            cmd.Parameters.AddWithValue("@SessionId", sessionId);

            await con.OpenAsync();
            var dt = new DataTable();
            using var adapter = new SqlDataAdapter(cmd);
            await Task.Run(() => adapter.Fill(dt));

            return dt;
        }
        public async Task<DataSet> BindPaidFeeSumaryAsync(int schoolId, int studentId, int sessionId)
        {
            using var con = _dbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand("UPS_BindPaidFeeSumary", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@SchoolId", schoolId);
            cmd.Parameters.AddWithValue("@StudentId", studentId);
            cmd.Parameters.AddWithValue("@SessionId", sessionId);

            await con.OpenAsync();

            var ds = new DataSet();
            using var adapter = new SqlDataAdapter(cmd);
            await Task.Run(() => adapter.Fill(ds));
            return ds;
        }
        public async Task<DataSet> GetStudentsFeeDetailsAsync(int schoolId, int classId, int studentId, string monthIds, int sessionId)
        {
            using var con = _dbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand("SP_GETSTUDENTFEE", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@SchoolId", schoolId);
            cmd.Parameters.AddWithValue("@StudentId", studentId);
            cmd.Parameters.AddWithValue("@ClassId", classId);
            cmd.Parameters.AddWithValue("@SessionId", sessionId);
            cmd.Parameters.AddWithValue("@MonthIds", monthIds ?? (object)DBNull.Value);

            await con.OpenAsync();

            var ds = new DataSet();
            using var adapter = new SqlDataAdapter(cmd);
            await Task.Run(() => adapter.Fill(ds));

            return ds;
        }
        public async Task<DataSet> CreateOnlinePaymentOrderAsync(OnlineCreateOrderRequest onlineCreateOrderRequest)
        {
            using var con = _dbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand("usp_PaymentGatwayOrder", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@SchoolId", onlineCreateOrderRequest.SchoolId);
            cmd.Parameters.AddWithValue("@StudentId", onlineCreateOrderRequest.StudentId);
            cmd.Parameters.AddWithValue("@MonthIds", onlineCreateOrderRequest.MonthIds ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@SessionId", onlineCreateOrderRequest.SessionId);
            cmd.Parameters.AddWithValue("@Amount", onlineCreateOrderRequest.Amount);

            await con.OpenAsync();

            var ds = new DataSet();
            using var adapter = new SqlDataAdapter(cmd);
            await Task.Run(() => adapter.Fill(ds));

            return ds;
        }
        public async Task<string> UpdateOnlinePaymentOrderAsync(OnlineUpdateOrderRequest onlineUpdateOrderRequest, int schoolId, int studentId)
        {
            using var con = _dbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand("usp_UpdatePaymentGatwayOrder", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@SchoolId", schoolId);
            cmd.Parameters.AddWithValue("@StudentId", studentId);
            cmd.Parameters.AddWithValue("@ReceiptNo", onlineUpdateOrderRequest.receipt);
            cmd.Parameters.AddWithValue("@APIOrderId", onlineUpdateOrderRequest.id);
            cmd.Parameters.AddWithValue("@APIAmount", onlineUpdateOrderRequest.amount);
            cmd.Parameters.AddWithValue("@APIAmountPaid", onlineUpdateOrderRequest.amount_paid);
            cmd.Parameters.AddWithValue("@APIAmountDue", onlineUpdateOrderRequest.amount_due);
            cmd.Parameters.AddWithValue("@Entity", onlineUpdateOrderRequest.entity);
            cmd.Parameters.AddWithValue("@Status", onlineUpdateOrderRequest.status);
            cmd.Parameters.AddWithValue("@attempts", onlineUpdateOrderRequest.attempts);
            cmd.Parameters.AddWithValue("@Notes", onlineUpdateOrderRequest.notes);
            cmd.Parameters.AddWithValue("@CreatedOn", onlineUpdateOrderRequest.created_at);

            // Output parameter
            var msgParam = new SqlParameter("@Msg", SqlDbType.VarChar, 100)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(msgParam);

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return msgParam.Value?.ToString();
        }
        public async Task<string> UpdateOnlineOrderStatusAsync(OnlineOrderStatusRequest onlineOrderStatus)
        {
            using var con = _dbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand("Usp_onlineOrderStatus", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@razorpay_order_id", onlineOrderStatus.razorpay_order_id);
            cmd.Parameters.AddWithValue("@razorpay_payment_id", onlineOrderStatus.razorpay_payment_id);
            cmd.Parameters.AddWithValue("@razorpay_signature", onlineOrderStatus.razorpay_signature);
            cmd.Parameters.AddWithValue("@status_code", onlineOrderStatus.status_code);
            cmd.Parameters.AddWithValue("@SchoolId", onlineOrderStatus.SchoolId);

            var msgParam = new SqlParameter("@Msg", SqlDbType.VarChar, 100)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(msgParam);

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return msgParam.Value?.ToString();
        }
        public async Task<DataTable> SaveOnlinePaymentOrderAsync(OnlinePaymentOrderRequest onlinePaymentOrderRequest)
        {
            using var con = _dbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand("usp_SaveOnlinePaymentOrder", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@SchoolId", onlinePaymentOrderRequest.SchoolId);
            cmd.Parameters.AddWithValue("@StudentId", onlinePaymentOrderRequest.StudentId);
            cmd.Parameters.AddWithValue("@RegistrationNumber", onlinePaymentOrderRequest.RegistrationNumber ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@OrderId", onlinePaymentOrderRequest.OrderId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@OrderAmount", onlinePaymentOrderRequest.OrderAmount);
            cmd.Parameters.AddWithValue("@OrderStatus", onlinePaymentOrderRequest.OrderStatus ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@OrderRemark", onlinePaymentOrderRequest.OrderRemark ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@PaymentId", onlinePaymentOrderRequest.PaymentId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedOn", onlinePaymentOrderRequest.CreatedOn);
            cmd.Parameters.AddWithValue("@ModifiedOn", onlinePaymentOrderRequest.ModifiedOn);

            await con.OpenAsync();

            var dt = new DataTable();
            using var adapter = new SqlDataAdapter(cmd);
            await Task.Run(() => adapter.Fill(dt));

            return dt;
        }
        public async Task<string> SaveOnlinePaymentAsync(OnlinePaymentRequest payment)
        {
            using var con = _dbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand("uSp_UpdateOnlinepayment", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Regno", payment.Regno);
            cmd.Parameters.AddWithValue("@PaymentId", payment.PaymentId);
            cmd.Parameters.AddWithValue("@SchoolId", payment.SchoolId);
            cmd.Parameters.AddWithValue("@PaymentStatus", payment.PaymentStatus ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@OrderId", payment.OrderId ?? (object)DBNull.Value);

            await con.OpenAsync();
            int rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0 ? "Data saved successfully!" : "There is some technical error!";
        }

        public async Task<DataTable> GetDaywiseFeeDAsync(int SchoolId, int SessionId)
        {
            using var con = _dbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand("USP_GetDaywiseCollection", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@SchoolId", SchoolId);
            cmd.Parameters.AddWithValue("@SessionId", SessionId);

            await con.OpenAsync();
            var dt = new DataTable();
            using var adapter = new SqlDataAdapter(cmd);
            await Task.Run(() => adapter.Fill(dt));
            return dt;
        }
        public async Task<DataTable> GetTodaysCollectionReportsAsync(int SchoolId, int SessionId, int userId, DateTime currentDate)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            using var command = new SqlCommand("USP_GetTodaysCollectionReport", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddRange(new SqlParameter[]
            {
            new SqlParameter("@SchoolId", SchoolId),
            new SqlParameter("@SessionId", SessionId),
            new SqlParameter("@userId", userId),
            new SqlParameter("@currentDate", currentDate)
            });

            await connection.OpenAsync();

            using var adapter = new SqlDataAdapter(command);
            var dataTable = new DataTable();
            await Task.Run(() => adapter.Fill(dataTable));
            return dataTable;
        }
        public async Task<DataTable> GetStudentsPaidFeeAsync(int SchoolId, int SessionId)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            using (var command = new SqlCommand("USP_GetStudentPaidFee", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddRange(new SqlParameter[]
                {
                    new SqlParameter("@SchoolId", SchoolId),
                    new SqlParameter("@SessionId", SessionId)
                });

                await connection.OpenAsync();

                using (var adapter = new SqlDataAdapter(command))
                {
                    var dataTable = new DataTable();
                    await Task.Run(() => adapter.Fill(dataTable));
                    return dataTable;
                }
            }
        }


    }
}
