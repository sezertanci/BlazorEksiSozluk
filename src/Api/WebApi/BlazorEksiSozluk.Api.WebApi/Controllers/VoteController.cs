using BlazorEksiSozluk.Api.Application.Features.Commands.EntryCommand.CreateVote;
using BlazorEksiSozluk.Api.Application.Features.Commands.EntryCommand.DeleteVote;
using BlazorEksiSozluk.Api.Application.Features.Commands.EntryCommentCommand.DeleteVote;
using BlazorEksiSozluk.Common.Models;
using BlazorEksiSozluk.Common.Models.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEksiSozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VoteController : ExtendBaseController
    {
        [HttpPost]
        [Route("EntryVote/{entryId}")]
        public async Task<IActionResult> CreateEntryVote(Guid entryId, VoteType voteType = VoteType.UpVote)
        {
            var result = await mediator.Send(new CreateEntryVoteCommand(entryId, UserId.Value, voteType));

            return Ok(result);
        }

        [HttpPost]
        [Route("EntryCommentVote/{entryCommentId}")]
        public async Task<IActionResult> CreateEntryCommentVote(Guid entryCommentId, VoteType voteType = VoteType.UpVote)
        {
            var result = await mediator.Send(new CreateEntryCommentVoteCommand(entryCommentId, UserId.Value, voteType));

            return Ok(result);
        }

        [HttpPost]
        [Route("DeleteEntryVote/{entryId}")]
        public async Task<IActionResult> DeleteEntryVote(Guid entryId)
        {
            var result = await mediator.Send(new DeleteEntryVoteCommand(entryId, UserId.Value));

            return Ok(result);
        }

        [HttpPost]
        [Route("DeleteEntryCommentVote/{entryCommentId}")]
        public async Task<IActionResult> DeleteEntryCommentVote(Guid entryCommentId)
        {
            var result = await mediator.Send(new DeleteEntryCommentVoteCommand(entryCommentId, UserId.Value));

            return Ok(result);
        }
    }
}
