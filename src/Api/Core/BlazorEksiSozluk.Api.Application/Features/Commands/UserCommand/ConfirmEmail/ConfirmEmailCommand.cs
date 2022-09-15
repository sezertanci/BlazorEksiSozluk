using MediatR;

namespace BlazorEksiSozluk.Api.Application.Features.Commands.UserCommand.ConfirmEmail
{
    public class ConfirmEmailCommand : IRequest<bool>
    {
        public Guid ConfirmationId { get; set; }
    }
}
