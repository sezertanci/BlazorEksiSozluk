using AutoMapper;
using BlazorEksiSozluk.Api.Application.Interfaces.Repositories;
using BlazorEksiSozluk.Api.Domain.Models;
using BlazorEksiSozluk.Common.Models.Queries;
using MediatR;

namespace BlazorEksiSozluk.Api.Application.Features.Queries.GetUserDetail
{
    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDetailViewModel>
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public GetUserDetailQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<UserDetailViewModel> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            var dbUser = new User();

            if(request.UserId != Guid.Empty)
                dbUser = await userRepository.GetByIdAsync(request.UserId);
            else if(!string.IsNullOrEmpty(request.UserName))
                dbUser = await userRepository.FirstOrDefaultAsync(x => x.UserName == request.UserName);
            else throw new Exception("Invalid UserName or UserId");

            return mapper.Map<UserDetailViewModel>(dbUser);
        }
    }
}
