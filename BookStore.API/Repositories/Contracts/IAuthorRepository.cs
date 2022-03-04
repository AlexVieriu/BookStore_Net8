namespace BookStore.API.Repositories.Contracts;
public interface IAuthorRepository : IBaseRepository<Author>
{
    Task<AuthorReadDto> GetAuthorAsync(int id);
}
