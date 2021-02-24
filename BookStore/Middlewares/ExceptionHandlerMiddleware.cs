using BookStore.BusinessLogicLayer.Exceptions;
using BookStore.BusinessLogicLayer.Models.Base;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BookStore.PresentationLayer.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILoggerService logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);

            }
            catch (CustomException ex)
            {
                await HandleExceptionAsync(ex.Code, ex, context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(HttpStatusCode.InternalServerError, ex, context);
                await _logger.LogAsync(ex);
            }
        }

        public async Task HandleExceptionAsync(HttpStatusCode code, Exception ex, HttpContext context)
        {
            BaseModel result = new BaseModel(ex);
            context.Response.Clear();
            context.Response.StatusCode = (int)code;

            await context.Response.WriteAsJsonAsync(result);
        }
    }
}
