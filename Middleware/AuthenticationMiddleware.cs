using System.Linq;

namespace UserManagementAPI.Middleware
{
	public class AuthenticationMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<AuthenticationMiddleware> _logger;

		public AuthenticationMiddleware(RequestDelegate next, ILogger<AuthenticationMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");

			if (string.IsNullOrEmpty(token))
			{
				context.Response.StatusCode = StatusCodes.Status401Unauthorized;
				await context.Response.WriteAsync("Unauthorized: Token missing");
				return;
			}

			// For assignment purposes, accept any non-empty token
			_logger.LogInformation("Token received: {token}", token);

			await _next(context);
		}
	}

	public static class AuthenticationMiddlewareExtensions
	{
		public static IApplicationBuilder UseTokenAuthentication(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<AuthenticationMiddleware>();
		}
	}
}
