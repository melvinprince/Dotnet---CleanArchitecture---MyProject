using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Infrastructure.Identity;

namespace Web.Endpoints.Authentication;

public class Session : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(this);
        group.MapGet(string.Empty, Check);
        group.MapPost("logout", Logout).RequireAuthorization();
    }

    public Results<NoContent, UnauthorizedHttpResult> Check(ClaimsPrincipal user) =>
        user.Identity?.IsAuthenticated == true
            ? TypedResults.NoContent()
            : TypedResults.Unauthorized();

    public async Task<NoContent> Logout(SignInManager<ApplicationUser> signInManager)
    {
        await signInManager.SignOutAsync();
        return TypedResults.NoContent();
    }
}
