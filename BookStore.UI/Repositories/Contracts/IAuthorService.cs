namespace BookStore.UI.Repositories.Contracts;
public interface IAuthorService
{
    Task<Response<int>> CreateAuthorAsync(AuthorCreateDto author);
    Task<Response<int>> DeleteAuthorAsync(int idAuthor);
    Task<Response<List<AuthorReadDto>>> GetAuthorsAsync();
    Task<Response<AuthorReadDto>> GetAuthorAsync(int authorId);
    Task<Response<int>> UpdateAuthorAsync(int id, AuthorUpdateDto author);
}
