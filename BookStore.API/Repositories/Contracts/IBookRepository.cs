namespace BookStore.API.Repositories.Contracts;
public interface IBookRepository : IBaseRepository<Book>
{
    Task<BookReadDto> GetBookAsync(int id);
    Task<List<BookReadDto>> GetAllBooksAsync();
}
