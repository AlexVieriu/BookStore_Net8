namespace BookStore.UI.Repositories.Base;
public class BaseHttpService
{
    private readonly IClient _client;
    private readonly ILocalStorageService _localStorage;

    public BaseHttpService(IClient client, ILocalStorageService localStorage)
    {
        _client = client;
        _localStorage = localStorage;
    }

    // converting the api exceptions 
    protected Response<Guid> ConvertApiException<Guid>(ApiException exception)
    {
        if(exception.StatusCode >=200 && exception.StatusCode <=299)
        {
            return new Response<Guid>()
            {
                Message = "Operation Reported Success",
                Success = true
            };
        }

        if (exception.StatusCode == 400)
        {
            return new Response<Guid>()
            {
                Message = "Validation errors has occured",
                ValidationErrors = exception.Response,
                Success = false
            };
        }

        if (exception.StatusCode == 404)
        {
            return new Response<Guid>()
            {
                Message = "Te request item could not be found",
                Success = false
            };
        }

        return new Response<Guid>
        {
            Message = "Something went wrong, please try again",
            Success = false
        };
    }

    protected async Task GetJwt()
    {
        //take the token from localhost, where is saved when i login
        var token = await _localStorage.GetItemAsync<string>(Token.accessToken);

        // put the token in the HttpHeader when we send the request 
        if(token != null)
            _client.HttpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue(Token.tokenType, token);  
    }
}
