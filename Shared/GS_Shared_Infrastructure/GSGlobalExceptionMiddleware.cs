using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;


namespace GS_Shared_Infrastructure
{
	public class GSGlobalExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<GSGlobalExceptionMiddleware> _logger;

		public GSGlobalExceptionMiddleware(RequestDelegate next, ILogger<GSGlobalExceptionMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An unhandled exception occurred.");
				await HandleExceptionAsync(context, ex);
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";

			// Default to 500
			var statusCode = HttpStatusCode.InternalServerError;
			var result = "An unexpected error occurred.";

			if (exception is ArgumentException || exception.Message.Contains("Invalid"))
			{
				statusCode = HttpStatusCode.BadRequest; // 400
				result = exception.Message;
			}
			else if (exception.Message.Contains("not found"))
			{
				statusCode = HttpStatusCode.NotFound; // 404
				result = exception.Message;
			}

			context.Response.StatusCode = (int)statusCode;

			var response = new { error = result, detail = exception.InnerException?.Message };
			return context.Response.WriteAsync(JsonSerializer.Serialize(response));
		}
	}
}
