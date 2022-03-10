using FluentValidation;

namespace WebApi.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandValidator:AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(cmd=>cmd.Model.Name).NotNull().NotEmpty().MinimumLength(3).MaximumLength(100);
            RuleFor(cmd=>cmd.Model.SurName).NotNull().NotEmpty().MinimumLength(3).MaximumLength(100);
            RuleFor(cmd=>cmd.Model.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(cmd=>cmd.Model.Password).NotNull().NotEmpty().MinimumLength(8).MaximumLength(16);
        }
    }
}