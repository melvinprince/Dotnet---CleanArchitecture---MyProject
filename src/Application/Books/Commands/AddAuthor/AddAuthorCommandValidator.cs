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

            RuleFor(x => x.Biography)
                .MaximumLength(1000)
                .When(x => x.Biography is not null);
        }
    }
}
