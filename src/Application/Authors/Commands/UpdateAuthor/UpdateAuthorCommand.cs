// File: src/Application/Authors/Commands/UpdateAuthor/UpdateAuthorCommand.cs
using MediatR;
using System;

namespace Application.Authors.Commands.UpdateAuthor
{
    public record UpdateAuthorCommand(
        Guid   Id,
        string FirstName,
        string LastName,
        DateTime? DateOfBirth,
        string? Biography
    ) : IRequest<Unit>;
}
