using Microsoft.AspNetCore.Http.HttpResults;

using Application.Books.Commands.AddBook;
using Application.Books.Commands.BorrowBook;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Infrastructure;

namespace Web.Endpoints.Books
{
    public class Books : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            var group = app
                .MapGroup(this)               // => "/api/Books"
                .RequireAuthorization();      // if you want auth

            // POST /api/Books
            group.MapPost(string.Empty, CreateBook)
                 .WithName("AddBook")
                 .WithDescription("Add a new book to the catalog");

            // POST /api/Books/borrow
            group.MapPost("borrow", BorrowBook)
                 .WithName("BorrowBook")
                 .WithDescription("Borrow an existing book");
        }

        // Handler for creating a new book
        public async Task<Created<Guid>> CreateBook(
            ISender sender,
            [FromBody] AddBookCommand command)
        {
            var id = await sender.Send(command);
            // returns 201 with Location: /api/Books/{id}
            return TypedResults.Created($"/api/{nameof(Books)}/{id}", id);
        }

        // Handler for borrowing a book
        public async Task<NoContent> BorrowBook(
            ISender sender,
            [FromBody] BorrowBookCommand command)
        {
            await sender.Send(command);
            return TypedResults.NoContent();
        }
    }
}
