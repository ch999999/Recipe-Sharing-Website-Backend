using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

namespace RecipeSiteBackend.CustomErrorHandlers;

public class CustomAuthErrorHandler:IAuthorizationMiddlewareResultHandler

{
    private readonly AuthorizationMiddlewareResultHandler defaultHandler = new();

    public async Task HandleAsync(
        RequestDelegate next,
        HttpContext context,
        AuthorizationPolicy policy,
        PolicyAuthorizationResult authorizeResult)
    {
        if(!authorizeResult.Succeeded) //Always return 404 not found
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            return;
        }
        await defaultHandler.HandleAsync(next, context, policy, authorizeResult);
    }
}

public class Show404Requirement : IAuthorizationRequirement { }
