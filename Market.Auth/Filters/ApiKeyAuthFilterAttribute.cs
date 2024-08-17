using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Market.Auth.Filters;

public class ApiKeyAuthFilterAttribute : Attribute, IAuthorizationFilter
{
    public const string APIKEYHEADER = "ApiKey";
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(APIKEYHEADER, out var clientApiKey))
        {
            context.Result = new UnauthorizedResult();
            return;//Why do I need to return here
        }
        //var tokenManageer = context.HttpContext.RequestServices.GetService(typeof(ICustomTokenManager)) as ICustomTokenManager;
        //if (tokenManageer == null || !tokenManageer.Verify(clientApiKey))
        //{
        //    context.Result = new UnauthorizedResult();
        //    return;
        //}

    }

}
