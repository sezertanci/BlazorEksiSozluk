using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlazorEksiSozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtendBaseController : ControllerBase
    {
        public Guid? UserId => new(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        private IMediator? _mediator;
        protected IMediator? mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
