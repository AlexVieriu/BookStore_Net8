namespace BookStore.UI.Repositories.Services;
public class BookService : BaseHttpService, IBookService
{
    private readonly IClient _client;
    private readonly ILocalStorageService _localStorage;

    public BookService(IClient client, ILocalStorageService localStorage)
        : base(client, localStorage)
    {
        _client = client;
        _localStorage = localStorage;
    }

    public async Task<Response<int>> CreateBookAsync(BookCreateDto bookCreateDto)
    {
        Response<int> response = new();
        try
        {
            await GetJwt();
            await _client.BookPOSTAsync(bookCreateDto);
            return response = new() { Success = true };
        }
        catch (ApiException ex)
        {
            response = ConvertApiException<int>(ex);
        }
        return response;
    }

    public async Task<Response<int>> DeleteBook(int id)
    {
        Response<int> response;
        try
        {
            await GetJwt();
            await _client.BookDELETEAsync(id);
            return response = new() { Success = true };
        }
        catch (ApiException ex)
        {
            response = ConvertApiException<int>(ex);
        }
        return response;
    }

    public async Task<Response<BookReadDto>> GetBook(int id)
    {
        Response<BookReadDto> response;
        try
        {
            await GetJwt();
            var data = await _client.BookGETAsync(id);
            return response = new() { Success = true };
        }
        catch (ApiException ex)
        {
            response = ConvertApiException<BookReadDto>(ex);
        }
        return response;
    }

    public async Task<Response<List<BookReadDto>>> GetBooks()
    {
        Response<List<BookReadDto>> response;
        try
        {
            await GetJwt();
            var data = (await _client.BooksAsync()).ToList();
            return response = new() { Success = true, Data = data };
        }
        catch (ApiException ex)
        {
            response = ConvertApiException<List<BookReadDto>>(ex);
        }
        return response;
    }

    public async Task<Response<int>> UpdateBookAsync(int id, BookUpdateDto bookUpdateDto)
    {
        Response<int> response;
        try
        {
            await GetJwt();
            var data = _client.BookPUTAsync(id, bookUpdateDto);
            return response = new() { Success = true };

        }
        catch (ApiException ex)
        {
            response = ConvertApiException<int>(ex);
        }
        return response;
    }
}
