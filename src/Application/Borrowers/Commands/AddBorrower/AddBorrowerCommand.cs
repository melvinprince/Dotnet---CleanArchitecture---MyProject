// File: src/Application/Borrowers/Commands/AddBorrower/AddBorrowerCommand.cs
using MediatR;
using System;

namespace Application.Borrowers.Commands.AddBorrower
{
    public record AddBorrowerCommand(
        string FullName,
        string Email,
        string? PhoneNumber
    ) : IRequest<Guid>;
}
