using FluentValidation;
using VinylStore.Models.DTO;

namespace VinylStore.Validators
{
    public class SongValidator : AbstractValidator<Song>
    {
        public SongValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Song ID cannot be empty.")
                .MaximumLength(50).WithMessage("Song ID cannot exceed 50 characters.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Song name cannot be empty.")
                .MaximumLength(100).WithMessage("Song name cannot exceed 100 characters.");

            RuleFor(x => x.Genre)
                .NotEmpty().WithMessage("Genre cannot be empty.")
                .MaximumLength(50).WithMessage("Genre cannot exceed 50 characters.");

            RuleFor(x => x.Year)
                .NotEmpty().WithMessage("Year cannot be empty.")
                .Matches(@"^\d{4}$").WithMessage("Year must be a valid four-digit year.");

            RuleFor(x => x.Singer)
                .NotNull().WithMessage("Singer cannot be null.")
                .SetValidator(new SingerValidator());
        }
    }
}