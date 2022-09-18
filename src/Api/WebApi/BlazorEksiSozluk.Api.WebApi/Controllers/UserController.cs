using BlazorEksiSozluk.Api.Application.Features.Commands.UserCommand.ConfirmEmail;
using BlazorEksiSozluk.Api.Application.Features.Queries.GetUserDetail;
using BlazorEksiSozluk.Common.Events.UserEvent;
using BlazorEksiSozluk.Common.Models.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEksiSozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ExtendBaseController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await mediator.Send(new GetUserDetailQuery(id));

            return Ok(result);
        }

        [HttpGet]
        [Route("UserName/{userName}")]
        public async Task<IActionResult> GetByUserName(string userName)
        {
            var result = await mediator.Send(new GetUserDetailQuery(Guid.Empty, userName));

            return Ok(result);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand loginUserCommand)
        {
            var result = await mediator.Send(loginUserCommand);

            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand createUserCommand)
        {
            var result = await mediator.Send(createUserCommand);

            return Ok(result);
        }

        [HttpPost]
        [Route("Update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand updateUserCommand)
        {
            var result = await mediator.Send(updateUserCommand);

            return Ok(result);
        }

        [HttpGet]
        [Route("Confirm/{id}")]
        public async Task<IActionResult> ConfirmEmail(Guid id)
        {
            var result = await mediator.Send(new ConfirmEmailCommand() { ConfirmationId = id });

            return Ok(result);
        }

        [HttpPost]
        [Route("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordCommand changeUserPasswordCommand)
        {
            var result = await mediator.Send(changeUserPasswordCommand);

            return Ok(result);
        }
    }
}
