using System.Net;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Middlewares
    {
    public class ExceptionHandlingMiddleware
        {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly JsonSerializerOptions _jsonOptions;

        public ExceptionHandlingMiddleware(RequestDelegate next,ILogger<ExceptionHandlingMiddleware> logger)
            {
            _next=next;
            _logger=logger;
            _jsonOptions=new JsonSerializerOptions
                {
                PropertyNamingPolicy=JsonNamingPolicy.CamelCase,
                WriteIndented=true
                };
            }

        public async Task InvokeAsync(HttpContext context)
            {
            try
                {
                await _next(context);
                }
            catch(Exception ex)
                {
                await HandleExceptionAsync(context,ex);
                }
            }

        private async Task HandleExceptionAsync(HttpContext context,Exception ex)
            { 
            var statusCode = HttpStatusCode.InternalServerError;
            string message = "An unexpected error occurred.";
            string? errorType = null;

            // Map known exception types
            switch(ex)
                {
                case ValidationException validationEx:
                    statusCode=HttpStatusCode.BadRequest;
                    message="Validation failed.";
                    errorType="ValidationError";
                    break;

                case UnauthorizedAccessException:
                    statusCode=HttpStatusCode.Unauthorized;
                    message="Unauthorized access.";
                    errorType="Unauthorized";
                    break;

                case ArgumentNullException argNullEx:
                    statusCode=HttpStatusCode.BadRequest;
                    message=$"Missing argument: {argNullEx.ParamName}";
                    errorType="BadRequest";
                    break;

                case KeyNotFoundException:
                    statusCode=HttpStatusCode.NotFound;
                    message="Requested resource not found.";
                    errorType="NotFound";
                    break;

                case InvalidOperationException:
                    statusCode=HttpStatusCode.BadRequest;
                    message="Operation could not be completed due to invalid state.";
                    errorType="InvalidOperation";
                    break;

                case TimeoutException:
                    statusCode=HttpStatusCode.RequestTimeout;
                    message="The request took too long to complete.";
                    errorType="Timeout";
                    break;

                // Example for your custom business exception class
                case AppException appEx:
                    statusCode=(HttpStatusCode)(appEx.StatusCode??400);
                    message=appEx.Message;
                    errorType="BusinessError";
                    break;
                
                default:
                    _logger.LogError(ex,"Unhandled exception");
                    break;
                }

            var result = new
                {
                success = false,
                error = message,
                type = errorType,
                timestamp = DateTime.UtcNow,
#if DEBUG
                details = ex.Message,
                stackTrace = ex.StackTrace
#endif
                };

            context.Response.ContentType="application/json";
            context.Response.StatusCode=(int)statusCode;

            await context.Response.WriteAsync(JsonSerializer.Serialize(result,_jsonOptions));
            }
        }

    // Custom AppException for business logic errors
    public class AppException:Exception
        {
        public int? StatusCode { get; }

        public AppException(string message,int? statusCode = null)
            : base(message)
            {
            StatusCode=statusCode;
            }
        }
    public class ValidationException(string message):AppException(message,400)
        {
        }

    public class NotFoundException(string message):AppException(message,404)
        {
        }
    }

