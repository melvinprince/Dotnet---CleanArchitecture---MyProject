// File: src/Application/Authors/Queries/GetAuthorsList/GetAuthorsListQuery.cs
using MediatR;
using Application.Common.Models;
using System.Collections.Generic;

namespace Application.Authors.Queries.GetAuthorsList
{
    public record GetAuthorsListQuery : IRequest<List<AuthorDto>>;
}
