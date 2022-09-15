using BlazorEksiSozluk.Api.Application.Interfaces.Repositories;
using BlazorEksiSozluk.Common.Events.UserEvent;
using BlazorEksiSozluk.Common.Infrastructure;
using BlazorEksiSozluk.Common.Infrastructure.Exceptions;
using MediatR;

namespace BlazorEksiSozluk.Api.Application.Features.Commands.UserCommand.ChangePassword
{
    public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, bool>
    {
        private readonly IUserRepository userRepository;

        public ChangeUserPasswordCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<bool> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            if(!request.UserId.HasValue)
                throw new ArgumentNullException(nameof(request.UserId));

            var dbUser = await userRepository.GetByIdAsync(request.UserId.Value);

            if(dbUser is null)
                throw new DatabaseValidationException("User not found!");

            if(dbUser.Password != PasswordEncryptor.Encrypt(request.OldPassword))
                throw new DatabaseValidationException("Old password wrong!");

            dbUser.Password = PasswordEncryptor.Encrypt(request.NewPassword);

            await userRepository.UpdateAsync(dbUser);

            return true;
        }
    }
}
