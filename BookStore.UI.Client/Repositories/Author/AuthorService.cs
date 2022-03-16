using BookStore.UI.Client.Models;

namespace BookStore.UI.Client.Repositories.Author;
public class AuthorService : BaseHttpClient, IAuthorService
{
    private readonly ILocalStorageService _localStorage;
    private readonly IClient _client;

    public AuthorService(ILocalStorageService localStorage, IClient client)
        : base(localStorage, client)
    {
        _localStorage = localStorage;
        _client = client;
    }

    // create
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

    // delete
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

    // get
    public async Task<Response<AuthorReadDto>> GetAuthorAsync(int idAuthor)
    {
        Response<AuthorReadDto> response;
        try
        {
            await GetJwt();
            var author = await _client.AuthorGETAsync(idAuthor);
            return response = new() { Success = true, Data = author };
        }
        catch (ApiException ex)
        {
            response = ConvertApiException<AuthorReadDto>(ex);
        }
        return response;
    }

    // get all 
    public async Task<Response<List<AuthorReadDto>>> GetAuthorsAsync()
    {
        Response<List<AuthorReadDto>> response;
        try
        {
            await GetJwt();
            var authors = (await _client.AuthorsAsync()).ToList();
            return response = new() { Success = true, Data = authors };
        }
        catch (ApiException ex)
        {
            response = ConvertApiException<List<AuthorReadDto>>(ex);
        }
        return response;
    }

    // get with pg
    public async Task<Response<AuthorReadDtoVirtualizeResponse>> GetAuthorsWithPgAsync(QueryParameters queryParams)
    {
        Response<AuthorReadDtoVirtualizeResponse> response;
        try
        {
            await GetJwt();
            var result = await _client.AuthorsWithPgAsync(queryParams.PageIndex, queryParams.PageSize);
            return response = new() { Success = true, Data = result };
        }
        catch (ApiException ex)
        {
            response = ConvertApiException<AuthorReadDtoVirtualizeResponse>(ex);
        }
        return response;
    }

    // update

}
