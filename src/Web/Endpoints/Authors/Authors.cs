// File: src/Web/Endpoints/Authors/Authors.cs
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Application.Common.Models;
using Application.Authors.Commands.AddAuthor;
using Application.Authors.Commands.UpdateAuthor;
using Application.Authors.Commands.DeleteAuthor;
using Application.Authors.Queries.GetAuthorsList;
using Application.Authors.Queries.GetAuthorById;
using Web.Infrastructure;

namespace Web.Endpoints.Authors
{
    public class Authors : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            var group = app.MapGroup(this);

            group.MapGet(string.Empty, GetAll)
                 .WithName("GetAuthors");

            group.MapGet("{id}", GetById)
                 .WithName("GetAuthorById");

            group.MapPost(string.Empty, Create)
                 .WithName("AddAuthor");

            group.MapPut("{id}", Update)
                 .WithName("UpdateAuthor");

            group.MapDelete("{id}", Delete)
                 .WithName("DeleteAuthor");
        }

        public async Task<Ok<List<AuthorDto>>> GetAll(ISender sender)
        {
            var list = await sender.Send(new GetAuthorsListQuery());
            return TypedResults.Ok(list);
        }

        public async Task<Ok<AuthorDto>> GetById(ISender sender, Guid id)
        {
            var dto = await sender.Send(new GetAuthorByIdQuery(id));
            return TypedResults.Ok(dto);
        }

        public async Task<Created<Guid>> Create(
            ISender sender,
            [FromBody] AddAuthorCommand cmd)
        {
            var id = await sender.Send(cmd);
            return TypedResults.Created($"/api/Authors/{id}", id);
        }

        public async Task<Results<NoContent, BadRequest>> Update(
            ISender sender,
            Guid id,
            [FromBody] UpdateAuthorCommand cmd)
        {
            if (id != cmd.Id)
                return TypedResults.BadRequest();

            await sender.Send(cmd);
            return TypedResults.NoContent();
        }

        public async Task<NoContent> Delete(ISender sender, Guid id)
        {
            await sender.Send(new DeleteAuthorCommand(id));
            return TypedResults.NoContent();
        }
    }
}
