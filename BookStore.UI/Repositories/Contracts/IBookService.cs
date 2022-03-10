namespace BookStore.UI.Repositories.Contracts;
public interface IBookService
{
    public Task<Response<int>> CreateBookAsync(BookCreateDto bookCreateDto);
    public Task<Response<int>> DeleteBook(int id);
    public Task<Response<BookReadDto>> GetBook(int id);
    public Task<Response<List<BookReadDto>>> GetBooks();
    public Task<Response<int>> UpdateBookAsync(int id, BookUpdateDto bookUpdateDto);
}
