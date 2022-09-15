using BlazorEksiSozluk.Common.Models.RequestModels;
using FluentValidation;

namespace BlazorEksiSozluk.Api.Application.Features.Commands.UserCommand.Login
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.EmailAddress).NotNull().EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).WithMessage("{PropertyName} not a valid email address");

            RuleFor(x => x.Password).NotNull().MinimumLength(6).WithMessage("{PropertyName} shoult at be {MinLength} characters");
        }
    }
}
