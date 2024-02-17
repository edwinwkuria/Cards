using System.IdentityModel.Tokens.Jwt;

namespace Cards.API.Middleware;

public class UserMiddleware
{
    private readonly RequestDelegate _next;

    public UserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        string token = context.Request.Headers["Authorization"];
        if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
        {
            token = token.Substring("Bearer ".Length).Trim();
            var handler = new JwtSecurityTokenHandler();
            var tokenClaims = handler.ReadJwtToken(token).Claims.ToList();
            
            context.Items["UserId"] = tokenClaims.FirstOrDefault(c => c.Type == "Id")?.Value;
            context.Items["Role"] = tokenClaims.FirstOrDefault(c => c.Type == "Role")?.Value;
        }
        

        
        await _next(context);
    }
}
