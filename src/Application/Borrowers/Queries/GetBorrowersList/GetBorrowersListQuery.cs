// File: src/Application/Borrowers/Queries/GetBorrowersList/GetBorrowersListQuery.cs
using MediatR;
using Application.Common.Models;
using System.Collections.Generic;

namespace Application.Borrowers.Queries.GetBorrowersList
{
    public record GetBorrowersListQuery : IRequest<List<BorrowerDto>>;
}
