// File: src/Application/Borrowers/Commands/DeleteBorrower/DeleteBorrowerCommand.cs
using MediatR;
using System;

namespace Application.Borrowers.Commands.DeleteBorrower
{
    public record DeleteBorrowerCommand(Guid Id) : IRequest<Unit>;
}
