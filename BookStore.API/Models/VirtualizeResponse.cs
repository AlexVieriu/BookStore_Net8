namespace BookStore.API.Models;
public class VirtualizeResponse<T>
{
    public int TotalSize { get; set; }
    public List<T>? Items { get; set; }
}
