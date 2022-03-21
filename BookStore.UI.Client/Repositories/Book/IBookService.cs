namespace BookStore.UI.Client.Repositories.Book;

internal interface IBookService
{
    Task<Response<int>> CreateBook(BookCreateDto book);
    Task<Response<int>> DeleteBook(int id);
    Task<Response<BookReadDtoVirtualizeResponse>> GetBooksWithPg(QueryParameters queryParams);
    Task<Response<BookReadDto>> GetBook(int id);
    Task<Response<BookUpdateDto>> UpdateBook(int id, BookUpdateDto book);
}