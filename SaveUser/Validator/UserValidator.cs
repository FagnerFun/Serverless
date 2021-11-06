using FluentValidation;
using SaveUser.Model;

namespace SaveUser.Validator
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Mail)
                .NotEmpty()
                .NotNull()
                .EmailAddress()
                .WithMessage("Email invalido");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .NotNull()
                .WithMessage("Nome invalido");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .NotNull()
                .WithMessage("Sobrenome invalido");
        }
    }
}
