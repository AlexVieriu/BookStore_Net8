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

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var authState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        var token = await _localStorage.GetItemAsync<string>(Token.accessToken);
        if (string.IsNullOrWhiteSpace(token))
            return authState;

        var tokenContent = _jwt.ReadJwtToken(token);
        if(tokenContent.ValidTo < DateTime.Now)
        {
            await _localStorage.RemoveItemAsync(Token.accessToken);
            return authState;
        }

        var claims = await GetClaims();

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt")));
    }

    public async Task LoggedIn()
    {
        var claims = await GetClaims();
        var authState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt")));
        NotifyAuthenticationStateChanged(Task.FromResult(authState));   
    }

    public async Task LoggedOut()
    {
        await _localStorage.RemoveItemAsync(Token.accessToken);
        var nobody = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        NotifyAuthenticationStateChanged(Task.FromResult(nobody));
    }

    public async Task<List<Claim>> GetClaims()
    {
        var token = await _localStorage.GetItemAsync<string>(Token.accessToken);
        var tokenContent = _jwt.ReadJwtToken(token);
        var claims = tokenContent.Claims.ToList();
        claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));

        return claims;
    }

}