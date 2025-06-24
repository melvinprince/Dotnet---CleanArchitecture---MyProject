// File: src/Application/Books/Commands/DeleteBook/DeleteBookCommand.cs
using MediatR;
using System;

namespace Application.Books.Commands.DeleteBook
{
    public record DeleteBookCommand(Guid Id) : IRequest<Unit>;
}
