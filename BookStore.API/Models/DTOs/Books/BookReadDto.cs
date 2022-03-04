namespace BookStore.API.Models.DTOs.Books;
public class BookReadDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public int Year { get; set; }
    public string? ISBN { get; set; }
    public decimal Price { get; set; }
    public int AuthorId { get; set; }
    public string? AuthorName { get; set; }

}
