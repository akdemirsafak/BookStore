using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator:AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(x=>x.Model.Name).NotEmpty().MinimumLength(4).MaximumLength(100);

            RuleFor(x => x.Model.LastName).NotEmpty().MinimumLength(4).MaximumLength(100);

            RuleFor(x => x.Model.BirthDate).NotEmpty().LessThan(DateTime.Now.Date);
        }
    }
}