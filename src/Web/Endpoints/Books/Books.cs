// File: src/Web/Endpoints/Books/Books.cs
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Application.Common.Models;
using Application.Books.Commands.AddBook;
using Microsoft.AspNetCore.Authorization; 
using Application.Books.Commands.BorrowBook;
using Application.Books.Commands.UpdateBook;
using Application.Books.Commands.DeleteBook;
using Application.Books.Queries.GetBooksList;
using Application.Books.Queries.GetBookById;
using Web.Infrastructure;
using Domain.Constants;


namespace Web.Endpoints.Books
{
    public class Books : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
             var group = app.MapGroup(this)
                   .RequireAuthorization(new AuthorizeAttribute { Roles = Roles.Administrator });

            group.MapGet(string.Empty, GetAll)
                 .WithName("GetBooks");

            group.MapGet("{id}", GetById)
                 .WithName("GetBookById");

            group.MapPost(string.Empty, Create)
                 .WithName("AddBook");

            // existing borrow route
            group.MapPost("borrow", Borrow)
                 .WithName("BorrowBook");

            group.MapPut("{id}", Update)
                 .WithName("UpdateBook");

            group.MapDelete("{id}", Delete)
                 .WithName("DeleteBook");
        }

        public async Task<Ok<List<BookDto>>> GetAll(ISender sender)
        {
            var list = await sender.Send(new GetBooksListQuery());
            return TypedResults.Ok(list);
        }

        public async Task<Ok<BookDto>> GetById(ISender sender, Guid id)
        {
            var dto = await sender.Send(new GetBookByIdQuery(id));
            return TypedResults.Ok(dto);
        }

        public async Task<Created<Guid>> Create(
            ISender sender,
            [FromBody] AddBookCommand cmd)
        {
            var id = await sender.Send(cmd);
            return TypedResults.Created($"/api/Books/{id}", id);
        }

        public async Task<NoContent> Borrow(
            ISender sender,
            [FromBody] BorrowBookCommand cmd)
        {
            await sender.Send(cmd);
            return TypedResults.NoContent();
        }

        public async Task<Results<NoContent, BadRequest>> Update(
            ISender sender,
            Guid id,
            [FromBody] UpdateBookCommand cmd)
        {
            if (id != cmd.Id)
                return TypedResults.BadRequest();

            await sender.Send(cmd);
            return TypedResults.NoContent();
        }

        public async Task<NoContent> Delete(ISender sender, Guid id)
        {
            await sender.Send(new DeleteBookCommand(id));
            return TypedResults.NoContent();
        }
    }
}
