namespace BookStore.API.Repositories.Services;
public class NewAuthorRepository : NewBaseRepository<Author>, INewAuthorRepository
{
    public NewAuthorRepository(BookStoreContext context, IMapper mapper) 
        : base(context, mapper)
    {

    }
}
