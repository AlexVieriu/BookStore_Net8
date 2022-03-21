namespace BookStore.UI.Client.Repositories.Book;
public class BookService : BaseHttpClient, IBookService
{
    private readonly ILocalStorageService _localStorage;
    private readonly IClient _client;

    public BookService(ILocalStorageService localStorage, IClient client)
        : base(localStorage, client)
    {
        _localStorage = localStorage;
        _client = client;
    }

    public async Task<Response<int>> CreateBook(BookCreateDto book)
    {
        Response<int> response;
        try
        {
            await GetJwt();
            await _client.BookPOSTAsync(book);
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

    public async Task<Response<BookReadDtoVirtualizeResponse>> GetBooksWithPg(QueryParameters queryParams)
    {
        Response<BookReadDtoVirtualizeResponse> response;
        try
        {
            await GetJwt();
            var authors = await _client.BooksWithPgAsync(queryParams.StartIndex, queryParams.PageSize);
            return response = new() { Success = true, Data = authors };
        }
        catch (ApiException ex)
        {
            response = ConvertApiException<BookReadDtoVirtualizeResponse>(ex);
        }
        return response;
    }

    public async Task<Response<BookReadDto>> GetBook(int id)
    {
        Response<BookReadDto> response;
        try
        {
            await GetJwt();
            var author = await _client.BookGETAsync(id);
            return response = new() { Success = true, Data = author };
        }
        catch (ApiException ex)
        {
            response = ConvertApiException<BookReadDto>(ex);
        }
        return response;
    }

    public async Task<Response<BookUpdateDto>> UpdateBook(int id, BookUpdateDto book)
    {
        Response<BookUpdateDto> response;
        try
        {
            await GetJwt();
            await _client.BookPUTAsync(id, book);
            return response = new() { Success = true };
        }
        catch (ApiException ex)
        {
            response = ConvertApiException<BookUpdateDto>(ex);
        }
        return response;
    }
}
