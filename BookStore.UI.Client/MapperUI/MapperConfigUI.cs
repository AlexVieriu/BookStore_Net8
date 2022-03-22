namespace BookStore.UI.Client.MapperUI;
public class MapperConfigUI : Profile
{
    public MapperConfigUI()
    {
        CreateMap<AuthorUpdateDto, AuthorReadDto>().ReverseMap();
        CreateMap<BookUpdateDto, BookReadDto>().ReverseMap();
    }
}
