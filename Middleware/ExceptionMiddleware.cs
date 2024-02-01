using System;
using System.Net;
using ForSendKH.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ForSendKH.Middleware
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next , ILogger<ExceptionMiddleware> logger)
		{
			this._next = next;
			this._logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);

			}catch(Exception ex)
			{
				_logger.LogError(ex,$"Something Went while processing {context.Request.Path}");
				await HandleExeptionAsync(context , ex);
			}
		}

        private Task HandleExeptionAsync(HttpContext context, Exception ex)
        {
			context.Response.ContentType = "application/json";
			HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

			var errorDetails = new ErrorDetails
			{
				ErrorType = "Failure",
				ErrorMessage = ex.Message
			};

			switch (ex)
			{
				case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
					errorDetails.ErrorType = "Not Found";
					break;
				default:
					break;

			}

			string response = JsonConvert.SerializeObject(errorDetails);
			context.Response.StatusCode = (int)statusCode;
			return context.Response.WriteAsync(response);
        }
    }

	public class ErrorDetails
	{
		public string ErrorType { get; set; }
		public string ErrorMessage { get; set; }
	}
}

