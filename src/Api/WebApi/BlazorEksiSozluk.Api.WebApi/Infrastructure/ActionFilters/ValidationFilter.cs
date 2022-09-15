using BlazorEksiSozluk.Common.Infrastructure.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BlazorEksiSozluk.Api.WebApi.Infrastructure.ActionFilters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(!context.ModelState.IsValid)
            {
                var message = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => !string.IsNullOrEmpty(x.ErrorMessage) ? x.ErrorMessage : x.Exception?.Message).Distinct().ToList();

                var result = new ValidationResponseModel(message);

                context.Result = new BadRequestObjectResult(result);

                return;
            }

            await next();
        }
    }
}
