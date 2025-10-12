using System.Data;
using Microsoft.Data.SqlClient;
using SchoolAPI.Infrastructure.Factory;
using SchoolAPI.Models.Free;

namespace SchoolAPI.Repositories.FeeRepository
    {
    public class PaymentgatwayRepository(IDbConnectionFactory dbConnectionFactory) : IPaymentgatwayRepository
        {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;
        public async Task<DataTable> GetPaymentAPIAsync(int SchoolId)
            {
            using var con = _dbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand("USP_PaymentAPI", con)
                {
                CommandType = CommandType.StoredProcedure
                };
            cmd.Parameters.AddWithValue("@SchoolId", SchoolId);

            await con.OpenAsync();
            var dt = new DataTable();
            using var adapter = new SqlDataAdapter(cmd);
            await Task.Run(() => adapter.Fill(dt));
            return dt;
            }
        public async Task<string> SubmitOnlineFeeAsync(DataTable dt)
            {
            using var con = _dbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand("usp_SubmitOnlineFee", con)
                {
                CommandType = CommandType.StoredProcedure
                };

            var detailsParam = cmd.Parameters.AddWithValue("@FeeDetails", dt);
            detailsParam.SqlDbType = SqlDbType.Structured;

            var msgParam = new SqlParameter("@Msg", SqlDbType.VarChar, 100)
                {
                Direction = ParameterDirection.Output
                };
            cmd.Parameters.Add(msgParam);

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return msgParam.Value?.ToString();
            }
        public async Task<DataSet> CreateOnlinePaymnetOrderAsync(OnlineCreateOrderRequest onlineCreatedOrderRequest)
            {
            using var con = _dbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand("usp_PaymentGatwayOrder", con)
                {
                CommandType = CommandType.StoredProcedure
                };

            cmd.Parameters.AddWithValue("@SchoolId", onlineCreatedOrderRequest.SchoolId);
            cmd.Parameters.AddWithValue("@StudentId", onlineCreatedOrderRequest.StudentId);
            cmd.Parameters.AddWithValue("@MonthIds", onlineCreatedOrderRequest.MonthIds);
            cmd.Parameters.AddWithValue("@SessionId", onlineCreatedOrderRequest.SessionId);
            cmd.Parameters.AddWithValue("@Amount", onlineCreatedOrderRequest.Amount);

            await con.OpenAsync();
            var ds = new DataSet();
            using var adapter = new SqlDataAdapter(cmd);
            await Task.Run(() => adapter.Fill(ds));
            return ds;
            }
        public async Task<DataTable> GetClietKeySecretAsync(int SchoolId)
            {
            using var con = _dbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand("usp_GetClientKeySecret", con)
                {
                CommandType = CommandType.StoredProcedure
                };
            cmd.Parameters.AddWithValue("@SchoolId", SchoolId);

            await con.OpenAsync();
            var dt = new DataTable();
            using var adapter = new SqlDataAdapter(cmd);
            await Task.Run(() => adapter.Fill(dt));
            return dt;
            }
        public async Task<string> UpdateOnlinePaymnetOrderAsync(OnlineUpdateOrderRequest onlineUpdateOrderRequest, int SchoolId, int StudentId)
            {
            using var con = _dbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand("usp_UpdatePaymentGatwayOrder", con)
                {
                CommandType = CommandType.StoredProcedure
                };

            cmd.Parameters.AddWithValue("@SchoolId", SchoolId);
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@ReceiptNo", onlineUpdateOrderRequest.receipt ?? ( object ) DBNull.Value);
            cmd.Parameters.AddWithValue("@APIOrderId", onlineUpdateOrderRequest.id ?? ( object ) DBNull.Value);
            cmd.Parameters.AddWithValue("@APIAmount", onlineUpdateOrderRequest.amount);
            cmd.Parameters.AddWithValue("@APIAmountPaid", onlineUpdateOrderRequest.amount_paid);
            cmd.Parameters.AddWithValue("@APIAmountDue", onlineUpdateOrderRequest.amount_due);
            cmd.Parameters.AddWithValue("@Entity", onlineUpdateOrderRequest.entity ?? ( object ) DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", onlineUpdateOrderRequest.status ?? ( object ) DBNull.Value);
            cmd.Parameters.AddWithValue("@attempts", onlineUpdateOrderRequest.attempts);
            cmd.Parameters.AddWithValue("@Notes", onlineUpdateOrderRequest.notes ?? ( object ) DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedOn", onlineUpdateOrderRequest.created_at);

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

            cmd.Parameters.AddWithValue("@razorpay_order_id", onlineOrderStatus.razorpay_order_id ?? ( object ) DBNull.Value);
            cmd.Parameters.AddWithValue("@razorpay_payment_id", onlineOrderStatus.razorpay_payment_id ?? ( object ) DBNull.Value);
            cmd.Parameters.AddWithValue("@razorpay_signature", onlineOrderStatus.razorpay_signature ?? ( object ) DBNull.Value);
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
            cmd.Parameters.AddWithValue("@RegistrationNumber", onlinePaymentOrderRequest.RegistrationNumber ?? ( object ) DBNull.Value);
            cmd.Parameters.AddWithValue("@OrderId", onlinePaymentOrderRequest.OrderId ?? ( object ) DBNull.Value);
            cmd.Parameters.AddWithValue("@OrderAmount", onlinePaymentOrderRequest.OrderAmount);
            cmd.Parameters.AddWithValue("@OrderStatus", onlinePaymentOrderRequest.OrderStatus ?? ( object ) DBNull.Value);
            cmd.Parameters.AddWithValue("@OrderRemark", onlinePaymentOrderRequest.OrderRemark ?? ( object ) DBNull.Value);
            cmd.Parameters.AddWithValue("@PaymentId", onlinePaymentOrderRequest.PaymentId ?? ( object ) DBNull.Value);
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

            cmd.Parameters.AddWithValue("@Regno", payment.Regno ?? ( object ) DBNull.Value);
            cmd.Parameters.AddWithValue("@PaymentId", payment.PaymentId ?? ( object ) DBNull.Value);
            cmd.Parameters.AddWithValue("@SchoolId", payment.SchoolId);
            cmd.Parameters.AddWithValue("@PaymentStatus", payment.PaymentStatus ?? ( object ) DBNull.Value);
            cmd.Parameters.AddWithValue("@OrderId", payment.OrderId ?? ( object ) DBNull.Value);

            await con.OpenAsync();
            int rows = await cmd.ExecuteNonQueryAsync();

            return rows > 0 ? "Data saved successfully!" : "There is some technical error!";
            }
        }
    }
