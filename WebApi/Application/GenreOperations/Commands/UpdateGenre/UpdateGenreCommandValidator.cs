using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            //RuleFor
            RuleFor(cmd=>cmd.Model.Name).MinimumLength(4).When(x=>x.Model.Name.Trim()!=string.Empty);
        }
    }
}