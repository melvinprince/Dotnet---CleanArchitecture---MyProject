// File: src/Application/Authors/Commands/UpdateAuthor/UpdateAuthorCommandValidator.cs
using FluentValidation;

namespace Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator
        : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
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
