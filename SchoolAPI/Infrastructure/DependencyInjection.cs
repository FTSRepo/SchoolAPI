using System.Reflection.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolAPI.Infrastructure.Factory;
using SchoolAPI.Repositories.AuthRepository;
using SchoolAPI.Repositories.CommonRepository;
using SchoolAPI.Repositories.FeeRepository;
using SchoolAPI.Repositories.HomeworkRepository;
using SchoolAPI.Repositories.LeaveRepository;
using SchoolAPI.Repositories.MessageRepository;
using SchoolAPI.Repositories.OnelineClassRepository;
using SchoolAPI.Services.AuthService;
using SchoolAPI.Services.CommonService;
using SchoolAPI.Services.FeeManagement;
using SchoolAPI.Services.Homework;
using SchoolAPI.Services.MessageService;
using SchoolAPI.Services.OnelineClassService;

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

            return services;
        }
    }
}
