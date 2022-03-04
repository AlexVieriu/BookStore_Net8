namespace BookStore.API.Models.DTOs.Authors;
public class AuthorUpdateDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }

    [StringLength(250)]
    public string? Bio { get; set; }
}
