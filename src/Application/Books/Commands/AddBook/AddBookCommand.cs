// src/Application/Books/Commands/AddBook/AddBookCommand.cs
using MediatR;
using System;

namespace Application.Books.Commands.AddBook
{
    public record AddBookCommand(
        Guid AuthorId,
        string Title,
        string ISBN,
        DateTime PublishedDate
    ) : IRequest<Guid>;
}
