namespace BookStore.API.Mapping;
public class MapConfig : Profile
{
    public MapConfig()
    {
        // User
        CreateMap<ApiUser, UserLogin>().ReverseMap();

        // Author
        CreateMap<Author, AuthorReadDto>().ReverseMap();
        CreateMap<Author, AuthorCreateDto>().ReverseMap();
        CreateMap<Author, AuthorUpdateDto>().ReverseMap();

        // Book
        CreateMap<Book, BookReadDto>()
            .ForMember(q => q.AuthorName, o => o.MapFrom(map => $"{map.Author.FirstName} {map.Author.FirstName}"))
            .ReverseMap();
        CreateMap<Book, BookCreateDto>().ReverseMap();
        CreateMap<Book, BookUpdateDto>().ReverseMap();

    }
}
