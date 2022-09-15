using BlazorEksiSozluk.Api.Application.Features.Queries.GetEntryComments;
using BlazorEksiSozluk.Common.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEksiSozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntryCommentController : ExtendBaseController
    {
        [HttpGet]
        [Route("EntryComments/{entryId}")]
        public async Task<IActionResult> GetEntryComments(Guid entryId, int pageNumber, int pageSize)
        {
            var result = await mediator.Send(new GetEntryCommentsQuery(entryId, UserId, pageNumber, pageSize));

            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] CreateEntryCommentCommand createEntryCommentCommand)
        {
            if(!createEntryCommentCommand.UserId.HasValue)
                createEntryCommentCommand.UserId = UserId;
            var result = await mediator.Send(createEntryCommentCommand);

            return Ok(result);
        }
    }
}
