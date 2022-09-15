using AutoMapper;
using BlazorEksiSozluk.Api.Application.Interfaces.Repositories;
using BlazorEksiSozluk.Api.Domain.Models;
using BlazorEksiSozluk.Common.Constants;
using BlazorEksiSozluk.Common.Events.UserEvent;
using BlazorEksiSozluk.Common.Infrastructure;
using BlazorEksiSozluk.Common.Infrastructure.Exceptions;
using BlazorEksiSozluk.Common.Models.RequestModels;
using MediatR;

namespace BlazorEksiSozluk.Api.Application.Features.Commands.UserCommand.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existsUser = await userRepository.GetSingleAsync(x => x.EmailAddress == request.EmailAddress);

            if(existsUser is not null)
                throw new DatabaseValidationException("User already exists!");

            var dbUser = mapper.Map<User>(request);
            dbUser.Password = PasswordEncryptor.Encrypt(request.Password);

            var rows = await userRepository.AddAsync(dbUser);

            if(rows > 0)
            {
                var @event = new UserEmailChangedEvent
                {
                    NewEmailAddress = dbUser.EmailAddress,
                    OldEmailAddress = null
                };

                QueryFactory.SendMessageToExchange(exchangeName: SozlukConstants.UserExchangeName,
                                                   exchangeType: SozlukConstants.DefaultExchangeType,
                                                   queueName: SozlukConstants.UserEmailChangedQueueName,
                                                   @event);
            }

            return dbUser.Id;
        }
    }
}
