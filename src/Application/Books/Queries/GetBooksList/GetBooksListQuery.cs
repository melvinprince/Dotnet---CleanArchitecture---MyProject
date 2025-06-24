// File: src/Application/Books/Queries/GetBooksList/GetBooksListQuery.cs
using MediatR;
using Application.Common.Models;
using System.Collections.Generic;

namespace Application.Books.Queries.GetBooksList
{
    public record GetBooksListQuery : IRequest<List<BookDto>>;
}
