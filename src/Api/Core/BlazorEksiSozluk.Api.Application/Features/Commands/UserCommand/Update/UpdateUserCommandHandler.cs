using AutoMapper;
using BlazorEksiSozluk.Api.Application.Interfaces.Repositories;
using BlazorEksiSozluk.Common.Constants;
using BlazorEksiSozluk.Common.Events.UserEvent;
using BlazorEksiSozluk.Common.Infrastructure;
using BlazorEksiSozluk.Common.Infrastructure.Exceptions;
using BlazorEksiSozluk.Common.Models.RequestModels;
using MediatR;

namespace BlazorEksiSozluk.Api.Application.Features.Commands.UserCommand.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public UpdateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await userRepository.GetByIdAsync(request.Id);

            if(dbUser is null)
                throw new DatabaseValidationException("User not found!");

            var dbEmailAddress = dbUser.EmailAddress;

            mapper.Map(request, dbUser);

            var rows = await userRepository.UpdateAsync(dbUser);

            var emailChanged = string.CompareOrdinal(dbEmailAddress, request.EmailAddress) != 0;

            if(emailChanged && rows > 0)
            {
                var @event = new UserEmailChangedEvent
                {
                    NewEmailAddress = dbUser.EmailAddress,
                    OldEmailAddress = dbEmailAddress
                };

                QueryFactory.SendMessageToExchange(exchangeName: SozlukConstants.UserExchangeName,
                                                   exchangeType: SozlukConstants.DefaultExchangeType,
                                                   queueName: SozlukConstants.UserEmailChangedQueueName,
                                                   @event);

                dbUser.EmailComfirmed = false;

                await userRepository.UpdateAsync(dbUser);
            }

            return dbUser.Id;
        }
    }
}
