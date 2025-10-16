using System.Diagnostics;
using System.Text;
using Microsoft.IO;

namespace SchoolAPI.Middlewares
    {
    public class RequestLoggingMiddleware
        {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;
        private static readonly RecyclableMemoryStreamManager _recyclableStreamManager = new();

        public RequestLoggingMiddleware(RequestDelegate next,ILogger<RequestLoggingMiddleware> logger)
            {
            _next=next;
            _logger=logger;
            }

        public async Task InvokeAsync(HttpContext context)
            {
            var stopwatch = Stopwatch.StartNew();
            var request = context.Request;

            // Skip logging for static files or uploads
            if(IsLargeOrFileUpload(request))
                {
                await _next(context);
                return;
                }

            var requestBody = await ReadRequestBodyAsync(request);
            _logger.LogInformation("➡️ Request: {method} {path} {query} Body: {body}",
                request.Method,request.Path,request.QueryString,requestBody);

            // Capture response
            var originalBodyStream = context.Response.Body;
            await using var responseBody = _recyclableStreamManager.GetStream();
            context.Response.Body=responseBody;

            try
                {
                await _next(context);
                stopwatch.Stop();

                var responseText = await ReadResponseBodyAsync(context.Response);
                _logger.LogInformation("⬅️ Response: {method} {path} Status: {statusCode} Time: {elapsed}ms Body: {body}",
                    request.Method,request.Path,context.Response.StatusCode,stopwatch.ElapsedMilliseconds,responseText);

                await responseBody.CopyToAsync(originalBodyStream);
                }
            catch(Exception ex)
                {
                stopwatch.Stop();
                _logger.LogError(ex,"🔥 Error during request {method} {path}",request.Method,request.Path);
                throw; // Let ExceptionHandlingMiddleware handle it
                }
            finally
                {
                context.Response.Body=originalBodyStream;
                }
            }

        private static bool IsLargeOrFileUpload(HttpRequest request)
            {
            var contentType = request.ContentType?.ToLower()??"";
            return contentType.Contains("multipart/form-data")||
                   contentType.Contains("application/octet-stream");
            }

        private static async Task<string> ReadRequestBodyAsync(HttpRequest request)
            {
            request.EnableBuffering();
            await using var stream = _recyclableStreamManager.GetStream();
            await request.Body.CopyToAsync(stream);
            request.Body.Position=0;
            stream.Position=0;

            using var reader = new StreamReader(stream,Encoding.UTF8);
            var body = await reader.ReadToEndAsync();
            return TruncateIfTooLong(body);
            }

        private static async Task<string> ReadResponseBodyAsync(HttpResponse response)
            {
            response.Body.Seek(0,SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0,SeekOrigin.Begin);
            return TruncateIfTooLong(text);
            }

        private static string TruncateIfTooLong(string text,int maxLength = 1000)
            {
            return text.Length>maxLength
                ? text.Substring(0,maxLength)+" ... (truncated)"
                : text;
            }
        }
    }
