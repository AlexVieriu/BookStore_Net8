namespace BookStore.API.Repositories.Services;
public class BookRepository : BaseRepository<Book>, IBookRepository
{
    private readonly BookStoreContext _context;
    private readonly IMapper _mapper;

    public BookRepository(BookStoreContext context, IMapper mapper)
        : base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<BookReadDto>> GetAllBooksAsync()
    {
        var books = await _context.Books.Include(q => q.Author)
                                        .ProjectTo<BookReadDto>(_mapper.ConfigurationProvider)
                                        .ToListAsync();
        return books;
    }

    public async Task<BookReadDto> GetBookAsync(int id)
    {
        var book = await _context.Books.Include(q => q.Author)
                                       .ProjectTo<BookReadDto>(_mapper.ConfigurationProvider)
                                       .FirstOrDefaultAsync(q => q.Id == id);

        return book;
    }
}
