using BlazorEksiSozluk.Common.Infrastructure.Exceptions;
using BlazorEksiSozluk.Common.Infrastructure.Results;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace BlazorEksiSozluk.Api.WebApi.Infrastructure.Extensions;

public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder ConfigureExceptionHandling(this IApplicationBuilder applicationBuilder, bool includeExceptionDetails = false, bool useDefaultHandlingResponse = true, Func<HttpContext, Exception, Task> handleException = null)
    {
        applicationBuilder.UseExceptionHandler(options =>
        {
            options.Run(context =>
            {
                var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();

                if(!useDefaultHandlingResponse && handleException == null)
                    throw new ArgumentNullException(nameof(handleException), $"{nameof(handleException)} cannot be null when {nameof(useDefaultHandlingResponse)} is false");

                if(!useDefaultHandlingResponse && handleException != null)
                    return handleException(context, exceptionObject.Error);

                return DefaultHandleException(context, exceptionObject.Error, includeExceptionDetails);
            });
        });

        return applicationBuilder;
    }

    private static async Task DefaultHandleException(HttpContext httpContext, Exception exception, bool includeExceptionDetails)
    {
        LogWrite(exception);

        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        string message = "Internal Server Error occured!";

        if(exception is UnauthorizedAccessException)
            statusCode = HttpStatusCode.Unauthorized;

        if(exception is DatabaseValidationException)
        {
            statusCode = HttpStatusCode.BadRequest;

            var validationResponse = new ValidationResponseModel(exception.Message);

            await WriteResponse(httpContext, statusCode, validationResponse);

            return;
        }

        var response = new
        {
            HttpStatusCode = (int)statusCode,
            Detail = includeExceptionDetails ? exception.ToString() : message
        };

        await WriteResponse(httpContext, statusCode, response);
    }

    private static async Task WriteResponse(HttpContext httpContext, HttpStatusCode httpStatusCode, object responseObject)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)httpStatusCode;

        await httpContext.Response.WriteAsJsonAsync(responseObject);
    }

    private static void LogWrite(Exception exception)
    {
        string divider = "-";
        string txtMessage = divider.PadLeft(20, '-') + "\t" + DateTime.Now.ToString("F") + "\t" + divider.PadRight(20, '-') + "\n";
        txtMessage += exception.ToString() + "\n";

        using(StreamWriter writetext = new("ExceptionLog.txt", true))
        {
            writetext.WriteLine(txtMessage);
        }
    }
}