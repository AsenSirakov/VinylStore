using FluentValidation;
using VinylStore.Models.Requests;

namespace VinylStore.Validators
{
    public class AddVinylRequestValidator : AbstractValidator<AddVinylRequest>
    {
        public AddVinylRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .MaximumLength(100)
                .MinimumLength(2);

            RuleFor(x => x.Description)
                .NotNull()
                .MaximumLength(250)
                .MinimumLength(10);

        }
    }
}
