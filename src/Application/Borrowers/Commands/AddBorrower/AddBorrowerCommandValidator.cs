// File: src/Application/Borrowers/Commands/AddBorrower/AddBorrowerCommandValidator.cs
using FluentValidation;

namespace Application.Borrowers.Commands.AddBorrower
{
    public class AddBorrowerCommandValidator
        : AbstractValidator<AddBorrowerCommand>
    {
        public AddBorrowerCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MaximumLength(200);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
        }
    }
}
