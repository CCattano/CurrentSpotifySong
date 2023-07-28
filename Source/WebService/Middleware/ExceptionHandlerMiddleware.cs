using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Exceptions;

namespace Torty.Web.Apps.CurrentSpotifySong.WebService.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpCtx)
    {
        try
        {
            await _next(httpCtx);
        }
        catch (WebException webEx)
        {
            httpCtx.Response.StatusCode = (int)webEx.ResponseCode;
            await httpCtx.Response.WriteAsync(webEx.Message);
        }
    }
}

public static class ExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}