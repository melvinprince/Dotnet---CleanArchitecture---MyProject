// File: src/Application/Authors/Queries/GetAuthorById/GetAuthorByIdQuery.cs
using MediatR;
using Application.Common.Models;
using System;

namespace Application.Authors.Queries.GetAuthorById
{
    public record GetAuthorByIdQuery(Guid Id) : IRequest<AuthorDto>;
}
