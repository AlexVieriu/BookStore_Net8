namespace BookStore.UI.Repositories.Services;
public class AuthorService : BaseHttpService, IAuthorService
{
    private readonly IClient _client;
    private readonly ILocalStorageService _localStorage;

    public AuthorService(IClient client, ILocalStorageService localStorage)
        : base(client, localStorage)
    {
        _client = client;
        _localStorage = localStorage;
    }

    public async Task<Response<int>> CreateAuthorAsync(AuthorCreateDto author)
    {
        Response<int> response;
        try
        {
            await GetJwt();
            await _client.AuthorPOSTAsync(author);
            return response = new() { Success = true };
        }
        catch (ApiException ex)
        {
            response = ConvertApiException<int>(ex);
        }
        return response;
    }

    public async Task<Response<int>> DeleteAuthorAsync(int idAuthor)
    {
        Response<int> response;
        try
        {
            await GetJwt();
            await _client.AuthorDELETEAsync(idAuthor);
            return response = new() { Success = true };
        }
        catch (ApiException ex)
        {
            response = ConvertApiException<int>(ex);
        }
        return response;
    }

    public async Task<Response<AuthorReadDto>> GetAuthorAsync(int authorId)
    {
        Response<AuthorReadDto> response;
        try
        {
            await GetJwt();
            var data = await _client.AuthorGETAsync(authorId);
            return response = new() { Success = true, Data = data };
        }
        catch (ApiException ex)
        {
            response = ConvertApiException<AuthorReadDto>(ex);
        }
        return response;
    }

    public async Task<Response<List<AuthorReadDto>>> GetAuthorsAsync()
    {
        Response<List<AuthorReadDto>> response;
        try
        {
            await GetJwt();
            var data = (await _client.AuthorsAsync()).ToList();
            return response = new() { Success = true, Data = data };
        }
        catch (ApiException ex)
        {
            response = ConvertApiException<List<AuthorReadDto>>(ex);
        }
        return response;
    }

    public async Task<Response<int>> UpdateAuthorAsync(int id, AuthorUpdateDto author)
    {
        Response<int> response;
        try
        {
            await GetJwt();
            await _client.AuthorPUTAsync(id, author);
            return response = new() { Success = true };
        }
        catch (ApiException ex)
        {
            return response = ConvertApiException<int>(ex); 
        }
        return response;
    }
}