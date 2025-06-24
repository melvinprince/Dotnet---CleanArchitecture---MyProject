// File: src/Application/Borrowers/Commands/UpdateBorrower/UpdateBorrowerCommandValidator.cs
using FluentValidation;

namespace Application.Borrowers.Commands.UpdateBorrower
{
    public class UpdateBorrowerCommandValidator
        : AbstractValidator<UpdateBorrowerCommand>
    {
        public UpdateBorrowerCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MaximumLength(200);
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress();
        }
    }
}
