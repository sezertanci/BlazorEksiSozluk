using BlazorEksiSozluk.Api.Application.Interfaces.Repositories;
using BlazorEksiSozluk.Common.Infrastructure.Exceptions;
using MediatR;

namespace BlazorEksiSozluk.Api.Application.Features.Commands.UserCommand.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, bool>
    {
        private readonly IUserRepository userRepository;
        private readonly IEmailConfirmationRepository emailConfirmationRepository;

        public ConfirmEmailCommandHandler(IUserRepository userRepository, IEmailConfirmationRepository emailConfirmationRepository)
        {
            this.userRepository = userRepository;
            this.emailConfirmationRepository = emailConfirmationRepository;
        }

        public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var confirmation = await emailConfirmationRepository.GetByIdAsync(request.ConfirmationId);

            if(confirmation is null)
                throw new DatabaseValidationException("Confirmation not found!");

            var dbUser = await userRepository.GetSingleAsync(x => x.EmailAddress == confirmation.NewEmailAddress);

            if(dbUser is null)
                throw new DatabaseValidationException("User not found with this email!");

            if(dbUser.EmailComfirmed)
                throw new DatabaseValidationException("Email address is already confirmed!");

            dbUser.EmailComfirmed = true;

            await userRepository.UpdateAsync(dbUser);

            return true;
        }
    }
}
