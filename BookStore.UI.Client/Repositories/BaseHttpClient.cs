namespace BookStore.UI.Client.Repositories;
public class BaseHttpClient
{
    private readonly ILocalStorageService _localStorage;
    private readonly IClient _client;

    public BaseHttpClient(ILocalStorageService localStorage, IClient client)
    {
        _localStorage = localStorage;
        _client = client;
    }


    public Response<Guid> ConvertApiException<Guid>(ApiException ex)
    {
        if (ex.StatusCode >= 200 && ex.StatusCode <= 299)
        {
            return new Response<Guid>
            {
                Message = "Oparation Successed",
                Success = true
            };
        }

        if (ex.StatusCode == 400)
        {
            return new Response<Guid>
            {
                Message = "Validation Erros",                
                ValidationErrors = $"{ex.InnerException} - {ex.Message}",
                Success = false,
            };
        }
        if (ex.StatusCode == 404)
        {
            return new Response<Guid>
            {
                Message = "Item not found",
                ValidationErrors = $"{ex.InnerException} - {ex.Message}",
                Success = false
            };
        }
        return new Response<Guid>
        {
            Message = "Something whent wrong, try again",
            ValidationErrors = $"{ex.InnerException} - {ex.Message}",
            Success = false
        };
    }

    public async Task GetJwt()
    {
        var token = await _localStorage.GetItemAsync<string>(TokenUI.TokenName);

        if (string.IsNullOrEmpty(token))
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(TokenUI.TokenType, token);
    }
}
