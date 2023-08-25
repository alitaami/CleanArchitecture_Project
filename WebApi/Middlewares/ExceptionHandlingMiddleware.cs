
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using MediatR; // Import MediatR namespace

namespace WebApi.Middlewares
{

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IMediator _mediator; // Inject IMediator

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IMediator mediator)
        {
            _next = next;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred.");

                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "Exception occurred.");

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "An error occurred while processing your request."
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
