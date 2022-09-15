using BlazorEksiSozluk.Api.Application.Features.Commands.EntryCommand.CreateFavorite;
using BlazorEksiSozluk.Api.Application.Features.Commands.EntryCommand.DeleteFavorite;
using BlazorEksiSozluk.Api.Application.Features.Commands.EntryCommentCommand.DeleteFavorite;
using BlazorEksiSozluk.Common.Models.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEksiSozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FavoriteController : ExtendBaseController
    {
        [HttpPost]
        [Route("Entry/{entryId}")]
        public async Task<IActionResult> CreateEntryFavorite(Guid entryId)
        {
            var result = await mediator.Send(new CreateEntryFavoriteCommand(entryId, UserId.Value));

            return Ok(result);
        }

        [HttpPost]
        [Route("EntryComment/{entryCommentId}")]
        public async Task<IActionResult> CreateEntryCommentFavorite(Guid entryCommentId)
        {
            var result = await mediator.Send(new CreateEntryCommentFavoriteCommand(entryCommentId, UserId.Value));

            return Ok(result);
        }

        [HttpPost]
        [Route("DeleteEntryFavorite/{entryId}")]
        public async Task<IActionResult> DeleteEntryFavorite(Guid entryId)
        {
            var result = await mediator.Send(new DeleteEntryFavoriteCommand(entryId, UserId.Value));

            return Ok(result);
        }

        [HttpPost]
        [Route("DeleteEntryCommentFavorite/{entryCommentId}")]
        public async Task<IActionResult> DeleteEntryCommentFavorite(Guid entryCommentId)
        {
            var result = await mediator.Send(new DeleteEntryCommentFavoriteCommand(entryCommentId, UserId.Value));

            return Ok(result);
        }
    }
}
