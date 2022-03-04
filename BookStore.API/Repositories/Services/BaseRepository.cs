namespace BookStore.API.Repositories.Services;
public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly BookStoreContext _context;
    private readonly IMapper _mapper;

    public BaseRepository(BookStoreContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> CreateAsync(T entity)
    {
        await _context.AddAsync(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await GetAsync(id);
        if (entity == null)
            return false;

        _context.Set<T>().Remove(entity);
        var result = await _context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<VirtualizeResponse<TResult>> GetAllWithPg<TResult>(QueryParameters queryParams)
        where TResult : class
    {
        var totalSize = await _context.Set<T>().CountAsync();
        var items = await _context.Set<T>().Skip(queryParams.StartIdex)
                                           .Take(totalSize)
                                           .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                                           .ToListAsync();

        return new VirtualizeResponse<TResult> { Items = items, TotalSize = totalSize };
    }

    public async Task<T> GetAsync(int? id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        _context.Update(entity);
        var result = await _context.SaveChangesAsync();

        return result > 0;
    }
}
