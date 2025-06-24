// File: src/Application/Books/Queries/GetBookById/GetBookByIdQuery.cs
using MediatR;
using Application.Common.Models;
using System;

namespace Application.Books.Queries.GetBookById
{
    public record GetBookByIdQuery(Guid Id) : IRequest<BookDto>;
}
