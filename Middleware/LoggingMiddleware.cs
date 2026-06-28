using System.Threading.Tasks;

namespace UserManagementAPI.Middleware
{
	public class LoggingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<LoggingMiddleware> _logger;

		public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			// Log request
			_logger.LogInformation("Incoming Request: {method} {path}",
				context.Request.Method, context.Request.Path);

			await _next(context);

			// Log response
			_logger.LogInformation("Outgoing Response: {statusCode}",
				context.Response.StatusCode);
		}
	}

	public static class LoggingMiddlewareExtensions
	{
		public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<LoggingMiddleware>();
		}
	}
}
