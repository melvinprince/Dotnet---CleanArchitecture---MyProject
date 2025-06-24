// File: src/Application/Books/Commands/UpdateBook/UpdateBookCommandValidator.cs
using FluentValidation;

namespace Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommandValidator
        : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200);

            RuleFor(x => x.ISBN)
                .NotEmpty().WithMessage("ISBN is required.")
                .Length(10,13).WithMessage("ISBN must be 10–13 chars.");

            RuleFor(x => x.PublishedDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Published date can't be in the future.");
            
            RuleFor(x => x.AuthorId)
                .NotEmpty().WithMessage("Author ID is required.");
        }
    }
}
