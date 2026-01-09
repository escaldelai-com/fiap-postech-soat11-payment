using Restaurant.Payment.Application.Interfaces.WebApi;

namespace Restaurant.Payment.WebApi.Services;

public class SecurityService(
    IHttpContextAccessor http) : ISecurityService
{

    private const string bearerPrefix = "Bearer ";

    public string Token => GetToken();



    private string GetToken()
    {
        var authorizationHeader = http.HttpContext?
            .Request.Headers["Authorization"]
            .ToString();

        if (string.IsNullOrWhiteSpace(authorizationHeader))
            return string.Empty;

        return authorizationHeader.StartsWith(bearerPrefix, StringComparison.OrdinalIgnoreCase)
            ? authorizationHeader[bearerPrefix.Length..].Trim()
            : authorizationHeader.Trim();
    }

}
