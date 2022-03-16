namespace BookStore.UI.Client.Providers;
public class ApiAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly IClient _client;
    private readonly JwtSecurityTokenHandler _jwt;

    public ApiAuthenticationStateProvider(ILocalStorageService localStorage, IClient client)
    {
        _localStorage = localStorage;
        _client = client;
        _jwt = new JwtSecurityTokenHandler();
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var userAuth = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        var token = await _localStorage.GetItemAsync<string>(TokenUI.TokenName);
        if (string.IsNullOrEmpty(token))
            return userAuth;

        var tokenContent = _jwt.ReadJwtToken(token);
        if (tokenContent.ValidTo < DateTime.Now)
        {
            await _localStorage.RemoveItemAsync(TokenUI.TokenName);
            return userAuth;
        }

        var claims = await GetClaims(tokenContent);

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt")));
    }

    // LoggedId
    public async Task LoggedIn()
    {
        var token = await _localStorage.GetItemAsync<string>(TokenUI.TokenName);
        var tokenContent = _jwt.ReadJwtToken(token);
        var claims = await GetClaims(tokenContent);
        var userAuth = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt")));
        NotifyAuthenticationStateChanged(Task.FromResult(userAuth));
    }

    // LoggedOut
    public async Task LoggedOut()
    {
        await _localStorage.RemoveItemAsync(TokenUI.TokenName);
        var nobody = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        NotifyAuthenticationStateChanged(Task.FromResult(nobody));
    }

    // GetClaims
    private async Task<List<Claim>> GetClaims(JwtSecurityToken tokenContent)
    {
        var claims = tokenContent.Claims.ToList();
        claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
        return claims;
    }
}
