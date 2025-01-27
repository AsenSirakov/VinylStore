using FluentValidation;
using VinylStore.Models.DTO;

namespace VinylStore.Validators
{
    public class SingerValidator : AbstractValidator<Singer>
    {
        public SingerValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("Singer ID is required.");

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100)
                .MinimumLength(2)
                .WithMessage("Singer Name must be between 2 and 100 characters.");
        }
    }
}
