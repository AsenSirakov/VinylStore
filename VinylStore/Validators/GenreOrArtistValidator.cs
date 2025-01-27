using FluentValidation;

namespace VinylStore.Validators
{
    public class GenreOrArtistValidator : AbstractValidator<string>
    {
        public GenreOrArtistValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Input cannot be empty or exceed 50 characters.");
        }
    }
}
