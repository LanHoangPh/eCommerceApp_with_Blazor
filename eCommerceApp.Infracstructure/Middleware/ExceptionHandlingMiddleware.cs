using eCommerceApp.Application.Services.Interfaces.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Infracstructure.Middleware
{
    public class ExceptionHandlingMiddleware(RequestDelegate request)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await request(context);
            }
            catch (DbUpdateException ex) 
            {
                var loger = context.RequestServices.GetRequiredService<IAppLogger<ExceptionHandlingMiddleware>>();
                context.Response.ContentType = "application/json";
                if (ex.InnerException is SqlException innerException)
                {
                    loger.LogError(innerException, "Sql Exception");
                    switch (innerException.Number)
                    {
                        case 2627:
                            context.Response.StatusCode = StatusCodes.Status409Conflict;
                            await context.Response.WriteAsync("Unique constraint violation");
                            break;
                        case 515:
                            context.Response.StatusCode = StatusCodes.Status400BadRequest;
                            await context.Response.WriteAsync("can not insert null");
                            break;
                        case 547:
                            context.Response.StatusCode = StatusCodes.Status400BadRequest;
                            await context.Response.WriteAsync("can not insert null");
                            break;
                         default:
                            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                            await context.Response.WriteAsync("An error occurred while saving the enity changes");
                            break;
                    }
                }
                else
                {
                    loger.LogError(ex, "Realated EFcore Exception");
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync("An error occurred while saving the enity changes");
                }
            }
            catch(Exception ex) 
            {
                var loger = context.RequestServices.GetRequiredService<IAppLogger<ExceptionHandlingMiddleware>>();
                loger.LogError(ex, "Unknow Exception");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync($"Error: {ex.Message}");
            }
        }
    }
}
