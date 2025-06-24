// File: src/Application/Authors/Commands/DeleteAuthor/DeleteAuthorCommand.cs
using MediatR;
using System;

namespace Application.Authors.Commands.DeleteAuthor
{
    public record DeleteAuthorCommand(Guid Id) : IRequest<Unit>;
}
