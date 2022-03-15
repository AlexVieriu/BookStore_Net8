namespace BookStore.UI.Client.Providers;
public class ApiAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly IClient _client;

    public ApiAuthenticationStateProvider(ILocalStorageService localStorage, IClient client)
    {
        _localStorage = localStorage;
        _client = client;
    }
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
       
    }

    // LoggedId

    // LoggedOut

    // GetClaims
}
