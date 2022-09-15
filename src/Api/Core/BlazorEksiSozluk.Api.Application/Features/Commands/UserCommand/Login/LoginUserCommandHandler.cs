using AutoMapper;
using BlazorEksiSozluk.Api.Application.Interfaces.Repositories;
using BlazorEksiSozluk.Common.Infrastructure;
using BlazorEksiSozluk.Common.Infrastructure.Exceptions;
using BlazorEksiSozluk.Common.Models.Queries;
using BlazorEksiSozluk.Common.Models.RequestModels;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorEksiSozluk.Api.Application.Features.Commands.UserCommand.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private IUserRepository userRepository;
        private IMapper mapper;
        private IConfiguration configuration;

        public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetSingleAsync(x => x.EmailAddress == request.EmailAddress);

            if(user is null)
                throw new DatabaseValidationException("User not found!");

            var password = PasswordEncryptor.Encrypt(request.Password);

            if(password != user.Password)
                throw new DatabaseValidationException("Password is wrong!");

            if(!user.EmailComfirmed)
                throw new DatabaseValidationException("Email address is not confirmed yet!");

            var result = mapper.Map<LoginUserViewModel>(user);

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.EmailAddress),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Surname,user.LastName),
                new Claim(ClaimTypes.Name,user.UserName),
            };

            result.Token = GenerateToken(claims);

            return result;
        }

        private string GenerateToken(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthConfig:Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(10);

            var token = new JwtSecurityToken(claims: claims, expires: expiry, signingCredentials: credentials, notBefore: DateTime.Now);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
