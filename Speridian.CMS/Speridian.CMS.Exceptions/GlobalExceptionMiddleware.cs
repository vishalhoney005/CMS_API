using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog;
using Speridian.CMS.Exceptions;

namespace Speridian.CMS.Exceptions
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                switch (error)
                {
                    case AllException e:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        Serilog.Log.Error(e.Message);
                        break;
                    case BadHttpRequestException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        Serilog.Log.Error(e.Message);
                        break;
                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        Serilog.Log.Error(e.Message);
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        Serilog.Log.Error(error?.Message);
                        break;
                }
                var result = JsonSerializer.Serialize(new { error = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
