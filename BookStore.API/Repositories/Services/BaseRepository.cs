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

    public async Task<bool> CreateAsync<TMap>(TMap entityDto) where TMap : class
    {
        var entity = _mapper.Map<T>(entityDto);

        await _context.AddAsync(entity);        
        var result = await _context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity == null)
            return false;

        _context.Set<T>().Remove(entity);
        var result = await _context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<List<TMap>> GetAllAsync<TMap>() where TMap : class
    {
        var entitiesDto = await _context.Set<T>().ProjectTo<TMap>(_mapper.ConfigurationProvider)
                                                 .ToListAsync();

        return entitiesDto;
    }

    public async Task<VirtualizeResponse<TMap>> GetAllWithPg<TMap>(QueryParameters queryParams)
        where TMap : class
    {
        var totalSize = await _context.Set<T>().CountAsync();
        var items = await _context.Set<T>().Skip(queryParams.StartIdex)
                                           .Take(queryParams.PageSize)
                                           .ProjectTo<TMap>(_mapper.ConfigurationProvider)
                                           .ToListAsync();

        return new VirtualizeResponse<TMap> { Items = items, TotalSize = totalSize };
    }

    public async Task<TMap> GetAsync<TMap>(int? id) where TMap : class
    {
        var entity = await _context.Set<T>().FindAsync(id);
        var entityDto = _mapper.Map<TMap>(entity);

        return entityDto;
    }

    public async Task<bool> UpdateAsync<TMap>(TMap entityDto) where TMap : class
    {
        var entity = _mapper.Map<T>(entityDto);
        _context.Update(entity);
        var result = await _context.SaveChangesAsync();

        return result > 0;
    }
}
