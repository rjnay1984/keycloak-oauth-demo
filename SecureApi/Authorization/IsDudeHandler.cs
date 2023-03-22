using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using SecureApi.Authorization.Requirements;

namespace SecureApi.Authorization.Handlers;

public class IsDudeHandler : AuthorizationHandler<IsDudeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsDudeRequirement requirement)
    {
        var email = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
        if (email!.Value.Contains("@dude.com")) context.Succeed(requirement);
        return Task.CompletedTask;
    }
}