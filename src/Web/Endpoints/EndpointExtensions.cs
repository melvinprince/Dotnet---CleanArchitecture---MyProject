using Web.Endpoints.Books;

namespace Microsoft.AspNetCore.Builder;

public static class EndpointExtensions
{
    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        // Register the Borrow Book endpoint
        app.MapBorrowBook();
    }
}
