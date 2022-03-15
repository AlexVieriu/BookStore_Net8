namespace BookStore.UI.MappingUI;
public class MapUiConfig : Profile
{
    public MapUiConfig()
    {
        CreateMap<AuthorUpdateDto, AuthorReadDto>().ReverseMap();
    }
}
