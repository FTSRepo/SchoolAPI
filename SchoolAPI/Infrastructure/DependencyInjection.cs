using SchoolAPI.Infrastructure.Factory;
using SchoolAPI.Repositories.AttendanceRepository;
using SchoolAPI.Repositories.AuthRepository;
using SchoolAPI.Repositories.CommonRepository;
using SchoolAPI.Repositories.ExamRepository;
using SchoolAPI.Repositories.FeeRepository;
using SchoolAPI.Repositories.HomeworkRepository;
using SchoolAPI.Repositories.LeaveRepository;
using SchoolAPI.Repositories.MessageRepository;
using SchoolAPI.Repositories.OnelineClassRepository;
using SchoolAPI.Repositories.RegistrationRepository;
using SchoolAPI.Services.AttendanceService;
using SchoolAPI.Services.AuthService;
using SchoolAPI.Services.CommonService;
using SchoolAPI.Services.ExamManagement;
using SchoolAPI.Services.FeeManagement;
using SchoolAPI.Services.Homework;
using SchoolAPI.Services.MessageService;
using SchoolAPI.Services.OnelineClassService;
using SchoolAPI.Services.RegistrationService;

namespace SchoolAPI.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register DbConnectionFactory
            services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();

            // Register repositories/Services
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<ILeaveRepository, LeaveRepository>();
            services.AddScoped<IHomeworkRepository, HomeworkRepository>();
            services.AddScoped<IHomeworkService, HomeworService>();
            services.AddScoped<IOnelineClassRepository, OnelineClassRepository>();
            services.AddScoped<IOnelineClassService, OnelineClassService>();  
            services.AddScoped<IPaymentgatwayRepository, PaymentgatwayRepository>();
            services.AddScoped<IPaymentgatewayService, PaymentgatwayService>();
            services.AddScoped<IFeeService, FeeService>();
            services.AddScoped<IFeeRepository, FeeRepository>();
            services.AddScoped<ICommonRepository, CommonRepository>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            services.AddScoped<IAttendanceService, AttendanceService>();
            services.AddScoped<IRegistrationRepository, RegistrationRepository>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IExamService, ExamService>();
            services.AddScoped<IExamRepository, ExamRepository>();

            return services;
        }
    }
}
