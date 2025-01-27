using FluentValidation;
using VinylStore.Models.Requests;

namespace VinylStore.Validators
{
    public class UpdateVinylRequestValidator : AbstractValidator<UpdateVinylRequest>
    {
        public UpdateVinylRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("Vinyl ID is required.");

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100)
                .MinimumLength(2)
                .WithMessage("Vinyl Name must be between 2 and 100 characters.");

            RuleFor(x => x.Description)
                .NotNull()
                .NotEmpty()
                .MaximumLength(250)
                .MinimumLength(10)
                .WithMessage("Vinyl Description must be between 10 and 250 characters.");
        }
    }
}
