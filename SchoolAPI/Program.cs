using System.Text;
using System.Text.Json.Serialization;
using Amazon;
using Amazon.S3;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchoolAPI.Infrastructure;
using SchoolAPI.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Infrastructure
builder.Services.AddInfrastructure(builder.Configuration);

// JWT
var key = Encoding.ASCII.GetBytes(builder.Configuration ["Jwt:Secret"]);
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.RequireHttpsMetadata = false; // false for dev
//        options.SaveToken = true;
//        options.TokenValidationParameters = new TokenValidationParameters
//            {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateIssuerSigningKey = true,
//            ValidateLifetime = true,
//            ClockSkew = TimeSpan.Zero,
//            ValidIssuer = builder.Configuration ["Jwt:Issuer"],
//            ValidAudience = builder.Configuration ["Jwt:Audience"],
//            IssuerSigningKey = new SymmetricSecurityKey(key)
//            };
//    });

// 🔹 Configure Serilog globally
Log.Logger=new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .Enrich.WithThreadId()
    .Enrich.WithProcessId()
    .Enrich.WithEnvironmentName()
    .WriteTo.Console()
    .WriteTo.File(
        path: "logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 15,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] ({TenantId}) {Message:lj}{NewLine}{Exception}"
    )
    .CreateLogger();

// 🔹 Replace default logger
builder.Host.UseSerilog();

// Controllers
builder.Services.AddControllers()
        .AddJsonOptions(opt =>
        {
            opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "School API", Version = "v1" });
    // Add JWT auth support
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Enter JWT token without 'Bearer ' prefix"
        });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
    c.SchemaFilter<EnumSchemaFilter>();
});

// Fallback auth (everything requires auth unless [AllowAnonymous])
//builder.Services.AddAuthorization(options =>
//{
//    options.FallbackPolicy = new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
//        .RequireAuthenticatedUser()
//        .Build();
//});

builder.Services.AddSingleton<IAmazonS3>(sp =>
{
    var config = builder.Configuration;
    var awsOptions = new AmazonS3Config
        {
        RegionEndpoint = RegionEndpoint.GetBySystemName(config ["AWS:Region"])
        };

    return new AmazonS3Client(
        config ["AWS:AccessKey"],
        config ["AWS:SecretKey"],
        awsOptions
    );
});
// CORS
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll", p =>
        p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

// Swagger must be public → place before auth
app.UseSwagger();
app.UseSwaggerUI();


//Custom middlewares
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Pipeline
app.UseHttpsRedirection();
app.UseCors("AllowAll");

// Auth
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
