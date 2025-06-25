// using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Infrastructure.Identity;

namespace Web.Endpoints.Authentication;

public class Login : EndpointGroupBase
{
    public record LoginRequest(string Email, string Password);

    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(this);
        group.MapPost(string.Empty, Handle);
    }

    public async Task<Results<NoContent, UnauthorizedHttpResult>> Handle(
        SignInManager<ApplicationUser> signInManager,
        [FromBody] LoginRequest request)
    {
        var result = await signInManager.PasswordSignInAsync(
            request.Email, request.Password, isPersistent: false, lockoutOnFailure: false);
        return result.Succeeded
            ? TypedResults.NoContent()
            : TypedResults.Unauthorized();
    }
}
