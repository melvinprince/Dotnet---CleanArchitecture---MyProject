// File: src/Application/Books/Commands/UpdateBook/UpdateBookCommand.cs
using MediatR;
using System;

namespace Application.Books.Commands.UpdateBook
{
    public record UpdateBookCommand(
        Guid     Id,
        string   Title,
        string   ISBN,
        DateTime PublishedDate,
        Guid     AuthorId
    ) : IRequest<Unit>;
}
