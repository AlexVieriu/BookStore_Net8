namespace BookStore.UI.Client.Repositories.Author;

internal interface IAuthorService
{
    Task<Response<int>> CreateAuthorAsync(AuthorCreateDto author);
    Task<Response<int>> DeleteAuthorAsync(int idAuthor);
    Task<Response<AuthorReadDto>> GetAuthorAsync(int idAuthor);
    Task<Response<List<AuthorReadDto>>> GetAuthorsAsync();
    Task<Response<AuthorReadDtoVirtualizeResponse>> GetAuthorsWithPgAsync(QueryParameters queryParams);
    Task<Response<int>> UpdateAuthorAsync(int id, AuthorUpdateDto author);
}