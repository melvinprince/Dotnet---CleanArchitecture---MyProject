using FluentValidation;

namespace Application.Books.Commands.BorrowBook
{
    public class BorrowBookCommandValidator : AbstractValidator<BorrowBookCommand>
    {
        public BorrowBookCommandValidator()
        {
            RuleFor(x => x.BookId)
                .NotEmpty().WithMessage("Book ID is required.");

            RuleFor(x => x.BorrowerId)
                .NotEmpty().WithMessage("Borrower ID is required.");
        }
    }
}
