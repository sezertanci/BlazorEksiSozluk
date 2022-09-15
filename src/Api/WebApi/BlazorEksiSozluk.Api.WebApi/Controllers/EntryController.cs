using BlazorEksiSozluk.Api.Application.Features.Queries.GetEntries;
using BlazorEksiSozluk.Api.Application.Features.Queries.GetEntries.GetMainPageEntries;
using BlazorEksiSozluk.Api.Application.Features.Queries.GetEntryComments;
using BlazorEksiSozluk.Api.Application.Features.Queries.GetEntryDetail;
using BlazorEksiSozluk.Api.Application.Features.Queries.GetUserEntries;
using BlazorEksiSozluk.Common.Models.Queries;
using BlazorEksiSozluk.Common.Models.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEksiSozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntryController : ExtendBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetEntries([FromQuery] GetEntriesQuery getEntriesQuery)
        {
            var result = await mediator.Send(getEntriesQuery);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await mediator.Send(new GetEntryDetailQuery(id, UserId));

            return Ok(result);
        }

        [HttpGet]
        [Route("UserEntries")]
        [Authorize]
        public async Task<IActionResult> GetUserEntries(Guid userId, string userName, int pageNumber, int pageSize)
        {
            if(userId == Guid.Empty && string.IsNullOrEmpty(userName))
                userId = UserId.Value;

            var result = await mediator.Send(new GetUserEntriesQuery(userId, userName, pageNumber, pageSize));

            return Ok(result);
        }

        [HttpGet]
        [Route("MainPageEntries")]
        public async Task<IActionResult> GetMainPageEntries(int pageNumber, int pageSize)
        {
            var result = await mediator.Send(new GetMainPageEntriesQuery(UserId, pageNumber, pageSize));

            return Ok(result);
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search([FromQuery] SearchEntryQuery searchEntryQuery)
        {
            var result = await mediator.Send(searchEntryQuery);

            return Ok(result);
        }

        [HttpGet]
        [Route("EntryComments/{entryId}")]
        [Authorize]
        public async Task<IActionResult> GetEntryComments(Guid entryId, int pageNumber, int pageSize)
        {
            var result = await mediator.Send(new GetEntryCommentsQuery(entryId, UserId, pageNumber, pageSize));

            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateEntryCommand createEntryCommand)
        {
            if(!createEntryCommand.UserId.HasValue)
                createEntryCommand.UserId = UserId;

            var result = await mediator.Send(createEntryCommand);

            return Ok(result);
        }

        [HttpPost]
        [Route("CreateEntryComment")]
        [Authorize]
        public async Task<IActionResult> CreateEntryComment([FromBody] CreateEntryCommentCommand createEntryCommentCommand)
        {
            if(!createEntryCommentCommand.UserId.HasValue)
                createEntryCommentCommand.UserId = UserId;
            var result = await mediator.Send(createEntryCommentCommand);

            return Ok(result);
        }
    }
}
