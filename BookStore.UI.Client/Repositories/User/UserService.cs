namespace BookStore.UI.Client.Repositories.User;
public class UserService : BaseHttpClient, IUserService
{
    private readonly ILocalStorageService _localStorage;
    private readonly IClient _client;
    private readonly ApiAuthenticationStateProvider _stateProvider;

    public UserService(ILocalStorageService localStorage, IClient client, ApiAuthenticationStateProvider stateProvider)
        : base(localStorage, client)
    {
        _localStorage = localStorage;
        _client = client;
        _stateProvider = stateProvider;
    }

    //loginp
    public async Task<Response<bool>> LoginAsync(UserLogin userLogin)
    {
        Response<bool> response;
        try
        {
            var userResoponse = await _client.LoginAsync(userLogin);
            await _localStorage.SetItemAsync(TokenUI.TokenName, userResoponse.Token);
            await _stateProvider.LoggedIn();
            response = new() { Success = true };
        }
        catch (ApiException ex)
        {
            response = ConvertApiException<bool>(ex);
        }
        return response;
    }

    //logout
    public async Task LogoutAsync()
    {
        await _stateProvider.LoggedOut();
    }

    //register
    public async Task<Response<bool>> RegisterAsync(UserRegister userRegister)
    {
        Response<bool> response = new();
        try
        {
            response.Success = await _client.RegisterAsync(userRegister);
            return response;
        }
        catch (ApiException ex)
        {
            response = ConvertApiException<bool>(ex);
        }
        return response;
    }

}
