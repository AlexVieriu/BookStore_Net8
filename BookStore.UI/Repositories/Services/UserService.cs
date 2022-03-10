namespace BookStore.UI.Repositories.Services;
public class UserService : BaseHttpService, IUserService
{
    private readonly IClient _client;
    private readonly ILocalStorageService _localStorage;
    private readonly ApiAuthenticationStateProvider _stateProvider;

    public UserService(IClient client, ILocalStorageService localStorage, ApiAuthenticationStateProvider stateProvider)
        : base(client, localStorage)
    {
        _client = client;
        _localStorage = localStorage;
        _stateProvider = stateProvider;
    }

    public async Task<Response<bool>> LoginAsync(UserLogin login)
    {
        Response<bool> response;
        try
        {
            var userResponse = await _client.LoginAsync(login);
            await _localStorage.SetItemAsync(Token.accessToken, userResponse.Token);
            await _stateProvider.LoggedIn();

            response = new() { Success = true };
        }
        catch (ApiException ex)
        {
            response = ConvertApiException<bool>(ex);
        }
        return response;
    }

    public async Task LogoutAsync()
    {
        await _stateProvider.LoggedOut();
    }

    public async Task<Response<bool>> RegisterAsync(UserRegister userRegister)
    {
        Response<bool> response;
        try
        {
            await _client.RegisterAsync(userRegister);
            response = new() { Success = true };
        }
        catch (ApiException ex)
        {
            response = ConvertApiException<bool>(ex);
        }
        return response;
    }
}
