using Blazored.LocalStorage;
using System.IdentityModel.Tokens.Jwt;

namespace BookStore.UI.Providers;
public class ApiAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly JwtSecurityTokenHandler _jwt;

    public ApiAuthenticationStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
        _jwt = new();
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        
    }

    // Login

    // Logout

    // GetClaims
}
