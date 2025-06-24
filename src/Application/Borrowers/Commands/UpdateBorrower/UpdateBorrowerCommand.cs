// File: src/Application/Borrowers/Commands/UpdateBorrower/UpdateBorrowerCommand.cs
using MediatR;
using System;

namespace Application.Borrowers.Commands.UpdateBorrower
{
    public record UpdateBorrowerCommand(
        Guid   Id,
        string FullName,
        string Email,
        string? PhoneNumber
    ) : IRequest<Unit>;
}
