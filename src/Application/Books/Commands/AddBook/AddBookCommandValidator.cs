// src/Application/Books/Commands/AddBook/AddBookCommandValidator.cs
using FluentValidation;

namespace Application.Books.Commands.AddBook
{
    public class AddBookCommandValidator 
        : AbstractValidator<AddBookCommand>
    {
        public AddBookCommandValidator()
        {
            RuleFor(x => x.AuthorId)
                .NotEmpty().WithMessage("Author ID is required.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200);

            RuleFor(x => x.ISBN)
                .NotEmpty().WithMessage("ISBN is required.")
                .Length(10, 13)
                .WithMessage("ISBN must be between 10 and 13 characters.");

            RuleFor(x => x.PublishedDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Published date cannot be in the future.");
        }
    }
}
