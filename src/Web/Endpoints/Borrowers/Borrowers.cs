// File: src/Web/Endpoints/Borrowers/Borrowers.cs
using MediatR;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Application.Common.Models;
using Application.Borrowers.Commands.AddBorrower;
using Application.Borrowers.Commands.DeleteBorrower;
using Application.Borrowers.Commands.UpdateBorrower;
using Application.Borrowers.Queries.GetBorrowerById;
using Application.Borrowers.Queries.GetBorrowersList;
using Web.Infrastructure;
using Domain.Constants;

namespace Web.Endpoints.Borrowers
{
    public class Borrowers : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            var group = app.MapGroup(this)
                   .RequireAuthorization(new AuthorizeAttribute { Roles = Roles.Administrator });

            // GET all
            group.MapGet(string.Empty, GetAll)
                 .WithName("GetBorrowers");

            // GET by id
            group.MapGet("{id}", GetById)
                 .WithName("GetBorrowerById");

            // POST
            group.MapPost(string.Empty, Create)
                 .WithName("AddBorrower");

            // PUT
            group.MapPut("{id}", Update)
                 .WithName("UpdateBorrower");

            // DELETE
            group.MapDelete("{id}", Delete)
                 .WithName("DeleteBorrower");
        }

        public async Task<Ok<List<BorrowerDto>>> GetAll(
            ISender sender)
        {
            var list = await sender.Send(new GetBorrowersListQuery());
            return TypedResults.Ok(list);
        }

        public async Task<Ok<BorrowerDto>> GetById(
            ISender sender,
            Guid id)
        {
            var dto = await sender.Send(new GetBorrowerByIdQuery(id));
            return TypedResults.Ok(dto);
        }

        public async Task<Created<Guid>> Create(
            ISender sender,
            [FromBody] AddBorrowerCommand cmd)
        {
            var id = await sender.Send(cmd);
            return TypedResults.Created($"/api/Borrowers/{id}", id);
        }

         public async Task<Results<NoContent, BadRequest>> Update(
            ISender sender,
            Guid id,
            [FromBody] UpdateBorrowerCommand cmd)
        {
            if (id != cmd.Id)
            return TypedResults.BadRequest();
            await sender.Send(cmd);
            return TypedResults.NoContent();
        }

        public async Task<NoContent> Delete(
            ISender sender,
            Guid id)
        {
            await sender.Send(new DeleteBorrowerCommand(id));
            return TypedResults.NoContent();
        }
    }
}
