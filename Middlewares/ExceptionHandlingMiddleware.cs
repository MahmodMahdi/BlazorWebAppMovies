using System.Net;
using System.Text.Json;

namespace BlazorWebAppMovies.Middlewares
{
	public class ExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionHandlingMiddleware> _logger;
		public ExceptionHandlingMiddleware(RequestDelegate next,ILogger<ExceptionHandlingMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}
		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An unhandled exception occured");
				await HandleExceptionAsync(context, ex);
			}
		}
		public static Task HandleExceptionAsync(HttpContext context, Exception ex)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			var result = JsonSerializer.Serialize(new
			{
				error = "An error occurred while processing your request",
				message = ex.Message
			});
			return context.Response.WriteAsync(result);
		}
	}
}
