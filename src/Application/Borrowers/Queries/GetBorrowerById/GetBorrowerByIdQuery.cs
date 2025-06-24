// File: src/Application/Borrowers/Queries/GetBorrowerById/GetBorrowerByIdQuery.cs
using MediatR;
using Application.Common.Models;
using System;

namespace Application.Borrowers.Queries.GetBorrowerById
{
    public record GetBorrowerByIdQuery(Guid Id) : IRequest<BorrowerDto>;
}
