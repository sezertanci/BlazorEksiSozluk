using MediatR;

namespace BlazorEksiSozluk.Common.Models.RequestModels
{
    public class CreateEntryCommand : IRequest<Guid>
    {
        public CreateEntryCommand()
        {

        }

        public CreateEntryCommand(string subject, string content, Guid? userId)
        {
            Subject = subject;
            Content = content;
            UserId = userId;
        }

        public string Subject { get; set; }
        public string Content { get; set; }
        public Guid? UserId { get; set; }
    }
}
