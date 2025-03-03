﻿using FluentValidation;
using VinylStore.Models.Requests;

namespace VinylStore.Validators
{
    public class AddVinylRequestValidator : AbstractValidator<AddVinylRequest>
    {
        public AddVinylRequestValidator()
        {
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
