// File: src/Application/Authors/Commands/AddAuthor/AddAuthorCommandValidator.cs
using FluentValidation;

namespace Application.Authors.Commands.AddAuthor
{
    public class AddAuthorCommandValidator
        : AbstractValidator<AddAuthorCommand>
    {
        public AddAuthorCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(100);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(100);

            RuleFor(x => x.DateOfBirth)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Date of birth cannot be in the future.");

            RuleFor(x => x.Biography)
                .MaximumLength(1000);
        }
    }
}
