using Market.Auth.Application.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Market.Auth.Filters
{
    public class JwtAuthFilterAttribute : Attribute, IAuthorizationFilter
    {
        public static JwtHelper JwtHelper { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (JwtHelper == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                return;
            }

            if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var token = authorizationHeader.ToString().Replace("Bearer ", string.Empty);

            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var principal = JwtHelper.ValidateToken(token);

            if (principal == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            context.HttpContext.User = principal;
        }
    }
}
