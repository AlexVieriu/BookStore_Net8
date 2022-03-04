namespace BookStore.API.Repositories.Contracts;
public interface IBaseRepository<T>
{
    Task<VirtualizeResponse<TResult>> GetAllWithPg<TResult>(QueryParameters queryParams) where TResult : class;
    Task<List<T>> GetAllAsync();
    Task<T> GetAsync(int? id);
    Task<bool> CreateAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);

}
