using Application.Books.Commands.BorrowBook;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Endpoints.Books;

public static class Borrow
{
    public static IEndpointRouteBuilder MapBorrowBook(this IEndpointRouteBuilder builder)
    {
        builder.MapPost(
            async ([FromBody] BorrowBookCommand command, IMediator mediator) =>
            {
                await mediator.Send(command);
                return Results.NoContent();
            },
            "/api/books/borrow"
        );

        return builder;
    }
}
