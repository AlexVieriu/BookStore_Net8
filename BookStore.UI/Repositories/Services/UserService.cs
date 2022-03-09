namespace BookStore.UI.Repositories.Services;
public class UserService
{
    private readonly IClient _client;
    private readonly ILocalStorageService _localStorage;
    private readonly ApiAuthenticationStateProvider _stateProvider;

    public UserService(IClient client, ILocalStorageService localStorage, ApiAuthenticationStateProvider stateProvider)
    {
        _client = client;
        _localStorage = localStorage;
        _stateProvider = stateProvider;
    }

    public async Task<string> Login(UserLogin login)
    {
        try
        {
            var userResponse = await _client.LoginAsync(login);
            await _localStorage.SetItemAsync(Token.accessToken, userResponse.Token);
            await _stateProvider.LoggedIn();

            return "Logged in";
        }
        catch (ApiException ex)
        {
            return $"{ex.Message} - {ex.InnerException}";
        }
    }

    public async Task Logout()
    {
        await _stateProvider.LoggedOut();
    }
}
