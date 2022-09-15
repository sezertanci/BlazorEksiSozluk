using MediatR;

namespace BlazorEksiSozluk.Common.Models.RequestModels
{
    public class CreateEntryCommentCommand : IRequest<Guid>
    {
        public CreateEntryCommentCommand()
        {

        }

        public CreateEntryCommentCommand(Guid entryId, Guid userId, string content)
        {
            EntryId = entryId;
            UserId = userId;
            Content = content;
        }

        public Guid? EntryId { get; set; }
        public Guid? UserId { get; set; }
        public string Content { get; set; }
    }
}
