using System;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Conference.CustomException;
using ConfService.ServiceException;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Conference.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            //if (ex is MyNotFoundException) code = HttpStatusCode.NotFound;
            //else if (ex is MyUnauthorizedException) code = HttpStatusCode.Unauthorized;
            //else if (ex is MyException) code = HttpStatusCode.BadRequest;

            object obj = null;
            if (ex is ConfException cex)
                obj = cex.Body;
            else if (ex is UserWithThisEmailExistsException)
            {
                code = HttpStatusCode.BadRequest;
                obj = new {error = "User with this email already exists"};
            }
            else if (ex is UnauthorizedAccessException)
            {
                code = HttpStatusCode.Unauthorized;
                obj = new { error = "Bad or missing credentials" };
            } 
            else
                obj = new { error = ex.Message };

            var result = JsonConvert.SerializeObject(obj);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}