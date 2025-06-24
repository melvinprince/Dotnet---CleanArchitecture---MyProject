// File: src/Application/Authors/Commands/AddAuthor/AddAuthorCommand.cs
using MediatR;
using System;

namespace Application.Authors.Commands.AddAuthor
{
    public record AddAuthorCommand(
        string FirstName,
        string LastName,
        DateTime? DateOfBirth,
        string? Biography
    ) : IRequest<Guid>;
}
