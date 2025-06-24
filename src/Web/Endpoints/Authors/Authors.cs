// src/Web/Endpoints/Authors/Authors.cs
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;  // ← add this
using Application.Authors.Commands.AddAuthor;
using Web.Infrastructure;

namespace Web.Endpoints.Authors
{
    public class Authors : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            var group = app.MapGroup(this);

            group.MapPost(string.Empty, CreateAuthor)
                 .WithName("AddAuthor");
        }

        public async Task<Created<Guid>> CreateAuthor(
            ISender sender,
            [FromBody] AddAuthorCommand command)
        {
            var id = await sender.Send(command);
            return TypedResults.Created($"/api/Authors/{id}", id);
        }
    }
}
